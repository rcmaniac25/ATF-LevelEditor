//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System;
using System.Drawing;
using Sce.Atf.VectorMath;

namespace Sce.Atf.Drawing
{
    /// <summary>
    /// Represents the drawing factory. Creates drawing resources.</summary>
    public static class AtfDrawingFactory
    {
        /// <summary>
        /// Creates IAtfHwndGraphics for given handle</summary>
        /// <param name="hwnd">Window handle</param>
        /// <returns>IAtfHwndGraphics object for given handle</returns>
        /// <remarks>Important: This factory forces 1 DIP (device independent pixel) = 1 pixel
        /// regardless of current DPI settings.</remarks>                
        public static IAtfHwndGraphics CreateHwndGraphics(IntPtr hwnd)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Creates an instance of IAtfGraphics 
        /// that is used for creating shared resources</summary>
        /// <param name="hwnd">The handle of the control or Form
        /// that is used for creating IAtfGraphics</param>
        public static void EnableResourceSharing(IntPtr hwnd)
        {
            //TODO
        }

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

        /// <summary>
        /// Creates a text format with the specified parameter</summary>
        /// <param name="fontFamilyName">The name of the font family</param>
        /// <param name="fontWeight">A value that indicates the font weight</param>
        /// <param name="fontStyle">A value that indicates the font style</param>        
        /// <param name="fontSize">The logical size of the font in pixels</param>        
        /// <returns>A new instance of IAtfTextFormat</returns>
        public static IAtfTextFormat CreateTextFormat(
            string fontFamilyName,
            AtfDrawingFontWeight fontWeight,
            AtfDrawingFontStyle fontStyle,
            float fontSize)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Creates a text format with the specified parameter</summary>
        /// <param name="fontFamilyName">The name of the font family</param>
        /// <param name="fontWeight">A value that indicates the font weight</param>
        /// <param name="fontStyle">A value that indicates the font style</param>
        /// <param name="fontStretch">Font stretch compared to normal aspect ratio (not used)</param>
        /// <param name="fontSize">The logical size of the font in pixels</param>
        /// <param name="localeName">Locale name</param>
        /// <returns>A new instance of IAtfTextFormat</returns>
        public static IAtfTextFormat CreateTextFormat(
            string fontFamilyName,
            AtfDrawingFontWeight fontWeight,
            AtfDrawingFontStyle fontStyle,
            AtfDrawingFontStretch fontStretch,
            float fontSize,
            string localeName)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Creates a text format object from GDI font and StringFormat.
        /// Text format is used for text layout with normal weight, style
        /// and stretch.</summary>        
        /// <param name="font">System.Drawing.Font used for creating TexFormat.</param>        
        /// <returns>A new instance of IAtfTextFormat</returns>
        public static IAtfTextFormat CreateTextFormat(System.Drawing.Font font)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Creates a TextLayout with the specified parameter</summary>
        /// <param name="text">The string to create a new IAtfTextLayout object from</param>
        /// <param name="textFormat">The text format to apply to the string</param>
        /// <returns>A new instance of IAtfTextLayout</returns>
        public static IAtfTextLayout CreateTextLayout(string text, IAtfTextFormat textFormat)
        {
            //TODO
            return null;
        }

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

        /// <summary>
        /// Creates a TextLayout with the specified parameter</summary>
        /// <param name="text">The string to create a new IAtfTextLayout object from</param>
        /// <param name="textFormat">The text format to apply to the string</param>
        /// <param name="transform">2D Matrix used to transform the text</param>
        /// <returns>A new instance of IAtfTextLayout</returns>
        public static IAtfTextLayout CreateTextLayout(string text, IAtfTextFormat textFormat, Matrix3x2F transform)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Creates a TextLayout with the specified parameter</summary>
        /// <param name="text">The string to create a new IAtfTextLayout object from</param>
        /// <param name="textFormat">The text format to apply to the string</param>
        /// <param name="layoutWidth">Text layout width</param>
        /// <param name="layoutHeight">Text layout height</param>
        /// <param name="transform">2D Matrix used to transform the text</param>
        /// <returns>A new instance of IAtfTextLayout</returns>
        public static IAtfTextLayout CreateTextLayout(string text, IAtfTextFormat textFormat, float layoutWidth, float layoutHeight, Matrix3x2F transform)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Measures the specified string when drawn with the specified System.Drawing.Font</summary>
        /// <param name="text">String to measure</param>
        /// <param name="textFormat">IAtfTextFormat that defines the font and format of the string</param>
        /// <returns>This method returns a System.Drawing.SizeF structure that represents the
        /// size, in DIP (Device Independent Pixels) units of the string specified by the text parameter as drawn 
        /// with the format parameter</returns>
        public static SizeF MeasureText(string text, IAtfTextFormat textFormat)
        {
            //TODO
            return SizeF.Empty;
        }

        /// <summary>
        /// Measures the specified string when drawn with the specified System.Drawing.Font</summary>
        /// <param name="text">String to measure</param>
        /// <param name="textFormat">IAtfTextFormat that defines the font and format of the string</param>
        /// <param name="maxSize">The maximum x and y dimensions</param>
        /// <returns>This method returns a System.Drawing.SizeF structure that represents the
        /// size, in DIP (Device Independent Pixels) units of the string specified by the text parameter as drawn 
        /// with the format parameter</returns>
        public static SizeF MeasureText(string text, IAtfTextFormat textFormat, SizeF maxSize)
        {
            //TODO
            return SizeF.Empty;
        }

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
        public static IAtfLinearGradientBrush CreateLinearGradientBrush(params AtfDrawingGradientStop[] gradientStops)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Creates a IAtfLinearGradientBrush that contains the specified gradient stops,
        /// extend mode, and gamma interpolation. It has no transform and has a base opacity of 1.0.</summary>
        /// <param name="pt1">A System.Drawing.PointF structure that represents the starting point of the linear gradient</param>
        /// <param name="pt2">A System.Drawing.PointF structure that represents the endpoint of the linear gradient</param>
        /// <param name="gradientStops">An array of AtfDrawingGradientStop structures that describe the colors in the brush's gradient 
        /// and their locations along the gradient line</param>
        /// <param name="extendMode">The behavior of the gradient outside the [0,1] normalized range</param>
        /// <param name="gamma">The space in which color interpolation between the gradient stops is performed</param>
        /// <returns>A new instance of IAtfLinearGradientBrush</returns>
        public static IAtfLinearGradientBrush CreateLinearGradientBrush(
            PointF pt1,
            PointF pt2,
            AtfDrawingGradientStop[] gradientStops,
            AtfDrawingExtendMode extendMode,
            AtfDrawingGamma gamma)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Creates a IAtfRadialGradientBrush that contains the specified gradient stops</summary>
        /// <param name="gradientStops">A array of AtfDrawingGradientStop structures that describe the
        /// colors in the brush's gradient and their locations along the gradient</param>
        /// <returns>A new instance of IAtfRadialGradientBrush</returns>
        public static IAtfRadialGradientBrush CreateRadialGradientBrush(params AtfDrawingGradientStop[] gradientStops)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Creates a IAtfRadialGradientBrush that uses the specified arguments</summary>
        /// <param name="center">In the brush's coordinate space, the center of the gradient ellipse</param>
        /// <param name="gradientOriginOffset">In the brush's coordinate space, the offset of the gradient origin relative
        /// to the gradient ellipse's center</param>
        /// <param name="radiusX">In the brush's coordinate space, the x-radius of the gradient ellipse</param>
        /// <param name="radiusY">In the brush's coordinate space, the y-radius of the gradient ellipse</param>
        /// <param name="gradientStops">An array of AtfDrawingGradientStop structures that describe the
        /// colors in the brush's gradient and their locations along the gradient</param>
        /// <returns>A new instance of IAtfRadialGradientBrush</returns>
        public static IAtfRadialGradientBrush AtfDrawingGradientStop(
            PointF center,
            PointF gradientOriginOffset,
            float radiusX,
            float radiusY,
            params AtfDrawingGradientStop[] gradientStops)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Creates a new IAtfBitmapBrush from the specified bitmap</summary>
        /// <param name="bitmap">The bitmap contents of the new brush</param>
        /// <returns>A new instance of IAtfBitmapBrush</returns>
        public static IAtfBitmapBrush CreateBitmapBrush(IAtfBitmap bitmap)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Creates a new instance of the IAtfBitmap class from a specified resource</summary>
        /// <param name="type">The type used to extract the resource</param>
        /// <param name="resource">The name of the resource</param>
        /// <returns>A new IAtfBitmap</returns>
        public static IAtfBitmap CreateBitmap(Type type, string resource)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Creates a new instance of the IAtfBitmap from the specified stream</summary>
        /// <param name="stream">The data stream used to load the image</param>
        /// <returns>A new IAtfBitmap</returns>
        /// <remarks>It is safe to close the stream after the function call.</remarks>
        public static IAtfBitmap CreateBitmap(System.IO.Stream stream)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Creates a new instance of the IAtfBitmap from the specified file</summary>
        /// <param name="filename">The name of the bitmap file</param>
        /// <returns>A new IAtfBitmap</returns>
        public static IAtfBitmap CreateBitmap(string filename)
        {
            //TODO
            return null;
        }


        /// <summary>
        /// Creates a Bitmap by copying the specified GDI Image</summary>
        /// <param name="img">A GDI bitmap from which to create new IAtfBitmap</param>
        /// <returns>A new IAtfBitmap</returns>
        public static IAtfBitmap CreateBitmap(System.Drawing.Image img)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Creates a Bitmap by copying the specified GDI bitmap</summary>
        /// <param name="bmp">A GDI bitmap from which to create new IAtfBitmap</param>
        /// <returns>A new IAtfBitmap</returns>
        public static IAtfBitmap CreateBitmap(System.Drawing.Bitmap bmp)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Creates an empty Bitmap from specified width and height
        /// Pixel format is set to 32 bit ARGB with premultiplied alpha</summary>      
        /// <param name="width">Width of the bitmap in pixels</param>
        /// <param name="height">Height of the bitmap in pixels</param>
        /// <returns>A new IAtfBitmap</returns>
        public static IAtfBitmap CreateBitmap(int width, int height)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Retrieves the current desktop dots per inch (DPI). To refresh this value,
        /// Call ReloadSystemMetrics.</summary>
        /// <remarks>
        /// Use this method to obtain the system DPI when setting physical pixel values,
        /// such as when you specify the size of a window.</remarks>
        public static SizeF DesktopDpi
        {
            get
            {
                //TODO
                return SizeF.Empty;
            }
        }

        /// <summary>
        /// Converts value in points to pixels</summary>
        /// <param name="point">Points value</param>
        /// <returns>Value in pixels</returns>
        public static float FontSizeToPixel(float point)
        {
            //TODO
            return 0;
        }

        /// <summary>
        /// Forces the factory to refresh any system defaults that it might have changed
        /// since factory creation</summary>
        /// <remarks>
        /// You should call this method before calling the {{GetDesktopDpi}} method
        /// to ensure that the system DPI is current.</remarks>
        public static void ReloadSystemMetrics()
        {
            //TODO
        }

        /// <summary>
        /// Gets the render target number for the underlining DIAtfGraphics.</summary>
        public static uint RenderTargetNumber
        {
            get
            {
                //TODO
                return 0;
            }
        }
    }

    /// <summary>
    /// Result of drawing operation</summary>
    public struct AtfDrawingResult : IEquatable<AtfDrawingResult>
    {
        /// <summary>Operation succeeded</summary>
        public readonly static AtfDrawingResult Ok;
        /// <summary>Operation was aborted</summary>
        public readonly static AtfDrawingResult Abord;
        /// <summary>Operation was denied access</summary>
        public readonly static AtfDrawingResult AccessDenied;
        /// <summary>Operation failed</summary>
        public readonly static AtfDrawingResult Fail;
        /// <summary>Operation result is handle</summary>
        public readonly static AtfDrawingResult Handle;
        /// <summary>Operation had invalid argument</summary>
        public static AtfDrawingResult InvalidArg;
        /// <summary>Operation has no interface</summary>
        public static AtfDrawingResult NoInterface;
        /// <summary>Operation is not implemented</summary>
        public static AtfDrawingResult NotImplemented;
        /// <summary>Operation ran out of memory</summary>
        public static AtfDrawingResult OutOfMemory;
        /// <summary>Operation had invalid pointer</summary>
        public static AtfDrawingResult InvalidPointer;
        /// <summary>Operation had an unexpected failure</summary>
        public static AtfDrawingResult UnexpectedFailure;

        internal AtfDrawingResult(int code)
        {
            m_code = code;
        }

        internal AtfDrawingResult(uint code)
        {
            m_code = (int)code;
        }

        /// <summary>
        /// Gets results code</summary>
        public int Code
        {
            get { return m_code; }
        }

        /// <summary>
        /// Gets whether factory succeeded in creating object</summary>
        public bool IsSuccess
        {
            get { return (this.Code >= 0); }
        }

        /// <summary>
        /// Gets whether factory failed in creating object</summary>
        public bool IsFailure
        {
            get { return (this.Code < 0); }
        }

        /// <summary>
        /// Gets whether last result code is same as input</summary>
        /// <param name="other">Other result code</param>
        /// <returns>True iff result codes are identical</returns>
        public bool Equals(AtfDrawingResult other)
        {
            return (this.Code == other.Code);
        }

        /// <summary>
        /// Gets whether last result code is same as input object</summary>
        /// <param name="obj">Other result code object</param>
        /// <returns>True iff result codes are identical</returns>
        public override bool Equals(object obj)
        {
            return ((obj is AtfDrawingResult) && this.Equals((AtfDrawingResult)obj));
        }

        /// <summary>
        /// Gets this objects hash code</summary>
        /// <returns>Hash code for object</returns>
        public override int GetHashCode()
        {
            return this.Code;
        }

        /// <summary>
        /// Result code equality operator</summary>
        /// <param name="left">Result code 1</param>
        /// <param name="right">Result code 2</param>
        /// <returns>True iff result codes are identical</returns>
        public static bool operator ==(AtfDrawingResult left, AtfDrawingResult right)
        {
            return (left.Code == right.Code);
        }

        /// <summary>
        /// Result code inequality operator</summary>
        /// <param name="left">Result code 1</param>
        /// <param name="right">Result code 2</param>
        /// <returns>True iff result codes are not identical</returns>
        public static bool operator !=(AtfDrawingResult left, AtfDrawingResult right)
        {
            return (left.Code != right.Code);
        }

        /// <summary>
        /// Provides string representation of result code</summary>
        /// <returns>String representation of result code</returns>
        public override string ToString()
        {
            // With SharpDX 2.3.1, I can't find this method anywhere. The docs say that everything
            //  in the SharpDX.Diagnostics dll got moved to SharpDX.dll, but it's not there. --Ron
            //return SharpDX.Diagnostics.ErrorManager.GetErrorMessage(Code);

            //return string.Format("result = 0x{0:X}", m_code);

            switch (m_code)
            {
                case 0: return "OK";
                case -2147467260: return "Abord? Is that supposed to be Abort?";
                case -2147024891: return "Access denied";
                case -2147467259: return "Fail";
                case -2147024890: return "Handle";
                case -2147024809: return "Invalid argument";
                case -2147467262: return "No interface";
                case -2147467263: return "Not implemented";
                case -2147024882: return "Out of memory";
                case -2147467261: return "Invalid pointer";
                case -2147418113: return "Unexpected failure";
            }

            return "Unknown error code".Localize();
        }

        static AtfDrawingResult()
        {
            Ok = new AtfDrawingResult(0);
            Abord = new AtfDrawingResult(-2147467260);
            AccessDenied = new AtfDrawingResult(-2147024891);
            Fail = new AtfDrawingResult(-2147467259);
            Handle = new AtfDrawingResult(-2147024890);
            InvalidArg = new AtfDrawingResult(-2147024809);
            NoInterface = new AtfDrawingResult(-2147467262);
            NotImplemented = new AtfDrawingResult(-2147467263);
            OutOfMemory = new AtfDrawingResult(-2147024882);
            InvalidPointer = new AtfDrawingResult(-2147467261);
            UnexpectedFailure = new AtfDrawingResult(-2147418113);
        }

        private readonly int m_code;
    }
}
