using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DayTen : IChallenge<int>
{
    public string ChallengeInputPath => DataProvider<DayTen>.GetChallengePath();

    public int RunTaskOne(string[] lines)
    {
        var cycle = 0;
        var x = 1;
        var nextCheck = 20;

        var signalStrength = 0;

        foreach (var line in lines)
        {
            if (line[..4] == "noop")
            {
                if (cycle + 1 >= nextCheck)
                {
                    signalStrength += nextCheck * x;
                    nextCheck += 40;
                }
                cycle++;
            }
            if (line[..4] == "addx")
            {
                var value = int.Parse(line[5..]);
                if (cycle + 2 >= nextCheck)
                {
                    signalStrength += nextCheck * x;
                    nextCheck += 40;
                }
                x += value;
                cycle += 2;
            }
        }

        return signalStrength;
    }

    public int RunTaskTwo(string[] lines)
    {
        var output = GenerateCrtOuput(lines);
        foreach(var line in output)
        {
            Console.WriteLine(line);
        }

        return 0;
    }

    public string[] GenerateCrtOuput(string[] lines)
    {
        var cycle = 0;
        var x = 1;
        var nextCheck = 40;
        var crtLine = 0;
        var crtLines = new int[6].Select(_ => new char[40]).ToArray();
        var signalStrength = 0;

        void Increment()
        {
            var crtX = cycle - (crtLine * 40);
            crtLines[crtLine][crtX] = GetChar(x, crtX);
            if (cycle + 1 >= nextCheck)
            {
                signalStrength += nextCheck * x;
                nextCheck += 40;
                crtLine++;
            }
            cycle++;
        }

        foreach (var line in lines)
        {
            if (line[..4] == "noop")
            {
                Increment();
            }
            if (line[..4] == "addx")
            {
                Increment();
                Increment();

                var value = int.Parse(line[5..]);
                x += value;
            }
        }

        return crtLines.Select(cl => new string(cl)).ToArray();
    }

    private char GetChar(int currX, int crtX) => crtX > currX - 2 && crtX < currX + 2 ? '#' : '.';
}

