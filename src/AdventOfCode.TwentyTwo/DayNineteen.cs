using System;
using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DayNineteen : IChallenge<int>
{
    public string ChallengeInputPath => DataProvider<DayNineteen>.GetChallengePath();

    public int RunTaskOne(string[] lines)
    {
        Blueprint[] blueprints = ParseBlueprints(lines);
        var total = 0;

        foreach (var blueprint in blueprints)
        {
            List<State> finalStates = new();
            var highestGeodesFound = 0;
            Stack<State> states = new(new State[] { new(Robots: new(Ore: 1), Inventory: new(), CurrMin: 0) });
            var maxRequired = new Minerals(
                Ore: (new int[] { blueprint.OreCost.Ore, blueprint.ClayCost.Ore, blueprint.ObsidianCost.Ore, blueprint.GeodeCost.Ore}).Max(),
                Clay: blueprint.ObsidianCost.Clay,
                Obsidian: blueprint.GeodeCost.Obsidian
                );

            while (states.TryPop(out State state))
            {
                static int triangleGeodes(int timeRemaining)
                {
                    timeRemaining--;
                    return timeRemaining * (timeRemaining + 1) / 2;
                }

                void handleNewState(State newState)
                {
                    if (newState.Robots > maxRequired)
                        return;

                    if (highestGeodesFound < newState.Inventory.Geode)
                    {
                        highestGeodesFound = newState.Inventory.Geode;
                        if (newState.CurrMin == 24)
                        {
                            finalStates.Add(newState);
                            return;
                        }
                        states.Push(newState);
                        return;
                    }

                    var possibleGeodes = newState.Inventory.Geode + newState.Robots.Geode * (24 - newState.CurrMin) + triangleGeodes(24 - newState.CurrMin);
                    if (possibleGeodes <= highestGeodesFound)
                    {
                        return;
                    }

                    if (newState.CurrMin == 24)
                    {
                        finalStates.Add(newState);
                        return;
                    }

                    states.Push(newState);
                }

                handleNewState(state with
                {
                    Inventory = state.Inventory + state.Robots,
                    CurrMin = state.CurrMin + 1
                });

                if (blueprint.OreCost <= state.Inventory)
                {
                    handleNewState(
                        state with
                        {
                            Inventory = state.Inventory - blueprint.OreCost + state.Robots,
                            Robots = state.Robots + new Minerals(Ore: 1),
                            CurrMin = state.CurrMin + 1
                        });
                }
                if (blueprint.ClayCost <= state.Inventory)
                {
                    handleNewState(
                        state with
                        {
                            Inventory = state.Inventory - blueprint.ClayCost + state.Robots,
                            Robots = state.Robots + new Minerals(Clay: 1),
                            CurrMin = state.CurrMin + 1
                        });
                }
                if (blueprint.ObsidianCost <= state.Inventory)
                {
                    handleNewState(
                        state with
                        {
                            Inventory = state.Inventory - blueprint.ObsidianCost + state.Robots,
                            Robots = state.Robots + new Minerals(Obsidian: 1),
                            CurrMin = state.CurrMin + 1
                        });
                }
                if (blueprint.GeodeCost <= state.Inventory)
                {
                    handleNewState(
                        state with
                        {
                            Inventory = state.Inventory - blueprint.GeodeCost + state.Robots,
                            Robots = state.Robots + new Minerals(Geode: 1),
                            CurrMin = state.CurrMin + 1
                        });
                }
            }
            if (finalStates.Count > 0)
            {
                total += finalStates.Max(s => s.Inventory.Geode) * blueprint.Id;
            }
        }

        return total;
    }

    private Blueprint[] ParseBlueprints(string[] lines)
    {
        var blueprints = new Blueprint[lines.Length];

        for (var i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split(' ');

            Blueprint blueprint = new(
                Id: int.Parse(parts[1][..^1]),
                OreCost: new(Ore: int.Parse(parts[6])),
                ClayCost: new(Ore: int.Parse(parts[12])),
                ObsidianCost: new(Ore: int.Parse(parts[18]), Clay: int.Parse(parts[21])),
                GeodeCost: new(Ore: int.Parse(parts[27]), Obsidian: int.Parse(parts[30]))
            );

            blueprints[i] = blueprint;
        }

        return blueprints;
    }

    public int RunTaskTwo(string[] lines)
    {
        return 0;
    }

    private record State (Minerals Robots, Minerals Inventory, int CurrMin);

    private record Blueprint (int Id, Minerals OreCost, Minerals ClayCost, Minerals ObsidianCost, Minerals GeodeCost) 
    {
        public int MaxNumOfGeodes { get; set; }
    }

    private record Minerals(int Ore = 0, int Clay = 0, int Obsidian = 0, int Geode = 0)
    {
        public static Minerals operator +(Minerals a, Minerals b) => a with
        {
            Ore = a.Ore + b.Ore,
            Clay = a.Clay + b.Clay,
            Obsidian = a.Obsidian + b.Obsidian,
            Geode = a.Geode + b.Geode
        };
        
        public static Minerals operator -(Minerals a, Minerals b) => a with
        {
            Ore = a.Ore - b.Ore,
            Clay = a.Clay - b.Clay,
            Obsidian = a.Obsidian - b.Obsidian,
            Geode = a.Geode - b.Geode
        };

        public static bool operator >(Minerals a, Minerals b) => 
            a.Ore > b.Ore || a.Clay > b.Clay || a.Obsidian > b.Obsidian;
        
        public static bool operator <(Minerals a, Minerals b) => 
            a.Ore < b.Ore || a.Clay < b.Clay || a.Obsidian < b.Obsidian;
        
        public static bool operator >=(Minerals a, Minerals b) => 
            a.Ore >= b.Ore && a.Clay >= b.Clay && a.Obsidian >= b.Obsidian && a.Geode >= b.Geode;

        public static bool operator <=(Minerals a, Minerals b) =>
            a.Ore <= b.Ore && a.Clay <= b.Clay && a.Obsidian <= b.Obsidian && a.Geode <= b.Geode;
    }
}

