using FluentAssertions;

namespace AdventOfCode.TwentyTwo.Tests;

public class DaySixTests
{
    
    [Theory]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
    public void RunTaskOne_WhenInputContains4ContiguousDifferentChars_ReturnsLengthOfInputToLastChar(string input, int expected)
    {
        // Arrange
        var sut = new DaySix();
        
        // Act
        var actual = sut.RunTaskOne(new[] { input });
        
        // Assert
        actual.Should().Be(expected);
    }
    
    [Theory]
    [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 23)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
    public void RunTaskTwo_WhenInputContains14ContiguousDifferentChars_ReturnsLengthOfInputToLastChar(string input, int expected)
    {
        // Arrange
        var sut = new DaySix();
        
        // Act
        var actual = sut.RunTaskTwo(new[] { input });
        
        // Assert
        actual.Should().Be(expected);
    }
}