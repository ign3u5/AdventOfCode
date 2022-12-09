using FluentAssertions;
using FluentAssertions.Formatting;

namespace AdventOfCode.TwentyTwo.Tests;

public class DayEightTests
{
    public const string input = """
        30373
        25512
        65332
        33549
        35390
        """;
    
    [Theory]
    [InlineData(input, 21)]
    public void RunTaskOne(string input, int expected)
    {
        // Arrange
        var lines = input.Split("\r\n");
        var sut = new DayEight();
        
        // Act
        var numberOfVisibleTrees = sut.RunTaskOne(lines);
        
        // Assert
        numberOfVisibleTrees.Should().Be(expected);
    }

    [Theory]
    [InlineData(input, 8)]
    public void RunTaskTwo(string input, int expected)
    {
        // Arrange
        var lines = input.Split("\r\n");
        var sut = new DayEight();
        
        // Act
        var highestScenicScore = sut.RunTaskTwo(lines);
        
        // Assert
        highestScenicScore.Should().Be(expected);
    }
}