using AdventOfCode2023.Solutions;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;

namespace AdventOfCode2023Tests.Solutions;

public class Day09Tests
{
    [Fact]
    public void Example_01_Should_Be_114()
    {
        var expected = 114;
        var result = Day09.Solve(Day09Inputs.Example01);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Real_01_Should_Be_1762065988()
    {
        var expected = 1762065988;
        var result = Day09.Solve(Day09Inputs.Real);
        result.Should().Be(expected);
    }
}