using AutoMapper;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Interfaces;
using HIMS.Data.EntityClasses;
using HIMS.WebMVC.Controllers;
using HIMS.WebMVC.Models;
using HIMS.WebMVC.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HIMS.Tests
{
    public class UserProgressServiceTest : IVUserProgressService
    {
        private List<VUserProgress> list;

        public UserProgressServiceTest(List<VUserProgress> userProgressList)
        {
            list = userProgressList;
        }

        public IEnumerable<VUserProgressTransferModel> GetProgressByUserId(int id)
        {
            return Mapper.Map<IEnumerable<VUserProgress>, List<VUserProgressTransferModel>>(list.Where(p => p.UserId == id));
        }

        public string GetUserNameById(int id)
        {
            var item = list.Where(p => p.UserId == id).FirstOrDefault();
            return item?.UserName;
        }
    }

    [TestClass]
    public class TestUserProgressController
    {
        private UserProgressServiceTest UPServiceTest;
        private UserProgressController UPController;

        [TestInitialize]
        public void Initialization()
        {
            List<VUserProgress> UPList = new List<VUserProgress>
            {
                new VUserProgress { UserId = 5, TaskId = 1, TaskTrackId = 1, UserName = "Isaac", TaskName = "Разработать class A", TrackNote = "Начал писать Class A", TrackDate = new DateTime(2018, 2, 15) },
                new VUserProgress { UserId = 5, TaskId = 1, TaskTrackId = 8, UserName = "Isaac", TaskName = "Разработать class A", TrackNote = "Почти закончил разработку Class A", TrackDate = new DateTime(2018, 2, 19) },
                new VUserProgress { UserId = 4, TaskId = 3, TaskTrackId = 5, UserName = "Nathaniel", TaskName = "Разработать контроллер", TrackNote = "Приступил к разработке контроллера", TrackDate = new DateTime(2018, 3, 10) },
                new VUserProgress { UserId = 1, TaskId = 6, TaskTrackId = 2, UserName = "Xavier", TaskName = "Развернуть проект", TrackNote = "Начал развёртывать проект на хосте, пока проблем не было", TrackDate = new DateTime(2018, 3, 17) },
                new VUserProgress { UserId = 8, TaskId = 3, TaskTrackId = 4, UserName = "Elizabeth", TaskName = "Разработать контроллер", TrackNote = "Начала разрабатывать контроллер", TrackDate = new DateTime(2018, 3, 11) },
                new VUserProgress { UserId = 2, TaskId = 4, TaskTrackId = 6, UserName = "Jacob", TaskName = "Разработать view", TrackNote = "Верстаю вьюхи", TrackDate = new DateTime(2018, 3, 15) }
            };
            UPServiceTest = new UserProgressServiceTest(UPList);

            Mapper.Reset();
            AutoMapperConfig.Initialize();

            UPController = new UserProgressController(UPServiceTest);
        }

        [TestMethod]
        public void UserProgress_WithValidId()
        {
            var result = UPController.Index(5) as ViewResult;
            var resultList = result.Model as UserProgressListViewModel;

            Assert.IsNotNull(resultList.UserName);
        }

        [TestMethod]
        public void UserProgress_WithNotValidId()
        {
            var result = UPController.Index(-11) as ViewResult;
            var resultList = result.Model as UserProgressListViewModel;

            Assert.IsNull(resultList.UserName);
        }

        [TestMethod]
        public void UserProgress_WithoutId()
        {
            var result = UPController.Index(null) as ViewResult;
            var resultList = result.Model as UserProgressListViewModel;

            Assert.IsNull(resultList.UserName);
        }
    }
}