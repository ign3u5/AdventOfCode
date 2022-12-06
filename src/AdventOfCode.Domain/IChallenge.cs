namespace AdventOfCode.Domain;

public interface IChallenge<out T>
{
    string ChallengeInputPath { get; }
    T RunTaskOne(string[] lines);
    T RunTaskTwo(string[] lines);
}