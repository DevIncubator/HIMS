using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Mvc;
using AutoMapper;
using Castle.Core.Internal;
using Moq;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Interfaces;
using HIMS.WebMVC.Controllers;
using HIMS.WebMVC.Models;
using HIMS.WebMVC.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Routing;

namespace HIMS.Tests
{
    [TestClass]
    public class TestTaskTrackController
    {

        private TaskTrackController _controller;


        [TestInitialize]
        public void SetupContext()
        {
            var iTaskTrackServiceMock = new Mock<ITaskTrackService>();
            var iVUserTrackServiceMock = new Mock<IVUserTrackService>();
            var iVUserProfileServiceMock = new Mock<IVUserProfileService>();


            //create lists
            List<TaskTrackTransferModel> taskTracks = new List<TaskTrackTransferModel>
            {
                new TaskTrackTransferModel{TaskTrackId = 1, TrackDate = new DateTime(2018, 02, 14), TrackNote = "Implementing view", UserTaskId = 1},
                new TaskTrackTransferModel{TaskTrackId = 2, TrackDate = new DateTime(2018, 03, 22), TrackNote = "Starting implement controller", UserTaskId = 2},
                new TaskTrackTransferModel{TaskTrackId = 3, TrackDate = new DateTime(2018, 04, 01), TrackNote = "Done 3-week task", UserTaskId = 3}
            };
            List<VUserTrackTransferModel> vUserTracks = new List<VUserTrackTransferModel>
            {
                new VUserTrackTransferModel{Name = "Deploy project", TaskId = 1, TaskTrackId = 2, TrackDate = new DateTime(2018, 01, 01), TrackNote = "Deployed successfully", UserId = 1},
                new VUserTrackTransferModel{Name = "Create view", TaskId = 2, TaskTrackId = 3, TrackDate = new DateTime(2018, 03, 03), TrackNote = "View created", UserId = 1},
                new VUserTrackTransferModel{Name = "Implement controller", TaskId = 3, TaskTrackId = 4, TrackDate = new DateTime(2018, 04, 01), TrackNote = "Starting implement controller", UserId = 1},
                new VUserTrackTransferModel{Name = "Implement class A", TaskId = 4, TaskTrackId = 5, TrackDate = new DateTime(2018, 04, 05), TrackNote = "Almost implemented class A", UserId = 2}
            };
            List<VUserProfileTransferModel> vUserProfiles = new List<VUserProfileTransferModel>
            {
                new VUserProfileTransferModel
                {
                    UserId = 1,
                    Address = "Minsk",
                    Age = 20,
                    Direction = ".NET",
                    Education = "BNTU",
                    Email = "test_user@gmail.com",
                    FullName = "Test_User",
                    MathScore = 8,
                    MobilePhone = "336421058",
                    Password = "",
                    Sex = "M"
                }
            };

            //setup itaskTrack mock
            iTaskTrackServiceMock.Setup(m => m.GetTaskTracks()).Returns(taskTracks);
            iTaskTrackServiceMock.Setup(m => m.GetTaskTrack(It.IsAny<int>()))
                .Returns((int? id) => taskTracks.SingleOrDefault(tt => tt.TaskTrackId == id));
            iTaskTrackServiceMock.Setup(m => m.SaveTaskTrack(It.IsAny<TaskTrackTransferModel>())).Callback
            (
                (TaskTrackTransferModel target) =>
                {
                    target.TaskTrackId = taskTracks.Count() + 1;
                    taskTracks.Add(target);
                }

            ).Verifiable();
            iTaskTrackServiceMock.Setup(m => m.UpdateTaskTrack(It.IsAny<TaskTrackTransferModel>())).Callback
            (
                (TaskTrackTransferModel target) =>
                {
                    var original = taskTracks.Single(tt => tt.TaskTrackId == target.TaskTrackId);
                    original.TrackDate = target.TrackDate;
                    original.TrackNote = target.TrackNote;
                    original.UserTaskId = target.UserTaskId;
                }
            ).Verifiable();
            iTaskTrackServiceMock.Setup(m => m.DeleteTaskTrack(It.IsAny<int?>())).Callback
            (
                (int? id) =>
                {
                    if (id.HasValue)
                    {
                        taskTracks.Remove(taskTracks.Single(tt => tt.TaskTrackId == id));
                    }
                }
            ).Verifiable();

            

            //setup iVUserTrackMock
            iVUserTrackServiceMock.Setup(m => m.GetVUserTrack(It.IsAny<int?>()))
                .Returns((int id) => vUserTracks.Where(ut => ut.UserId == id));
            iVUserTrackServiceMock.Setup(m => m.Get(It.IsAny<int?>()))
                .Returns((int id) => vUserTracks.SingleOrDefault(ut => ut.UserId == id));
            iVUserTrackServiceMock.Setup(m => m.GetByTaskTrack(It.IsAny<int?>()))
                .Returns((int id) => vUserTracks.SingleOrDefault(ut => ut.TaskTrackId == id));
            iVUserTrackServiceMock.Setup(m => m.UpdateUserTrack(It.IsAny<VUserTrackTransferModel>())).Callback(
                (VUserTrackTransferModel target) =>
                {
                    var original = vUserTracks.Single(ut => ut.TaskTrackId == target.TaskTrackId);
                    original.Name = target.Name;
                    original.TrackDate = target.TrackDate;
                    original.TrackNote = target.TrackNote;

                }).Verifiable();

            //setup iVUserProfileServiceMock
            iVUserProfileServiceMock.Setup(m => m.GetVUserProfile(It.IsAny<string>()))
                .Returns((string email) => vUserProfiles.Single(up => up.Email == email));


            //setup identity context
            var identity = new GenericIdentity("test_user@gmail.com");
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));
            var principal = new GenericPrincipal(identity, new[] { "user" });
            var context = new Mock<HttpContextBase>();
            context.Setup(s => s.User).Returns(principal);


            Mapper.Reset();
            AutoMapperConfig.Initialize();

            _controller = new TaskTrackController(iTaskTrackServiceMock.Object, iVUserTrackServiceMock.Object, iVUserProfileServiceMock.Object, null, null);
            _controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), _controller);
        }


        [TestMethod]
        public void Index_ValidId_ResultNotNull()
        {
            ViewResult result = _controller.Index(1) as ViewResult;
            TaskTracksListViewModel list = result.Model as TaskTracksListViewModel;
            Assert.IsFalse(list.TaskTracks.IsNullOrEmpty());

        }

        [TestMethod]
        public void Index_NegativId_ResultNull()
        {
            ViewResult result = _controller.Index(-100) as ViewResult;
            TaskTracksListViewModel list = result.Model as TaskTracksListViewModel;
            Assert.IsTrue(list.TaskTracks.IsNullOrEmpty());
        }

        [TestMethod]
        public void Index_NullId_ResultNull()
        {
            ViewResult result = _controller.Index(null) as ViewResult;
            TaskTracksListViewModel list = result.Model as TaskTracksListViewModel;
            Assert.IsFalse(list.TaskTracks.IsNullOrEmpty());

        }

        [TestMethod]
        public void DeleteView_ValidId_ResultNotNull()
        {
            ViewResult result = _controller.Delete(2, false) as ViewResult;
            VUserTrackViewModel model = result.Model as VUserTrackViewModel;
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void DeleteView_NegativeId_ResultIsHttpNotFound()
        {
            HttpNotFoundResult result = _controller.Delete(-1000, false) as HttpNotFoundResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteView_NullId_ResultIsHttpNotFound()
        {
            HttpStatusCodeResult result = _controller.Delete(null, false) as HttpStatusCodeResult;

            Assert.AreEqual(result.StatusCode, 400);
        }

        [TestMethod]
        public void DeletePost_ValidId_ResultRedirectToAction()
        {
            var result = (RedirectToRouteResult)_controller.Delete(2);

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void DeletePost_InvalidId_ResultRedirectToAction()
        {
            HttpStatusCodeResult result = (HttpStatusCodeResult)_controller.Delete(-1000);

            Assert.AreEqual(result.StatusCode, 400);
        }
    }
}
