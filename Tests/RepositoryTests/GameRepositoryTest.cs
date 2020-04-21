using Data;
using Data.Entities;
using Data.Repositories;
using GamingCatalog.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.AspNetCore.Mvc;

namespace Tests.RepositoryTests
{
    public class GameRepositoryTest
    {

        private readonly List<Game> GetTestData = new List<Game>
            {
                new Game()
                {
                    Id = 1,
                    Title = "fdfdsds",
                    Price = 13m,
                    Rating = 2,
                    Description = "dsdsdsd",
                    Platform = "ds",
                    Manufacturer = new Manufacturer()
                    {
                        Name = "ivan"
                    },
                    ReleaseDate = DateTime.UtcNow.AddDays(10),
                    Image = new byte[] { 1, 1, 2, 3, 2, 3, 2, 3, 2, 3, 2 },
                },
                new Game()
                {
                    Id = 2,
                    Title = "fdf",
                    Price = 13m,
                    Rating = 2,
                    Description = "dsdqsfdfsfsdsd",
                    Platform = "ds",
                    Manufacturer = new Manufacturer()
                    {
                        Name = "ivfdan"
                    },
                    ReleaseDate = DateTime.UtcNow.AddDays(12),
                    Image = new byte[] { 1, 1, 2, 3, 2, 3, 2, 3, 2, 3, 2, 2,3,2,1,1,1,1,1,1,1 }
                }
            };
                
        
        
        private void SeedData(GameDbContext context)
        {
            context.Games.AddRange(GetTestData);
            context.SaveChanges();
        }


        [Fact]
        public void TestGetAllGames_ShouldReturnAllGames()
        {
            var options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new GameDbContext(options);
            SeedData(context);
            var repository = new GameRepository(context);

           
            var actualResult = repository.GetAll().ToList();
            var expectedResult = GetTestData;


            Assert.Equal(expectedResult.Count, actualResult.Count);
        }


        [Fact]
        public void TestGetGamesById_ShouldReturnGameWithThisId()
        {
            var options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new GameDbContext(options);
            SeedData(context);
            var repository = new GameRepository(context);


            var actualResult = repository.GetById(1);
            var expectedResult = GetTestData[0];


            Assert.Equal(expectedResult.Id, actualResult.Id);
        }


        [Fact]
        public void TestAddGames_ShouldAddGame()
        {
            var options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new GameDbContext(options);
            SeedData(context);
            var repository = new GameRepository(context);

            var game = new Game()
            {
                Id = 3,
                Title = "fdf",
                Price = 13m,
                Rating = 2,
                Description = "dsdqsfdfsfsdsd",
                Platform = "ds",
                Manufacturer = new Manufacturer()
                {
                    Name = "ivfdan"
                },
                ReleaseDate = DateTime.UtcNow.AddDays(12),
                Image = new byte[] { 1, 1, 2, 3, 2, 3, 2, 3, 2, 3, 2, 2, 3, 2, 1, 1, 1, 1, 1, 1, 1 }
            };

            repository.Add(game);

            var expectedResult = context.Games.Count();

            Assert.Equal(expectedResult, repository.Count());
        }

        [Fact]
        public void TestUpdate_ShouldUpdateGameById()
        {
            var options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new GameDbContext(options);
            SeedData(context);
            var repository = new GameRepository(context);
            
            var gameToUpdate = repository.GetById(1);

            gameToUpdate.Title = "Lorem";

            var actualState = context.Entry(gameToUpdate).State;

            repository.Update(gameToUpdate);
            

            EntityState expectedState = EntityState.Modified;

            
            Assert.Equal(expectedState, actualState);
        }

        [Fact]
        public void TestRemove_ShouldRemoveGame()
        {
            var options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new GameDbContext(options);
            SeedData(context);
            var repository = new GameRepository(context);


            var gameToRemove = repository.GetById(1);

            repository.Remove(gameToRemove);

            var expectedCount = context.Games.Count();

            Assert.Equal(expectedCount, repository.Count());
        }

        [Fact]
        public void TestSearchByTitle_ShouldRrturnAllGamesWithGivenTitle()
        {
            var options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var title = "fdf";
            var context = new GameDbContext(options);
            SeedData(context);
            var repository = new GameRepository(context);


            var actualResult = repository.SearchByTittle(title).ToList();
           
            Assert.Equal(2, actualResult.Count);
        }

        [Fact]
        public void TestSearchByManufacturer_ShouldRrturnAllGamesWithGivenManufacturer()
        {
            var options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var manufacturer = "iv";
            var context = new GameDbContext(options);
            SeedData(context);
            var repository = new GameRepository(context);


            var actualResult = repository.SearchByManufacturer(manufacturer).ToList();

            Assert.Equal(2, actualResult.Count);
        }


    }
}
