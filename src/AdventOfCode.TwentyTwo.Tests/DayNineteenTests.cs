using System;
namespace AdventOfCode.TwentyTwo.Tests;

public class DayNineteenTests : BaseTests<DayNineteen, int>
{
    private const string _input = """
            Blueprint 1: Each ore robot costs 4 ore. Each clay robot costs 2 ore. Each obsidian robot costs 3 ore and 14 clay. Each geode robot costs 2 ore and 7 obsidian.
            Blueprint 2: Each ore robot costs 2 ore. Each clay robot costs 3 ore. Each obsidian robot costs 3 ore and 8 clay. Each geode robot costs 3 ore and 12 obsidian.
            """;

    private const string _input2 = """
            Blueprint 1: Each ore robot costs 3 ore. Each clay robot costs 3 ore. Each obsidian robot costs 3 ore and 16 clay. Each geode robot costs 3 ore and 20 obsidian.
            """;

    //[Theory]
    //[InlineData(_input, 33)]
    //[InlineData(_input2, 33)]
    // Process intesive tests
    public void RunTaskOne(string input, int expected) => TaskOne(input, expected);
}

