using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IssueTrackerDotnetMVC.DatabaseContext;
using IssueTrackerDotnetMVC.Models;
using System.Data.Entity;

namespace IssueTrackerDotnetMVC.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        IssueDbContext db = new IssueDbContext();

        public List<Issue> ReadAll()
        {
            return db.Issues.OrderBy(x => x.Deadline).ToList();
        }

        public void Delete(Issue issue)
        {
            db.Issues.Remove(issue);
            db.SaveChanges();
        }

        public void Save(Issue issue)
        {
                db.Issues.Add(issue);
                db.SaveChanges();
        }

        public Issue ReadOne(Guid? id)
        {
            if (id==null)
            {
                return null;
            }
            Issue issue = db.Issues.Find(id);
            return issue;
        }


    }
}