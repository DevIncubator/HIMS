using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Castle.Core.Internal;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Interfaces;
using HIMS.WebMVC.Controllers;
using HIMS.WebMVC.Models;
using HIMS.WebMVC.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HIMS.Tests
{
    [TestClass]
    public class TestTaskController
    {
        private TaskController taskController;

        [TestInitialize]
        public void Initialization()
        {
            var itaskService = new Mock<ITaskService>();
            var iVUserProfileService = new Mock<IVUserProfileService>();
            var iVUserTaskService = new Mock<IVUserTaskService>();


            List<UserTaskTransferModel> userTasks = new List<UserTaskTransferModel>
            {
                new UserTaskTransferModel{UserId = 1, TaskId = 2, Name = "Create unit tests", Start = new DateTime(2018,4,12), Deadline = new DateTime(2018,4,13), Status = "Active" },
                new UserTaskTransferModel{UserId = 2, TaskId = 1, Name = "Create DB", Start = new DateTime(2018,4,13), Deadline = new DateTime(2018,4,15), Status = "Active" },
                new UserTaskTransferModel{UserId = 3, TaskId = 1, Name = "Create DB", Start = new DateTime(2018,4,11), Deadline = new DateTime(2018,4,12), Status = "Fail" },
                new UserTaskTransferModel{UserId = 2, TaskId = 2, Name = "Create unit tests", Start = new DateTime(2018,4,15), Deadline = new DateTime(2018,4,16), Status = "Active" },
            };

            List<TaskTransferModel> tasks = new List<TaskTransferModel>
            {
                new TaskTransferModel
                {
                    TaskId=1,
                    Name ="Create DB",
                    Description ="Create DB for your project",
                    StartDate = new DateTime(2018,4,12),
                    DeadlineDate =new DateTime(2018,4,22)
                },
                new TaskTransferModel
                {
                    TaskId=2,
                    Name ="Create unit tests",
                    Description ="Create unit tests for your controller",
                    StartDate = new DateTime(2018,4,12),
                    DeadlineDate =new DateTime(2018,4,22)
                }
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
                    FullName = "Test_User1",
                    MathScore = 8,
                    MobilePhone = "336421058",
                    Password = "",
                    Sex = "M"
                },
                new VUserProfileTransferModel
                {
                    UserId = 2,
                    Address = "Minsk",
                    Age = 20,
                    Direction = "Java",
                    Education = "BNTU",
                    Email = "test_user@gmail.com",
                    FullName = "Test_User2",
                    MathScore = 8,
                    MobilePhone = "336421058",
                    Password = "",
                    Sex = "M"
                },
                new VUserProfileTransferModel
                {
                    UserId = 3,
                    Address = "Minsk",
                    Age = 20,
                    Direction = "Front-End",
                    Education = "BNTU",
                    Email = "test_user@gmail.com",
                    FullName = "Test_User3",
                    MathScore = 8,
                    MobilePhone = "336421058",
                    Password = "",
                    Sex = "M"
                }
            };

            int lastTask = 2;

            itaskService.Setup(m => m.GetAllTasks()).Returns(tasks);

            itaskService.Setup(m => m.GetTask(It.IsAny<int>()))
                .Returns((int? id) => tasks.FirstOrDefault(tt => tt.TaskId == id));

            itaskService.Setup(m => m.SaveTask(It.IsAny<TaskTransferModel>()))
                .Callback(
                (TaskTransferModel target) =>
                {

                    target.TaskId = tasks.Count() + 1;
                    tasks.Add(target);

                }
                ).Verifiable();

            itaskService.Setup(m => m.GetLastTaskId()).Returns(lastTask);

            itaskService.Setup(m => m.UpdateTask(It.IsAny<TaskTransferModel>()))
                .Callback(
                (TaskTransferModel target) =>
                {
                    var original = tasks.Single(tt => tt.TaskId == target.TaskId);
                    original.Name = target.Name;
                    original.Description = target.Description;
                    original.StartDate = original.StartDate;
                    original.DeadlineDate = original.DeadlineDate;
                }
                ).Verifiable();

            itaskService.Setup(m => m.DeleteTask(It.IsAny<int?>()))
                .Callback(
                (int? id) =>
                {
                    if (id.HasValue)
                    {
                        tasks.Remove(tasks.Single(tt => tt.TaskId == id));
                    }
                }
                ).Verifiable();

            iVUserProfileService.Setup(m => m.GetVUserProfiles()).Returns(vUserProfiles);

            iVUserTaskService.Setup(m => m.SaveTaskForUser(It.IsAny<UserTaskTransferModel>()))
                .Callback(
                (UserTaskTransferModel target) =>
                {
                    target.TaskId = userTasks.Count() + 1;
                    userTasks.Add(target);

                }
                ).Verifiable();

            iVUserTaskService.Setup(m => m.DeleteUserTask(It.IsAny<int?>(), It.IsAny<int?>()))
                .Callback(
                (int? taskId, int? userId) =>
                {
                    if (taskId.HasValue && userId.HasValue)
                    {
                      //  var usertask = userTasks.FirstOrDefault(f => f.TaskId == taskId && f.UserId == userId);
                    userTasks.Remove(userTasks
                        .FirstOrDefault(f => f.TaskId == taskId && f.UserId == userId));
                        
                    }
                }
                ).Verifiable();

            iVUserTaskService.Setup(m => m.GetAllUsersForTask(It.IsAny<int?>()))
                .Returns((int? id) => userTasks.Where(tt => tt.TaskId == id));



             taskController = new TaskController(itaskService.Object, iVUserProfileService.Object, iVUserTaskService.Object);

            Mapper.Reset();
            AutoMapperConfig.Initialize();

        }

        [TestMethod]
        public void TaskIndex_ResultNotNul()
        {
            var results = taskController.Index() as ViewResult;
            var resultList = results.Model as TaskListViewModel;
            Assert.IsNotNull(resultList.Tasks.IsNullOrEmpty());
            
        }
     

        [TestMethod]
        public void TaskEdit_ResultNotNul()
        {
            var results = taskController.Edit(1) as ViewResult;
            var result = results.Model as TaskViewModel;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void TaskDeteils_ResultNotNul()
        {
            var results = taskController.Details(1) as ViewResult;
            var result = results.Model as TaskViewModel;
            Assert.IsNotNull(result);
        }
       
        [TestMethod]
        public void TaskEdit_NegativeId()
        {
            var results = taskController.Edit(-100) as HttpNotFoundResult;
          
            Assert.IsNotNull(results);
        }
        [TestMethod]
        public void TaskDeteils_NegativeId()
        {
            var results = taskController.Details(-100) as HttpNotFoundResult;

            Assert.IsNotNull(results);
        }
        [TestMethod]
        public void TaskEdit_NullId()
        {
            HttpStatusCodeResult result = taskController.Edit(null) as HttpStatusCodeResult;

            Assert.AreEqual(result.StatusCode, 400);
        }
        [TestMethod]
        public void TaskDeteils_NullId()
        {
            HttpStatusCodeResult result = taskController.Details(null) as HttpStatusCodeResult;

            Assert.AreEqual(result.StatusCode, 400);
        }

        [TestMethod]
        public void TaskDelete_ResultNotNul()
        {
            ViewResult result = taskController.Delete(2, false) as ViewResult;
            TaskViewModel model = result.Model as TaskViewModel;
           
            Assert.IsNotNull(model);

        }
        [TestMethod]
        public void TaskDelete_NegativeId_ResultIsHttpNotFound()
        {
            HttpNotFoundResult result = taskController.Delete(-100, false) as HttpNotFoundResult;
            Assert.IsNotNull(result);
           
        }
        [TestMethod]
        public void TaskDelete_NullId_ResultIsHttpNotFound()
        {
            HttpStatusCodeResult result = taskController.Delete(null, false) as HttpStatusCodeResult;
            Assert.AreEqual(result.StatusCode, 400);

        }


    }
}
