using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eticaret2.Models;

namespace Eticaret2.Controllers
{
    public class UrunController : Controller
    {
        private Context db = new Context();

        // GET: Urun
        public ActionResult Index()
        {
            return View(db.Uruns.ToList());
        }

        // GET: Urun/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Uruns.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // GET: Urun/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Urun/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Urun urun)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string extn = Path.GetExtension(Request.Files[0].FileName);
                string url = "/UrunFoto/" + filename + extn;
                Request.Files[0].SaveAs(Server.MapPath(url));
                urun.Foto = "/UrunFoto/" + filename + extn;
                db.Uruns.Add(urun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(urun);
        }

        // GET: Urun/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Uruns.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // POST: Urun/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Urun urun)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string extn = Path.GetExtension(Request.Files[0].FileName);
                string url = "/UrunFoto/" + filename + extn;
                Request.Files[0].SaveAs(Server.MapPath(url));
                urun.Foto = "/UrunFoto/" + filename + extn;
                db.Entry(urun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(urun);
        }

        // GET: Urun/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Uruns.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // POST: Urun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Urun urun = db.Uruns.Find(id);
            db.Uruns.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
