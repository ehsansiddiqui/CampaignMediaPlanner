// Services/GoalSeekService.cs
namespace CampaignMediaPlanner.Services
{
    using System;

    public class GoalSeekService
    {
        public double GoalSeek(Func<double, double> equation, double target, double initialGuess, double tolerance = 0.0001, int maxIterations = 1000)
        {
            double guess = initialGuess;
            double result = equation(guess);
            int iterations = 0;

            while (Math.Abs(result - target) > tolerance && iterations < maxIterations)
            {
                guess = guess - (result - target) / Derivative(equation, guess);
                result = equation(guess);
                iterations++;
            }

            if (iterations == maxIterations)
            {
                throw new Exception("Goal Seek did not converge.");
            }

            return Math.Round(guess, 2);
        }

        private double Derivative(Func<double, double> equation, double x, double h = 0.0001)
        {
            return (equation(x + h) - equation(x)) / h;
        }
    }
}
