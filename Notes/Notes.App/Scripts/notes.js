function login(e) {
   if (e !== undefined)
        e.preventDefault();
    var returnUrl = $("#returnUrl").val();
    returnUrl = (returnUrl == "") ? "/" : returnUrl;
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
                $("login-error").text(data.Message);
                $("#Password").val("");
            }
        }
    })
}

function showLoggedUser(e) {
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: "Пользователь",
            close: function () { $(this).remove() },
            modal: true,
            closeOnEscape: true,
            width: "50%",
            autoOpen: false,
            position: { my: "center center", of: window },
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
        .load("/Account/Details", function (response, status, xhr) {
            if (status == "error")
                $(this).text("Error: " + response);
            else
                dlg.dialog("open");
            $(this).tabs({
                class: "ui-tabs"
            });
        });
    return void (0);
}

function changePassword() {    
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: "Смена пароля",
            modal: true,
            closeOnEscape: true,
            closeText: "x",
            width: "25%",
            autoOpen: false,
            buttons:
                [
                    {
                        text: "Сменить",
                        click: function () {
                            var NewPassword = $("#NewPassword").val();
                            var ConfirmPassword = $("#ConfirmPassword").val();
                            if (NewPassword != ConfirmPassword) {
                                $("#save-error").text("Новый пароль и его подтверждение должны совпадать");
                                return;
                            }
                            $.ajax({
                                url: "/Account/ChangePassword",
                                method: "POST",
                                data: {
                                    Id: $("#Id").val(),
                                    Login: $("#Login").val(),
                                    OldPassword: $("#OldPassword").val(),
                                    NewPassword: NewPassword,
                                    ConfirmPassword: ConfirmPassword
                                },
                                success: function (data) {
                                    if (data.Code == 0) {
                                        message("Пароль успешно изменен");
                                        $(this).remove();
                                    } else {
                                        $("#save-error").text(data.Message);
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
        .load("/Account/ChangePassword", function (response, status, xhr) {
            if (status == "error")
                $(this).text("Error: " + response);
            else {
                dlg.dialog("open");
            }
        });
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
    if (caption === undefined)
        caption = "Сообщение";
    $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: caption,
            modal: true,
            width: "30%",
            closeText: "x",
            position: { my: "center center", of: window },
            buttons:
                [
                    {
                        text: "OK",
                        click: function () { $(this).dialog("close") }
                    }]
        })
        .html(text);
}

function createCategory(e) {
    if (e !== undefined) {
        e.preventDefault();
    }
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: "Создание категории",
            modal: true,
            closeOnEscape: true,
            width: "30%",
            autoOpen: false,
            buttons:
                [
                    {
                        text: "Создать",
                        click: function () {
                            if (!$("#createCategoryForm").valid())
                                return;
                            $.ajax({
                                url: "/Category/DoCreate",
                                method: "POST",
                                data: {
                                    Name: $("#Name").val()
                                },
                                success: function (data) {
                                    if (data.Code == 0) {
                                        message("Категория успешно создана");
                                        dlg.remove();
                                    } else {
                                        $("#save-error").text(data.Message);
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
        .load("/Category/Create", function (response, status, xhr) {
            if (status == "error")
                $(this).html("Error: " + response);
            else {
                dlg.dialog("open");
            }
        });
}        

function showCategoryDetails(e) {
    if (e !== undefined)
        e.preventDefault();
    var id = $(this).attr("data-id");
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: "Свойства категории",            
            modal: true,
            closeOnEscape: true,
            closeText: "x",
            width: "30%",
            autoOpen: false,
            posiion: {my: "center center", of: window},
            buttons:
                [
                    {
                        text: "Закрыть",
                        click: function () {
                            dlg.remove();
                        }
                    }
                ]
        })
        .load(this.href+'/'+id, function (response, status, xhr) {
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

function editCategory(e) { 
    if (e !== undefined)
        e.preventDefault();
    var id = $(this).attr("data-id");
    var dlg = $("<div><div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: "Редактирование категории",
            close: false,
            closeOnEscape: true,
            closeText: "x",
            width: "30%",
            position: { my: "center center", of: window },
            buttons:
                [
                    {
                        text: "Сохранить",
                        click: function () {
                            if (!$("#editCategoryForm").valid())
                                return;
                            $.ajax({
                                url: "/Category/DoEdit",
                                method: "POST",
                                data: {
                                    Id: id,
                                    Name: $("#Name").val()
                                },
                                success: function (data) {
                                    if (data.Code == 0) {
                                        message("Успешно изменено");
                                        dlg.remove();
                                    }
                                    else {
                                        $("#save-error").text(data.Message);
                                    }
                                }
                            })
                        }
                    },
                    {
                        text: "Отмена",
                        click: function () {
                            dlg.remove();
                        }
                    }
                ]
        })
        .load("/Category/Edit/" + id, function (response, status, xhr) {
            if (status == "error")
                $(this).html("Error: " + response);
            else {
                var data = $("#editCategoryForm .details");
                data.on("click", showNoteDetails);
                dlg.dialog("open");
                $(this).tabs({
                    class: "ui-tabs"
                })
            }
        });
}

function deleteCategory(e) {
    if (e !== undefined)
        e.preventDefault();
    var id = $(this).attr("data-id");
    var link = this.href;
    var dlg = $("<div></div>")
        .addClass("dialog")        
        .appendTo("body")
        .dialog({
            title: "Подтверждение удаления",
            modal: true,
            closeOnEscape: true, 
            closeText: "x",
            width: "30%",
            position: { my: "center center", of: window },
            buttons:
                [
                    {
                        text: "Да",
                        click: function () {
                            $.ajax({
                                url: link,
                                method: "POST",
                                data:
                                {
                                    Id: id
                                },
                                success: function (data) {
                                    if (data.Code != 0) {                                        
                                        dlg.remove();
                                        message(data.Message);                                        
                                    }
                                    dlg.remove();                                    
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
                        text: "Закрыть",
                        click: function () {
                            $(this).dialog("close");
                        }
                    }
                ]
        })
        .load(this.href, function (response, status, xhr) {
            $.ajax({
                method: "POST",                
            })
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
    if (e !== undefined)
        e.preventDefault();
    var id = $(this).attr("data-id");
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
                        $.ajax(
                            {
                                url: "/Note/DoDelete",
                                method: "POST",
                                data: {
                                    Id: id
                                },
                                success: function (data) {
                                    if (data.Code != 0) {
                                        message(data.Message);
                                    }
                                    else {
                                        dlg.remove();
                                    }
                                }
                            });
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

function listRolesForUser(userId) {
    $.ajax({
        url: "/User/ListRoles",
        method: "POST",
        data:
        {
            userId: userId
        },
        success: function (data) {
            if (data.Code != 0)
                return;
            var html = '<table class="table"><thead><tr><th>Роль</th><th>Локализованное название</th><th>&nbsp;</th></tr></thead><tbody>';
            for (var i = 0; i < data.Roles.length; i++)
            {
                var role = data.Roles[i];
                html += '<tr><td>' + role.RoleName + '</td><td>' + role.LocalizedRoleName + '</td><td><a href="#" class="deletelink" data-id="' +role.Id + '">' +
                    '<img src="/Content/images/delete32.png"></a></td></tr>';
            }
            html += '</tbody></table>';
            $("#roles").html(html);
            $("#roles .deletelink").on("click", deleteUserRole);
        }
    })
}

function addUserRole(e) {
    if (e !== undefined)
        e.preventDefault();
    var userId = $(this).attr("data-id");
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: "Добавление роли",
            close: function () { $(this).remote() },
            modal: true,
            closeOnEscape: true,
            width: "30%",
            autoOpen: false,
            position: { my: "center center", of: window },
            buttons:
                [
                    {
                        text: "Добавить",
                        click: function () {
                            if (!$("#addUserRoleForm").valid())
                                return;
                            $.ajax({
                                url: "/User/DoAddUserRole",
                                method: "POST",
                                data:
                                {
                                    UserId: userId,
                                    SelectedRole: $("#SelectedRole").val()
                                },
                                success: function (data) {
                                    if (data.Code == 0) {
                                        dlg.remove();
                                        var roles = $("#roles");
                                        if (roles !== undefined)
                                            listRolesForUser(userId);
                                    }
                                    else {
                                        message(data.Message);
                                    }
                                }
                            })
                        }
                    },
                    {
                        text: "Закрыть",
                        click: function () { $(this).remove()}
                    }
                ]
        })
        .load("/User/AddUserRole/" + userId, function (response, status, xhr) {
            if (status == "error")
                $(this).html("Error: " + response);
            else {
                dlg.dialog("open")
            }
        });                 
}

function deleteUserRole(e) {
    if (e !== undefined)
        e.preventDefault();
    var id = $(this).attr("data-id");
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: "Подтверждение удаления",
            close: function () { $(this).remove() },
            closeOnEscape: true,
            autoOpen: true,
            modal: true,
            buttons:
                [
                    {
                        text: "Да",
                        click: function () {
                            $.ajax({
                                url: "/User/DoDeleteUserRole",
                                method: "POST",
                                data:
                                {
                                    id: id
                                },
                                success: function (data) {
                                    if (data.Code != 0) {

                                        message(data.Message);
                                    }
                                    else {
                                        dlg.remove();
                                        var userId = $(".addrolelink").attr("data-id");
                                        listRolesForUser(userId);
                                    }
                                }

                            })
                        }
                    },
                    {
                        text: "Нет",
                        click: function () { dlg.remove() }
                    }
                ]
        })
        .html("Удалить роль пользователя?")
}

function createUser(e) {
    if (e !== undefined) {
        e.preventDefault();
    }
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: "Создание пользователя",
            modal: true,
            closeOnEscape: true,
            width: "30%",
            autoOpen: false,
            buttons:
                [
                    {
                        text: "Создать",
                        click: function () {
                            if (!$("#createUserForm").valid())
                                return;
                            var password = $("#Password").val();
                            var confirmPassword = $("#ConfirmPassword").val();
                            if (password != confirmPassword) {
                                $("#save-error").text("Пароли должны совпадать");
                                dlg.remove();
                            }
                            $.ajax({
                                url: "/User/DoCreate",
                                method: "POST",
                                data: {
                                    Login: $("#Login").val(),
                                    Password: password,
                                    ConfirmPassword: confirmPassword,
                                    Name: $("#Name").val(),
                                    EMail: $("#EMail").val(),
                                    Status: $("#Status").val(),
                                    Role: $("#Role").val()                                  

                                },
                                success: function (data) {
                                    if (data.Code == 0) {
                                        message("Пользователь создан");
                                        dlg.remove();
                                    } else {
                                        $("#save-error").text(data.Message);
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
        .load("/User/Create", function (response, status, xhr) {
            if (status == "error")
                $(this).html("Error: " + response);
            else {
                dlg.dialog("open");
            }
        });
}

function showUserDetails(e) {
    if (e !== undefined)
        e.preventDefault();
    var link = this.href;
    var id = $(this).attr("data-id");
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: "Информация о пользователе",
            close: function () { $(this).remove() },
            modal: true,
            closeOnEscape: true,
            closeText: "x",
            width: "30%",
            autoOpen: false,
            position: { my: "center center", of: window },
            buttons:
                [
                    {
                        text: "Закрыть",
                        click: function () {
                            dlg.remove();
                        }
                    }
                ]
        })
        .load(link+'/'+id, function (response, status, xhr) {
            if (status == "error")
                $(this).html("Error: " + response);
            else {
                dlg.dialog("open");
                $(this).tabs({
                    class: "ui-tabs"
                });
            }
        });    
}

function editUser(e) {
    if (e !== undefined)
        e.preventDefault();
    var link = this.href;
    var id = $(this).attr("data-id");
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: "Информация о пользователе",
            close: function () { $(this).remove() },
            modal: true,
            closeOnEscape: true,
            width: "30%",
            position: { my: "center-15% center", of: window },
            buttons:
                [
                    {
                        text: "Сохранить",
                        click: function () {
                            if (!$("#editUserForm").valid()) 
                                return;
                            $.ajax({
                                url: "/User/DoEdit",
                                method: "POST",
                                data: {
                                    Id: $("#Id").val(),
                                    Login: $("#Login").val(),
                                    Name: $("#Name").val(),
                                    EMail: $("#EMail").val(),
                                    Status: $("#Status").val()
                                },
                                success: function (data) {
                                    if (data.Code == 0) {
                                        dlg.remove();
                                        message("Успешно сохранено");
                                        location.reload();
                                    } else {
                                        message(data.Message);
                                    }
                                }
                            })
                        }
                    },
                    {
                        text: "Закрыть",
                        click: function () {
                            dlg.remove();
                        }
                    }
                ]
        })
        .load(link + '/' + id, function (response, status, xhr) {
            if (status == "error")
                $(this).html("Error: " + response);
            else {
                $(this).tabs({
                    class: "ui-tabs"
                });
                $.validator.unobtrusive.parseDynamicContent($("#editUserForm"));
            }
        }); 
}

function deleteUser(e) {
    if (e !== undefined)
        e.preventDefault();
    message("Удаление пользователя в разработке");
}

function searchByName(e) {
    if (e !== undefined)
        e.preventDefault();        
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: "Поиск по названию",
            close: function () { $(this).remove() },
            modal: true,
            closeOnEscape: true,
            width: "30%",
            position: { my: "center-15% center", of: window },
            buttons:
                [
                    {
                        text: "Искать",
                        click: function () {
                            var searchText = $("#SearchText").val().trim();
                            if (searchText == "")
                                return;
                            dlg.remove();
                            window.location.href = "/Note/SearchByName/?searchText=" + searchText;                            
                        }
                    },
                    {
                        text: "Закрыть",
                        click: function () {
                            dlg.remove();
                        }
                    }
                ]
        })
        .html('<form id="searchByNameForm"><div class="form-group"><label>Что ищем?</label><input type="text" class="form-control col-md-12 text-box single-line" id="SearchText"/></div></form>');    
}

function searchByCategoryName(e) {
    if (e !== undefined)
        e.preventDefault();
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: "Поиск по названию категории",
            close: function () { $(this).remove() },           
            modal: true,
            closeOnEscape: true,
            width: "30%",
            position: { my: "center-15% center", of: window },
            buttons:
                [
                    {
                        text: "Искать",
                        icons: "ui-icon-search",
                        click: function () {
                            var searchText = $("#SearchText").val().trim();
                            if (searchText == "")
                                return;
                            dlg.remove();
                            window.location.href = "/Note/SearchByCategoryName/?categoryName=" + searchText;
                        }
                    },
                    {
                        text: "Закрыть",
                        click: function () {
                            dlg.remove();
                        }
                    }
                ]
        })
        .html('<div class="form-group"><label>Что ищем?</label><input type="text" class="form-control col-md-12 text-box single-line" id="SearchText"/></div>');    

}

function searchByDate(e) {
    if (e !== undefined)
        e.preventDefault();
    var defaultDate = new Date();
    var dlg = $("<div></div>")
        .addClass("dialog")
        .appendTo("body")
        .dialog({
            title: "Поиск по дате",
            close: function () { $(this).remove() },
            modal: true,
            closeOnEscape: true,
            width: "30%",
            position: { my: "center-15% center", of: window },
            buttons:
                [
                    {
                        text: "Искать",
                        click: function () {
                            var searchDate = new Date($("#SearchDate").val());
                            if (searchDate == "")
                                return;
                            dlg.remove();
                            window.location.href = "/Note/SearchByDate/?date=" + searchDate.toISOString();
                        }
                    },
                    {
                        text: "Закрыть",
                        click: function () {
                            dlg.remove();
                        }
                    }
                ]
        })
        .html('<form><div class="form-group"><label>Что ищем?</label><input type="date" class="form-control col-md-12 text-box single-line" id="SearchDate" value="' +
            defaultDate.toISOString() + '"/></div ></form >');    

}