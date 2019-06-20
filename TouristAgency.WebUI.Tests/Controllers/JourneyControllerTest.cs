using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using TouristAgency.Domain.Models.EfModels;
using TouristAgency.Domain.Models.Repositoryes.Interfases;
using TouristAgency.WebUI.Controllers;

namespace TouristAgency.WebUI.Tests.Controllers
{
    class JourneyControllerTest
    {
        [Test]
        public void Index()
        {
            JourneysController controller = new JourneysController();
            ViewResult result = controller.Index(1) as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void IndexViewModelNotNull()
        {
            var mockRepository = new Mock<IRepository<Journey>>();
            var mockRepositoryTour = new Mock<IRepository<Tour>>();
            var controller = new JourneysController(mockRepository.Object, mockRepositoryTour.Object);
            ViewResult actionResult = controller.Index(1) as ViewResult;
            Assert.IsNotNull(actionResult.Model);
        }
    }
}
