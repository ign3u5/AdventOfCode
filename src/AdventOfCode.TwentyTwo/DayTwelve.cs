using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DayTwelve : IChallenge<int>
{
    public string ChallengeInputPath => DataProvider<DayTwelve>.GetChallengePath();
    public int RunTaskOne(string[] lines)
    {
        var end = (i: -1, j: -1);

        for (var i = 0; i < lines.Length; i++)
        {
            var endIndex = lines[i].IndexOf('E');
            if (endIndex != -1)
            {
                end = (i, endIndex);
            }
        }
        
        PriorityQueue<Node, int> unvisitedNodes = new();
        PriorityQueue<Node, int> visitedNodes = new();
        Node[][] nodes = new Node[lines.Length][];

        for (var i = 0; i < lines.Length; i++)
        {
            nodes[i] = new Node[lines[i].Length];
            for (var j = 0; j < lines[i].Length; j++)
            {
                Node node = new()
                {
                    HScore = Math.Abs(end.i - i) + Math.Abs(end.j - j),
                    FScore = int.MaxValue,
                    GScore = int.MaxValue,
                    Position = (i, j),
                    Val = lines[i][j]
                };
                if (lines[i][j] == 'S')
                {
                    node.GScore = 0;
                    node.FScore = Math.Abs(end.i - i) + Math.Abs(end.j - j);
                    unvisitedNodes.Enqueue(node, node.FScore);
                }

                nodes[i][j] = node;
            }
        }

        while (unvisitedNodes.TryDequeue(out var node, out _))
        {
            if (node.IsDiscovered)
            {
                continue;
            }
            node.IsDiscovered = true;
            if (node.Val == 'E')
            {
                return node.GScore;
            }
            var neighbors = GetUnvisitedNeighbors(node);
            foreach (var neighbor in neighbors)
            {
                if ((neighbor.Val != 'E' && neighbor.Val <= node.Val + 1) || node.Val == 'S' || (neighbor.Val == 'E' && 'z' <= node.Val + 1))
                {
                    if (node.GScore + 1 < neighbor.GScore)
                    {
                        neighbor.GScore = node.GScore + 1;
                        neighbor.FScore = neighbor.HScore + neighbor.GScore;
                        neighbor.Previous = node;
                        unvisitedNodes.Enqueue(neighbor, node.FScore);
                    }
                }
            }
            visitedNodes.Enqueue(node, node.FScore);
        }

        Node[] GetUnvisitedNeighbors(Node node)
        {
            List<Node> neighbors = new();
            if (node.Position.i > 0)
            {
                var neighbor = nodes[node.Position.i - 1][node.Position.j];
                if (!neighbor.IsDiscovered)
                    neighbors.Add(neighbor);
            }

            if (node.Position.i < lines.Length - 1)
            {
                var neighbor = nodes[node.Position.i + 1][node.Position.j];
                if (!neighbor.IsDiscovered)
                    neighbors.Add(neighbor);
            }

            if (node.Position.j > 0)
            {
                var neighbor = nodes[node.Position.i][node.Position.j - 1];
                if (!neighbor.IsDiscovered)
                    neighbors.Add(neighbor);
            }

            if (node.Position.j < lines[0].Length - 1)
            {
                var neighbor = nodes[node.Position.i][node.Position.j + 1];
                if (!neighbor.IsDiscovered)
                    neighbors.Add(neighbor);
            }

            return neighbors.ToArray();
        }

        return 0;
    }

    public int RunTaskTwo(string[] lines)
    {
        return 0;
    }

    public class Node
    {
        public (int i, int j) Position { get; set; }
        public bool IsDiscovered { get; set; }
        public int HScore { get; set; }
        public int GScore { get; set; }
        public int FScore { get; set; }
        public Node Previous { get; set; }
        public char Val { get; set; }
    }
}