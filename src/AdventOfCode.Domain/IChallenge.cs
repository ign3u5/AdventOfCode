namespace AdventOfCode.Domain;

public interface IChallenge<out T>
{
    public string ChallengeInputPath { get; }
    public T Run(string[] lines);
}