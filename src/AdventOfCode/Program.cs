﻿using AdventOfCode.TwentyTwo;

var challenge = new DayEleven();

var lines = await File.ReadAllLinesAsync(challenge.ChallengeInputPath);

var taskOneOutput = challenge.RunTaskOne(lines);
Console.WriteLine(taskOneOutput);

var taskTwoOutput = challenge.RunTaskTwo(lines);
Console.WriteLine(taskTwoOutput);