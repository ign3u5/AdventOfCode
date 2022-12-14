using FluentAssertions;

namespace AdventOfCode.TwentyTwo.Tests;

public class DayFourteenTests
{
    private const string _input = """
        498,4 -> 498,6 -> 496,6
        503,4 -> 502,4 -> 502,9 -> 494,9
        """;
    
    [Theory]
    [InlineData(_input, 21)]
    public void RunTaskOne(string input, int expected)
    {
        // Arrange
        var lines = input.ReplaceLineEndings("\n").Split("\n");
        var sut = new DayFourteen();
        
        // Act
        var settledSand = sut.RunTaskOne(lines);
        
        // Assert
        settledSand.Should().Be(expected);
    }
}