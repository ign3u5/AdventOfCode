using System;
using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DayNineteen : IChallenge<int>
{
    public string ChallengeInputPath => DataProvider<DayTwenty>.GetChallengePath();

    public int RunTaskOne(string[] lines)
    {
        var blueprints = ParseBlueprints(lines);
        foreach(var blueprint in blueprints)
        {
            var totalMinutes = 25;
            var state = new State { OreRobots = 1 };

            for (var currMinute = 1; currMinute < totalMinutes; currMinute++)
            {
                if (TryBuy(blueprint.Geode, out bool shouldContinue))
                {
                    state.UpdateTotals();
                    state.GeodeRobots++;
                    continue;
                }
                else if (shouldContinue)
                {
                    state.UpdateTotals();
                    continue;
                }

                if (TryBuy(blueprint.Obsidian, out shouldContinue))
                {
                    state.UpdateTotals();
                    state.ObsidianRobots++;
                    continue;
                }
                else if (shouldContinue)
                {
                    state.UpdateTotals();
                    continue;
                }

                if (TryBuy(blueprint.Clay, out shouldContinue))
                {
                    state.UpdateTotals();
                    state.ClayRobots++;
                    continue;
                }
                else if (shouldContinue)
                {
                    state.UpdateTotals();
                    continue;
                }

                if (TryBuy(blueprint.Ore, out shouldContinue))
                {
                    state.UpdateTotals();
                    state.OreRobots++;
                    continue;
                }
                else if (shouldContinue)
                {
                    state.UpdateTotals();
                    continue;
                }
                state.UpdateTotals();
            }
            blueprint.MaxNumOfGeodes = state.Totals.Geode;

            bool TryBuy(Cost cost, out bool shouldContinue)
            {
                shouldContinue = false;
                if (state.Totals.Obsidian >= cost.Obsidian)
                {
                    if (state.Totals.Clay >= cost.Clay)
                    {
                        if (state.Totals.Ore >= cost.Ore)
                        {
                            state.Totals.Ore -= cost.Ore;
                            state.Totals.Clay -= cost.Clay;
                            state.Totals.Obsidian -= cost.Obsidian;
                            return true;
                        }
                        else if (cost.Clay != 0 || cost.Obsidian != 0)
                        {
                            shouldContinue = true;
                        }
                    }
                    else if (cost.Obsidian != 0)
                    {
                        shouldContinue = true;
                    }
                    else if (cost.Clay != 0)
                    {
                        var minsUntilRequiredOre = Math.Round((double)(cost.Ore / state.OreRobots));
                        var clayAtRequiredOre = state.Totals.Clay + (minsUntilRequiredOre * state.ClayRobots);
                        if (clayAtRequiredOre >= cost.Clay)
                        {
                            shouldContinue = true;
                        }
                    }
                }
                else if (cost.Obsidian != 0)
                {
                    var minsUntilRequiredOre = Math.Round((double)(cost.Ore / state.OreRobots));
                    var obsidianAtRequiredOre = state.Totals.Obsidian + (minsUntilRequiredOre * state.ObsidianRobots);
                    if (obsidianAtRequiredOre >= cost.Obsidian)
                    {
                        shouldContinue = true;
                    }
                }
                return false;
            }
        }

        var total = 0;
        foreach(var blueprint in blueprints)
        {
            total += blueprint.MaxNumOfGeodes * blueprint.Id;
        }

        return total;
    }

    private Blueprint[] ParseBlueprints(string[] lines)
    {
        var blueprints = new Blueprint[lines.Length];

        for (var i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split(' ');

            Blueprint bp = new(
                Id: int.Parse(parts[1][..^1]),
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
        public int OreRobots { get; set; }
        public int ClayRobots { get; set; }
        public int ObsidianRobots { get; set; }
        public int GeodeRobots { get; set; }
        public Cost Totals { get; } = new();

        public void UpdateTotals()
        {
            Totals.Ore += OreRobots;
            Totals.Clay += ClayRobots;
            Totals.Obsidian += ObsidianRobots;
            Totals.Geode += GeodeRobots;
        }
    }

    private record Blueprint (int Id, Cost Ore, Cost Clay, Cost Obsidian, Cost Geode) 
    {
        public int MaxNumOfGeodes { get; set; }
    }

    private record Cost
    {
        public int Ore { get; set; }
        public int Clay { get; set; }
        public int Obsidian { get; set; }
        public int Geode { get; set; }
    }
}

