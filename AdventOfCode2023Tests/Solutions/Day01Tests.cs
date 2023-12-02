using AdventOfCode2023.Solutions;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;

namespace AdventOfCode2023Tests.Solutions;

public class Day01Tests
{
    [Fact]
    public void Example_01_Should_Be_142()
    {
        var expected = 142;
        var result = Day01.Solve(Day01Inputs.Example01);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Real_01_Should_Be_55002()
    {
        var expected = 55002;
        var result = Day01.Solve(Day01Inputs.Real);
        result.Should().Be(expected);
    }
}