using FluentAssertions;

namespace AdventOfCode.TwentyTwo.Tests;

public class DayTwelveTests
{
    private const string _input = """
        Sabqponm
        abcryxxl
        accszExk
        acctuvwj
        abdefghi
        """;
    
    [Theory]
    [InlineData(_input, 31)]
    public void RunTaskOne(string input, int expected)
    {
        // Arrange
        var lines = input.ReplaceLineEndings("\n").Split("\n");
        var sut = new DayTwelve();
        
        // Act
        var fewestSteps = sut.RunTaskOne(lines);
        
        // Assert
        fewestSteps.Should().Be(expected);
    }

    [Theory]
    [InlineData(_input, 29)]
    public void RunTaskTwo(string input, int expected)
    {
        // Arrange
        var lines = input.ReplaceLineEndings("\n").Split("\n");
        var sut = new DayTwelve();
        
        // Act
        var fewestSteps = sut.RunTaskTwo(lines);
        
        // Assert
        fewestSteps.Should().Be(expected);
    }
}