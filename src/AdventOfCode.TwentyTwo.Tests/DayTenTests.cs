using FluentAssertions;

namespace AdventOfCode.TwentyTwo.Tests;

public class DayTenTests
{
    [Theory]
    [InlineData(_input, 13140)]
    public void RunTaskOne(string input, int expected)
    {
        // Arrange
        var lines = input.ReplaceLineEndings("\n").Split('\n');
        var sut = new DayTen();

        // Act
        var signalStrengthProduct = sut.RunTaskOne(lines);

        // Assert
        signalStrengthProduct.Should().Be(expected);
    }

    [Theory]
    [InlineData(_input, _expectedCRT)]
    public void RunTaskTwo(string input, string expected)
    {
        // Arrange
        var lines = input.ReplaceLineEndings("\n").Split('\n');
        var expectedOutput = expected.ReplaceLineEndings("\n").Split('\n');
        var sut = new DayTen();

        // Act
        var signalStrengthProduct = sut.GenerateCrtOuput(lines);

        // Assert
        for (var i = 0; i < 6; i++)
        {
            signalStrengthProduct[i].Should().Be(expectedOutput[i]);
        }
    }

    private const string _expectedCRT = """
        ##  ##  ##  ##  ##  ##  ##  ##  ##  ##  
        ###   ###   ###   ###   ###   ###   ### 
        ####    ####    ####    ####    ####    
        #####     #####     #####     #####     
        ######      ######      ######      ####
        #######       #######       #######     
        """;

    private const string _input = """
        addx 15
        addx -11
        addx 6
        addx -3
        addx 5
        addx -1
        addx -8
        addx 13
        addx 4
        noop
        addx -1
        addx 5
        addx -1
        addx 5
        addx -1
        addx 5
        addx -1
        addx 5
        addx -1
        addx -35
        addx 1
        addx 24
        addx -19
        addx 1
        addx 16
        addx -11
        noop
        noop
        addx 21
        addx -15
        noop
        noop
        addx -3
        addx 9
        addx 1
        addx -3
        addx 8
        addx 1
        addx 5
        noop
        noop
        noop
        noop
        noop
        addx -36
        noop
        addx 1
        addx 7
        noop
        noop
        noop
        addx 2
        addx 6
        noop
        noop
        noop
        noop
        noop
        addx 1
        noop
        noop
        addx 7
        addx 1
        noop
        addx -13
        addx 13
        addx 7
        noop
        addx 1
        addx -33
        noop
        noop
        noop
        addx 2
        noop
        noop
        noop
        addx 8
        noop
        addx -1
        addx 2
        addx 1
        noop
        addx 17
        addx -9
        addx 1
        addx 1
        addx -3
        addx 11
        noop
        noop
        addx 1
        noop
        addx 1
        noop
        noop
        addx -13
        addx -19
        addx 1
        addx 3
        addx 26
        addx -30
        addx 12
        addx -1
        addx 3
        addx 1
        noop
        noop
        noop
        addx -9
        addx 18
        addx 1
        addx 2
        noop
        noop
        addx 9
        noop
        noop
        noop
        addx -1
        addx 2
        addx -37
        addx 1
        addx 3
        noop
        addx 15
        addx -21
        addx 22
        addx -6
        addx 1
        noop
        addx 2
        addx 1
        noop
        addx -10
        noop
        noop
        addx 20
        addx 1
        addx 2
        addx 2
        addx -6
        addx -11
        noop
        noop
        noop
        """;
}

