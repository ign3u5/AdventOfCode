using System;
namespace AdventOfCode.TwentyTwo.Tests;

public class DayTwentyThreeTests : BaseTests<DayTwentyThree, int>
{
    private const string _input = """
            ....#..
            ..###.#
            #...#.#
            .#...##
            #.###..
            ##.#.##
            .#..#..
            """;

    [Theory]
    [InlineData(_input, 110)]
    public void RunTaskOne(string input, int expected) => TaskOne(input, expected);
}

