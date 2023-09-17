using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_Assignment.Models;

namespace FIT5032_Assignment.Controllers
{
    public class CTMachinesController : Controller
    {
        private CTMachineEntities db = new CTMachineEntities();

        // GET: CTMachines
        public ActionResult CTMachineManagement()
        {
            return View(db.CTMachines.ToList());
        }

        // GET: CTMachines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTMachine cTMachine = db.CTMachines.Find(id);
            if (cTMachine == null)
            {
                return HttpNotFound();
            }
            return View(cTMachine);
        }

        // GET: CTMachines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CTMachines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Status")] CTMachine cTMachine)
        {
            if (ModelState.IsValid)
            {
                db.CTMachines.Add(cTMachine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cTMachine);
        }

        // GET: CTMachines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTMachine cTMachine = db.CTMachines.Find(id);
            if (cTMachine == null)
            {
                return HttpNotFound();
            }
            return View(cTMachine);
        }

        // POST: CTMachines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Status")] CTMachine cTMachine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTMachine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cTMachine);
        }

        // GET: CTMachines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTMachine cTMachine = db.CTMachines.Find(id);
            if (cTMachine == null)
            {
                return HttpNotFound();
            }
            return View(cTMachine);
        }

        // POST: CTMachines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CTMachine cTMachine = db.CTMachines.Find(id);
            db.CTMachines.Remove(cTMachine);
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
