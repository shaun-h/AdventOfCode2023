using AdventOfCode2023.Solutions;
using AdventOfCode2023Tests.Inputs;
using FluentAssertions;

namespace AdventOfCode2023Tests.Solutions;

public class Day06Tests
{
    [Fact]
    public void Example_01_Should_Be_288()
    {
        var expected = 288;
        var result = Day06.Solve(Day06Inputs.Example01);
        result.Should().Be(expected);
    }
    
    [Fact]
    public void Real_01_Should_Be_160816()
    {
        var expected = 160816;
        var result = Day06.Solve(Day06Inputs.Real);
        result.Should().Be(expected);
    }
}