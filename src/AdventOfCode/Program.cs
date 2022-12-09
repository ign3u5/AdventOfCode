﻿using System.Security.Authentication.ExtendedProtection;
using AdventOfCode.TwentyTwo;

var challenge = new DayNine();

var lines = await File.ReadAllLinesAsync(challenge.ChallengeInputPath);

var taskOneOutput = challenge.RunTaskOne(lines);
Console.WriteLine(taskOneOutput);

var taskTwoOutput = challenge.RunTaskTwo(lines);
Console.WriteLine(taskTwoOutput);