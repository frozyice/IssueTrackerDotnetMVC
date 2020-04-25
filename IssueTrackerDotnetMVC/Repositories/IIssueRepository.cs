using IssueTrackerDotnetMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTrackerDotnetMVC.Repositories
{
    public interface IIssueRepository
    {
        List<Issue> ReadAll();
        Issue ReadOne(Guid? id);
        void Save(Issue issue);
        void Delete(Issue issue);
    }
}
