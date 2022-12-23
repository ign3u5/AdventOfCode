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

    private const string _input1 = """
        .....
        ..##.
        ..#..
        .....
        ..##.
        .....
        """;

    [Theory]
    [InlineData(_input, 110)]
    public void RunTaskOne(string input, int expected) => TaskOne(input, expected);

    [Theory]
    [InlineData(_input, 20)]
    public void RunTaskTwo(string input, int expected) => TaskTwo(input, expected);
}

