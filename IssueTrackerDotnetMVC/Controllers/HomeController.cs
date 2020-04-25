using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IssueTrackerDotnetMVC.DatabaseContext;
using IssueTrackerDotnetMVC.Models;
using IssueTrackerDotnetMVC.Repositories;

namespace IssueTrackerDotnetMVC.Controllers
{
    public class HomeController : Controller
    {
        private IIssueRepository repo;

        public HomeController()
        {
            repo = new IssueRepository();
        }

        public HomeController(IIssueRepository issueRepository)
        {
            repo = issueRepository;
        }

        // GET: Issues 
        public ViewResult Index()
        {
            return View(repo.ReadAll());
        }

        // GET: Issues/Create 
        public ViewResult Create()
        {
            return View();
        }

        // POST: Issues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "IssueId,Description,Deadline")] Issue issue)
        public ActionResult Create([Bind(Include = "Description,Deadline")] Issue issue)
        {
            if (issue.Description != null && issue.Deadline != default(DateTime))
            {
                repo.Save(issue);
            }
            return RedirectToAction("Index");
        }

        // GET: Issues/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = repo.ReadOne(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issue issue = repo.ReadOne(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            repo.Delete(issue);
            return RedirectToAction("Index");
        }
    }
}
