//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System;
using System.Drawing;

namespace Sce.Atf.Drawing
{
    /// <summary>
    /// Represents the drawing factory. Creates drawing resources.</summary>
    public static class AtfDrawingFactory
    {
        //TODO

        /// <summary>
        /// Creates a text format object used for text layout with normal weight, style
        /// and stretch
        /// </summary>
        /// <param name="fontFamilyName">The name of the font family</param>
        /// <param name="fontSize">The logical size of the font in pixel units.</param>
        /// <returns>New instance of IAtfTextFormat</returns>
        public static IAtfTextFormat CreateTextFormat(string fontFamilyName, float fontSize)
        {
            //TODO
            return null;
        }

        //TODO

        /// <summary>
        /// Creates a TextLayout with the specified parameter</summary>
        /// <param name="text">The string to create a new IAtfTextLayout object from</param>
        /// <param name="textFormat">The text format to apply to the string</param>
        /// <param name="layoutWidth">Text layout width</param>
        /// <param name="layoutHeight">Text layout height</param>
        /// <returns>A new instance of IAtfTextLayout</returns>
        public static IAtfTextLayout CreateTextLayout(string text, IAtfTextFormat textFormat, float layoutWidth, float layoutHeight)
        {
            //TODO
            return null;
        }

        //TODO

        /// <summary>
        /// Creates a new IAtfSolidColorBrush that has the specified color.
        /// It is recommended to create AtfSolidBrushes and reuse them instead
        /// of creating and disposing them for each method call.
        /// </summary>
        /// <param name="color">The red, green, blue, and alpha values of the brush's color</param>
        /// <returns>A new IAtfSolidColorBrush</returns>
        public static IAtfSolidColorBrush CreateSolidBrush(Color color)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Creates a IAtfLinearGradientBrush with the specified points and colors
        /// </summary>        
        /// <param name="gradientStops">An array of AtfGradientStop structures that describe the colors in the brush's gradient 
        /// and their locations along the gradient line</param>
        /// <returns>A new instance of IAtfLinearGradientBrush</returns>
        public static IAtfLinearGradientBrush CreateLinearGradientBrush(params AtfGraphicsGradientStop[] gradientStops)
        {
            //TODO
            return null;
        }

        //TODO

        /// <summary>
        /// Creates a Atf Bitmap by copying the specified GDI Image</summary>
        /// <param name="img">A GDI bitmap from which to create new IAtfBitmap</param>
        /// <returns>A new IAtfBitmap</returns>
        public static IAtfBitmap CreateBitmap(System.Drawing.Image img)
        {
            //TODO
            return null;
        }

        //TODO
    }
}
