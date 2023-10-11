using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_Assignment.Models;
using System.Web.Services.Description;

namespace FIT5032_Assignment.Controllers
{
    public class AppointmentsController : Controller
    {
        private AppointmentEntities1 db = new AppointmentEntities1();
        private CTMachineEntities db2 = new CTMachineEntities();


        // Interactive table
        public ActionResult AppointmentList()
        {
            var role = Session["Role"] as string;

            if (role == "STAFF")
            {
                return View(db.Appointments.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
                
        }

        // GET: Appointments
        public ActionResult AppointmentManagement()
        {
            var role = Session["Role"] as string;

            if (role == "ADMIN")
            {
                return View(db.Appointments.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            // Fetch the list of machine IDs from your database
            var machineList = db2.CTMachines.Select(m => new { m.ID }).ToList();

            // Create a SelectList
            ViewBag.MachineID = new SelectList(machineList, "ID", "ID");
            var role = Session["Role"] as string;

            if (role == "STAFF" || role == "ADMIN")
            {
                return View();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MachineID,Date,PatientName")] Appointment appointment)
        {
            // Fetch the list of machine IDs from your database
            var machineList = db2.CTMachines.Select(m => new { m.ID }).ToList();

            // Create a SelectList
            ViewBag.MachineID = new SelectList(machineList, "ID", "ID");
            if (ModelState.IsValid)
            {
                // Check for day conflict
                var conflictingAppointment = db.Appointments
                                                .Where(a => a.MachineID == appointment.MachineID && DbFunctions.TruncateTime(a.Date) == DbFunctions.TruncateTime(appointment.Date))
                                                .FirstOrDefault();
                if (conflictingAppointment != null)
                {
                    ModelState.AddModelError("Date", "There is already an appointment for this machine on the same day.");
                    return View(appointment);
                }
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("AppointmentManagement");
            }

            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MachineID,Date,PatientName")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AppointmentManagement");
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("AppointmentManagement");
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
