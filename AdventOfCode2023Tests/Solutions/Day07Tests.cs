using AdventOfCode2023.Solutions;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;

namespace AdventOfCode2023Tests.Solutions;

public class Day07Tests
{
    [Fact]
    public void Example_01_Should_Be_6440()
    {
        var expected = 6440;
        var result = Day07.Solve(Day07Inputs.Example01);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Real_Should_Be_252656917()
    {
        var expected = 252656917;
        var result = Day07.Solve(Day07Inputs.Real);
        result.Should().Be(expected);
    }
}