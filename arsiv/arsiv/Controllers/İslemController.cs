using arsiv.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace arsiv.Controllers
{
    [Authorize]
    public class İslemController : Controller
    {
        ArsivEntities db = new ArsivEntities();
  
        public ActionResult Index()
        {
            var model = db.İslem.Include(x => x.Uyeler).Include(x => x.Kitaplar).ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Yeni()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Yeni(İslem islem)
        {
    
            if (islem.ID == 0)
            {
                db.İslem.Add(islem);
            }
            else
            {
                var guncellenecekİslem = db.İslem.Find(islem.ID);
                if (guncellenecekİslem == null)
                {
                    return HttpNotFound();
                }
                guncellenecekİslem.Uye_id = islem.Uye_id;
                guncellenecekİslem.Kitap_id = islem.Kitap_id;
                guncellenecekİslem.atarih = islem.atarih;
                guncellenecekİslem.vtarih = islem.vtarih;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "İslem");
        }
        public ActionResult Guncelle(int ID)
        {
            var model = db.İslem.Find(ID);
            if (model == null)
                return HttpNotFound();
            return View("Yeni", model);
        }
        public ActionResult Sil(int ID)
        {
            var silinecekİslem = db.İslem.Find(ID);
            if (silinecekİslem == null)
                return HttpNotFound();
            db.İslem.Remove(silinecekİslem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}