using System;
using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo
{
	public class DayFifteen : IChallenge<int>
	{
        public string ChallengeInputPath => DataProvider<DayFifteen>.GetChallengePath();

        public int RunTaskOne(string[] lines)
        {
            int targetY = 10;
            HashSet<(int x, int y)> beacons = new();
            HashSet<(int x, int y, Datum type)> data = new();
            foreach (var line in lines)
            {
                var rawCoords = line[line.IndexOf('x')..line.IndexOf(':')].Split(", ");
                var sensorCoords = (x: int.Parse(rawCoords[0][2..]), y: int.Parse(rawCoords[1][2..]));
                data.Add((sensorCoords.x, sensorCoords.y, Datum.SENSOR));

                rawCoords = line[line.LastIndexOf('x')..].Split(", ");
                var beaconCoords = (x: int.Parse(rawCoords[0][2..]), y: int.Parse(rawCoords[1][2..]));
                data.Add((beaconCoords.x, beaconCoords.y, Datum.BEACON));

                var mDistance = Math.Abs(sensorCoords.x - beaconCoords.x) + Math.Abs(sensorCoords.y - beaconCoords.y);

                if (targetY > sensorCoords.y - mDistance && targetY < sensorCoords.y + mDistance)
                {
                    if (targetY > sensorCoords.y)
                    {
                        var n = sensorCoords.y + mDistance - targetY - 1;
                        for (var i = sensorCoords.x - n; i < sensorCoords.x + n; i++)
                        {
                            if (!beacons.Contains((i, targetY))) data.Add((i, targetY, Datum.FULL));
                        } 
                    }
                }

                var yCount = 0;
                var isPivot = false;
                for (var y = sensorCoords.y - mDistance; y < sensorCoords.y + mDistance + 1; y++)
                {
                    int start = sensorCoords.x - yCount;
                    int end = sensorCoords.x + yCount;
                    for (var x = start; x < end + 1; x++)
                    {
                        data.Add((x, y, Datum.FULL));
                    }

                    if (yCount == mDistance)
                    {
                        isPivot = true;
                    }

                    if (isPivot)
                    { 
                        yCount--;
                    }
                    else
                    {
                        yCount++;
                    }
                    
                }
            }

            var totalSpacesBeaconNotPresent = 0;
            foreach (var datum in data)
            {
                if (datum.y == 10 && !data.Contains((datum.x, datum.y, Datum.BEACON)))
                {
                    totalSpacesBeaconNotPresent++;
                }
            }

            return totalSpacesBeaconNotPresent;
        }

        public int RunTaskTwo(string[] lines)
        {
            return 0;
        }

        private enum Datum
        {
            BEACON,
            SENSOR,
            FULL
        }

        private class Sensor
        {
            public (int x, int y) sCoord { get; set; }
            public (int x, int y) bCoord { get; set; }
            public int MDistance { get; }
            
        }

        private class Node
        {
            public (int x, int y) Coord { get; set; }
            public NodeType NodeType { get; set; }

            public static Node NewSensor((int x, int y) coord) => new()
            {
                Coord = coord,
                NodeType = NodeType.SENSOR
            };

            public static Node NewBeacon((int x, int y) coord) => new()
            {
                Coord = coord,
                NodeType = NodeType.BEACON
            };
        }

        private enum NodeType
        {
            BEACON,
            SENSOR
        }
    }
}