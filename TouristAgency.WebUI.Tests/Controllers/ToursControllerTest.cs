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
    class ToursControllerTest
    {
        [Test]
        public void Index()
        {
            OrdersController controller = new OrdersController();
            ViewResult result = controller.Index(1) as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
