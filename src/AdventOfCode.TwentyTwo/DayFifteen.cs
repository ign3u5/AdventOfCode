using System;
using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

	public class DayFifteen : IChallenge<int>
	{
    public string ChallengeInputPath => DataProvider<DayFifteen>.GetChallengePath();

    public int RunTaskOne(string[] lines)
    {
        int targetY = 2000000;
        return GetNumberOfTakenPositions(lines, targetY);
    }

    public int GetNumberOfTakenPositions(string[] lines, int targetY)
    {
        HashSet<(int x, int y)> beacons = new();
        HashSet<int> data = new();
        foreach (var line in lines)
        {
            var rawCoords = line[line.IndexOf('x')..line.IndexOf(':')].Split(", ");
            var sensorCoords = (x: int.Parse(rawCoords[0][2..]), y: int.Parse(rawCoords[1][2..]));
            
            rawCoords = line[line.LastIndexOf('x')..].Split(", ");
            var beaconCoords = (x: int.Parse(rawCoords[0][2..]), y: int.Parse(rawCoords[1][2..]));
            beacons.Add((beaconCoords.x, beaconCoords.y));

            var mDistance = Math.Abs(sensorCoords.x - beaconCoords.x) + Math.Abs(sensorCoords.y - beaconCoords.y);

            if (targetY >= sensorCoords.y - mDistance && targetY <= sensorCoords.y + mDistance)
            {
                if (targetY > sensorCoords.y)
                {
                    var n = Math.Abs(sensorCoords.y + mDistance - targetY);
                    for (var i = sensorCoords.x - n; i < sensorCoords.x + n + 1; i++)
                    {
                        if (!beacons.Contains((i, targetY))) data.Add(i);
                    } 
                }

                if (targetY < sensorCoords.y)
                {
                    var n = targetY - Math.Abs(sensorCoords.y - mDistance);
                    for (var i = sensorCoords.x - n; i < sensorCoords.x + n + 1; i++)
                    {
                        if (!beacons.Contains((i, targetY))) data.Add(i);
                    }
                }

                if (targetY == sensorCoords.y)
                {
                    for (var i = sensorCoords.x - mDistance; i < sensorCoords.x + mDistance + 1; i++)
                    {
                        if (!beacons.Contains((i, targetY))) data.Add(i);
                    }
                }
            }
        }

        
        return data.Count;
    }
    
    public int RunTaskTwo(string[] lines)
    {
        return 0;
    }

    public int FindAlertBeacon(string[] lines, int range)
    {
        HashSet<(int x, int y)> beacons = new();
        List<Sensor> sensors = new();
        var sensorId = 0;
        foreach (var line in lines)
        {
            var rawCoords = line[line.IndexOf('x')..line.IndexOf(':')].Split(", ");
            var sensorCoords = (x: int.Parse(rawCoords[0][2..]), y: int.Parse(rawCoords[1][2..]));

            rawCoords = line[line.LastIndexOf('x')..].Split(", ");
            var beaconCoords = (x: int.Parse(rawCoords[0][2..]), y: int.Parse(rawCoords[1][2..]));

            beacons.Add(beaconCoords);
            var sensor = new Sensor(sensorId++, sensorCoords, beaconCoords);
            sensors.Add(sensor);
        }

        var previousRange = (min: int.MaxValue, max: int.MinValue);
        for (var i = 0; i < range + 1; i++)
        {
            var ranges = sensors.Where(s => s.TryGetRangeForY(i, out _)).Select(s =>
            {
                if (s.TryGetRangeForY(i, out var range))
                {
                    return range;
                }

                return (min: 0, max: 0);
            }).QuickSort((curr, pivot) => curr.min < pivot.min);
            previousRange = ranges[0];
            
            foreach (var rang in ranges.Skip(1))
            {
                if (rang.min > previousRange.max + 1)
                {
                    
                    return previousRange.max + 1 * 4000000 + i;
                }
                if (rang.max > previousRange.max)
                {
                    previousRange = rang;
                }
            }                
        }
        

        foreach (var curr in sensors)
        {
            Sensor closestRight = null;
            Sensor closesDown = null;
            foreach (var next in sensors)
            {
                if (curr.Id == next.Id) continue;
                var signalDelt = Math.Abs(curr.Coords.x - next.Coords.x) + Math.Abs(curr.Coords.y - next.Coords.y);
                var boundsDelt = curr.BeaconDistance + next.BeaconDistance - signalDelt;
                if (boundsDelt > 0)
                {
                    
                }
            }
        }

        return 0;
    }

    private record Sensor
    {
        public int Id { get; }
        public (int x, int y) Coords { get; }
        public int BeaconDistance { get; }

        public Sensor(int id, (int x, int y) coords, (int x, int y) beacon)
        {
            Id = id;
            Coords = coords;
            BeaconDistance = Math.Abs(coords.x - beacon.x) + Math.Abs(coords.y - beacon.y);
        }

        public bool TryGetRangeForY(int y, out (int min, int max) range)
        {
            if (y >= Coords.y - BeaconDistance && y <= Coords.y + BeaconDistance)
            {
                var n = 0;
                if (y > Coords.y)
                {
                    n = Math.Abs(Coords.y + BeaconDistance - y);
                    range = (Coords.x - n, Coords.x + n);
                    return true;
                }

                if (y < Coords.y)
                {
                    n = Math.Abs(y - Math.Abs(Coords.y - BeaconDistance));
                    range = (Coords.x - n, Coords.x + n);
                    return true;
                }

                
                range = (Coords.x - BeaconDistance, Coords.x + BeaconDistance);
                
                return true;
            }

            range = (0, 0);
            return false;
        }
    }
}