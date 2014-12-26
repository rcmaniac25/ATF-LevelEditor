//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System;

using SizeF = System.Drawing.SizeF;
using Color = System.Drawing.Color;

namespace Sce.Atf.Drawing
{
    /// <summary>
    /// Represents an object that can recieve drawing commands.</summary>
    /// <remarks>
    /// Use DrawingFactory to create an instance of IAtfGraphics, or use ATF's derived classes.
    /// New abstract methofs may be added to this interface in the future.
    /// 
    /// Your application should create a IAtfGraphics object once per control and hold
    /// onto it for the life of the control.</remarks>
    public interface IAtfGraphics : IAtfResource
    {
        //TODO

        /// <summary>
        /// Draws a line between the specified points</summary>
        /// <param name="pt1X">The starting x-coordinate of the line, in pixels</param>
        /// <param name="pt1Y">The starting y-coordinate of the line, in pixels</param>
        /// <param name="pt2X">The ending x-coordinate of the line, in pixels</param>
        /// <param name="pt2Y">The ending y-coordinate of the line, in pixels</param>
        /// <param name="color">The color used to paint the line's stroke</param>
        /// <param name="strokeWidth">A value greater than or equal to 0.0f that specifies the width of the stroke</param>
        /// <param name="strokeStyle">The style of stroke to paint, or null to paint a solid line</param>
        void DrawLine(float pt1X, float pt1Y, float pt2X, float pt2Y, Color color, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        //TODO

        /// <summary>
        /// Measures the specified string when drawn with the specified System.Drawing.Font</summary>
        /// <param name="text">String to measure</param>
        /// <param name="format">IAtfTextFormat that defines the font and format of the string</param>
        /// <returns>This method returns a System.Drawing.SizeF structure that represents the
        /// size, in DIP (Device Independent Pixels) units of the string specified by the text parameter as drawn 
        /// with the format parameter</returns>
        SizeF MeasureText(string text, IAtfTextFormat format);

        //TODO
    }
}
