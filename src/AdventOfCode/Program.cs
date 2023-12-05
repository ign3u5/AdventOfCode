var challenge = new DayFive();

var lines = await File.ReadAllLinesAsync(challenge.ChallengeInputPath);

//161102249 - answer is too low
var taskOneOutput = challenge.RunTaskOne(lines);
Console.WriteLine(taskOneOutput);

//178159714 - answer is too high
var taskTwoOutput = challenge.RunTaskTwo(lines);
Console.WriteLine(taskTwoOutput);