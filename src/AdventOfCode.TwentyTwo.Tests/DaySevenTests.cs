using FluentAssertions;

namespace AdventOfCode.TwentyTwo.Tests;

public class DaySevenTests
{
    private const string TestInput = """
        $ cd /
        $ ls
        dir a
        14848514 b.txt
        8504156 c.dat
        dir d
        $ cd a
        $ ls
        dir e
        29116 f
        2557 g
        62596 h.lst
        $ cd e
        $ ls
        584 i
        $ cd ..
        $ cd ..
        $ cd d
        $ ls
        4060174 j
        8033020 d.log
        5626152 d.ext
        7214296 k
        """;

    [Theory]
    [InlineData(TestInput, 95437)]
    public void RunTaskOne(string input, int actualTotal)
    {
        // Arrange
        var lines = input.ReplaceLineEndings("\n").Split("\n");
        var sut = new DaySeven();
        
        // Act
        var totalSizeOfDir = sut.RunTaskOne(lines);
        
        // Assert
        totalSizeOfDir.Should().Be(actualTotal);
    }

    [Theory]
    [InlineData(TestInput, 24933642)]
    public void RunTaskTwo(string input, int actualSmallest)
    {
        var lines = input.ReplaceLineEndings("\n").Split("\n");
        var sut = new DaySeven();
        
        // Act
        var sizeOfFreedSpace = sut.RunTaskTwo(lines);
        
        // Assert
        sizeOfFreedSpace.Should().Be(actualSmallest);
    }
}