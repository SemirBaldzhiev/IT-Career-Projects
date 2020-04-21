using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Data.Entities;
using GamingCatalog.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.ControllerTests
{
    public class ManufacturersControllerTests
    {
        private readonly List<Manufacturer> GetTestData = new List<Manufacturer>()
        {
            new Manufacturer
            {
                Id =1,
                Name = "manufacturerName"
            },
            new Manufacturer
            {
                Id =2,
                Name = "newNamemanufacturer"
            },
            new Manufacturer
            {
                Id = 3,
                Name = "name"
            }
        };

        private void SeedData(GameDbContext context)
        {
            context.Manufacturers.AddRange(GetTestData);
            context.SaveChanges();
        }

        [Fact]
        public void Edit_ReturnRedirectToAction_WhenModelSateIsValid()
        {
            var options = new DbContextOptionsBuilder<GameDbContext>()
                  .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                  .Options;

            var context = new GameDbContext(options);
            var controller = new ManufacturersController(context);
            SeedData(context);

            var manufacturer = new Manufacturer()
            {
                Id = 4,
                Name = "dsdsad"
            };

            var result = (RedirectToActionResult)controller.Create(manufacturer);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Delete_ReturnNotFound_WhenIdISNull()
        {
            var options = new DbContextOptionsBuilder<GameDbContext>()
                  .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                  .Options;

            var context = new GameDbContext(options);
            var controller = new ManufacturersController(context);
            SeedData(context);

            var result = (NotFoundResult)controller.Delete(null).Result;

            var actualResult = Assert.IsType<NotFoundResult>(result);

            Assert.Equal(result.StatusCode, actualResult.StatusCode);
        }
    }
}
