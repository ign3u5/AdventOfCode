namespace AdventOfCode.TwentyTwo.Tests;

public class DayTwentyTests : BaseTests<DayTwenty, int>
{
    private const string _input = """
            1
            2
            -3
            3
            -2
            0
            4
            """;

    [Theory]
    [InlineData(_input, 3)]
    public void RunTaskOne(string input, int expected) => TaskOne(input, expected);

    [Theory]
    [InlineData(_input, 0)]
    public void RunTaskTwo(string input, int expected) => TaskTwo(input, expected);
}
