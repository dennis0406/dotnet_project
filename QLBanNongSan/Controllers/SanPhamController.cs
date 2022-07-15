﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBanNongSan.Models;

namespace QLBanNongSan.Controllers
{

    public class SanPhamController : BaseCheckController
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        
        // GET: SanPham
        public ActionResult Index()

        {
            CheckCart();
            var sp = from s in data.San_phams orderby s.ma_san_pham descending select s;
            List < San_pham > sanpham = sp.ToList();

            var spbc = from p in data.San_phams
                      join o in data.CT_hoa_dons
                      on p.ma_san_pham equals o.ma_san_pham
                      select p;

            
            List<San_pham> spbcf = spbc.ToList();

            ViewBag.spbc = spbcf;
            return View(sanpham);
        }

        public ActionResult DanhMuc(int id)
        {
            CheckCart();
            var loai = from l in data.Loai_nong_sans where l.ma_nong_san == id select l;
            ViewBag.loai = loai.Single();
            var danhMucSP = from s in data.San_phams where s.ma_nong_san == id select s;
            if (danhMucSP.Count() == 0)
            {
                ViewBag.empty = "Danh mục đang trong quá trình cập nhật!";
            }
            return View(danhMucSP.ToList());
        }

        
        //Hiển thị chi tiết sản phẩm
        public ActionResult ChiTiet(int id)
        {
            CheckCart();

            var sanpham = from s in data.San_phams where s.ma_san_pham == id select s;
            return View(sanpham.Single());
        }
    }
}