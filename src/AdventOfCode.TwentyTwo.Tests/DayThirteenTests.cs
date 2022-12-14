using FluentAssertions;

namespace AdventOfCode.TwentyTwo.Tests;

public class DayThirteenTests
{
    [Theory]
    [InlineData(_input, 13)]
    public void RunTaskOne(string input, int expected)
    {
        // Arrange
        var lines = input.ReplaceLineEndings("\n").Split("\n");
        var sut = new DayThirteen();
        
        // Act
        var sumOfPairsIndices = sut.RunTaskOne(lines);
        
        // Assert
        sumOfPairsIndices.Should().Be(expected);
    }

    private const string _input = """
        [1,1,3,1,1]
        [1,1,5,1,1]

        [[1],[2,3,4]]
        [[1],4]

        [9]
        [[8,7,6]]

        [[4,4],4,4]
        [[4,4],4,4,4]

        [7,7,7,7]
        [7,7,7]

        []
        [3]

        [[[]]]
        [[]]

        [1,[2,[3,[4,[5,6,7]]]],8,9]
        [1,[2,[3,[4,[5,6,0]]]],8,9]
        """;
}