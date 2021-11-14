using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Schedule.Data;
using Schedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Schedule.Controllers
{
    public class RaspisanieController : Controller
    {
        private readonly ILogger<RaspisanieController> _logger;
        private readonly ApplicationContext db;

        public RaspisanieController(
           ApplicationContext _db,
           ILogger<RaspisanieController> logger
           )

        {
            db = _db;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedulee>>> Get()
        {
            return await db.Schedulee.ToListAsync();
        }
        public IActionResult Index()
        {
            ViewBag.Spec = db.Schedulee.Include(x=>x.Specialization).Select(x => x.Specialization.Name).Distinct().ToList();
            return View();
        }
        [HttpGet]
        public IActionResult Table(string level,string trainingFormat,string specialization,int course)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.TrainingFormat = trainingFormat;
            ViewBag.Level = level;
            ViewBag.Course = course;
            ViewBag.Specialization = specialization;
            ViewBag.EducationalInstitutions = db.Users.FirstOrDefault(x=>x.Id==userId).EducationalInstitutions;
            var user = db.Users.FirstOrDefault(x => x.Id == userId);
            ViewBag.Schedule = db.Schedulee
                .Include(x => x.Lecture)
                .Include(x => x.Teachers)
                .Include(x => x.Group)
                .Include(x => x.Level)
                .Include(x => x.EducationalInstitutions)
                .Include(x => x.Time)
                .Include(x => x.Specialization)
                .Include(x => x.Group.subGroups)
                .Where(x=>x.EducationalInstitutions.Name==user.EducationalInstitutions&&x.Faculty==user.Faculty&&x.Specialization.Name==specialization&&x.TrainingFormat==trainingFormat&&x.Level.Name==level&&x.Course==course)
                .ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddLine(Schedulee schedule)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            schedule.Faculty = db.Users.FirstOrDefault(x => x.Id == userId).Faculty;
            db.Schedulee.Add(schedule);
            await db.SaveChangesAsync();
            return RedirectToAction("Table", "Raspisanie", new { @trainingFormat = schedule.TrainingFormat,@level=schedule.Level.Name, @specialization=schedule.Specialization.Name, @course=schedule.Course });
        }
        public async Task<IActionResult> EditLine(int id,Schedulee schedule)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            schedule.Faculty = db.Users.FirstOrDefault(x => x.Id == userId).Faculty;
            schedule.Id = id;
            db.Schedulee.Update(schedule);
            await db.SaveChangesAsync();
            return RedirectToAction("Table", "Raspisanie", new { @trainingFormat = schedule.TrainingFormat, @level = schedule.Level.Name, @specialization = schedule.Specialization.Name, @course = schedule.Course });
        }
        public async Task<IActionResult> DelLine(int id)
        {
            Schedulee schedule = db.Schedulee
                .Include(x => x.Lecture)
                .Include(x => x.Teachers)
                .Include(x => x.Group)
                .Include(x => x.Level)
                .Include(x => x.EducationalInstitutions)
                .Include(x => x.Time)
                .Include(x => x.Specialization)
                .Include(x => x.Group.subGroups).FirstOrDefault(x => x.Id == id);
            db.Schedulee.Remove(schedule);
            await db.SaveChangesAsync();
            return RedirectToAction("Table", "Raspisanie", new { @trainingFormat = schedule.TrainingFormat, @level = schedule.Level.Name, @specialization = schedule.Specialization.Name, @course = schedule.Course });
        }
        public IActionResult Proba()
        {
            return View();
        }
        
    }
}
