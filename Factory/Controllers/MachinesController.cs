using System.Collections.Generic;
using System.Linq;
using Factory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Factory.Controllers
{
    public class MachinesController : Controller
    {
        private readonly FactoryContext _db;
        public MachinesController(FactoryContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            List<Machine> model = _db.Machines.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Machine machine, int engineerId)
        {
            _db.Machines.Add(machine);
            if (engineerId != 0)
            {
                _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = engineerId, MachineId = machine.MachineId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Machine thisMachine = _db.Machines
                .Include(machine => machine.Engineers)
                    .ThenInclude(join => join.Engineer)
                .FirstOrDefault(machine => machine.MachineId == id);
            return View(thisMachine);
        }

        public ActionResult AddEngineer(int id)
        {
            Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
            ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
            return View(thisMachine);
        }

        [HttpPost]
        public ActionResult AddEngineer(Machine machine, int engineerId)
        {
            if (engineerId != 0)
            {
                _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = engineerId, MachineId = machine.MachineId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
            ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
            return View(thisMachine);
        }

        [HttpPost]
        public ActionResult Edit(Machine machine, int engineerId)
        {
            bool duplicate = _db.EngineerMachine.Any(engiMach => engiMach.EngineerId == engineerId && engiMach.MachineId == machine.MachineId);
            if (engineerId != 0 && !duplicate)
            {
                _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = engineerId, MachineId = machine.MachineId });
            }
            _db.Entry(machine).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
            return View(thisMachine);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
            _db.Machines.Remove(thisMachine);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}