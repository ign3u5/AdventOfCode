using AdventOfCode;
using AdventOfCode.Domain;

public class DayFive : IChallenge<long>
{
    public string ChallengeInputPath { get; } = DataProvider<DayFive>.GetChallengePath();

    public long RunTaskOne(string[] lines)
    {
        long[] seeds = lines[0].Split(':')[1].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(long.Parse).ToArray();

        List<RangeData> rangeDatas = new();

        for (int lineNo = 2; lineNo < lines.Length; lineNo++)
        {
            string line = lines[lineNo];

            if (string.IsNullOrWhiteSpace(line))
            {
                UpdateSeeds();
            }

            if (line.Length == 0 || !char.IsDigit(line[0]))
            {
                continue;
            }

            long[] mapParts = line.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(long.Parse).ToArray();

            rangeDatas.Add(new RangeData(mapParts[0], mapParts[1], mapParts[2]));
        }

        UpdateSeeds();

        return seeds.QuickSort((curr, pivot) => curr < pivot).First();

        void UpdateSeeds()
        {
            for (int seedNo = 0; seedNo < seeds.Length; seedNo++)
            {
                long seed = seeds[seedNo];

                foreach (RangeData rangeData in rangeDatas)
                {
                    if (seed >= rangeData.SourceRangeStart && seed < rangeData.SourceRangeStart + rangeData.RangeLength)
                    {
                        seeds[seedNo] = rangeData.DestinationRangeStart + (seed - rangeData.SourceRangeStart);
                    }
                }
            }

            rangeDatas = new();
        }
    }

    private record RangeData(long DestinationRangeStart, long SourceRangeStart, long RangeLength);

    public long RunTaskTwo(string[] lines)
    {
        long[] initialSeeds = lines[0].Split(':')[1].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(long.Parse).ToArray();

        HashSet<long> seeders = new();
        for (int iSeedNo = 0; iSeedNo < initialSeeds.Length; iSeedNo += 2)
        {
            for (var start = initialSeeds[iSeedNo]; start < initialSeeds[iSeedNo] + initialSeeds[iSeedNo + 1]; start++)
            {
                seeders.Add(start);
            }
        }

        long[] seeds = seeders.ToArray();

        List<RangeData> rangeDatas = new();

        for (int lineNo = 2; lineNo < lines.Length; lineNo++)
        {
            string line = lines[lineNo];

            if (string.IsNullOrWhiteSpace(line))
            {
                UpdateSeeds();
            }

            if (line.Length == 0 || !char.IsDigit(line[0]))
            {
                continue;
            }

            long[] mapParts = line.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(long.Parse).ToArray();

            rangeDatas.Add(new RangeData(mapParts[0], mapParts[1], mapParts[2]));
        }

        UpdateSeeds();

        return seeds.QuickSort((curr, pivot) => curr < pivot).First();

        void UpdateSeeds()
        {
            for (int seedNo = 0; seedNo < seeds.Length; seedNo++)
            {
                long seed = seeds[seedNo];

                foreach (RangeData rangeData in rangeDatas)
                {
                    if (seed >= rangeData.SourceRangeStart && seed < rangeData.SourceRangeStart + rangeData.RangeLength)
                    {
                        seeds[seedNo] = rangeData.DestinationRangeStart + (seed - rangeData.SourceRangeStart);
                    }
                }
            }

            rangeDatas = new();
        }
    }
}