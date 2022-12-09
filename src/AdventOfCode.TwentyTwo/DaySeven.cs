using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DaySeven : IChallenge<int>
{
    public string ChallengeInputPath => DataProvider<DaySeven>.GetChallengePath();
    
    public int RunTaskOne(string[] lines)
    {
        var root = AnalyseAndGetRoot(lines);

        return root.GetTotalOfSizesLessThan(100000);
    }

    public int RunTaskTwo(string[] lines)
    {
        var root = AnalyseAndGetRoot(lines);

        return root.GetDirectoryToMakeSpace(70000000, 30000000);
    }

    private Directory AnalyseAndGetRoot(string[] lines)
    {
        var root = new Directory("/");
        var curDir = root;
            
        for (var i = 1; i < lines.Length; i++)
        {
            // var command = CommandParser.ParseLine(lines[0]);
            // curDir = command(curDir);
            var curLine = lines[i];
            if (curLine[0] == '$')
            {
                if (curLine[2..4] == "cd")
                {
                    if (curLine[5..] == "..")
                    {
                        curDir = curDir.ParentDir;
                        continue;
                    }
                    
                    curDir = curDir.CreateSubDirectory(curLine[5..]);
                }
            
                continue;
            }
            
            if (curLine[..3] == "dir")
            {
                continue;
            }
            
            curDir.Size += int.Parse(curLine.Split(' ')[0]);
        }

        return root;
    }

    private static class CommandParser
    {
        public static Func<Directory, Directory> ParseLine(string line)
        {
            if (line[0] == '$') return ParseCommand(line);
            return line[..3] switch
            {
                "dir" => dir => dir,
                _ => dir =>
                {
                    dir.Size += int.Parse(line.Split(' ')[0]);

                    return dir;
                }
            };
        }

        private static Func<Directory, Directory> ParseCommand(string line) => line[2..4] switch
        {
            "cd" when line[5..] == ".." => dir => dir.ParentDir,
            "cd" => dir => dir.CreateSubDirectory(line[5..]),
            _ => dir => dir
        };
    }

    private record Directory
    {
        private int _size = 0;
        private bool _isRoot = false;
        public int Size
        {
            get => _size;
            set
            {
                if (!_isRoot)
                {
                    ParentDir.Size += value - _size;
                }
                _size = value;
            }
        }
        public List<Directory>? subDirs { get; } = new();
        
        public Directory ParentDir { get; }
        
        public string Name { get; }

        public Directory(string name)
        {
            Name = name;
            _isRoot = true;
        }

        private Directory(string name, Directory parentDir)
        {
            ParentDir = parentDir;
            Name = name;
        }

        public Directory CreateSubDirectory(string name)
        {
            var subDir = new Directory(name, this);
            subDirs.Add(subDir);

            return subDir;
        }

        public int GetDirectoryToMakeSpace(int totalDiskSpace, int freeSpaceWanted)
        {
            var closestSize = int.MaxValue;
            var sizeLeftToDelete = freeSpaceWanted - (totalDiskSpace - Size); 

            return GetDirectoryThatCanFit(sizeLeftToDelete, closestSize);
        }
        
        private int GetDirectoryThatCanFit(int size, int closestSize)
        {
            if (Size >= size && Size < closestSize)
                closestSize = Size;

            foreach (var dir in subDirs)
            {
                closestSize = dir.GetDirectoryThatCanFit(size, closestSize);
            }

            return closestSize;
        }

        public int GetTotalOfSizesLessThan(int sizeLimit)
        {
            var total = 0;
            if (Size < sizeLimit)
            {
                total += Size;
            }
            
            foreach (var dir in subDirs)
            {
                total += dir.GetTotalOfSizesLessThan(sizeLimit);
            }

            return total;
        }
    };
}