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

        var headPosition = (x: 0, y: 0);
        var hFinal = (x: 0, y: 0);
        var tPositions = new (int x, int y)[9];
        HashSet<(int, int)> positions = new() { (0, 0) };

        for (int i = 0; i < lines.Length; i++)
        {
            int numberOfSteps = int.Parse(lines[i][2..]);
            char direction = lines[i][0];
            headPosition = direction switch
            {
                'L' => (headPosition.x - numberOfSteps, headPosition.y),
                'R' => (headPosition.x + numberOfSteps, headPosition.y),
                'U' => (headPosition.x, headPosition.y + numberOfSteps),
                'D' => (headPosition.x, headPosition.y - numberOfSteps),
                _ => throw new ArgumentException($"Char {direction} is not a valid direction", nameof(direction))
            };

            GetTPos(headPosition, tPositions[0], 0);

            //Console.Write($"Head: {headPosition.x}, {headPosition.y} - ");

            //for (var j = 0; j < tPositions.Length - 1; j++)
            //{
            //    Console.Write($"Pos {j + 1}: {tPositions[j].x}, {tPositions[j].y} - ");
            //}
            //Console.WriteLine($"Pos 9: {tPositions[8].x}, {tPositions[8].y}");

            //PrintBoard(tPositions, headPosition, 0);

            void GetTPos((int x, int y) hPosition, (int x, int y) tPosition, int index, bool isFinal = false)
            {
                int horizontalDifference = diff(hPosition.x - tPosition.x);
                int verticalDifference = diff(hPosition.y - tPosition.y);

                if (horizontalDifference > 1)
                {
                    for (var j = 1; j < horizontalDifference;)
                    {
                        tPosition.x += tPosition.x < hPosition.x ? 1 : -1;

                        horizontalDifference = diff(hPosition.x - tPosition.x);
                        if (verticalDifference > 0)
                        {
                            verticalDifference--;
                            if (verticalDifference == 1 && horizontalDifference == 1)
                                tPosition.y += tPosition.y < hPosition.y ? 1 : -1;
                            else
                                tPosition.y = hPosition.y;
                        }

                        if (isFinal)
                            positions.Add(tPosition);

                        tPositions[index] = tPosition;

                        //PrintBoard(tPositions, headPosition, index);
                        if (!isFinal)
                            GetTPos(tPositions[index], tPositions[index + 1], index + 1, index + 1 == 8);

                        if (verticalDifference == 1 && horizontalDifference == 1) return;
                    }

                    return;
                }

                if (verticalDifference > 1)
                {
                    for (var j = 1; j < verticalDifference;)
                    {
                        tPosition.y += tPosition.y < hPosition.y ? 1 : -1;

                        verticalDifference = diff(hPosition.y - tPosition.y);
                        if (horizontalDifference > 0)
                        {
                            horizontalDifference--;
                            if (verticalDifference == 1 && horizontalDifference == 1)
                                tPosition.x += tPosition.x < hPosition.x ? 1 : -1;
                            else
                                tPosition.x = hPosition.x;
                        }

                        if (isFinal)
                            positions.Add(tPosition);

                        tPositions[index] = tPosition;

                        //PrintBoard(tPositions, headPosition, index);
                        if (!isFinal)
                            GetTPos(tPositions[index], tPositions[index + 1], index + 1, index + 1 == 8);

                        if (verticalDifference == 1 && horizontalDifference == 1) return;
                    }
                }

                return;
            }
        }            

        return positions.Count;
    }

    private void PrintBoard((int x, int y)[] tPositions, (int x, int y) hPosition, int currentIndex)
    {
        Console.WriteLine($"Knot: {currentIndex + 1}");
        for (var i = -5; i < 16; i++)
        {
            for (var j = -11; j < 15; j++)
            {
                if (hPosition.y == i && hPosition.x == j)
                {
                    Console.Write('H');
                    continue;
                }
                var position = tPositions.Select((pos, ind) => pos.x == j && pos.y == i ? (pos, ind: ind) : (pos, ind: -1));

                if (position.Any(val => val.ind != -1))
                {
                    var pos = position.First(val => val.ind != -1);
                    if (pos != default)
                    {
                        Console.Write($"{pos.ind + 1}");
                        continue;
                    }
                }

                Console.Write('.');
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}