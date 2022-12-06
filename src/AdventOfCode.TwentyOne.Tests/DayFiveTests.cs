using FluentAssertions;

namespace AdventOfCode.TwentyOne.Tests;

public class DayFiveTests
{
    [Theory]
    [InlineData("""
        0,9 -> 5,9
        8,0 -> 0,8
        9,4 -> 3,4
        2,2 -> 2,1
        7,0 -> 7,4
        6,4 -> 2,0
        0,9 -> 2,9
        3,4 -> 1,4
        0,0 -> 8,8
        5,5 -> 8,2
        """, 5)]
    public void RunTaskOne_(string input, int expectedOverlaps)
    {
        // Arrange
        var lines = input.Split("\n");
        var sut = new DayFive();
        
        // Act
        var overlaps = sut.RunTaskOne(lines);
        
        // Assert
        overlaps.Should().Be(expectedOverlaps);
    }
}