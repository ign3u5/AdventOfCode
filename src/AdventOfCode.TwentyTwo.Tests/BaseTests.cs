using AdventOfCode.Domain;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.TwentyTwo.Tests;

public abstract class BaseTests<T, U> where T : IChallenge<U>, new()
{
    public void TaskOne(string input, U expected) => 
        GenericTaskTest(input, expected, (sut, lines) => sut.RunTaskOne(lines));
    
    public void TaskTwo(string input, U expected) => 
        GenericTaskTest(input, expected, (sut, lines) => sut.RunTaskTwo(lines));

    private void GenericTaskTest(string input, U expected, Func<IChallenge<U>, string[], U> act)
    {
        // Arrange
        string[] lines = input.GetLines();
        T sut = new();

        // Act
        U actualValue = act(sut, lines);

        // Assert
        actualValue.Should().Be(expected);
    }
}
