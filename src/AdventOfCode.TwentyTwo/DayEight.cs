using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DayEight : IChallenge<int>
{
    public string ChallengeInputPath => DataProvider<DayEight>.GetChallengePath();
    public int RunTaskOne(string[] lines)
    {
        HashSet<(int, int)> visibleTrees = new();
        var verticalMax = new int[lines[0].Length].Select(x => -1).ToArray();
        for (var y = 0; y < lines.Length; y++)
        {
            var horizontalMax = -1;
            for (var x = 0; x < lines[y].Length; x++)
            {
                var curVal = int.Parse($"{lines[y][x]}");
                if (curVal > horizontalMax)
                {
                    visibleTrees.Add((x, y));
                    horizontalMax = curVal;
                }

                if (curVal > verticalMax[x])
                {
                    visibleTrees.Add((x, y));
                    verticalMax[x] = curVal;
                }
            }
        }

        verticalMax = new int[lines[0].Length].Select(x => -1).ToArray();
        for (var y = lines.Length - 1; y > -1; y--)
        {
            var horizontalMax = -1;
            for (var x = lines[0].Length - 1; x > -1; x--)
            {
                var curVal = int.Parse($"{lines[y][x]}");

                if (curVal > horizontalMax)
                {
                    visibleTrees.Add((x, y));
                    horizontalMax = curVal;
                }

                if (curVal > verticalMax[x])
                {
                    visibleTrees.Add((x, y));
                    verticalMax[x] = curVal;
                }
            }
        }

        return visibleTrees.Count;
    }
    
    delegate void IncDec(ref int i);

    public int RunTaskTwo(string[] lines)
    {
        int FindNextHeight(int x, int y, int h, int v)
        {
            
            
            var count = 1;
            var current = int.Parse($"{lines[y][x]}");

            void Run(int startingValue, Func<int, bool> comparison, IncDec changeIndex, Func<int, char> nextValue)
            {
                for (var i = startingValue; comparison(i); changeIndex(ref i))
                {
                    var next = int.Parse($"{nextValue(i)}");
                    if (current > next)
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            
            if (h == 1)
            {
                Run(x + 1, i => i < lines[0].Length - 1, (ref int i) => i++, i => lines[y][i]);
            }

            if (h == -1)
            {
                Run(x - 1, i => i > 0, (ref int i) => i--, i => lines[y][i]);
            }
            
            if (v == 1)
            {
                Run(y + 1, i => i < lines[0].Length - 1, (ref int i) => i++, i => lines[i][x]);
            }
            
            if (v == -1)
            {
                Run(y - 1, i => i > 0, (ref int i) => i--, i => lines[i][x]);
            }

            return count;
        }
        
        int largestResult = -1;
        for (var y = 1; y < lines.Length - 1; y++)
        {
            for (var x = 1; x < lines[0].Length - 1; x++)
            {
                var cur = int.Parse($"{lines[y][x]}");
                
                int left = FindNextHeight(x, y, -1, 0);
                int right = FindNextHeight(x, y, 1, 0);
                int up = FindNextHeight(x, y, 0, -1);
                int down = FindNextHeight(x, y, 0, 1);

                var temp = left * right * up * down;
                if (temp > largestResult)
                {
                    largestResult = temp;
                }
            }
        }

        return largestResult;
    }

    private class Tree
    {
        private bool _isRoot = false;
        private int _left = 0;
        private int _right = 0;
        private int _up = 0;
        private int _down = 0;

        public int Left
        {
            set => _left = value;
        }
        public Tree PrevHorizontal { get; set; }
        public Tree PrevVertical { get; set; }
        public int X { get; }
        public int Y { get; }
        public int Height { get; init; }
        public int ScenicScore => _left * _right * _up * _down;

        public Tree(int x, int y, int height)
        {
            X = x;
            Y = y;
            Height = height;
        }
        
        public Tree(int x, int y, char height)
        {
            X = x;
            Y = y;
            Height = int.Parse($"{height}");
        }
        
        public static Tree CreateRoot(int x, int y, char height) => new(x, y, height) { _isRoot = true };
        public static Tree CreateRoot(int x, int y, int height) => new(x, y, height) { _isRoot = true };

        public void ChainUpdateScenicScoreHorizontal(int maxHeight = -1)
        {
            if (_isRoot) return;

            if (maxHeight == -1) maxHeight = Height;
            
            if (Height > maxHeight)
            {
                maxHeight = Height;
                _right += 1;
            }

            if (Height > PrevHorizontal.Height)
            {
                PrevHorizontal._right = 1;
            }

            if (Height < PrevHorizontal.Height)
            {
                _left = 1;
            }

            if (Height == PrevHorizontal.Height)
            {
                _left = 1;
                PrevHorizontal._right = 1;
            }
            
            PrevHorizontal.ChainUpdateScenicScoreHorizontal(maxHeight);
        }
    }
}