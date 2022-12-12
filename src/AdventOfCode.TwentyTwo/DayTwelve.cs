using System.Data;
using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DayTwelve : IChallenge<int>
{
    public string ChallengeInputPath => DataProvider<DayTwelve>.GetChallengePath();
    public int RunTaskOne(string[] lines)
    {
        var start = (i: -1, j: -1);
        var end = (i: -1, j: -1);

        for (var i = 0; i < lines.Length; i++)
        {
            var startIndex = lines[i].IndexOf('S');
            var endIndex = lines[i].IndexOf('E');
            if (startIndex != -1)
            {
                start = (i, startIndex);
            }
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
                
                if ((i, j) == start)
                {
                    node.Val = 'a';
                    node.GScore = 0;
                    node.FScore = Math.Abs(end.i - i) + Math.Abs(end.j - j);
                    unvisitedNodes.Enqueue(node, node.FScore);
                }

                if (lines[i][j] == 'E')
                {
                    end = (i, j);
                    node.Val = 'z';
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
            if (node.Position == end)
            {
                return node.GScore;
            }
            var neighbors = GetUnvisitedNeighbors(node);
            foreach (var neighbor in neighbors)
            {
                if (neighbor.Val <= node.Val + 1)
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

    private delegate bool FindCanMove(int from, int to);
    private Node Dijkstras(Node[][] nodes, (int i, int j) startingPos, FindCanMove canMove, char targetVal)
    {
        Queue<Node> unvisitedNodes = new();
        unvisitedNodes.Enqueue(nodes[startingPos.i][startingPos.j]);

        while (unvisitedNodes.TryDequeue(out var node))
        {
            if (node.Val == targetVal) return node;
                node.IsDiscovered = true;
            var neighbors = GetUnvisitedNeighbors(nodes, node);
            foreach (var neighbor in neighbors)
            {
                if (canMove(node.Val, neighbor.Val))
                {
                    if (node.GScore + 1 < neighbor.GScore)
                    {
                        neighbor.GScore = node.GScore + 1;
                        neighbor.Previous = node;
                        unvisitedNodes.Enqueue(neighbor);
                    }
                }
            }
        }

        throw new Exception("Cannot find target node");
    }

    private class DNode
    {
        public bool IsDiscovered { get; set; }
        public int Val { get; set; }
        public int Score { get; set; }
        public (int, int) Position { get; set; }
        public DNode Previous { get; set; }
    }
    
    List<Node> GetUnvisitedNeighbors(Node[][] nodes, Node node)
    {
        List<Node> neighbors = new();
        if (node.Position.i > 0)
        {
            var neighbor = nodes[node.Position.i - 1][node.Position.j];
            if (!neighbor.IsDiscovered)
                neighbors.Add(neighbor);
        }

        if (node.Position.i < nodes.Length - 1)
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

        if (node.Position.j < nodes[0].Length - 1)
        {
            var neighbor = nodes[node.Position.i][node.Position.j + 1];
            if (!neighbor.IsDiscovered)
                neighbors.Add(neighbor);
        }

        return neighbors;
    }

    public int RunTaskTwo(string[] lines)
    {
        Queue<Node> unvisitedNodes = new();
        DNode[][] nodes = new DNode[lines.Length][]
            .Select((_, i) => new Node[lines[i].Length]
                .Select((_, j) =>
                {
                    var curVal = lines[i][j];
                    var dnode = new DNode
                    {
                        Position = (i, j),
                        Score = curVal == 'E' ? 0 : int.MaxValue,
                        Val = curVal == 'E' ? 'E' : 'z'
                    };

                    return dnode;
                }).ToArray()).ToArray();

        for (var i = 0; i < lines.Length; i++)
        {
            nodes[i] = new Node[lines[i].Length];
            for (var j = 0; j < lines[i].Length; j++)
            {
                Node node = new()
                {
                    FScore = int.MaxValue,
                    GScore = int.MaxValue,
                    Position = (i, j),
                    Val = lines[i][j]
                };

                if (lines[i][j] == 'E')
                {
                    node.Val = 'z';
                    node.GScore = 0;
                    unvisitedNodes.Enqueue(node);
                }

                nodes[i][j] = node;
            }
        }

        while (unvisitedNodes.TryDequeue(out var node))
        {
            node.IsDiscovered = true;
            if (node.Val == 'a')
            {
                return node.GScore;
            }
            var neighbors = GetUnvisitedNeighbors(node);
            foreach (var neighbor in neighbors)
            {
                if (neighbor.Val >= node.Val - 1)
                {
                    if (node.GScore + 1 < neighbor.GScore)
                    {
                        neighbor.GScore = node.GScore + 1;
                        neighbor.Previous = node;
                        unvisitedNodes.Enqueue(neighbor);
                    }
                }
            }
        }

        List<Node> GetUnvisitedNeighbors(Node node)
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

            return neighbors;
        }

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