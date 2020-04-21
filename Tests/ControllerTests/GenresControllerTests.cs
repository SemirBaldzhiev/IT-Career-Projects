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
    public class GenresControllerTests
    {
        private readonly List<Genre> GetTestData = new List<Genre>()
        {
            new Genre
            {
                Id =1,
                Name = "GenresName"
            },
            new Genre
            {
                Id =2,
                Name = "newName"
            },
            new Genre
            {
                Id = 3,
                Name = "name"
            }
        };

        private void SeedData(GameDbContext context)
        {
            context.Genres.AddRange(GetTestData);
            context.SaveChanges();
        }

        [Fact]
        public void Index_ReturnAViewResult_WithAListOfGenres()
        {
            var options = new DbContextOptionsBuilder<GameDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var context = new GameDbContext(options);
            SeedData(context);

            var controller = new GenresController(context);

            var result = (ViewResult)controller.Index().Result;

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<Genre>>(
                viewResult.ViewData.Model);

            Assert.Equal(3, model.Count());

        }

        [Fact]
        public void Edit_ReturnNotFound_WhenIdIsNull()
        {
            var options = new DbContextOptionsBuilder<GameDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var context = new GameDbContext(options);
            var controller = new GenresController(context);


            var result = (NotFoundResult)controller.Details(null).Result;

            var contentResult = Assert.IsType<NotFoundResult>(result);

            Assert.Equal(result.StatusCode, contentResult.StatusCode);
        }

        [Fact]
        public void Create_ReturnRedirectToAction_WhenModelSateIsValid()
        {
            var options = new DbContextOptionsBuilder<GameDbContext>()
                  .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                  .Options;

            var context = new GameDbContext(options);
            var controller = new GenresController(context);
            SeedData(context);

            var genre = new Genre()
            {
                Id = 4,
                Name = "dsdsad"
            };

            var result = (RedirectToActionResult)controller.Create(genre);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

    }
}
