using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DayTen : IChallenge<int>
{
    public string ChallengeInputPath => DataProvider<DayTen>.GetChallengePath();

    public int RunTaskOne(string[] lines)
    {
        var cycle = 0;
        var nextCheck = 20;
        var signalStrengthTotal = 0;

        void runCommandForCycle(int x)
        {
            cycle++;
            tryUpdateSignalStrengthAtCheckFor(x);
        }

        void tryUpdateSignalStrengthAtCheckFor(int x)
        {
            if (cycle >= nextCheck)
            {
                signalStrengthTotal += nextCheck * x;
                nextCheck += 40;
            }
        }

        ParseAndActionCommands(lines, runCommandForCycle, x: 1);

        return signalStrengthTotal;
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
        var currCrtLine = 0;
        var crtLines = new int[6].Select(_ => new char[40]).ToArray();

        ParseAndActionCommands(lines, runCommandForCycle, x: 1);

        void runCommandForCycle(int x)
        {
            addCrtPixelFor(x);
            cycle++;
            tryIncementLine();
        }

        void addCrtPixelFor(int x) =>
            crtLines[currCrtLine][cycle] = GetCrtPixel(x, cycle);

        void tryIncementLine()
        {
            if(cycle >= 40)
            {
                cycle -= 40;
                currCrtLine++;
            }
        }

        return crtLines.Select(cl => new string(cl)).ToArray();
    }

    private static void ParseAndActionCommands(string[] lines, Action<int> incrementCycle, int x)
    {
        foreach (var line in lines)
        {
            if (line[..4] == "noop")
            {
                incrementCycle(x);
            }
            if (line[..4] == "addx")
            {
                incrementCycle(x);
                incrementCycle(x);

                var value = int.Parse(line[5..]);
                x += value;
            }
        }
    }

    private char GetCrtPixel(int currX, int crtX) => crtX > currX - 2 && crtX < currX + 2 ? '#' : ' ';
}

