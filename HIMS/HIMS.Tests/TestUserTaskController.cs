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
    public class TestUserTaskController
    {
        private UserTaskController controller;

        [TestInitialize]
        public void Initialization()
        {
            Mock<IVUserTaskService> iUserTaskServiceMock = new Mock<IVUserTaskService>();
            Mock<IUserProfileService> iUserProfileServiceMock = new Mock<IUserProfileService>();

            List<UserTaskTransferModel> listDTO = new List<UserTaskTransferModel>
            {
                new UserTaskTransferModel{UserId = 1, TaskId = 5, TaskName = "Write view", StartDate = new DateTime(2018,4,12), DeadlineDate = new DateTime(2018,4,13), State = "Active" },
                new UserTaskTransferModel{UserId = 2, TaskId = 3, TaskName = "Write controller", StartDate = new DateTime(2018,4,13), DeadlineDate = new DateTime(2018,4,15), State = "Active" },
                new UserTaskTransferModel{UserId = 3, TaskId = 1, TaskName = "Write four tests", StartDate = new DateTime(2018,4,11), DeadlineDate = new DateTime(2018,4,12), State = "Fail" },
                new UserTaskTransferModel{UserId = 5, TaskId = 4, TaskName = "implement ViewModel", StartDate = new DateTime(2018,4,15), DeadlineDate = new DateTime(2018,4,16), State = "Active" },
            };

            List<UserProfileTransferModel> userDTO = new List<UserProfileTransferModel>
            {
                new UserProfileTransferModel
                {
                     Address = "test",
                     BirthDate = new DateTime(1990,12,12),
                     DirectionId = 1,
                     Education = "test",
                     Email = "test",
                     LastName = "test",
                     MathScore = 2.3,
                     MobilePhone = "12345678",
                     Name = "test",
                     Sex = "test",
                     Skype = "test",
                     StartDate = new DateTime(),
                     UniversityAverageScore = 2.2,
                     UserId = 1,
                     Password = ""
                },
                new UserProfileTransferModel
                {
                     Address = "test",
                     BirthDate = new DateTime(1990,12,13),
                     DirectionId = 2,
                     Education = "test",
                     Email = "test",
                     LastName = "test",
                     MathScore = 2.3,
                     MobilePhone = "12345672",
                     Name = "test",
                     Sex = "test",
                     Skype = "test",
                     StartDate = new DateTime(),
                     UniversityAverageScore = 2.5,
                     UserId = 100,
                     Password = ""
                }
            };


            iUserTaskServiceMock.Setup(m => m.GetAllTasksForUser(It.IsAny<int>())).Returns((int id) => listDTO.Where(w => w.UserId == id));
            iUserTaskServiceMock.Setup(s => s.UpdateTaskStatusForUser(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).Callback
                (
                     (int userid, int taskid, bool status) =>
                     {
                         if (status == true)
                         {
                             var stat = listDTO.FirstOrDefault(f => f.UserId == userid && f.TaskId == taskid);
                         }
                         else
                         {
                             var stat = listDTO.FirstOrDefault(f => f.UserId == userid && f.TaskId == taskid) ;
                         }

                     }
                ).Verifiable();
            iUserProfileServiceMock.Setup(m => m.GetUserProfile(It.IsAny<int>())).Returns((int? id) => userDTO.SingleOrDefault(s => s.UserId == id));

            //controller = new UserTaskController(iUserTaskServiceMock.Object, iUserProfileServiceMock.Object);

            Mapper.Reset();
            AutoMapperConfig.Initialize();
        }


        [TestMethod]
        public void Controller_With_Valid_ID()
        {
            var results = controller.GetTasksForUser(1) as ViewResult;
            var resultList = results.Model as UserTasksListViewModel;
            Assert.IsNotNull(resultList.UserTasksList);
            Assert.IsNotNull(resultList.UserName);

        }

        [TestMethod]
        public void Controller_With_InValid_ID()
        {
            var results = controller.GetTasksForUser(100) as ViewResult;
            UserTasksListViewModel resultList = results.Model as UserTasksListViewModel;
            Assert.AreEqual(resultList.UserTasksList.Count(), 0);
        }

        [TestMethod]
        public void Controller_Show_View_Name()
        {
            var results = controller.SetTaskAsSuccess(1,1,true) as ViewResult;
            Assert.AreEqual("GetTasksForUser", results.ViewName);
        }

        [TestMethod]
        public void Set_Task_Status()
        {
            var result = controller.SetTaskAsSuccess(1, 5, true) as ViewResult;
            UserTaskViewModel resultStatus = result.Model as UserTaskViewModel;
            Assert.IsNull(resultStatus);
        }
    }
}
