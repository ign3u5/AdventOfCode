using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DayNine : IChallenge<int>
{
    public string ChallengeInputPath => DataProvider<DayNine>.GetChallengePath();

    public int RunTaskOne(string[] lines)
    {
        int diff(int diff)
        {
            return diff < 0 ? -diff : diff;
        }

        var tPosition = (x: 0, y: 0);
        var hPosition = (x: 0, y: 0);
        HashSet<(int, int)> positions = new() { (0, 0) };

        for (int i = 0; i < lines.Length; i++)
        {
            int numberOfSteps = int.Parse(lines[i][2..]);
            char direction = lines[i][0];
            hPosition = direction switch
            {
                'L' => (hPosition.x - numberOfSteps, hPosition.y),
                'R' => (hPosition.x + numberOfSteps, hPosition.y),
                'U' => (hPosition.x, hPosition.y - numberOfSteps),
                'D' => (hPosition.x, hPosition.y + numberOfSteps),
                _ => throw new ArgumentException($"Char {direction} is not a valid direction", nameof(direction))
            };

            int horizontalDifference = diff(hPosition.x - tPosition.x);
            int verticalDifference = diff(hPosition.y - tPosition.y);

            if (horizontalDifference > 1)
            {
                for (var j = 1; j < horizontalDifference; j++)
                {
                    tPosition.x += tPosition.x < hPosition.x ? 1 : -1;

                    if (verticalDifference > 0)
                    {
                        tPosition.y = hPosition.y;
                        verticalDifference--;
                    }

                    positions.Add(tPosition);
                }
                continue;
            }

            if (verticalDifference > 1)
            {
                for (var j = 1; j < verticalDifference; j++)
                {
                    tPosition.y += tPosition.y < hPosition.y ? 1 : -1;

                    if (horizontalDifference > 0)
                    {
                        tPosition.x = hPosition.x;
                        horizontalDifference--;
                    }

                    positions.Add(tPosition);
                }
            }
        }

        return positions.Count;
    }

    public int RunTaskTwo(string[] lines)
    {
        int diff(int diff)
        {
            return diff < 0 ? -diff : diff;
        }

        //var tPosition = (x: 0, y: 0);
        var tPositions = new (int x, int y)[9];
        var hPosition = (x: 0, y: 0);
        HashSet<(int, int)> positions = new() { (0, 0) };

        for (int i = 0; i < lines.Length; i++)
        {
            int numberOfSteps = int.Parse(lines[i][2..]);
            char direction = lines[i][0];
            hPosition = direction switch
            {
                'L' => (hPosition.x - numberOfSteps, hPosition.y),
                'R' => (hPosition.x + numberOfSteps, hPosition.y),
                'U' => (hPosition.x, hPosition.y - numberOfSteps),
                'D' => (hPosition.x, hPosition.y + numberOfSteps),
                _ => throw new ArgumentException($"Char {direction} is not a valid direction", nameof(direction))
            };

            tPositions[0] = GetTPos(hPosition, tPositions[0]);

            for (var j = 1; j < tPositions.Length - 1; j++)
            {
                tPositions[j] = GetTPos(tPositions[j - 1], tPositions[j]);
            }

            tPositions[8] = GetTPos(tPositions[7], tPositions[8], true);

            (int, int) GetTPos((int x, int y) hPosition, (int x, int y) tPosition, bool isFinal = false)
            {
                int horizontalDifference = diff(hPosition.x - tPosition.x);
                int verticalDifference = diff(hPosition.y - tPosition.y);

                if (horizontalDifference > 1)
                {
                    for (var j = 1; j < horizontalDifference; j++)
                    {
                        tPosition.x += tPosition.x < hPosition.x ? 1 : -1;

                        if (verticalDifference > 0)
                        {
                            tPosition.y = hPosition.y;
                            verticalDifference--;
                        }

                        if (isFinal)
                            positions.Add(tPosition);
                    }
                    return tPosition;
                }

                if (verticalDifference > 1)
                {
                    for (var j = 1; j < verticalDifference; j++)
                    {
                        tPosition.y += tPosition.y < hPosition.y ? 1 : -1;

                        if (horizontalDifference > 0)
                        {
                            tPosition.x = hPosition.x;
                            horizontalDifference--;
                        }

                        if (isFinal)
                            positions.Add(tPosition);
                    }
                }

                return tPosition;
            }
        }            

        return positions.Count;
    }
}

