using AdventOfCode.TwentyThree;

var challenge = new DayThree();

var lines = await File.ReadAllLinesAsync(challenge.ChallengeInputPath);

// 609678622 is too high
var taskOneOutput = challenge.RunTaskOne(lines);
Console.WriteLine(taskOneOutput);

var taskTwoOutput = challenge.RunTaskTwo(lines);
Console.WriteLine(taskTwoOutput);