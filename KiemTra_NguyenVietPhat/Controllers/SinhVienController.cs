using KiemTra_NguyenVietPhat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KiemTra_NguyenVietPhat.Controllers
{
    public class SinhVienController : Controller
    {
        MyDaTaDataContext data = new MyDaTaDataContext();

        public object E_sinhvien { get; private set; }

        // GET: SinhVien
        public ActionResult ListSinhVien()
        {
            var all_sinhvien = from ss in data.SinhViens select ss;
            return View(all_sinhvien);
        }
        public ActionResult Detail(string id)
        {
            var D_sinhvien = data.SinhViens.Where(m => m.MaSV == id).First();
            return View(D_sinhvien);
        }
        public ActionResult Create(string id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string id, FormCollection collection, SinhVien s)
        {
            var E_hoten = collection["hoten"];
            var E_gioitinh = collection["gioitinh"];
            var E_ngaysinh = Convert.ToDateTime(collection["ngaysinh"]);
            var E_hinh = collection["hinh"];
            var E_manganh = collection["manganh"];
            if (string.IsNullOrEmpty(E_hoten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.HoTen = E_hoten.ToString();
                s.GioiTinh = E_gioitinh.ToString();
                s.NgaySinh = E_ngaysinh;
                s.Hinh = E_hinh.ToString();
                s.MaNganh = E_manganh.ToString();
                data.SinhViens.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("ListSinhVien");
            }
            return this.Create(id);
        }

        public ActionResult Edit(string id)
        {
            var E_sach = data.SinhViens.First(m => m.MaSV == id);
            return View(E_sach);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var E_sinhvien = data.SinhViens.First(m => m.MaSV == id);
            var E_hoten = collection["hoten"];
            var E_gioitinh = collection["gioitinh"];
            var E_ngaysinh = Convert.ToDateTime(collection["ngaysinh"]);
            var E_hinhh = collection["hinh"];
            var E_manganh = collection["manganh"];
            E_sinhvien.MaSV = id;
            if (string.IsNullOrEmpty(E_hoten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_sinhvien.HoTen = E_hoten;
                E_sinhvien.GioiTinh = E_gioitinh;
                E_sinhvien.NgaySinh = E_ngaysinh;
                E_sinhvien.Hinh = E_hinhh;
                E_sinhvien.MaNganh = E_manganh;
                UpdateModel(E_sinhvien);
                data.SubmitChanges();
                return RedirectToAction("ListSinhVien");
            }
            return this.Edit(id);
        }
    }
}