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
        var result = Day07.Solve(Day07Inputs.Example01, false);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Example_02_Should_Be_5905()
    {
        var expected = 5905;
        var result = Day07.Solve(Day07Inputs.Example02, true);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Real_Should_Be_252656917()
    {
        var expected = 252656917;
        var result = Day07.Solve(Day07Inputs.Real, false);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Real02_Should_Be_253499763()
    {
        var expected = 253499763;
        var result = Day07.Solve(Day07Inputs.Real, true);
        result.Should().Be(expected);
    }
}