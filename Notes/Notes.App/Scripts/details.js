function showCategoryDetails(e) {
    e.preventDefault();
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: $(this).attr("dialog-title"),
            close: function () { $(this).remove() },
            modal: true,
            closeOnEscape: true,
            width: "auto",
            autoOpen:  false,
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
            else {
                var data = $("#categoryDetailsInfo .details");
                data.on("click", showNoteDetails);
                dlg.dialog("open");
            }
        });
}

function editCategory(e){
    e.preventDefault();
    $("<div><div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: $(this).attr("dialog-title"),
            close: true,
            closeOnEscape: true,
            width: "auto",
            buttons:
                [
                    {
                        text: "Сохранить",
                        click: function () {
                            $.ajax("/Category/Edit",
                                {

                                })
                        }
                    }
                ]
        })
        .load(this.href);
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