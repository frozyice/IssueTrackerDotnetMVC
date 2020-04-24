using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IssueTrackerDotnetMVC.Models
{
    public class Issue
    {
        public Guid IssueId { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime DateCreated { get; set; }

        public Issue()
        {
            IssueId = Guid.NewGuid();
            DateCreated = DateTime.Now;
        }
    }
}