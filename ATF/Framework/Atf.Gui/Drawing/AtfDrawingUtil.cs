//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System;
using System.Drawing;

using Matrix = System.Drawing.Drawing2D.Matrix;

namespace Sce.Atf.Drawing
{
    /// <summary>
    /// Class with drawing utility static methods
    /// </summary>
    public static class AtfDrawingUtil
    {
        //TODO

        /// <summary>
        /// Transforms vector</summary>
        /// <param name="matrix">Matrix representing transform</param>
        /// <param name="v">Vector</param>
        /// <returns>Transformed vector</returns>
        public static PointF TransformVector(Matrix matrix, PointF v)
        {
            //TODO
            return v;
        }

        //TODO

        /// <summary>
        /// Calculates tick anchor, i.e. the lowest value on an axis where a tick mark is placed</summary>
        /// <param name="min">Minumum</param>
        /// <param name="max">Maximum</param>
        /// <returns>Tick anchor</returns>
        public static double CalculateTickAnchor(double min, double max)
        {
            double tickAnchor = min * max <= 0 ? 0 : Math.Pow(10.0, Math.Floor(Math.Log10(Snap10(Math.Abs(max)))));

            return tickAnchor;
        }

        /// <summary>
        /// Calculates the step size in graph (world) space between major ticks</summary>
        /// <param name="graphMin">Minumum value on tick mark axis</param>
        /// <param name="graphMax">Maximum value on tick mark axis</param>
        /// <param name="screenLength">Graph rectangle width</param>
        /// <param name="majorScreenSpacing">Spacing, in pixels, between major tick marks</param>
        /// <param name="minimumGraphSpacing">Minimum spacing, in graph (world) space, between ticks.</param>
        /// <returns>Step size</returns>
        public static double CalculateStep(double graphMin, double graphMax, double screenLength,
            int majorScreenSpacing, double minimumGraphSpacing)
        {
            double graphRange = graphMax - graphMin;
            double screenSteps = screenLength / majorScreenSpacing;
            double requestedSteps = graphRange / screenSteps;
            return Snap10(requestedSteps);
        }

        /// <summary>
        /// Calculates the number of minor ticks per major tick. This could be described as the number
        /// of minor ticks in between major ticks, plus 1. The lowest result is 1.</summary>
        /// <param name="majorGraphStep">Step size in graph (world) space between major ticks</param>
        /// <param name="minimumGraphStep">Minimum spacing, in graph (world) space, between ticks.
        /// For example, 1.0 would limit ticks to being drawn on whole integers.</param>
        /// <param name="maxMinorTicks">Maximum value for minor ticks</param>
        /// <returns>Number of minor ticks per major tick</returns>
        public static int CalculateNumMinorTicks(double majorGraphStep, double minimumGraphStep, int maxMinorTicks)
        {
            if (minimumGraphStep <= 0)
                return maxMinorTicks;

            int num = (int)(majorGraphStep / minimumGraphStep); //round down
            if (num > maxMinorTicks)
                num = maxMinorTicks;

            if (num <= 0)
                num = 1;

            return num;
        }

        /// <summary>
        /// Snaps value to an "aesthetically pleasing" value</summary>
        /// <param name="proposed">Proposed value</param>
        /// <returns>Snapped value</returns>
        public static double Snap10(double proposed)
        {
            double lesserPowerOf10 = Math.Pow(10.0, Math.Floor(Math.Log10(proposed)));
            int msd = (int)(proposed / lesserPowerOf10 + .5);

            // snap msd to 2, 5 or 10
            if (msd > 5)
                msd = 10;
            else if (msd > 2)
                msd = 5;
            else if (msd > 1)
                msd = 2;

            return msd * lesserPowerOf10;
        }

        //TODO
    }
}
