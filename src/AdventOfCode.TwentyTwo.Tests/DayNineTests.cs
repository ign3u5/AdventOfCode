using FluentAssertions;

namespace AdventOfCode.TwentyTwo.Tests;

public class DayNineTests
{
    public const string input = """
        R 4
        U 4
        L 3
        D 1
        R 4
        D 1
        L 5
        R 2
        """;

    public const string test2 = """
        R 2
        U 2
        L 2
        D 2
        """;

    public const string test3 = """
        L 2
        D 2
        R 2
        U 2
        """;

    public const string test4 = """
        L 2
        D 2
        R 2
        U 4
        """;

    public const string inputTask2 = """
        R 5
        U 8
        L 8
        D 3
        R 17
        D 10
        L 25
        U 20
        """;

    [Theory]
    [InlineData(input, 13)]
    [InlineData(test2, 5)]
    [InlineData(test3, 5)]
    [InlineData(test4, 6)]
    public void RunTaskOne(string input, int expected)
    {
        // Arrange
        var lines = input.ReplaceLineEndings("\n").Split("\n");
        var sut = new DayNine();

        // Act
        var numberOfPointsCrossed = sut.RunTaskOne(lines);

        // Assert
        numberOfPointsCrossed.Should().Be(expected);
    }

    [Theory]
    [InlineData(input, 1)]
    [InlineData(input, 36)]
    public void RunTaskTwo(string input, int expected)
    {
        // Arrange
        var lines = input.ReplaceLineEndings("\n").Split("\n");
        var sut = new DayNine();

        // Act
        var numberOfPointsCrossed = sut.RunTaskTwo(lines);

        // Assert
        numberOfPointsCrossed.Should().Be(expected);
    }
}

