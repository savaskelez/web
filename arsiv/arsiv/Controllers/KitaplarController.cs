using arsiv.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using arsiv.ViewModels;

namespace arsiv.Controllers
{
    [Authorize]
    public class KitaplarController : Controller
    {
        ArsivEntities db = new ArsivEntities();
        // GET: Kitaplar
        public ActionResult Index()
        {
            var model = db.Kitaplar.Include(x => x.Turler).Include(x => x.Yazarlar).ToList();
            return View(model);
        }
        public ActionResult Yeni()
        {
            var model = new KitaplarFormViewModel()
            {
                Turlers=db.Turler.ToList(),
                Yazarlars=db.Yazarlar.ToList()
            };
            return View("KitaplarForm", model);
        }
        [HttpGet]
        public ActionResult Kaydet()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Kaydet(Kitaplar kitaplar)
        {
            if (kitaplar.ID == 0 )
            {
                db.Kitaplar.Add(kitaplar);
            }
            else//Güncelleme
            {
                db.Entry(kitaplar).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Guncelle(int id)
        {
            var model = new KitaplarFormViewModel() {
                Turlers = db.Turler.ToList(),
                Yazarlars = db.Yazarlar.ToList(),
                Kitaplar = db.Kitaplar.Find(id)
            };
            return View("KitaplarForm", model);
        }
        public ActionResult Sil(int id)
        {
            var silinecekKitaplar = db.Kitaplar.Find(id);
            if (silinecekKitaplar == null)
                return HttpNotFound();
            db.Kitaplar.Remove(silinecekKitaplar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}