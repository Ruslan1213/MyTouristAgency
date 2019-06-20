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
    [TestFixture]
    class TypeToursControllerTest
    {
        [Test]
        public void Index()
        {
            TypesToursController controller = new TypesToursController();
            ViewResult result = controller.Index(1) as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void IndexViewModelNotNull()
        {
            var mockRepository = new Mock<IRepository<TypesTour>>();
            var controller = new TypesToursController(mockRepository.Object);
            ViewResult actionResult = controller.Index(1) as ViewResult;
            Assert.IsNotNull(actionResult.Model);
        }
    }
}
