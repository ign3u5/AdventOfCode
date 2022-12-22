using System;
using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DayNineteen : IChallenge<int>
{
    public string ChallengeInputPath => DataProvider<DayTwenty>.GetChallengePath();

    public int RunTaskOne(string[] lines)
    {
        var blueprints = ParseBlueprints(lines);

        var totalMinutes = 25;
        var state = new State { Clay = 1 };

        // 219 - Geode
        for (var currMinute = 1; currMinute < totalMinutes; currMinute++)
        {
            state.UpdateTotals();
        }

        return 0;
    }

    private Blueprint[] ParseBlueprints(string[] lines)
    {
        var blueprints = new Blueprint[lines.Length];

        for (var i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split(' ');

            Blueprint bp = new(

                Id: int.Parse(parts[1]),
                Ore: new() { Ore = int.Parse(parts[6]) },
                Clay: new() { Ore = int.Parse(parts[12]) },
                Obsidian: new() { Ore = int.Parse(parts[18]), Clay = int.Parse(parts[21]) },
                Geode: new() { Ore = int.Parse(parts[27]), Obsidian = int.Parse(parts[30]) }
            );

            blueprints[i] = bp;
        }

        return blueprints;
    }

    public int RunTaskTwo(string[] lines)
    {
        return 0;
    }

    private class State
    {
        public int Ore { get; set; }
        public int Clay { get; set; }
        public int Obsidian { get; set; }
        public int Geode { get; set; }
        public Cost Totals { get; } = new();

        public void UpdateTotals()
        {
            Totals.Ore += Ore;
            Totals.Clay += Clay;
            Totals.Obsidian += Obsidian;
            Totals.Geode += Geode;
        }
    }

    private record Blueprint (int Id, Cost Ore, Cost Clay, Cost Obsidian, Cost Geode);

    private record Cost
    {
        public int Ore { get; set; }
        public int Clay { get; set; }
        public int Obsidian { get; set; }
        public int Geode { get; set; }
    }
}

