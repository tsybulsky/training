function login (e) {
    e.preventDefault();
    var returnUrl = $("#returnUrl").val();    
    $.ajax({
        url: "/Account/DoLogin",
        type: "POST",
        data: {
            Username: $("#UserName").val(),
            Password: $("#Password").val()
        },
        success: function (data) {
            if (data.Code == 0) {
                window.location = returnUrl;
            } else {
                $("login-error").html(data.Message);
                $("#Password").val("");
            }
        }
    })
}

function logout() {    
    $.ajax({
        url: "/Account/Logout",
        type: "POST",
        success: function (data) {
            if (data.Code == 0) {
                window.location = "/";
            } else {

            }
        }
    })
}

function message(text, caption) {
    $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: caption,
            modal: true,
            width: "30%",
            autoOpen: true,
            position: { my: "center center", of: window },
            buttons:
                [
                    {
                        text: "OK",
                        close: function () { $(this).remove() }
                    }]
        })
        .html(text);
}

function createCategory(e) {
    e.preventDefault();
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: "Создание категории",
            modal: true,
            closeOnEscape: true,
            width: "50%",
            autoOpen: false,
            buttons:
                [
                    {
                        text: "Создать",
                        click: function () {
                            e.preventDefault();
                            $.ajax({
                                url: "/Category/DoCreate",
                                method: "POST",
                                data: {
                                    Name: $("#Name").val()
                                },
                                success: function (data) {
                                    if (data.Code == 0) {
                                        document.forms.namedItem('createCategoryForm').reset();
                                        $("#save-error").html = "Категория успешно создана";
                                    } else {
                                        $("#save-error").html = data.Message;
                                    }
                                }
                            })
                        }
                    },
                    {
                        text: "Закрыть",
                        click: function () {
                            $(this).dialog("close");
                        }
                    }
                ]
        })
        .load(this.href, function (response, status, xhr) {
            if (status == "error")
                $(this).html("Error: " + response);
            else {
                dlg.dialog("open");
            }
        });
}        

function showCategoryDetails(e) {
    e.preventDefault();
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: $(this).attr("dialog-title"),            
            modal: true,
            closeOnEscape: true,
            width: "50%",
            autoOpen: false,
            posiion: {my: "center center", of: window},
            buttons:
                [
                    {
                        text: "Закрыть",
                        click: function () {
                            $(this).dialog("close");
                        }
                    }
                ]
        })
        .load(this.href, function (response, status, xhr) {
            if (status == "error")
                $(this).html("Error: " + response);
            else {
                var data = $("#categoryNotes .details");
                data.on("click", showNoteDetails);
                dlg.dialog("open");
                $(this).tabs({
                    class: "ui-tabs"
                })
            }
        });
}

function editCategory(e){
    e.preventDefault();
    $("<div><div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: "Редактирование категории",
            close: false,
            closeOnEscape: true,
            width: "50%",
            position: {my: "center center", of: window},
            buttons:
                [
                    {
                        text: "Сохранить",
                        click: function () {
                            document.forms.namedItem("editCategoryForm").submit();
                            $(this).dialog("close");
                        }
                    },
                    {
                        text: "Отмена",
                        click: function () {
                            $(this).dialog("close");
                        }
                    }
                ]
        })
        .load(this.href);
}

function deleteCategory(e) {
    e.preventDefault();
    var id = this.attr("catId");
    var dlg = $("<div></div>")
        .addClass("dialog")        
        .appendTo("body")
        .dialog({
            title: "Подтверждение удаления",
            modal: true,
            closeOnEscape: true,            
            width: "30%",
            position: { my: "center center", of: window },
            buttons:
                [
                    {
                        text: "Да",
                        click: function () {
                            $.ajax({
                                url: link,
                                method: "GET",
                                success: function (data) {
                                    if (data.Code != 0) {                                        
                                        message(data.Message);                                        
                                    }
                                    $(this).remove();
                                }
                            })
                        }
                    },
                    {
                        text: "Нет",
                        click: function () { $(this).remove()}
                    }
                    ]
        })
        .html("Удалить категорию?");
}

function createNote(e) {
    $.ajaxSetup({ cache: false });
    e.preventDefault();
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: $(this).attr("dialog-title"),
            close: function () { $(this).remove() },
            modal: true,
            closeOnEscape: true,
            width: "50%",
            autoOpen: false,
            position: { my: "center center", of: window }
        })
        .load(this.href, function (response, status, xhr) {
            if (status == "error")
                $(this).html("Error: " + response);
            else
                dlg.dialog("open");
            $(this).tabs({
                class: "ui-tabs"
            });
        });    
}
function showNoteDetails(e) {
    e.preventDefault();
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: $(this).attr("dialog-title"),
            close: function () { $(this).remove() },
            modal: true,
            closeOnEscape: true,
            width: "50%",
            autoOpen: false,
            position: {my: "center center", of: window},
            buttons:
                [
                    {
                        text: "OK",
                        click: function () {
                            $(this).dialog("close");
                        }
                    }
                ]
        })
        .load(this.href, function (response, status, xhr) {
            if (status == "error")
                $(this).html("Error: " + response);
            else
                dlg.dialog("open");
            $(this).tabs({
                class: "ui-tabs"
            });
        });    
}

function editNote(e) {
    $.ajaxSetup({ cache: false });
    e.preventDefault();
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: $(this).attr("dialog-title"),
            close: function () { $(this).remove() },
            modal: true,
            closeOnEscape: true,
            width: "50%",
            autoOpen: false,
            position: { my: "center center", of: window }
        })
        .load(this.href, function (response, status, xhr) {
            if (status == "error")
                $(this).html("Error: " + response);
            else {
                dlg.dialog("open");
                $(this).tabs({
                    class: "ui-tabs"
                });
                $(this).find(":input[data-val=true]").each(function () {
                    $jQval.unobtrusive.parseElement(this, true);
                })
            }
        }); 
}

function deleteNote(e) {
    $.ajaxSetup({ cache: false });
    e.preventDefault();
    var link = this.href;
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: "Подтверждение",
            close: function () { $(this).remove() },
            modal: true,
            closeOnEscape: true,
            buttons: [
                {
                    text: "Да",
                    click: function () {
                        window.location = link;
                        $(this).remove();
                    }
                },
                {
                    text: "Нет",
                    click: function () { $(this).remove() }
                }],
            width: "25%",
            position: { my: "center center", of: window }
        })
        .html("Удалить заметку?");
}