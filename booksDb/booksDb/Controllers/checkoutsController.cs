using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using booksDb.Models;

namespace booksDb.Controllers
{
    public class checkoutsController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: checkouts
        public ActionResult Index()
        {
            var checkouts = db.checkouts.Include(c => c.book).Include(c => c.user);
            return View(checkouts.ToList());
        }

        // GET: checkouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            checkout checkout = db.checkouts.Find(id);
            if (checkout == null)
            {
                return HttpNotFound();
            }
            return View(checkout);
        }

        // GET: checkouts/Create
        public ActionResult Create()
        {
            ViewBag.book_id = new SelectList(db.books, "Id", "title");
            ViewBag.user_id = new SelectList(db.users, "Id", "firstName");
            return View();
        }

        // POST: checkouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,user_id,book_id,checkout_date,return_date")] checkout checkout)
        {
            if (ModelState.IsValid)
            {
                db.checkouts.Add(checkout);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.book_id = new SelectList(db.books, "Id", "title", checkout.book_id);
            ViewBag.user_id = new SelectList(db.users, "Id", "firstName", checkout.user_id);
            return View(checkout);
        }

        // GET: checkouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            checkout checkout = db.checkouts.Find(id);
            if (checkout == null)
            {
                return HttpNotFound();
            }
            ViewBag.book_id = new SelectList(db.books, "Id", "title", checkout.book_id);
            ViewBag.user_id = new SelectList(db.users, "Id", "firstName", checkout.user_id);
            return View(checkout);
        }

        // POST: checkouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,user_id,book_id,checkout_date,return_date")] checkout checkout)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkout).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.book_id = new SelectList(db.books, "Id", "title", checkout.book_id);
            ViewBag.user_id = new SelectList(db.users, "Id", "firstName", checkout.user_id);
            return View(checkout);
        }

        // GET: checkouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            checkout checkout = db.checkouts.Find(id);
            if (checkout == null)
            {
                return HttpNotFound();
            }
            return View(checkout);
        }

        // POST: checkouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            checkout checkout = db.checkouts.Find(id);
            db.checkouts.Remove(checkout);
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
