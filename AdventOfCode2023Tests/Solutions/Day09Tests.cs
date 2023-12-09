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
        var result = Day09.Solve(Day09Inputs.Example01, true);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Example_02_Should_Be_2()
    {
        var expected = 2;
        var result = Day09.Solve(Day09Inputs.Example01, false);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Real_01_Should_Be_1762065988()
    {
        var expected = 1762065988;
        var result = Day09.Solve(Day09Inputs.Real, true);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Real_02_Should_Be_1066()
    {
        var expected = 1066;
        var result = Day09.Solve(Day09Inputs.Real, false);
        result.Should().Be(expected);
    }
}