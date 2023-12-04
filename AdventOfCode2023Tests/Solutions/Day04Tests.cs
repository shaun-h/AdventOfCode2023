using AdventOfCode2023.Solutions;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;

namespace AdventOfCode2023Tests.Solutions;

public class Day04Tests
{
    [Fact]
    public void Example_01_Should_Be_13()
    {
        var expected = 13;
        var result = Day04.Solve(Day04Inputs.Example01);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Real_01_Should_Be_32001()
    {
        var expected = 32001;
        var result = Day04.Solve(Day04Inputs.Real);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Example_02_Should_Be_30()
    {
        var expected = 30;
        var result = Day04.SolveTotal(Day04Inputs.Example02);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Real_02_Should_Be_5037841()
    {
        var expected = 5037841;
        var result = Day04.SolveTotal(Day04Inputs.Real);
        result.Should().Be(expected);
    }
}