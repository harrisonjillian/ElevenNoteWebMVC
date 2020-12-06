using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        // GET: Note
        public ActionResult Index()  // Displays all the notes for the current user
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            var model = service.GetNotes();

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)  //Makes sure teh model is valid, grabs the current userID, calls on teh Create Note and returns back to the index view
        {
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var userId = Guid.Parse(User.Identity.GetUserId());
                var service = new NoteService(userId);

                service.CreateNote(model);

                return RedirectToAction("Index");
            }
        }
    }
}