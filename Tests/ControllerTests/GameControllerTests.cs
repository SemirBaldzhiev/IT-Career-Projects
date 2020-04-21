using Data;
using Data.Entities;
using Data.Repositories;
using GamingCatalog.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.ControllerTests
{
    public class GameControllerTests
    {
        private readonly List<Game> GetTestData = new List<Game>
            {
                new Game()
                {
                    Id = 1,
                    Title = "dsds",
                    Price = 13m,
                    Rating = 2,
                    Description = "dsdsdsd",
                    Platform = "ds",
                    Manufacturer = new Manufacturer()
                    {
                        Name = "ivan"
                    },
                    ReleaseDate = DateTime.UtcNow.AddDays(10),
                    Image = new byte[] { 1, 1, 2, 3, 2, 3, 2, 3, 2, 3, 2 }
                },
                new Game()
                {
                    Id = 2,
                    Title = "dsdfdfss",
                    Price = 13m,
                    Rating = 2,
                    Description = "dsdsdsadadsd",
                    Platform = "dsdfsf",
                    Manufacturer = new Manufacturer()
                    {
                        Name = "ivdsdsan"
                    },
                    ReleaseDate = DateTime.UtcNow.AddDays(10),
                    Image = new byte[] { 1, 1, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2,2,3,2 }
                }
            };

        private void SeedData(GameDbContext context)
        {
            context.Games.AddRange(GetTestData);
            context.SaveChanges();
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfGames()
        {
            var options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new GameDbContext(options);
            SeedData(context);

            var controller = new GamesController(context);

            var result = (ViewResult)controller.Index(null, null);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<Game>>(
                viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Details_ReturnsNotFound_WhenIdIsNull()
        {
            var options = new DbContextOptionsBuilder<GameDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var context = new GameDbContext(options);
            var controller = new GamesController(context);
            

            var result = (NotFoundResult)controller.Details(null).Result;

            var contentResult = Assert.IsType<NotFoundResult>(result);

            Assert.Equal(result.StatusCode, contentResult.StatusCode);

        }

        [Fact]
        public void DeleteConfirmed_ReturnRedirectToAction_WhenGameIsDeleted()
        {
            var options = new DbContextOptionsBuilder<GameDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var id = 1;
            var context = new GameDbContext(options);
            var controller = new GamesController(context);
            SeedData(context);

            var result = (RedirectToActionResult)controller.DeleteConfirmed(id);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
            
        }


    }
}
