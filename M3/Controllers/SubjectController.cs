using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using M3.Models;
using M3.DB;

namespace M3.Controllers
{
    public class SubjectController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new DbContext())
            {
                var subjects = db.Subjects.Select(s => new SubjectModel()
                {
                    Id = s.Id,
                    Subject = s.Subject,
                }).OrderBy(s => s.Subject).ToList();

                return View(subjects);
            }
        }

[HttpGet]
        public IActionResult Add()
        {
            var subject = new SubjectModel();
            return View(subject);
        }

        [HttpPost]
        public IActionResult Add(SubjectModel model)
        {
            if(ModelState.IsValid)
            {
                using (var db = new DbContext())
                {
                    db.Subjects.Add(new Subjects()
                    {
                        Subject = model.Subject,
                    });
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        public IActionResult View(int id)
        {
            using (var db = new DbContext())
            {
                var subject = db.Subjects.FirstOrDefault(s => s.Id == id);
                var grades = db.Grades.Where(g => g.Subject == subject.Subject)
                .Select(x => new GradeModel()
                {
                    Id = x.Id,
                    Subject = x.Subject,
                    Surname = x.Surname,
                    Name = x.Name,
                    Grade = x.Grade,
                    Description = x.Description,
                }).ToList();

                return View(grades);
            }
        }
    }
}