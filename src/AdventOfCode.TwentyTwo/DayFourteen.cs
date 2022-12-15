using System.Runtime.InteropServices;
using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DayFourteen : IChallenge<int>
{
    public string ChallengeInputPath => DataProvider<DayFourteen>.GetChallengePath();
    
    public int RunTaskTwo(string[] lines)
    {
        var setsOfCoords = lines.Select(l => l.Split(" -> ")
                .Select(rawCoords => rawCoords.Split(',').ToArray())
                .Select(a => (x: int.Parse(a[0]), y: int.Parse(a[1]))).ToArray())
            .ToArray();
        var taken = GetTakenPositions(setsOfCoords, out _, out _);
        var positions = GetTakenPositions(setsOfCoords, out var yBounds, out var xBounds);

        var pipeIsNotBlocked = true;
        var numberAtRest = 0;
        
        while (pipeIsNotBlocked)
        {
            var x = 500;
            var y = 0;
            for (; y < yBounds.upper + 1; y++)
            {
                if (positions.Contains((x, y)))
                {
                    pipeIsNotBlocked = false;
                    break;
                }
                if (!positions.Contains((x, y + 1))) continue;
                
                if (!positions.Contains((x - 1, y + 1)))
                {
                    x--;
                    continue;
                }

                if (!positions.Contains((x + 1, y + 1)))
                {
                    x++;
                    continue;
                }
                
                positions.Add((x, y));
                numberAtRest++;
                break;
            }

            if (y == yBounds.upper + 1)
            {
                positions.Add((x, y));
                numberAtRest++;
            }
        }
        
        return numberAtRest;
    }

    public int RunTaskOne(string[] lines)
    {
        var setsOfCoords = lines.Select(l => l.Split(" -> ")
            .Select(rawCoords => rawCoords.Split(',').ToArray())
            .Select(a => (x: int.Parse(a[0]), y: int.Parse(a[1]))).ToArray())
            .ToArray();
        var taken = GetTakenPositions(setsOfCoords, out _, out _);
        var positions = GetTakenPositions(setsOfCoords, out var yBounds, out var xBounds);

        var isInfiniteFreeFall = false;
        var numberAtRest = 0;
        
        while (!isInfiniteFreeFall)
        {
            var x = 500;
            var y = 0;
            for (; y < yBounds.upper; y++)
            {
                if (!positions.Contains((x, y + 1))) continue;
                
                if (x > xBounds.lower - 1 && !positions.Contains((x - 1, y + 1)))
                {
                    x--;
                    continue;
                }
                if (x == xBounds.lower - 1)
                {
                    isInfiniteFreeFall = true;
                    break;
                }

                if (x < xBounds.upper + 1 && !positions.Contains((x + 1, y + 1)))
                {
                    x++;
                    continue;
                }
                if (x == xBounds.upper + 1)
                {
                    isInfiniteFreeFall = true;
                    break;
                }
                positions.Add((x, y));
                numberAtRest++;
                break;
            }
            
            if (isInfiniteFreeFall || y == yBounds.upper)
                isInfiniteFreeFall = true;
        }
        
        // VisualisePos(taken, positions, yBounds, xBounds);
        return numberAtRest;
    }

    private void VisualisePos(HashSet<(int, int)> taken, HashSet<(int, int)> positions, (int lower, int upper) yBounds, (int lower, int upper) xBounds)
    {
        for (var y = yBounds.lower; y < yBounds.upper + 1; y++)
        {
            for (var x = xBounds.lower; x < xBounds.upper + 1; x++)
            {
                if (taken.Contains((x, y)))
                {
                    Console.Write('#');
                }
                else if (positions.Contains((x, y)))
                {
                    Console.Write('0');
                }
                else
                {
                    Console.Write('.');
                }
            }
            Console.WriteLine();
        }
    }

    private HashSet<(int x, int y)> GetTakenPositions((int x, int y)[][] setsOfCoords, out (int lower, int upper) yBounds, out (int lower, int upper) xBounds)
    {
        yBounds = (0, -1);
        xBounds = (int.MaxValue, -1);
        HashSet<(int, int)> positions = new();
        foreach (var setOfCoords in setsOfCoords)
        {
            for (var i = 0; i < setOfCoords.Length - 1; i++)
            {
                var curCoords = setOfCoords[i];
                var nextCoords = setOfCoords[i + 1];
                
                var deltaX = Math.Abs(curCoords.x - nextCoords.x);
                var deltaY = Math.Abs(curCoords.y - nextCoords.y);

                if (deltaX > 0)
                {
                    var lowerX = curCoords.x < nextCoords.x ? curCoords.x : nextCoords.x;
                    var upperX = curCoords.x > nextCoords.x ? curCoords.x : nextCoords.x;
                    if (lowerX < xBounds.lower) xBounds.lower = lowerX;
                    if (upperX > xBounds.upper) xBounds.upper = upperX;
                    if (curCoords.y > yBounds.upper) yBounds.upper = curCoords.y;
                    if (curCoords.y < yBounds.lower) yBounds.lower = curCoords.y;
                    
                    for (var j = lowerX; j < upperX + 1; j++)
                    {
                        positions.Add((j, curCoords.y));
                    }
                }

                if (deltaY > 0)
                {
                    var lowerY = curCoords.y < nextCoords.y ? curCoords.y : nextCoords.y;
                    var upperY = curCoords.y > nextCoords.y ? curCoords.y : nextCoords.y;
                    if (lowerY < yBounds.lower) yBounds.lower = lowerY;
                    if (upperY > yBounds.upper) yBounds.upper = upperY;
                    if (curCoords.x > xBounds.upper) xBounds.upper = curCoords.x;
                    if (curCoords.x < xBounds.lower) xBounds.lower = curCoords.x;

                    for (var j = lowerY; j < upperY + 1; j++)
                    {
                        positions.Add((curCoords.x, j));
                    }
                }
            }
        }

        return positions;
    }
}