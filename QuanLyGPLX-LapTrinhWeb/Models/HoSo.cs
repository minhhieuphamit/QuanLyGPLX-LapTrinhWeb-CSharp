using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace QuanLyGPLX_LapTrinhWeb.Models
{
    public class HoSo
    {
        [DisplayName("Hình ảnh")]
        public string HinhAnh { get; set; }
        [DisplayName("Mã GPLX")]
        public string MaGPLX { get; set; }
        [DisplayName("Họ và tên")]
        public string HoTen { get; set; }
        [DisplayName("Ngày/Tháng/Năm")]
        public string NgaySinh { get; set; }
        [DisplayName("Giới tính")]
        public string GioiTinh { get; set; }
        [DisplayName("Số điện thoại")]
        public string SDT { get; set; }
        [DisplayName("Dân tộc")]
        public string DanToc { get; set; }
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }
        [DisplayName("Quốc tịch")]
        public string QuocTich { get; set; }
        [DisplayName("Ngày cấp")]
        public string NgayCap { get; set; }
        [DisplayName("Ngày hết hạn")]
        public string NgayHetHan { get; set; }
        [DisplayName("Hạng GPLX")]
        public string HangGPLX { get; set; }
        [DisplayName("Điểm lý thuyết")]
        public string DiemLT { get; set; }
        [DisplayName("Điểm thực hành")]
        public string DiemTH { get; set; }
        [DisplayName("Trung tâm sát hạch")]
        public string TTSH { get; set; }
    }
}