using AdventOfCode.TwentyTwo.Tests;
using Xunit;

namespace AdventOfCode.TwentyThree.Tests;

public class DayThreeTests : BaseTests<DayThree, int>
{
    private const string InputOne = """
    467..114..
    ...*......
    ..35..633.
    ......#...
    617*......
    .....+.58.
    ..592.....
    ......755.
    ...$.*....
    .664.598..
    """;

    [Theory]
    [InlineData(InputOne, 4361)]
    public void RunTaskOne(string input, int expected) => TaskOne(input, expected);
    
    [Theory]
    [InlineData(InputOne, 467835)]
    public void RunTaskTwo(string input, int expected) => TaskTwo(input, expected);
}