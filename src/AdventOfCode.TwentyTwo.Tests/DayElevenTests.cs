using System;
using FluentAssertions;

namespace AdventOfCode.TwentyTwo.Tests;

public class DayElevenTests
{
    [Theory]
    [InlineData(_input, 10605)]
    public void RunTaskOne(string input, double expected)
    {
        // Arrange
        var lines = input.ReplaceLineEndings("\n").Split('\n');
        var sut = new DayEleven();

        // Act
        var monkeyBusiness = sut.RunTaskOne(lines);

        // Assert
        monkeyBusiness.Should().Be(expected);
    }

    [Theory]
    [InlineData(_input, 2713310158)]
    public void RunTaskTwo(string input, double expected)
    {
        // Arrange
        var lines = input.ReplaceLineEndings("\n").Split('\n');
        var sut = new DayEleven();

        // Act
        var monkeyBusiness = sut.RunTaskTwo(lines);

        // Assert
        monkeyBusiness.Should().Be(expected);
    }

    private const string _input = """
        Monkey 0:
          Starting items: 79, 98
          Operation: new = old * 19
          Test: divisible by 23
            If true: throw to monkey 2
            If false: throw to monkey 3

        Monkey 1:
          Starting items: 54, 65, 75, 74
          Operation: new = old + 6
          Test: divisible by 19
            If true: throw to monkey 2
            If false: throw to monkey 0

        Monkey 2:
          Starting items: 79, 60, 97
          Operation: new = old * old
          Test: divisible by 13
            If true: throw to monkey 1
            If false: throw to monkey 3

        Monkey 3:
          Starting items: 74
          Operation: new = old + 3
          Test: divisible by 17
            If true: throw to monkey 0
            If false: throw to monkey 1
        """;
}

