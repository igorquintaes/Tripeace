$(document).ready(function ($) {
    $(".btn-ban-action").click(function () {
        var accountId = $(this).attr("data-id");
        var action = $(this).attr("data-action");

        if (action == "ban") {
            $("#modal-ban-confirmation").attr("id-to-ban", accountId);
            $("#ban-reason").val("");
        }

        if (action == "unban") {
            $("#modal-unban-confirmation").attr("id-to-unban", accountId);
            $("#unban-reason").val("");
        }
    }),

    $(".btn-unban").click(function () {
    }),

    $(function () {
        $('#datetimepicker-ban').datepicker({
            startDate: '+1d',
            autoclose: true
        });
    }),

    $("#ban-submit").click(function () {
        var dataType = 'application/json; charset=utf-8';
        var model = {
            "Id": parseInt($("#modal-ban-confirmation").attr("id-to-ban")),
            "Reason": $("#ban-reason").val(),
            "Date": $("#datetimepicker-ban").val()
        };

        var json = JSON.stringify(model);

        $('#modal-ban-confirmation').modal('hide');

        $.ajax({
            type: 'POST',
            url: '/Admin/Account/Ban',
            dataType: 'json',
            contentType: 'application/json',
            data: json,
            success: function (result) {
                $("#modal-server-response-title").html(result.title);
                $("#modal-server-response-body").html(result.message);
                $("#modal-server-response").modal("show");

                toogleButton("unban", model.Id);
            }
        });
    });

    $("#unban-submit").click(function () {
        var dataType = 'application/json; charset=utf-8';
        var model = {
            "Id": parseInt($("#modal-unban-confirmation").attr("id-to-unban")),
            "Reason": $("#unban-reason").val(),
        };

        var json = JSON.stringify(model);

        $('#modal-unban-confirmation').modal('hide');

        $.ajax({
            type: 'POST',
            url: '/Admin/Account/Unban',
            dataType: 'json',
            contentType: 'application/json',
            data: json,
            success: function (result) {
                $("#modal-server-response-title").html(result.title);
                $("#modal-server-response-body").html(result.message);
                $("#modal-server-response").modal("show");

                toogleButton("ban", model.Id);
            }
        });
    });

    function toogleButton(newButton, id) {
        var button = $(".btn-ban-action[data-id='" + id + "']");

        if (newButton == "ban") {
            button.html('<i class="fa fa-warning"></i> &thinsp;&thinsp;' + localizer.BanButton);
            button.attr("data-target", "#modal-ban-confirmation");
            button.attr("data-action", "ban");
        }

        if (newButton == "unban") {
            button.html('<i class="fa fa-warning"></i> &thinsp;&thinsp;' + localizer.UnbanButton);
            button.attr("data-target", "#modal-unban-confirmation");
            button.attr("data-action", "unban");
        }
    }
});