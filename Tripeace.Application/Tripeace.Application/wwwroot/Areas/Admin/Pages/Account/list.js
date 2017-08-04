$(document).ready(function ($) {
    $(".btn-ban").click(function () {
        var idToBan = $(this).attr("data-id");
        $("#modal-ban-confirmation").attr("id-to-ban", idToBan);

        $("#ban-reason").val("");
    }),

    $(".btn-unban").click(function () {
        var idToUnban = $(this).attr("data-id");
        $("#modal-unban-confirmation").attr("id-to-unban", idToUnban);

        $("#unban-reason").val("");
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
            }
        });
    });
});