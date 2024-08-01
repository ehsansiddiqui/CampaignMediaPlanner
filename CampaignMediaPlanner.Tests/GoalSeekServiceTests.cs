// CampaignMediaPlanner.Tests/GoalSeekServiceTests.cs
using CampaignMediaPlanner.Services;
using System;
using Xunit;

public class GoalSeekServiceTests
{
    private readonly GoalSeekService _goalSeekService;

    public GoalSeekServiceTests()
    {
        _goalSeekService = new GoalSeekService();
    }

    [Theory]
    [InlineData(1000, 0.05, 0.02, 300, 500, 200, 100)]
    [InlineData(1500, 0.10, 0.03, 400, 600, 300, 150)]
    [InlineData(2000, 0.15, 0.05, 500, 700, 400, 200)]
    public void GoalSeek_ShouldFindCorrectAdBudget(double totalBudget, double agencyFeePercentage, double thirdPartyFeePercentage, double fixedCosts, double otherAdSpend, double initialGuess, double tolerance)
    {
        double Equation(double x)
        {
            double X = x + otherAdSpend;
            return X + totalBudget * X + agencyFeePercentage * X + thirdPartyFeePercentage * (x + otherAdSpend) + fixedCosts;
        }

        double result = _goalSeekService.GoalSeek(Equation, totalBudget, initialGuess, tolerance);

        Assert.True(Math.Abs(Equation(result) - totalBudget) <= tolerance);
    }
}
