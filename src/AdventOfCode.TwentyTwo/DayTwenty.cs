using AdventOfCode.Domain;

namespace AdventOfCode.TwentyTwo;

public class DayTwenty : IChallenge<int>
{
    public string ChallengeInputPath => DataProvider<DayTwenty>.GetChallengePath();

    public int RunTaskOne(string[] lines)
    {
        lines = """
            1
            2
            -3
            3
            -2
            0
            4
            """.ReplaceLineEndings("\n").Split("\n");

        Node[] nodes = Node.GetNodesList(ParseInput(lines));

        foreach(var node in nodes)
        {
            node.Move();
        }

        var startNode = nodes[0];
        var currNode = startNode;
        do
        {
            Console.Write($"{currNode.Value}, ");
            currNode = currNode.Next;
        }
        while (currNode != startNode);

        return 0;
    }

    private static int[] ParseInput(string[] lines) => lines.Select(int.Parse).ToArray();

    public int RunTaskTwo(string[] lines)
    {
        return 0;
    }

    private class Node
    {
        public int Value { get; set; }
        public Node Next { get; set; }
        public Node Previous { get; set; }
        public int CurrentPos { get; set; }

        public Node() { }

        public static Node[] GetNodesList(int[] values)
        {
            var nodes = new Node[values.Length];
            var firstNode = new Node
            {
                Value = values[0],
                CurrentPos = 0
            };

            nodes[0] = firstNode;

            var prev = firstNode;
            for (var i = 1; i < values.Length - 1; i++)
            {
                var curr = new Node
                {
                    Value = values[i],
                    CurrentPos = i,
                    Previous = prev
                };
                nodes[i] = curr;
                prev.Next = curr;
                prev = curr;
            }

            var lastNode = new Node
            {
                Value = values[values.Length - 1],
                CurrentPos = values.Length - 1,
                Previous = prev,
                Next = firstNode
            };
            nodes[nodes.Length - 1] = lastNode;
            prev.Next = lastNode;

            firstNode.Previous = lastNode;

            return nodes;
        }

        public void Move()
        {
            if (Value == 0) return;

            var isPos = Value > 0;

            Previous.Next = Next;
            Next.Previous = Previous;

            var prev = Previous;
            for (var i = 0; i < Value; i++)
            {
                prev = isPos ? prev.Next : prev.Previous;
            }

            Previous = prev;
            Next = prev.Next;
            Previous.Next = this;
            Next.Previous = this;
        }
    }
}
