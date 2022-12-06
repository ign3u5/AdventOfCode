using AdventOfCode.Domain;
using AdventOfCode.TwentyTwo;

IChallenge<int> challenge = new DaySix();

var lines = await File.ReadAllLinesAsync(challenge.ChallengeInputPath);
var challengeOutput = challenge.Run(lines);

Console.WriteLine(challengeOutput);