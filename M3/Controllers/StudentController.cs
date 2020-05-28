using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using M3.Models;
using M3.DB;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace M3.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new DbContext())
            {
                var students = db.Students.Select(s => new StudentModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Surname = s.Surname,
                    BirthDate = s.BirthDate,
                    Class = s.Class,
                }).OrderBy(s => s.Surname).ThenBy(s => s.Name).ToList();

                return View(students);
            }
        }


        [HttpGet]
        public IActionResult Add()
        {
            var student = new StudentModel();
            return View(student);
        }

        [HttpPost]
        public IActionResult Add(StudentModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DbContext())
                {
                    db.Students.Add(new Students()
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        BirthDate = model.BirthDate,
                        Class = model.Class,
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
                var student = db.Students.FirstOrDefault(s => s.Id == id);
                var grades = db.Grades.Where(g => g.Surname == student.Surname && g.Name == student.Name)
                    .Select(x => new GradeModel()
                    {
                        Id = x.Id,
                        Surname = x.Surname,
                        Name = x.Name,
                        Subject = x.Subject,
                        Grade = x.Grade,
                        Description = x.Description,
                    }).ToList();

                return View(grades);
            }
        }
        public IActionResult AvgGrade()
        {
            using (var db = new DbContext())
            {
                var students = db.Students.Select(s => new StudentModel()
                {
                    Surname = s.Surname,
                    Name = s.Name,
                    Class = s.Class
                }).OrderBy(s => s.Surname).ThenBy(s => s.Name).ToList();

                foreach(var student in students)
                {
                    var grades = db.Grades.Where(x => x.Name == student.Name && x.Surname == student.Surname)
                        .Select(x => x.Grade)
                        .ToList();
                    student.AvgGrade = grades.Any() ? grades.Average() : 0;
                }

                return View(students);
            }
        }
    }
}