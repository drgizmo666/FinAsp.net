using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Crossword.Models;

namespace Crossword.Controllers
{
    public class ClueController : Controller
    {
        private CrosswordDBContext db = new CrosswordDBContext();

        // GET: /Clue/
        public ActionResult Index()
        {
            return View(db.Clues.ToList());
        }

        // GET: /Clue/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clue clue = db.Clues.Find(id);
            if (clue == null)
            {
                return HttpNotFound();
            }
            return View(clue);
        }

        // GET: /Clue/Create
        public ActionResult Create()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Across", Value = "Across", Selected = true });
            items.Add(new SelectListItem { Text = "Down", Value = "Down" });
            ViewBag.DirectionList = items;
            return View();
        }

        // POST: /Clue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClueId,XCoord,YCoord,Direction,Number,Answer,AnswerClue")] Clue clue, string DirectionList)
        {
            if (ModelState.IsValid)
            {
                clue.Direction = DirectionList;
                db.Clues.Add(clue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clue);
        }

        // GET: /Clue/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clue clue = db.Clues.Find(id);
            if (clue == null)
            {
                return HttpNotFound();
            }
            return View(clue);
        }

        // POST: /Clue/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ClueId,XCoord,YCoord,Direction,Number,Answer,AnswerClue")] Clue clue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clue);
        }

        // GET: /Clue/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clue clue = db.Clues.Find(id);
            if (clue == null)
            {
                return HttpNotFound();
            }
            return View(clue);
        }

        // POST: /Clue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clue clue = db.Clues.Find(id);
            db.Clues.Remove(clue);
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
