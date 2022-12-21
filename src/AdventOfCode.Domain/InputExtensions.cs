using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Domain;

public static class InputExtensions
{
    public static string[] GetLines(this string input) => input.ReplaceLineEndings("\n").Split('\n');
}
