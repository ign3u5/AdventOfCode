using System;
using System.Drawing;
using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DayTwentyThree : IChallenge<int>
{
    public string ChallengeInputPath => DataProvider<DayTwentyThree>.GetChallengePath();

    private delegate bool Check((int x, int y) coord, out (int x, int y) newCoord);

    public int RunTaskOne(string[] lines)
    {
        var coords = ParseInput(lines);

        var (checks, hasNoSurroundingElves) = GetChecks(coords);

        (int min, int max) xRange = (int.MaxValue, int.MinValue);
        (int min, int max) yRange = (int.MaxValue, int.MinValue);

        for (var round = 1; round < 11; round++)
        {
            Dictionary<(int x, int y), (int x, int y)> newCoords = new();
            foreach(var coord in coords)
            {
                if (hasNoSurroundingElves(coord)) continue;

                foreach (var check in checks)
                {
                    if (check(coord, out var newCoord))
                    {
                        newCoords.TryAddAndMaintainUnique(newCoord, coord);
                        break;
                    }
                }
            }

            checks.Enqueue(checks.Dequeue());

            UpdateCoords(newCoords);
        }

        foreach(var coord in coords)
        {
            UpdateBounds(coord);
        }

        return CountEmptySpaces();

        void UpdateCoords(Dictionary<(int x, int y), (int x, int y)> newCoords)
        {
            foreach (var coordSet in newCoords)
            {
                coords.Remove(coordSet.Value);
                coords.Add(coordSet.Key);
            }
        }

        void UpdateBounds((int x, int y) coord)
        {
            if (coord.x < xRange.min) xRange.min = coord.x;
            if (coord.x > xRange.max) xRange.max = coord.x;

            if (coord.y < yRange.min) yRange.min = coord.y;
            if (coord.y > yRange.max) yRange.max = coord.y;
        }

        int CountEmptySpaces()
        {
            int emptySpaces = 0;

            for (var y = yRange.min; y < yRange.max + 1; y++)
            {
                for (var x = xRange.min; x < xRange.max + 1; x++)
                {
                    if (!coords.Contains((x, y))) emptySpaces++;
                }
            }

            return emptySpaces;
        }
    }
    
    public int RunTaskTwo(string[] lines)
    {
        var coords = ParseInput(lines);

        var (checks, hasNoSurroundingElves) = GetChecks(coords);

        var changesPerRound = 0;
        var numberOfRounds = 0;

        do
        {
            changesPerRound = 0;
            numberOfRounds++;
            Dictionary<(int x, int y), (int x, int y)> newCoords = new();
            foreach (var coord in coords)
            {
                if (hasNoSurroundingElves(coord)) continue;
                changesPerRound++;

                foreach (var check in checks)
                {
                    if (check(coord, out var newCoord))
                    {
                        newCoords.TryAddAndMaintainUnique(newCoord, coord);
                        break;
                    }
                }
            }

            checks.Enqueue(checks.Dequeue());

            foreach (var coordSet in newCoords)
            {
                coords.Remove(coordSet.Value);
                coords.Add(coordSet.Key);
            }

        } while (changesPerRound > 0);

        return numberOfRounds;
    }

    private static HashSet<(int x, int y)> ParseInput(string[] lines)
    {
        HashSet<(int, int)> coords = new();

        for (var y = 0; y < lines.Length; y++)
        {
            for (var x = 0; x < lines[0].Length; x++)
            {
                if (lines[y][x] == '#')
                {
                    coords.Add((x, y));
                }
            }
        }

        return coords;
    }

    private static (Queue<Check>, Func<(int, int), bool>) GetChecks(HashSet<(int, int)> coords)
    {
        Check tryMoveNorth = ((int x, int y) coord, out (int x, int y) outCoord) =>
        {
            outCoord = (coord.x, coord.y - 1);
            return canMoveNorth(coord) != 0;
        };

        int canMoveNorth((int x, int y) coord) =>
            !coords.Contains((coord.x, coord.y - 1))
            && !coords.Contains((coord.x - 1, coord.y - 1))
            && !coords.Contains((coord.x + 1, coord.y - 1)) ? 1 : 0;

        Check tryMoveSouth = ((int x, int y) coord, out (int x, int y) outCoord) =>
        {
            outCoord = (coord.x, coord.y + 1);
            return canMoveSouth(coord) != 0;
        };

        int canMoveSouth((int x, int y) coord) =>
            !coords.Contains((coord.x, coord.y + 1))
            && !coords.Contains((coord.x - 1, coord.y + 1))
            && !coords.Contains((coord.x + 1, coord.y + 1)) ? 2 : 0;

        Check tryMoveWest = ((int x, int y) coord, out (int x, int y) outCoord) =>
        {
            outCoord = (coord.x - 1, coord.y);
            return canMoveWest(coord) != 0;
        };

        int canMoveWest((int x, int y) coord) =>
            !coords.Contains((coord.x - 1, coord.y))
            && !coords.Contains((coord.x - 1, coord.y - 1))
            && !coords.Contains((coord.x - 1, coord.y + 1)) ? 4 : 0;

        Check tryMoveEast = ((int x, int y) coord, out (int x, int y) outCoord) =>
        {
            outCoord = (coord.x + 1, coord.y);
            return canMoveEast(coord) != 0;
        };

        int canMoveEast((int x, int y) coord) =>
            !coords.Contains((coord.x + 1, coord.y))
            && !coords.Contains((coord.x + 1, coord.y - 1))
            && !coords.Contains((coord.x + 1, coord.y + 1)) ? 8 : 0;

        bool hasNoSurroudingElves((int, int) coord) => (canMoveNorth(coord) | canMoveSouth(coord) | canMoveWest(coord) | canMoveEast(coord)) == 15;

        Queue<Check> checks = new(new Check[] { tryMoveNorth, tryMoveSouth, tryMoveWest, tryMoveEast });

        return (checks, hasNoSurroudingElves);
    }
}

