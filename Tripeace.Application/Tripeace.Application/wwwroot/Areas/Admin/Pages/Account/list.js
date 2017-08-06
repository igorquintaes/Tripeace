$(document).ready(function ($) {
    $(".btn-modal-action").click(function () {
        var accountId = $(this).attr("data-id");
        var action = $(this).attr("data-action");

        if (action === "ban") {
            $("#modal-ban-confirmation").attr("account-id", accountId);
            $("#ban-reason").val("");
            $("#datetimepicker-ban").val("");
        }

        if (action === "unban") {
            $("#modal-unban-confirmation").attr("account-id", accountId);
            $("#unban-reason").val("");
        }

        if (action === "delete")
        {
            $("#modal-delete-confirmation").attr("account-id", accountId);
        }
    }),

    $(function () {
        $('#datetimepicker-ban').datepicker({
            startDate: '+1d',
            autoclose: true
        });
    }),

    $("#ban-submit").click(function (e) {
        var dataType = 'application/json; charset=utf-8';
        var model = {
            "Id": parseInt($("#modal-ban-confirmation").attr("account-id")),
            "Reason": $("#ban-reason").val(),
            "Date": $("#datetimepicker-ban").val()
        };

        if (model.Date.length <= 0) {
            $("#datetimepicker-ban").parent().addClass("has-error");

            e.preventDefault();
            e.stopPropagation();
            return;
        }

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

                $("#datetimepicker-ban").parent().removeClass("has-warning");
                toogleButton("unban", model.Id);
            }
        });
    });

    $("#unban-submit").click(function () {
        var dataType = 'application/json; charset=utf-8';
        var model = {
            "Id": parseInt($("#modal-unban-confirmation").attr("account-id")),
            "Reason": $("#unban-reason").val()
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

    $("#delete-submit").click(function () {
        var dataType = 'application/json; charset=utf-8';
        var id = parseInt($("#modal-delete-confirmation").attr("account-id"));

        var json = JSON.stringify(id);

        $('#modal-delete-confirmation').modal('hide');

        $.ajax({
            type: 'POST',
            url: '/Admin/Account/Delete',
            dataType: 'json',
            contentType: 'application/json',
            data: json,
            success: function (result) {
                $("#modal-server-response-title").html(result.title);
                $("#modal-server-response-body").html(result.message);
                $("#modal-server-response").modal("show");

                var baseRow = $(".btn-modal-action[data-id='" + id + "'][data-action='delete']").closest('tr.account-data');
                baseRow.html("<td>" + localizer.DeletedRow + "</td>");
                baseRow.addClass("danger")
                baseRow.find("td").attr("colspan", "100%");
            }
        });
    });

    function toogleButton(newButton, id) {
        var button = $(".btn-modal-action[data-id='" + id + "'][data-action='ban'], .btn-modal-action[data-id='" + id + "'][data-action='unban']");

        if (newButton === "ban") {
            button.html('<i class="fa fa-warning"></i> &thinsp;&thinsp;' + localizer.BanButton);
            button.attr("data-target", "#modal-ban-confirmation");
            button.attr("data-action", "ban");
        }

        if (newButton === "unban") {
            button.html('<i class="fa fa-warning"></i> &thinsp;&thinsp;' + localizer.UnbanButton);
            button.attr("data-target", "#modal-unban-confirmation");
            button.attr("data-action", "unban");
        }
    }
});