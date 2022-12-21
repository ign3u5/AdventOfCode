using System;
using FluentAssertions;

namespace AdventOfCode.TwentyTwo.Tests;

	public class DayFifteenTests
	{
    [Theory]
    [InlineData(_input, 26)]
    public void RunTaskOne(string input, int expected)
    {
        // Arrange
        var lines = input.ReplaceLineEndings("\n").Split('\n');
        var sut = new DayFifteen();

        // Act
        var totalSpacesBeaconNotPresent = sut.GetNumberOfTakenPositions(lines, 10);

        // Assert
        totalSpacesBeaconNotPresent.Should().Be(expected);
    }

    [Theory]
    [InlineData(_input, 56000011)]
    public void RunTaskTwo(string input, int expected)
    {
        // Arrange
        var lines = input.ReplaceLineEndings("\n").Split('\n');
        var sut = new DayFifteen();

        // Act
        var totalSpacesBeaconNotPresent = sut.FindAlertBeacon(lines, 20);

        // Assert
        totalSpacesBeaconNotPresent.Should().Be(expected);
    }

    private const string _input = """
            Sensor at x=2, y=18: closest beacon is at x=-2, y=15
            Sensor at x=9, y=16: closest beacon is at x=10, y=16
            Sensor at x=13, y=2: closest beacon is at x=15, y=3
            Sensor at x=12, y=14: closest beacon is at x=10, y=16
            Sensor at x=10, y=20: closest beacon is at x=10, y=16
            Sensor at x=14, y=17: closest beacon is at x=10, y=16
            Sensor at x=8, y=7: closest beacon is at x=2, y=10
            Sensor at x=2, y=0: closest beacon is at x=2, y=10
            Sensor at x=0, y=11: closest beacon is at x=2, y=10
            Sensor at x=20, y=14: closest beacon is at x=25, y=17
            Sensor at x=17, y=20: closest beacon is at x=21, y=22
            Sensor at x=16, y=7: closest beacon is at x=15, y=3
            Sensor at x=14, y=3: closest beacon is at x=15, y=3
            Sensor at x=20, y=1: closest beacon is at x=15, y=3
            """;
}

