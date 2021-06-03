using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Controllers;
using Movies.Api.Database;
using Movies.Api.Models;
using Xunit;

namespace Movies.Api.Tests
{
    public class MetadataControllerTests
    {
        public MoviesDatabase GetDatabase()
        {
            var movies = new List<MovieMetadata>()
            {
                new MovieMetadata(1, 1, "Test 1", "EN", TimeSpan.FromMinutes(1), "2010"),
                new MovieMetadata(2, 2, "Test 2", "EN", TimeSpan.FromMinutes(2), "2011"),
                new MovieMetadata(3, 3, "Test 3", "EN", TimeSpan.FromMinutes(3), "2012"),
                new MovieMetadata(4, 3, "Test 3", "FR", TimeSpan.FromMinutes(3), "2013"),
                new MovieMetadata(5, 4, "Test 4", "EN", TimeSpan.FromMinutes(4), "2014"),
                new MovieMetadata(6, 5, "Test 5", "EN", TimeSpan.FromMinutes(5), "2015"),
                new MovieMetadata(7, 6, "Test 6", "", TimeSpan.FromMinutes(5), "2016"),
                new MovieMetadata(8, 7, "", "", TimeSpan.FromMinutes(5), "2017"),
                new MovieMetadata(9, 7, "Test 7", "EN", TimeSpan.FromMinutes(5), "2017"),
            };

            var stats = new List<MovieStats>()
            {
                new MovieStats(1, TimeSpan.FromMinutes(1), 100),
                new MovieStats(2, TimeSpan.FromMinutes(2), 200),
                new MovieStats(3, TimeSpan.FromMinutes(3), 300),
                new MovieStats(4, TimeSpan.FromMinutes(4), 400),
                new MovieStats(5, TimeSpan.FromMinutes(5), 500),
                new MovieStats(6, TimeSpan.FromMinutes(6), 600),
                new MovieStats(7, TimeSpan.FromMinutes(7), 700),
            };

            return new MoviesDatabase(movies, stats);
        }

        [Fact]
        public void Get_MovieId_MovieDoesNotExist_Returns404NotFound()
        {
            //Arrange
            var database = GetDatabase();
            var controller = new MetadataController(database);
            //Act
            var result = controller.GetMetadata(8);
            //Assert
            var actionResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, actionResult.StatusCode);
        }

        [Fact]
        public void Get_MovieId_Returns200Ok()
        {
            //Arrange
            var database = GetDatabase();
            var controller = new MetadataController(database);
            //Act
            var result = controller.GetMetadata(8);
            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, actionResult.StatusCode);
        }

        [Fact]
        public void Get_MovieId_MultipleMatchingIdsAndLanguages_ReturnsHighestId()
        {
            //Arrange
            var movies = new List<MovieMetadata>()
            {
                new MovieMetadata(1, 1, "1", "EN", TimeSpan.FromMinutes(1), "2010"),
                new MovieMetadata(2, 1, "1", "EN", TimeSpan.FromMinutes(1), "2010"),
            };

            var stats = new List<MovieStats>()
            {
                new MovieStats(1, TimeSpan.FromMinutes(1), 100),
                new MovieStats(2, TimeSpan.FromMinutes(2), 200),
            };

            var database = new MoviesDatabase(movies, stats);
            var controller = new MetadataController(database);
            //Act
            var result = controller.GetMetadata(1);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsAssignableFrom<IEnumerable<MovieMetadata>>(actionResult.Value);
            Assert.Collection(data, m => Assert.Equal(2, m.Id));
        }

        [Fact]
        public void Get_MovieId_OrderAlphabetically()
        {
            //Arrange
            var movies = new List<MovieMetadata>()
            {
                new MovieMetadata(1, 1, "1", "FR", TimeSpan.FromMinutes(1), "2010"),
                new MovieMetadata(2, 1, "1", "EN", TimeSpan.FromMinutes(1), "2010"),
            };

            var stats = new List<MovieStats>()
            {
                new MovieStats(1, TimeSpan.FromMinutes(1), 100),
                new MovieStats(2, TimeSpan.FromMinutes(2), 200),
            };

            var database = new MoviesDatabase(movies, stats);
            var controller = new MetadataController(database);
            //Act
            var result = controller.GetMetadata(1);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsAssignableFrom<IEnumerable<MovieMetadata>>(actionResult.Value);
            Assert.Collection(data,
                m =>
                {
                    Assert.Equal(2, m.Id);
                    Assert.Equal(1, m.MovieId);
                    Assert.Equal("EN", m.LanguageCode);
                },
                m =>
                {
                    Assert.Equal(1, m.Id);
                    Assert.Equal(1, m.MovieId);
                    Assert.Equal("FR", m.LanguageCode);
                });
        }
    }
}
