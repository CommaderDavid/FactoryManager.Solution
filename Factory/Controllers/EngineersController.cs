using System.Collections.Generic;
using System.Linq;
using Factory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Factory.Controllers
{
    public class EngineersController : Controller
    {
        private readonly FactoryContext _db;

        public EngineersController(FactoryContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Engineer> model = _db.Engineers.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Engineer engineer, int machineId)
        {
            _db.Engineers.Add(engineer);
            if (machineId != 0)
            {
                _db.EngineerMachine.Add(new EngineerMachine() { MachineId = machineId, EngineerId = engineer.EngineerId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Engineer thisEngineer = _db.Engineers
                .Include(engineer => engineer.Machines)
                    .ThenInclude(join => join.Machine)
                .FirstOrDefault(engineer => engineer.EngineerId == id);
            return View(thisEngineer);
        }

        public ActionResult AddMachine(int id)
        {
            Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
            ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
            return View(thisEngineer);
        }

        [HttpPost]
        public ActionResult AddMachine(Engineer engineer, int machineId)
        {
            if (machineId != 0)
            {
                _db.EngineerMachine.Add(new EngineerMachine() { MachineId = machineId, EngineerId = engineer.EngineerId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
            ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
            return View(thisEngineer);
        }

        [HttpPost]
        public ActionResult Edit(Engineer engineer, int machineId)
        {
            bool duplicate = _db.EngineerMachine.Any(engiMach => engiMach.MachineId == machineId && engiMach.EngineerId == engineer.EngineerId);
            if (machineId != 0 && !duplicate)
            {
                _db.EngineerMachine.Add(new EngineerMachine() { MachineId = machineId, EngineerId = engineer.EngineerId });
            }
            _db.Entry(engineer).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
            return View(thisEngineer);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
            _db.Engineers.Remove(thisEngineer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}