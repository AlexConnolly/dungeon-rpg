using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Helpers
{
    public static class LDGMathHelpers
    {
        public static float LogFade(float low, float high, float percentage)
        {
            // Ensure percentage is between 0 and 1
            percentage = MathHelper.Clamp(percentage, 0f, 1f);

            // Adjusting the coefficients a and b to control the steepness of the curve
            float a = 1.0f;
            float b = 9.0f; // This value controls the steepness; increasing it makes the curve steeper

            // Calculate the logarithmic curve value
            float logValue = a * (float)Math.Log(b * percentage + 1);

            // Normalize the logarithmic value to the interval [0, 1]
            float normalizedLogValue = logValue / (a * (float)Math.Log(b + 1));

            // Map this value to the interval [low, high]
            return low + (high - low) * normalizedLogValue;
        }
    }
}
