using NUnit.Framework;
using System;
using IssueTrackerDotnetMVC.Controllers;
using System.Web.Mvc;
using IssueTrackerDotnetMVC.Repositories;
using System.Collections.Generic;
using IssueTrackerDotnetMVC.Models;
using Moq;

namespace Tests
{
    public class TestHomeController
    {

        [Test]
        public void Index_ReturnsView()
        {
            Mock<IIssueRepository> mock = new Mock<IIssueRepository>();
            HomeController controller = new HomeController(mock.Object);

            var result = controller.Index();
            
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Index_ViewContainsModel()
        {
            Mock<IIssueRepository> mock = new Mock<IIssueRepository>();
            mock.Setup(m => m.ReadAll()).Returns(new List<Issue> {new Issue {}});
            HomeController controller = new HomeController(mock.Object);

            var result = (List<Issue>)controller.Index().Model;

            Assert.IsInstanceOf<List<Issue>>(result);
        }

        [Test]
        public void Index_ReturnsCorrectNumberOfIssues()
        {
            Mock<IIssueRepository> mock = new Mock<IIssueRepository>();
            mock.Setup(m => m.ReadAll()).Returns(new List<Issue> { new Issue { }, new Issue { }, new Issue { } });
            HomeController controller = new HomeController(mock.Object);

            var model = (List<Issue>)controller.Index().Model;
            var result = model.Count;

            var expected = 3;
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void CreateGET_ReturnsView()
        {
            Mock<IIssueRepository> mock = new Mock<IIssueRepository>();
            HomeController controller = new HomeController(mock.Object);

            var result = controller.Create();

            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void CreatePOST_RedirectToRouteIndex()
        {
            var issue = new Issue();
            Mock<IIssueRepository> mock = new Mock<IIssueRepository>();
            HomeController controller = new HomeController(mock.Object);

            var redirectToRouteResult = (RedirectToRouteResult) controller.Create(issue);
            var result = redirectToRouteResult.RouteValues["action"];

            var expected = "Index";
            Assert.AreEqual(result, expected);  
        }

        [Test]
        public void CreatePOST_ModelIsSaved()
        {
            Issue savedIssue = null;
            Mock<IIssueRepository> mock = new Mock<IIssueRepository>();
            mock.Setup(m => m.Save(It.IsAny<Issue>())).Callback<Issue>(x => savedIssue = x);
            var verifyIssue = new Issue
            {
                Description = "MockDescription",
                Deadline = DateTime.Now
            };
            HomeController controller = new HomeController(mock.Object);

            controller.Create(verifyIssue);

            Assert.AreEqual(verifyIssue, savedIssue);
        }

        //Created using Test-driven development
        [Test]
        public void CreatePOST_DescriptionIsRequired_modelNotSaved()
        {
            Issue savedIssue = null;
            Mock<IIssueRepository> mock = new Mock<IIssueRepository>();
            mock.Setup(m => m.Save(It.IsAny<Issue>())).Callback<Issue>(x => savedIssue = x);
            var verifyIssue = new Issue
            {
                Description = null,
                Deadline = DateTime.Now
            };
            HomeController controller = new HomeController(mock.Object);

            controller.Create(verifyIssue);

            Assert.IsNull(savedIssue);
        }

        //Created using Test-driven development
        [Test]
        public void CreatePOST_DeadlineIsRequired_modelNotSaved()
        {
            Issue savedIssue = null;
            Mock<IIssueRepository> mock = new Mock<IIssueRepository>();
            mock.Setup(m => m.Save(It.IsAny<Issue>())).Callback<Issue>(x => savedIssue = x);
            var verifyIssue = new Issue
            {
                Description = "MockDescription",
            };
            HomeController controller = new HomeController(mock.Object);

            controller.Create(verifyIssue);

            Assert.IsNull(savedIssue);
        }

        [Test]
        public void Delete_ReturnsView()
        {
            var id = Guid.NewGuid();
            Mock<IIssueRepository> mock = new Mock<IIssueRepository>();
            mock.Setup(m => m.ReadOne(id)).Returns( new Issue { IssueId = id} );
            HomeController controller = new HomeController(mock.Object);

            var result = controller.Delete(id);

            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Delete_ViewContainsModel()
        {
            var id = Guid.NewGuid();
            Mock<IIssueRepository> mock = new Mock<IIssueRepository>();
            mock.Setup(m => m.ReadOne(id)).Returns(new Issue { IssueId = id });
            HomeController controller = new HomeController(mock.Object);

            var viewResult = (ViewResult)controller.Delete(id);
            var result = viewResult.Model;

            Assert.IsInstanceOf<Issue>(result);
        }

        [Test]
        public void Delete_ReturnsCorrectModelById()
        {
            var id = Guid.NewGuid();
            var issue = new Issue { IssueId=id};
            Mock<IIssueRepository> mock = new Mock<IIssueRepository>();
            mock.Setup(m => m.ReadOne(id)).Returns(issue);
            HomeController controller = new HomeController(mock.Object);

            var viewResult = (ViewResult)controller.Delete(id);
            var result = viewResult.Model;

            Assert.AreEqual(result, issue);
        }

        [Test]
        public void Delete_IdIsNull_ReturnsBadRequest()
        {
            var id = Guid.NewGuid();
            var issue = new Issue { IssueId = id };
            Mock<IIssueRepository> mock = new Mock<IIssueRepository>();
            mock.Setup(m => m.ReadOne(id)).Returns(issue);
            HomeController controller = new HomeController(mock.Object);

            var httpStatus = (HttpStatusCodeResult)controller.Delete(null);
            var result = httpStatus.StatusCode;

            var expected = 400;
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Delete_ModelNotFound_ReturnsHttpNotFound()
        {
            var id = Guid.NewGuid();
            var issue = new Issue();
            issue = null;
            Mock<IIssueRepository> mock = new Mock<IIssueRepository>();
            mock.Setup(m => m.ReadOne(id)).Returns(issue);
            HomeController controller = new HomeController(mock.Object);

            var httpNotFound = (HttpNotFoundResult) controller.Delete(id);
            var result = httpNotFound.StatusCode;

            var expected = 404;
            Assert.AreEqual(result, expected);
        }

       [Test]
        public void DeleteConfirmed_IdIsNull_ReturnsBadRequest()
        {
            var id = Guid.NewGuid();
            var issue = new Issue { IssueId = id };
            Mock<IIssueRepository> mock = new Mock<IIssueRepository>();
            mock.Setup(m => m.ReadOne(id)).Returns(issue);
            HomeController controller = new HomeController(mock.Object);

            var httpStatus = (HttpStatusCodeResult)controller.DeleteConfirmed(null);
            var result = httpStatus.StatusCode;

            var expected = 400;
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void DeleteConfirmed_ModelNotFound_ReturnsHttpNotFound()
        {
            var id = Guid.NewGuid();
            var issue = new Issue();
            issue = null;
            Mock<IIssueRepository> mock = new Mock<IIssueRepository>();
            mock.Setup(m => m.ReadOne(id)).Returns(issue);
            HomeController controller = new HomeController(mock.Object);

            var httpNotFound = (HttpNotFoundResult)controller.DeleteConfirmed(id);
            var result = httpNotFound.StatusCode;

            var expected = 404;
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void DeleteConfirmed_ModelIsDeleted()
        {
            var id = Guid.NewGuid();
            var issue = new Issue();
            Mock<IIssueRepository> mock = new Mock<IIssueRepository>();
            mock.Setup(m => m.ReadOne(id)).Returns(issue);
            HomeController controller = new HomeController(mock.Object);

            controller.DeleteConfirmed(id);

            mock.Verify(x => x.Delete(It.IsAny<Issue>()), Times.Once);
        }
    }
}
