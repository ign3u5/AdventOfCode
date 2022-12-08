using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DaySeven : IChallenge<int>
{
    public string ChallengeInputPath => DataProvider<DaySeven>.GetChallengePath();
    public int RunTaskOne(string[] lines)
    {
        var root = new Directory("/");
        var curDir = root;
            
        for (var i = 1; i < lines.Length; i++)
        {
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

        return root.GetTotalOfSizesLessThan(100000);
    }

    public int RunTaskTwo(string[] lines)
    {
        return 0;
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