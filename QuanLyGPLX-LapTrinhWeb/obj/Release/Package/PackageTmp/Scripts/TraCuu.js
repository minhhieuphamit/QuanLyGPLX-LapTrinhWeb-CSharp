$(function () {
    $("#btnTraCuu").click(function () {
        var MaGPLX = $("#MaGPLX").val()
        var HangGPLX = $("#HangGPLX").val()
        $.ajax({
            url: '/Home/TraCuuHoSo',
            type: 'POST',
            data: { MaGPLX: MaGPLX, HangGPLX: HangGPLX },
            dataType: "html",
            success: function (response) {
                $("#partialModal").find(".modal-body").html(response);
                $("#partialModal").modal('show');
            },
            error: function (response) {
                $("#errorModal").modal('show');
            }
        });
    });
    $("#btnCancel").click(function () {
        $("#partialModal").modal("hide");
        $("#errorModal").modal('hide')
    });
});

