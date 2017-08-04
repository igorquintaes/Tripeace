$(document).ready(function ($) {
    $(".btn-ban").click(function () {
        var idToBan = $(this).attr("data-id");
        $("#modal-ban-confirmation").attr("id-to-ban", idToBan);

        $("#ban-reason").val("");
    }),

    $("#ban-submit").click(function () {
        var dataType = 'application/json; charset=utf-8';
        var model = {
            Id: $("#modal-ban-confirmation").attr("id-to-ban"),
            Reason: $("#ban-reason").val()
        };

        $('#modal-ban-confirmation').modal('hide')

        $.ajax({
            type: 'POST',
            url: '/Admin/Account/Ban',
            dataType: 'json',
            contentType: dataType,
            data: model,
            success: function (result) {
                $("#modal-server-response-title").html(result.title);
                $("#modal-server-response-body").html(result.message);
                
                $("#modal-server-response").modal("show");
            }
        });
    });
});