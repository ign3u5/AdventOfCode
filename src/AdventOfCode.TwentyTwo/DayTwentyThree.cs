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
        // for each round
        // first half - each elf considers where to move based on current positions
            // if no elves are in any of the eight positions, the elf does not move
            // the checks go, N, S, W, E (check for N, NE, NW and same for others)
            // the at the end of the first round, the first check is moved to the end of the list (i.e. round 2 is S, W, E, N)
        // second half - if they were the only elf to propose to move to a location, they move there
            // if more than one elf proposed to move to a speific location, none of the elves move
        // Find the number of empty squares within the smallest rectangle that fits around all of the elves
        // (keep track of furthers North, South, East, and West)
        var coords = ParseInput(lines);

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

        (int min, int max) xRange = (int.MaxValue, int.MinValue);
        (int min, int max) yRange = (int.MaxValue, int.MinValue);

        for (var round = 1; round < 11; round++)
        {
            Dictionary<(int x, int y), (int x, int y)> newCoords = new();
            foreach(var coord in coords)
            {
                if (hasNoSurroudingElves(coord)) continue;

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

            foreach(var coordSet in newCoords)
            {
                coords.Remove(coordSet.Value);
                coords.Add(coordSet.Key);
            }
        }

        foreach(var coord in coords)
        {
            UpdateBounds(coord);
        }

        void UpdateBounds((int x, int y) coord)
        {
            if (coord.x < xRange.min) xRange.min = coord.x;
            if (coord.x > xRange.max) xRange.max = coord.x;

            if (coord.y < yRange.min) yRange.min = coord.y;
            if (coord.y > yRange.max) yRange.max = coord.y;
        }

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

    public int RunTaskTwo(string[] lines)
    {
        var coords = ParseInput(lines);

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

        var changesPerRound = 0;
        var numberOfRounds = 0;

        do
        {
            changesPerRound = 0;
            numberOfRounds++;
            Dictionary<(int x, int y), (int x, int y)> newCoords = new();
            foreach (var coord in coords)
            {
                if (hasNoSurroudingElves(coord)) continue;
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
}

