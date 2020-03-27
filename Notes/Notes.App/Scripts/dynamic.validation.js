(function ($) {
    $.validator.unobtrusive.parseDynamicContent = function (selector) {        
        $.validator.unobtrusive.parse(selector);        
        var form = $(selector).first().closest('form');
        var unobtrusiveValidation = form.data('unobtrusiveValidation');
        var validator = form.validate();

        $.each(unobtrusiveValidation.options.rules, function (elname, elrules) {
            if (validator.settings.rules[elname] == undefined) {
                var args = {};
                $.extend(args, elrules);
                args.messages = unobtrusiveValidation.options.messages[elname];                
                $("[name='" + elname + "']").rules("add", args);
            } else {
                $.each(elrules, function (rulename, data) {
                    if (validator.settings.rules[elname][rulename] == undefined) {
                        var args = {};
                        args[rulename] = data;
                        args.messages = unobtrusiveValidation.options.messages[elname][rulename];                        
                        $("[name='" + elname + "']").rules("add", args);
                    }
                });
            }
        });
    }
})($);