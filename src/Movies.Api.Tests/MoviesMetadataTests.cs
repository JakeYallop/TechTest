using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Api.Database;
using Xunit;

namespace Movies.Api.Tests
{
    public class MoviesMetadataTests
    {
#pragma warning disable CA2211 // Non-constant fields should not be visible
        public static TheoryData<int, int, string?, string?, TimeSpan, int, bool> Data = new()
#pragma warning restore CA2211 // Non-constant fields should not be visible
        {
            { 1, 1, "title", "EN", TimeSpan.FromMinutes(1), 2010, true },
            { 1, 1, null, "EN", TimeSpan.FromMinutes(1), 2010, false },
            { 1, 1, "title", "EN", TimeSpan.FromMinutes(1), 0, false },
            { 1, 1, "title", null, TimeSpan.FromMinutes(1), 2010, false },
            { 1, 0, "title", "EN", default, 2010, false },
            { 1, 1, "", "EN", TimeSpan.FromMinutes(1), 2010, false },
            { 1, 1, "title", "", default, 2010, false },
            { 1, 1, "title", "\t\t", TimeSpan.FromMinutes(1), 2010, false },
            { 1, 1, "\t\t", "EN", TimeSpan.FromMinutes(1), 2010, false },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void IsValid_ReturnsExpectedResult(int id, int movieId, string? title, string? languageCode, TimeSpan duration, int releaseYear, bool expected)
        {
            //Arrange
            var metadata = new MovieMetadata(id, movieId, title, languageCode, duration, releaseYear);
            //Act
            var actual = metadata.IsValid();
            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
