using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M3.DB;
using M3.Models;
using Microsoft.AspNetCore.Mvc;

namespace M3.Controllers
{
    public class GradeController : Controller
    {
        public IActionResult Index()
        {
            using(var db = new DbContext())
            {
                var grades = db.Grades.Select(g => new GradeModel()
                {
                    Id = g.Id,
                    Surname = g.Surname,
                    Name = g.Name,
                    Subject = g.Subject,
                    Grade = g.Grade,
                    Description = g.Description,
                }).OrderByDescending(g => g.Grade).ToList();

                return View(grades);
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new GradeModel();
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Add(GradeModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DbContext())
                {
                    var student = db.Students.FirstOrDefault(s => s.Name == model.Name && s.Surname == model.Surname);
                    var subject = db.Subjects.FirstOrDefault(s => s.Subject == model.Subject);
                    var isGradeValid = model.Grade >= 1 && model.Grade <= 10;

                    if (student == null)
                    {
                        ModelState.AddModelError("StudentNotFound", "Student not found!");
                    }

                    if (subject == null)
                    {
                        ModelState.AddModelError("SubjectNotFound", "Subject not found!");
                    }

                    if (!isGradeValid)
                    {
                        ModelState.AddModelError("GradeOutOfRange", "Grade has to be between 1 - 10!");
                    }

                    if (student != null && subject != null && isGradeValid)
                    {
                        db.Grades.Add(new DB.Grades()
                        {
                            Surname = model.Surname,
                            Name = model.Name,
                            Subject = model.Subject,
                            Grade = model.Grade,
                            Description = model.Description,
                        });
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                }
            }
            return View(model);
        }
    }
}
