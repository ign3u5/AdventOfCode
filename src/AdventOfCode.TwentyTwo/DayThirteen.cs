using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;
public class DayThirteen : IChallenge<int>
{
    public string ChallengeInputPath => DataProvider<DayThirteen>.GetChallengePath();
    
    private enum Result
    {
        PASS,
        EQUAL,
        FAIL
    }
    
    private class Directory
    {
        public bool IsKey { get; private set; }
        public int Length => Contents.Count;
        public List<(Directory? dir, int? file)> Contents { get; } = new();
        public Directory? Parent { get; init; }

        public Directory GetValAsDirFor(int index) => Contents[index].dir ?? new Directory(Contents[index].file);

        public Directory() { }

        private Directory(int? file) { Contents.Add((null, file)); }

        public Directory AsKey()
        {
            IsKey = true;
            return this;
        }
    }
    
    public int RunTaskTwo(string[] lines)
    {
        var rawPackets = lines.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        var directories = new Directory[rawPackets.Length + 2];
        for (var i = 0; i < rawPackets.Length; i++)
        {
            directories[i] = GetDirectory(rawPackets[i]);
        }
        directories[rawPackets.Length] = GetDirectory("[[6]]").AsKey();
        directories[rawPackets.Length + 1] = GetDirectory("[[2]]").AsKey();
        QuickSort(directories, 0, directories.Length - 1);

        return directories.Select((d, i) => d.IsKey ? i + 1 : 1).Aggregate((acc, cur) => acc * cur);
    }

    void QuickSort(Directory[] directories, int start, int end)
    {
        if (start < end)
        {
            var pIndex = Partition(directories, start, end);
            QuickSort(directories, start, pIndex - 1);
            QuickSort(directories, pIndex + 1, end);
        }
    }

    int Partition(Directory[] directories, int start, int end)
    {
        var pivot = directories[end];

        var i = start - 1;

        for (var j = start; j < end; j++)
        {
            if (CompareValues(directories[j], pivot) == Result.PASS)
            {
                i++;
                (directories[i], directories[j]) = (directories[j], directories[i]);
            }
        }
        
        (directories[i + 1], directories[end]) = (directories[end], directories[i + 1]);
        return i + 1;
    }

    public int RunTaskOne(string[] lines)
    {
        var pairs = GetPairs(lines);
        var output = 0;
        for (var j = 0; j < pairs.Length; j++)
        {
            var left = GetDirectory(pairs[j][0]);
            var right = GetDirectory(pairs[j][1]);
        
            var comparison = CompareValues(left, right);
            if (comparison == Result.PASS)
            {
                output += j + 1;
            }
        }

        
        //Between 5605
        return output;
    }
    
    private Result CompareValues(Directory left, Directory right)
    {
        var index = 0;
        while (true)
        {
            if (left.Length == index && right.Length == index) return Result.EQUAL;
            if (left.Length == index) return Result.PASS;
            if (right.Length == index) return Result.FAIL;

            var leftVal = left.Contents[index];
            var rightVal = right.Contents[index];

            if (leftVal.dir != null || rightVal.dir != null)
            {
                var comparisonResult = CompareValues(left.GetValAsDirFor(index), right.GetValAsDirFor(index));
                if (comparisonResult != Result.EQUAL) return comparisonResult;
            }

            if (leftVal.file < rightVal.file) return Result.PASS;
            if (leftVal.file > rightVal.file) return Result.FAIL;
            index++;
        }
    }
    
    private string[][] GetPairs(string[] lines)
    {
        string[][] pairs = new string[(lines.Length + 1) / 3][];

        var pairsCount = -1;

        for (var i = 0; i < lines.Length; i++)
        {
            if (i % 3 == 0)
            {
                pairsCount++;
                pairs[pairsCount] = new string[2];
            }

            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;

            pairs[pairsCount][i - pairsCount * 3] = lines[i];
        }

        return pairs;
    }

    private Directory GetDirectory(string side)
    {
        Directory parent = new Directory();
        Directory currDir = parent;
        for (var i = 1; i < side.Length; i++)
        {
            if (side[i] == '[')
            {
                Directory subDir = new() { Parent = currDir };
                currDir.Contents.Add((subDir, null));
                currDir = subDir;
                continue;
            }

            if (side[i] == ']')
            {
                if (currDir.Parent == null) return currDir;
                currDir = currDir.Parent;
                continue;
            }

            if (char.IsNumber(side[i]))
            {
                if (side[i..(i + 2)] == "10")
                {
                    i++;
                    currDir.Contents.Add((null, 10));
                    continue;
                }

                currDir.Contents.Add((null, int.Parse($"{side[i]}")));
            }
        }

        return parent;
    }

    
}