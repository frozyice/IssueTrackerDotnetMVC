using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTrackerDotnetMVC.Models
{
    public class Issue
    {
        public Guid IssueId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime DateCreated { get; set; }

        public Issue()
        {
            IssueId = Guid.NewGuid();
        }
    }
}