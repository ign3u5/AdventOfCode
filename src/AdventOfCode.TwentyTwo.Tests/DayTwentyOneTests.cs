namespace AdventOfCode.TwentyTwo.Tests
{
    public class DayTwentyOneTests : BaseTests<DayTwentyOne, double>
    {
        private const string _input = """
            root: pppw + sjmn
            dbpl: 5
            cczh: sllz + lgvd
            zczc: 2
            ptdq: humn - dvpt
            dvpt: 3
            lfqf: 4
            humn: 5
            ljgn: 2
            sjmn: drzm * dbpl
            sllz: 4
            pppw: cczh / lfqf
            lgvd: ljgn * ptdq
            drzm: hmdt - zczc
            hmdt: 32
            """;

        [Theory]
        [InlineData(_input, 152)]
        public void RunTaskOne(string input, double expected) => TaskOne(input, expected);
        
        [Theory]
        [InlineData(_input, 301)]
        public void RunTaskTwo(string input, double expected) => TaskTwo(input, expected);
    }
}
