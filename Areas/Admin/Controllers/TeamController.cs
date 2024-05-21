using Lumia.DAL;
using Lumia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Numerics;

namespace Lumia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public TeamController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View(_context.Teams.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]


        public IActionResult Create(Team team)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            string fileName = Guid.NewGuid() + team.ImgFile.FileName;
            string path = _environment.WebRootPath + @"\Upload\Team\";

            using(FileStream stream = new FileStream(path+fileName,FileMode.Create))
            {
                team.ImgFile.CopyTo(stream);
            }
            team.ImgUrl = fileName;
            _context.Teams.Add(team);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        public IActionResult Update(int id) { 
        
            Team team = _context.Teams.FirstOrDefault(x=>x.Id==id);    

            if (team == null)
            {
                return RedirectToAction("Index");
            }


            return View(team);

        }
        [HttpPost]

        public IActionResult Update(Team newteam)

        {
            Team oldteam = _context.Teams.FirstOrDefault(x => x.Id == newteam.Id);

            if (!ModelState.IsValid)
            {
                return View(oldteam);

            }
            if (newteam.ImgFile != null)
            {
                string fileName = Guid.NewGuid() + newteam.ImgFile.FileName;
                string path = _environment.WebRootPath + @"\Upload\Team\";

                using (FileStream stream = new FileStream(path + fileName, FileMode.Create))
                {
                    newteam.ImgFile.CopyTo(stream);
                }
                oldteam.ImgUrl = fileName;
            }

            oldteam.FUllName=newteam.FUllName;
            oldteam.Position=newteam.Position;
            oldteam.Description=newteam.Description;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Team team = _context.Teams.FirstOrDefault(x => x.Id == id);
            if (team == null) return NotFound();


            string imagePath = Path.Combine(_environment.WebRootPath, "Upload", "Doctor", team.ImgUrl);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _context.Teams.Remove(team);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
