//Autocomplete số cccd cho view create
$(document).ready(function () {
    $("#SoCCCD").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/HoSo/GetSoCCCD",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.SoCCCD, value: item.SoCCCD };
                    }))
                }
            })
        },
        messages: {
            noResults: "", function(resultsCount) { }
        }
    });
})

//Process upload
$(function () {
    $('#btnUpload').click(function () {
        $('#fileUpload').trigger('click');
    });
});
$('#fileUpload').change(function () {
    if (window.FormData !== undefined) {
        var fileUpload = $('#fileUpload').get(0);
        var files = fileUpload.files;
        var formData = new FormData();
        formData.append('file', files[0]);

        $.ajax(
            {
                type: 'POST',
                url: '/HoSo/ProcessUpload',
                contentType: false,
                processData: false,
                data: formData,
                success: function (urlImage) {

                    $('#pictureUpload').attr('src', urlImage);
                    $('#HinhAnh').val(urlImage);
                },
                error: function (err) {
                    alert('Error ', err.statusText);
                }
            });
    }
});

//Chặn lỗi
$(document).ready(function () {
    $("form").validate({
        rules: {
            SoCCCD: {
                required: true,
                number: true,
                minlength: 12,
                maxlength: 12
            },
            MaGPLX: {
                required: true,
                number: true,
                minlength: 12,
                maxlength: 12
            },
            MaHang: {
                required: true
            },
            TenTT: {
                required: true
            },
            NgayCapGPLX: {
                required: true
            },
            NgayHetHanGPLX: {
                required: true
            },
            DiemLT: {
                required: true,
                min: 80,
                max: 100
            },
            DiemTH: {
                required: true,
                min: 80,
                max: 100
            },
            HinhAnh: {
                required: true,
            }
        },
        messages: {
            SoCCCD: {
                required: "Vui lòng nhập số căn cước công dân",
                minlength: "Vui lòng nhập đủ 12 số căn cước công dân",
                maxlength: "Số căn cước công dân không hợp lệ",
                number: "Vui lòng nhập số không nhập chữ"
            },
            MaGPLX: {
                required: "Vui lòng nhập mã giấy phép lái xe",
                minlength: "Vui lòng nhập đủ số 12 số mã giấy phép lái xe",
                maxlength: "Mã giấy phép lái xe không hợp lệ",
                number: "Vui lòng nhập số không nhập chữ"
            },
            MaHang: {
                required: "Vui lòng chọn mã hạng"
            },
            TenTT: {
                required: "Vui lòng chọn trung tâm sát hạch"
            },
            NgayCapGPLX: {
                required: "Vui lòng chọn ngày cấp giấy phép lái xe"
            },
            NgayHetHanGPLX: {
                required: "Vui lòng chọn ngày hết hạn giấy phép lái xe"
            },
            DiemLT: {
                required: "Vui lòng nhập điểm lý thuyết",
                min: "Điểm lý thuyết phải lớn hơn hoặc bằng 80",
                max: "Điểm lý thuyết phải nhỏ hơn hoặc bằng 100",
            },
            DiemTH: {
                required: "Vui lòng nhập điểm lý thuyết",
                min: "Điểm lý thuyết phải lớn hơn hoặc bằng 80",
                max: "Điểm lý thuyết phải nhỏ hơn hoặc bằng 100",
            },
            HinhAnh: {
                required: "Vui lòng chọn hình ảnh"
            }
        },
    });
});


//Danh sách hồ sơ
//Tìm kiếm
$(document).ready(function () {
    function Contains(text_one, text_two) {
        if (text_one.indexOf(text_two) != -1)
            return true;
    }
    $("#search").keyup(function () {
        var searchText = $("#search").val().toLowerCase();
        $(".search").each(function () {
            if (!Contains($(this).text().toLowerCase(), searchText))
                $(this).hide();
            else
                $(this).show();
        });
    })
});

//Delete hồ sơ gplx modal popup
function DeleteHoSo(id) {
    $("#myModal").modal('show');
    $("#btnDelete").click(function () {
        $.ajax({
            type: "POST",
            url: "/HoSo/DeleteHoSo",
            data: { Id: id },
            success: function (result) {
                $("#myModal").modal("hide");
                $("#row_" + id).remove();
                location.reload();
            }
        })
    });
    $("#btnCancel").click(function () {
        $("#myModal").modal("hide");
    });
}

//Delete trung tâm sát hạch modal popup
function DeleteTrungTam(id) {
    $("#myModal").modal('show');
    $("#btnDelete").click(function () {
        $.ajax({
            type: "POST",
            url: "/TrungTamSatHach/DeleteTrungTam",
            data: { Id: id },
            success: function (result) {
                $("#myModal").modal("hide");
                $("#row_" + id).remove();
                location.reload();
            }
        })
    });
    $("#btnCancel").click(function () {
        $("#myModal").modal("hide");
    });
}

/*---------Edit---------*/
$(function () {
    $("#NgayCapGPLX").datepicker({
        changeMonth: true,
        changeYear: true,
        yearRange: "1900:9999"
    });
});

$(function () {
    $("#NgayHetHanGPLX").datepicker({
        changeMonth: true,
        changeYear: true,
        yearRange: "1900:9999"
    });
});

$(function () {
    $('#NgayCapGPLX').change(function () {
        var ngayCap = $('#NgayCapGPLX').val();
        var ngayHetHan = $('#NgayHetHanGPLX').val();
        var hangGPLX = $('#MaHang').val();
        var ngayHetHanMoi = new Date(ngayCap);
        var dd, mm, yyyy;
        switch (hangGPLX) {
            case 'A1':
            case 'A2':
            case 'A3':
                ngayHetHanMoi = '12/31/9999';
                $('#NgayHetHanGPLX').val(ngayHetHanMoi);
                break;
            case 'A4':
            case 'B11':
            case 'B12':
            case 'B2':
                ngayHetHanMoi.setFullYear(ngayHetHanMoi.getFullYear() + 10);
                dd = ngayHetHanMoi.getDate();
                mm = ngayHetHanMoi.getMonth() + 1;
                yyyy = ngayHetHanMoi.getFullYear();
                if (dd < 10) {
                    dd = '0' + dd;
                }
                if (mm < 10) {
                    mm = '0' + mm;
                }
                ngayHetHanMoi = mm + '/' + dd + '/' + yyyy;
                $('#NgayHetHanGPLX').val(ngayHetHanMoi);
                break;
            case 'C':
            case 'D':
            case 'E':
            case 'FB2':
            case 'FC':
            case 'FD':
            case 'FE':
                ngayHetHanMoi.setFullYear(ngayHetHanMoi.getFullYear() + 5);
                dd = ngayHetHanMoi.getDate();
                mm = ngayHetHanMoi.getMonth() + 1;
                yyyy = ngayHetHanMoi.getFullYear();
                if (dd < 10) {
                    dd = '0' + dd;
                }
                if (mm < 10) {
                    mm = '0' + mm;
                }
                ngayHetHanMoi = mm + '/' + dd + '/' + yyyy;
                $('#NgayHetHanGPLX').val(ngayHetHanMoi);
                break;
        }
    });
});

//người dùng tra cứu hồ sơ


//window.addEventListener('DOMContentLoaded', event => {

//    // Toggle the side navigation
//    const sidebarToggle = document.body.querySelector('#sidebarToggle');
//    if (sidebarToggle) {
//        // Uncomment Below to persist sidebar toggle between refreshes
//        // if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
//        //     document.body.classList.toggle('sb-sidenav-toggled');
//        // }
//        sidebarToggle.addEventListener('click', event => {
//            event.preventDefault();
//            document.body.classList.toggle('sb-sidenav-toggled');
//            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
//        });
//    }

//});