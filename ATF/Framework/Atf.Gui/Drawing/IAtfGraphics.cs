//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System;
using System.Collections.Generic;

using Sce.Atf.Adaptation;
using Sce.Atf.VectorMath;
using Sce.Atf.Controls.Adaptable.Graphs;

using Graphics = System.Drawing.Graphics;
using Size = System.Drawing.Size;
using SizeF = System.Drawing.SizeF;
using PointF = System.Drawing.PointF;
using RectangleF = System.Drawing.RectangleF;
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
        /// <summary>
        /// Gets and sets the current transform of the IAtfGraphics
        /// </summary>
        Matrix3x2F Transform { get; set; }

        /// <summary>
        /// Changes the origin of the coordinate system by prepending the specified translation
        /// to the Transform property</summary>
        /// <param name="dx">X translation</param>
        /// <param name="dy">Y translation</param>
        /// <remarks>Modeled after GDI version http://msdn.microsoft.com/en-us/library/6a1d65f4.aspx </remarks>
        void TranslateTransform(float dx, float dy);

        /// <summary>
        /// Changes the scale of the coordinate system by prepending the specified scale
        /// to the Transform property</summary>
        /// <param name="sx">Amount to scale by in the x-axis direction</param>
        /// <param name="sy">Amount to scale by in the y-axis direction</param>
        void ScaleTransform(float sx, float sy);

        /// <summary>
        /// Changes the rotation of the coordinate system by prepending the specified rotation
        /// to the Transform property</summary>
        /// <param name="angle">Angle of rotation, in degrees</param>
        void RotateTransform(float angle);

        /// <summary>
        /// Gets and sets the current antialiasing mode for nontext drawing operations</summary>
        AtfDrawingAntialiasMode AntialiasMode { get; set; }

        /// <summary>
        /// Gets the pixel size of the IAtfGraphics in device pixels</summary>
        Size PixelSize { get; }

        /// <summary>
        /// Gets the size of the IAtfGraphics in device-independent pixels (DIPs)</summary>
        SizeF Size { get; }

        /// <summary>
        /// Gets the current antialiasing mode for text and glyph drawing operations</summary>
        AtfDrawingTextAntialiasMode TextAntialiasMode { get; set; }

        /// <summary>
        ///  Get or sets the dots per inch (DPI) of the IAtfGraphics</summary>
        /// <remarks>
        ///  This method specifies the mapping from pixel space to device-independent
        ///  space for the IAtfGraphics. If both dpiX and dpiY are 0, the factory-read
        ///  system DPI is chosen. If one parameter is zero and the other unspecified,
        ///  the DPI is not changed. For IAtfHwndGraphics, the DPI
        ///  defaults to the most recently factory-read system DPI. The default value
        ///  for all other IAtfGraphics is 96 DPI.</remarks>
        SizeF DotsPerInch { get; set; }

        /// <summary>
        /// Clears the drawing area to the specified color</summary>
        /// <param name="color">The color to which the drawing area is cleared</param>
        void Clear(Color color);

        /// <summary>
        /// Initiates drawing on this IAtfGraphics</summary>
        void BeginDraw();

        /// <summary>
        /// Ends drawing operations on the IAtfGraphics and indicates the current error state</summary>
        /// <returns>
        /// If the method succeeds, it returns AtfDrawingResult.OK. Otherwise, it throws an exception.
        /// Also it may raise RecreateResources event.</returns>
        AtfDrawingResult EndDraw();

        /// <summary>
        /// Draws an arc representing a portion of an ellipse specified by a AtfDrawingEllipse</summary>
        /// <param name="ellipse">Ellipse to draw</param>
        /// <param name="brush">The brush used to paint the arc's outline</param>
        /// <param name="startAngle">Starting angle in degrees measured clockwise from the x-axis 
        /// to the starting point of the arc</param>
        /// <param name="sweepAngle">Sweep angle in degrees measured clockwise from the startAngle 
        /// parameter to ending point of the arc</param>
        /// <param name="strokeWidth">The thickness of the ellipse's stroke. The stroke is centered 
        /// on the ellipse's outline.</param>
        /// <param name="strokeStyle">The style of stroke to apply to the arc's outline or null to draw a solid line</param>
        void DrawArc(AtfDrawingEllipse ellipse, IAtfBrush brush, float startAngle, float sweepAngle, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws an arc representing a portion of an ellipse specified by a AtfDrawingEllipse</summary>
        /// <param name="ellipse">Ellipse to draw</param>
        /// <param name="color">The color used to paint the arc's outline</param>
        /// <param name="startAngle">Starting angle in degrees measured clockwise from the x-axis 
        /// to the starting point of the arc</param>
        /// <param name="sweepAngle">Sweep angle in degrees measured clockwise from the startAngle 
        /// parameter to ending point of the arc</param>
        /// <param name="strokeWidth">The thickness of the ellipse's stroke. The stroke is centered 
        /// on the ellipse's outline.</param>
        /// <param name="strokeStyle">The style of stroke to apply to the arc's outline or null to draw a solid line</param>
        void DrawArc(AtfDrawingEllipse ellipse, Color color, float startAngle, float sweepAngle, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws a Bézier spline defined by four System.Drawing.PointF structures</summary>
        /// <param name="pt1">Represents the starting point of the curve</param>
        /// <param name="pt2">Represents the first control point for the curve</param>
        /// <param name="pt3">Represents the second control point for the curve</param>
        /// <param name="pt4">Represents the ending point of the curve</param>
        /// <param name="brush">The brush used to paint the curve's stroke</param>
        /// <param name="strokeWidth">The thickness of the geometry's stroke. The stroke is centered on the geometry's outline.</param>
        /// <param name="strokeStyle">The style of stroke to apply to the geometry's outline or null to draw a solid line</param>
        void DrawBezier(PointF pt1, PointF pt2, PointF pt3, PointF pt4, IAtfBrush brush, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws a Bézier spline defined by four System.Drawing.PointF structures</summary>
        /// <param name="pt1">Represents the starting point of the curve</param>
        /// <param name="pt2">Represents the first control point for the curve</param>
        /// <param name="pt3">Represents the second control point for the curve</param>
        /// <param name="pt4">Represents the ending point of the curve</param>
        /// <param name="color">The color used to paint the curve's stroke</param>
        /// <param name="strokeWidth">The thickness of the geometry's stroke. The stroke is centered on the geometry's outline.</param>
        /// <param name="strokeStyle">The style of stroke to apply to the geometry's outline or null to draw a solid line</param>
        void DrawBezier(PointF pt1, PointF pt2, PointF pt3, PointF pt4, Color color, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws the specified bitmap at a specified point using the
        /// IAtfGraphics's DPI instead of the bitmap's DPI</summary>
        /// <param name="bmp">The bitmap to render</param>
        /// <param name="point">Point structure that represents the location of the upper-left
        /// corner of the drawn bitmap</param>
        /// <param name="opacity">A value between 0.0f and 1.0f, inclusive, that specifies an opacity value
        /// to apply to the bitmap. This value is multiplied against the alpha values
        /// of the bitmap's contents.</param>
        void DrawBitmap(IAtfBitmap bmp, PointF point, float opacity = 1.0f);

        /// <summary>
        /// Draws the specified bitmap after scaling it to the size of the specifiedrectangle</summary>
        /// <param name="bmp">The bitmap to render</param>
        /// <param name="destRect">The size and position, in pixels, in the IAtfGraphics's coordinate
        /// space, of the area in which the bitmap is drawn. If the rectangle is not well-ordered,
        /// nothing is drawn, but the IAtfGraphics does not enter an error state.</param>
        /// <param name="opacity">A value between 0.0f and 1.0f, inclusive, that specifies an opacity value        
        /// to apply to the bitmap. This value is multiplied against the alpha values
        /// of the bitmap's contents.</param>
        /// <param name="interpolationMode">The interpolation mode to use if the bitmap is scaled or rotated 
        /// by the drawing operation</param>
        void DrawBitmap(IAtfBitmap bmp, RectangleF destRect, float opacity = 1.0f, AtfDrawingBitmapInterpolationMode interpolationMode = AtfDrawingBitmapInterpolationMode.Linear);

        /// <summary>
        /// Draws the specified bitmap after scaling it to the size of the specified rectangle</summary>
        /// <param name="bmp">The bitmap to render</param>
        /// <param name="destRect">The size and position, in pixels, in the IAtfGraphics's coordinate
        /// space, of the area in which the bitmap is drawn. If the rectangle is not well-ordered,
        /// nothing is drawn, but the IAtfGraphics does not enter an error state.</param>
        /// <param name="opacity">A value between 0.0f and 1.0f, inclusive, that specifies an opacity value        
        /// to apply to the bitmap. This value is multiplied against the alpha values
        /// of the bitmap's contents.</param>
        /// <param name="interpolationMode">The interpolation mode to use if the bitmap is scaled or rotated 
        /// by the drawing operation</param>
        /// <param name="sourceRect">The size and position, in pixels in the bitmap's coordinate space, of the area
        /// within the bitmap to draw</param>
        void DrawBitmap(IAtfBitmap bmp, RectangleF destRect, float opacity,
            AtfDrawingBitmapInterpolationMode interpolationMode, RectangleF sourceRect);

        /// <summary>
        /// Draws the outline of the specified ellipse</summary>
        /// <param name="rect">The rectangle, in pixels, that encloses the ellipse to paint</param>
        /// <param name="brush">The brush used to paint the ellipse's outline</param>
        /// <param name="strokeWidth">The thickness of the ellipse's stroke. The stroke is centered on the ellipse's outline.</param>
        /// <param name="strokeStyle">The style of stroke to apply to the ellipse's outline or null to draw a solid line</param>
        void DrawEllipse(RectangleF rect, IAtfBrush brush, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws the outline of the specified ellipse</summary>
        /// <param name="rect">The rectangle, in pixels, that encloses the ellipse to paint</param>
        /// <param name="color">The color used to paint the ellipse's outline</param>
        /// <param name="strokeWidth">The thickness of the ellipse's stroke. The stroke is centered on the ellipse's outline.</param>
        /// <param name="strokeStyle">The style of stroke to apply to the ellipse's outline or null to draw a solid line</param>
        void DrawEllipse(RectangleF rect, Color color, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws the outline of the specified ellipse using the specified stroke style</summary>
        /// <param name="ellipse">Position and radius of the ellipse to draw in pixels</param>
        /// <param name="brush">The brush used to paint the ellipse's outline</param>
        /// <param name="strokeWidth">The thickness of the ellipse's stroke. The stroke is centered on the ellipse's outline.</param>
        /// <param name="strokeStyle">The style of stroke to apply to the ellipse's outline or null to draw a solid line</param>
        void DrawEllipse(AtfDrawingEllipse ellipse, IAtfBrush brush, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws the outline of the specified ellipse</summary>
        /// <param name="ellipse">Position and radius of the ellipse to draw in pixels</param>
        /// <param name="color">The color used to paint the ellipse's outline</param>
        /// <param name="strokeWidth">The thickness of the ellipse's stroke. The stroke is centered on the ellipse's outline.</param>
        /// <param name="strokeStyle">The style of stroke to apply to the ellipse's outline or null to draw a solid line</param>
        void DrawEllipse(AtfDrawingEllipse ellipse, Color color, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws a line between the specified points</summary>
        /// <param name="pt1X">The starting x-coordinate of the line, in pixels</param>
        /// <param name="pt1Y">The starting y-coordinate of the line, in pixels</param>
        /// <param name="pt2X">The ending x-coordinate of the line, in pixels</param>
        /// <param name="pt2Y">The ending y-coordinate of the line, in pixels</param>
        /// <param name="brush">The brush used to paint the line's stroke</param>
        /// <param name="strokeWidth">A value greater than or equal to 0.0f that specifies the width of the stroke</param>
        /// <param name="strokeStyle">The style of stroke to paint, or null to paint a solid line</param>
        void DrawLine(float pt1X, float pt1Y, float pt2X, float pt2Y, IAtfBrush brush, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

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

        /// <summary>
        /// Draws a line between the specified points</summary>
        /// <param name="pt1">The start point of the line, in pixels</param>
        /// <param name="pt2">The end point of the line, in pixels</param>
        /// <param name="brush">The brush used to paint the line's stroke</param>
        /// <param name="strokeWidth">A value greater than or equal to 0.0f that specifies the width of the stroke</param>
        /// <param name="strokeStyle">The style of stroke to paint, or NULL to paint a solid line</param>
        void DrawLine(PointF pt1, PointF pt2, IAtfBrush brush, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws a line between the specified points</summary>
        /// <param name="pt1">The start point of the line, in pixels</param>
        /// <param name="pt2">The end point of the line, in pixels</param>
        /// <param name="color">The color used to paint the line's stroke</param>
        /// <param name="strokeWidth">A value greater than or equal to 0.0f that specifies the width of the stroke</param>
        /// <param name="strokeStyle">The style of stroke to paint, or NULL to paint a solid line</param>
        void DrawLine(PointF pt1, PointF pt2, Color color, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws a series of line segments that connect an array of System.Drawing.PointF</summary>
        /// <param name="points">Array of PointF that represent the points to connect</param>
        /// <param name="brush">The brush used to paint the line's stroke</param>
        /// <param name="strokeWidth">A value greater than or equal to 0.0f that specifies the width of the stroke</param>
        /// <param name="strokeStyle">The style of stroke to paint, or NULL to paint a solid line</param>
        void DrawLines(IEnumerable<PointF> points, IAtfBrush brush, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws a series of line segments that connect an array of System.Drawing.PointF</summary>
        /// <param name="points">Array of PointF that represent the points to connect</param>
        /// <param name="color">The color used to paint the line's stroke</param>
        /// <param name="strokeWidth">A value greater than or equal to 0.0f that specifies the width of the stroke</param>
        /// <param name="strokeStyle">The style of stroke to paint, or NULL to paint a solid line</param>
        void DrawLines(IEnumerable<PointF> points, Color color, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws a polygon defined by an array of PointF structures</summary>
        /// <param name="points">Array of System.Drawing.PointF structures 
        /// that represent the vertices of the polygon</param>
        /// <param name="brush">The brush used to paint the polygon's stroke</param>
        /// <param name="strokeWidth">A value greater than or equal to 0.0f that specifies the width of the stroke</param>
        /// <param name="strokeStyle">The style of stroke to paint, or NULL to paint a solid line</param>
        void DrawPolygon(IEnumerable<PointF> points, IAtfBrush brush, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws a polygon defined by an array of PointF structures</summary>
        /// <param name="points">Array of System.Drawing.PointF structures 
        /// that represent the vertices of the polygon</param>
        /// <param name="color">The color used to paint the line's stroke</param>
        /// <param name="strokeWidth">A value greater than or equal to 0.0f that specifies the width of the stroke</param>
        /// <param name="strokeStyle">The style of stroke to paint, or NULL to paint a solid line</param>
        void DrawPolygon(IEnumerable<PointF> points, Color color, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws the outline of a rectangle that has the specified dimensions and stroke style</summary>
        /// <param name="rect">The dimensions of the rectangle to draw, in pixels</param>
        /// <param name="brush">The brush used to paint the rectangle's stroke</param>
        /// <param name="strokeWidth">A value greater than or equal to 0.0f that specifies the width 
        /// of the rectangle's stroke. The stroke is centered on the rectangle's outline.</param>
        /// <param name="strokeStyle">The style of stroke to paint or null to draw a solid line</param>
        void DrawRectangle(RectangleF rect, IAtfBrush brush, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws the outline of a rectangle that has the specified dimensions and stroke style</summary>
        /// <param name="rect">The dimensions of the rectangle to draw, in pixels</param>
        /// <param name="color">The color used to paint the rectangle's stroke</param>
        /// <param name="strokeWidth">A value greater than or equal to 0.0f that specifies the width 
        /// of the rectangle's stroke. The stroke is centered on the rectangle's outline.</param>
        /// <param name="strokeStyle">The style of stroke to paint or null to draw a solid line</param>
        void DrawRectangle(RectangleF rect, Color color, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        ///  Draws the outline of the specified rounded rectangle</summary>
        /// <param name="roundedRect">The dimensions of the rounded rectangle to draw, in pixels</param>
        /// <param name="brush">The brush used to paint the rounded rectangle's outline</param>
        /// <param name="strokeWidth">The width of the rounded rectangle's stroke. The stroke is centered on the
        /// rounded rectangle's outline.</param>
        /// <param name="strokeStyle">The style of the rounded rectangle's stroke, or null to paint a solid stroke</param>
        void DrawRoundedRectangle(AtfDrawingRoundedRect roundedRect, IAtfBrush brush, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        ///  Draws the outline of the specified rounded rectangle</summary>
        /// <param name="roundedRect">The dimensions of the rounded rectangle to draw, in pixels</param>
        /// <param name="color">The color used to paint the rounded rectangle's stroke</param>
        /// <param name="strokeWidth">The width of the rounded rectangle's stroke. The stroke is centered on the
        /// rounded rectangle's outline.</param>
        /// <param name="strokeStyle">The style of the rounded rectangle's stroke, or null to paint a solid stroke</param>
        void DrawRoundedRectangle(AtfDrawingRoundedRect roundedRect, Color color, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws the specified text using the format information provided at the specified location</summary>
        /// <param name="text">The text string to draw</param>
        /// <param name="textFormat">The text format object to use</param>
        /// <param name="upperLeft">Upper left corner of the text</param>
        /// <param name="brush">The brush to use to draw the text</param>
        void DrawText(string text, IAtfTextFormat textFormat, PointF upperLeft, IAtfBrush brush);

        /// <summary>
        /// Draws the specified text using the format information provided at the specified location</summary>
        /// <param name="text">The text string to draw</param>
        /// <param name="textFormat">The text format object to use</param>
        /// <param name="upperLeft">Upper left corner of the text</param>
        /// <param name="color">The color to use to draw the text</param>
        void DrawText(string text, IAtfTextFormat textFormat, PointF upperLeft, Color color);

        /// <summary>
        /// Draws the specified text using the format information provided by an SharpDX.DirectWrite.TextFormat object</summary>
        /// <param name="text">A string to draw</param>
        /// <param name="textFormat">An object that describes formatting details of the text to draw, such as
        /// the font, the font size, and flow direction</param>
        /// <param name="layoutRect">The size and position of the area in which the text is drawn</param>
        /// <param name="brush">The brush used to paint the text</param>
        void DrawText(string text, IAtfTextFormat textFormat, RectangleF layoutRect, IAtfBrush brush);

        /// <summary>
        /// Draws the specified text using the format information provided by an SharpDX.DirectWrite.TextFormat object</summary>
        /// <param name="text">A string to draw</param>
        /// <param name="textFormat">An object that describes formatting details of the text to draw, such as
        /// the font, the font size, and flow direction</param>
        /// <param name="layoutRect">The size and position of the area in which the text is drawn</param>
        /// <param name="color">The color used to paint the text</param>
        void DrawText(string text, IAtfTextFormat textFormat, RectangleF layoutRect, Color color);

        /// <summary>
        /// Draws the formatted text described by the specified IAtfTextLayout</summary>
        /// <param name="origin">The point, described in pixels, at which the upper-left
        /// corner of the text described by textLayout is drawn</param>
        /// <param name="textLayout">The formatted text to draw</param>
        /// <param name="brush">The brush used to paint any text in textLayout 
        /// that does not already have a brush associated with it as a drawing effect</param>
        void DrawTextLayout(PointF origin, IAtfTextLayout textLayout, IAtfBrush brush);

        /// <summary>
        /// Draws the formatted text described by the specified IAtfTextLayout</summary>
        /// <param name="origin">The point, described in pixels, at which the upper-left
        /// corner of the text described by textLayout is drawn</param>
        /// <param name="textLayout">The formatted text to draw</param>
        /// <param name="color">The color used to paint any text in textLayout 
        /// that does not already have a brush associated with it as a drawing effect</param>
        void DrawTextLayout(PointF origin, IAtfTextLayout textLayout, Color color);

        /// <summary>
        /// Measures the specified string when drawn with the specified System.Drawing.Font</summary>
        /// <param name="text">String to measure</param>
        /// <param name="format">IAtfTextFormat that defines the font and format of the string</param>
        /// <returns>This method returns a System.Drawing.SizeF structure that represents the
        /// size, in DIP (Device Independent Pixels) units of the string specified by the text parameter as drawn 
        /// with the format parameter</returns>
        SizeF MeasureText(string text, IAtfTextFormat format);

        /// <summary>
        /// Measures the specified string when drawn with the specified System.Drawing.Font</summary>
        /// <param name="text">String to measure</param>
        /// <param name="format">IAtfTextFormat that defines the font and format of the string</param>
        /// <param name="maxSize">The maximum x and y dimensions</param>
        /// <returns>This method returns a System.Drawing.SizeF structure that represents the
        /// size, in DIP (Device Independent Pixels) units of the string specified by the text parameter as drawn 
        /// with the format parameter</returns>
        SizeF MeasureText(string text, IAtfTextFormat format, SizeF maxSize);

        /// <summary>
        /// Paints the interior of the specified ellipse</summary>
        /// <param name="rect">The rectangle, in pixels, that encloses the ellipse to paint</param>
        /// <param name="brush">The brush used to paint the interior of the ellipse</param>
        void FillEllipse(RectangleF rect, IAtfBrush brush);

        /// <summary>
        /// Paints the interior of the specified ellipse</summary>
        /// <param name="rect">The rectangle, in pixels, that encloses the ellipse to paint</param>
        /// <param name="color">The color used to paint the interior of the ellipse</param>
        void FillEllipse(RectangleF rect, Color color);

        /// <summary>
        /// Paints the interior of the specified ellipse</summary>
        /// <param name="ellipse">The position and radius, in pixels, of the ellipse to paint</param>
        /// <param name="brush">The brush used to paint the interior of the ellipse</param>
        void FillEllipse(AtfDrawingEllipse ellipse, IAtfBrush brush);

        /// <summary>
        /// Paints the interior of the specified ellipse</summary>
        /// <param name="ellipse">The position and radius, in pixels, of the ellipse to paint</param>
        /// <param name="color">The color used to paint the interior of the ellipse</param>
        void FillEllipse(AtfDrawingEllipse ellipse, Color color);

        /// <summary>
        /// Draws a solid rectangle with the specified brush while using the alpha channel of the given
        /// bitmap to control the opacity of each pixel.</summary>
        /// <param name="opacityMask">The opacity mask to apply to the brush. The alpha value of each pixel
        /// is multiplied with the alpha value of the brush after the brush has been mapped to the area
        /// defined by destinationRectangle.</param>
        /// <param name="brush">The brush used to paint</param>
        /// <param name="destRect">The region of the render target to paint, in device-independent pixels.</param>
        void FillOpacityMask(IAtfBitmap opacityMask, IAtfBrush brush, RectangleF destRect);

        /// <summary>
        /// Draws a solid rectangle with the specified brush while using the alpha channel of the given
        /// bitmap to control the opacity of each pixel.</summary>
        /// <param name="opacityMask">The opacity mask to apply to the brush. The alpha value of each pixel
        /// is multiplied with the alpha value of the brush after the brush has been mapped to the area
        /// defined by destinationRectangle.</param>
        /// <param name="color">The color used to paint</param>
        /// <param name="destRect">The region of the render target to paint, in device-independent pixels.</param>
        void FillOpacityMask(IAtfBitmap opacityMask, Color color, RectangleF destRect);

        /// <summary>
        /// Draws a solid rectangle with the specified brush while using the alpha channel of the given
        /// bitmap to control the opacity of each pixel.</summary>
        /// <param name="opacityMask">The opacity mask to apply to the brush. The alpha value of
        /// each pixel in the region specified by sourceRectangle is multiplied with the alpha value
        /// of the brush after the brush has been mapped to the area defined by destinationRectangle.</param>
        /// <param name="brush">The brush used to paint the region of the render target specified by destinationRectangle.</param>
        /// <param name="destRect">The region of the render target to paint, in device-independent pixels.</param>
        /// <param name="sourceRect">The region of the bitmap to use as the opacity mask, in device-independent pixels.</param>
        void FillOpacityMask(IAtfBitmap opacityMask, IAtfBrush brush, RectangleF destRect, RectangleF sourceRect);

        /// <summary>
        /// Draws a solid rectangle with the specified brush while using the alpha channel of the given
        /// bitmap to control the opacity of each pixel.</summary>
        /// <param name="opacityMask">The opacity mask to apply to the brush. The alpha value of
        /// each pixel in the region specified by sourceRectangle is multiplied with the alpha value
        /// of the brush after the brush has been mapped to the area defined by destinationRectangle.</param>
        /// <param name="color">The color used to paint the region of the render target specified by destinationRectangle.</param>
        /// <param name="destRect">The region of the render target to paint, in device-independent pixels.</param>
        /// <param name="sourceRect">The region of the bitmap to use as the opacity mask, in device-independent pixels.</param>
        void FillOpacityMask(IAtfBitmap opacityMask, Color color, RectangleF destRect, RectangleF sourceRect);

        /// <summary>
        /// Fills the interior of a polygon defined by given points</summary>
        /// <param name="points">Array of PointF structures that represent the vertices of
        /// the polygon to fill</param>
        /// <param name="brush">Brush that determines the characteristics of the fill</param>
        void FillPolygon(IEnumerable<PointF> points, IAtfBrush brush);

        /// <summary>
        /// Fills the interior of a polygon defined by given points</summary>
        /// <param name="points">Array of PointF structures that represent the vertices of
        /// the polygon to fill</param>
        /// <param name="color">Color used for the fill</param>
        void FillPolygon(IEnumerable<PointF> points, Color color);

        /// <summary>
        /// Paints the interior of the specified rectangle</summary>
        /// <param name="rect">Rectangle to paint, in pixels</param>
        /// <param name="brush">The brush used to paint the rectangle's interior</param>
        void FillRectangle(RectangleF rect, IAtfBrush brush);

        /// <summary>
        /// Paints the interior of the specified rectangle using a single color</summary>
        /// <param name="rect">Rectangle to paint, in pixels</param>
        /// <param name="color">Color used to paint the rectangle</param>
        void FillRectangle(RectangleF rect, Color color);

        /// <summary>
        /// Paints the interior of the specified rectangle with a smooth color gradient</summary>
        /// <param name="rect">Rectangle to paint, in pixels</param>
        /// <param name="pt1">The point, in pixels, that color1 gets mapped to</param>
        /// <param name="pt2">The point, in pixels, that color2 gets mapped to</param>
        /// <param name="color1">The color to use at the first point</param>
        /// <param name="color2">The color to use at the second point</param>
        /// <remarks>Note that each color combination is used to create a brush that
        /// is cached, so you cannot use an unlimited number of color combinations.</remarks>
        void FillRectangle(RectangleF rect, PointF pt1, PointF pt2, Color color1, Color color2);

        /// <summary>
        /// Paints the interior of the specified rounded rectangle</summary>
        /// <param name="roundedRect">The dimensions of the rounded rectangle to paint, in pixels</param>
        /// <param name="brush">The brush used to paint the interior of the rounded rectangle</param>
        void FillRoundedRectangle(AtfDrawingRoundedRect roundedRect, IAtfBrush brush);

        /// <summary>
        /// Paints the interior of the specified rounded rectangle</summary>
        /// <param name="roundedRect">The dimensions of the rounded rectangle to paint, in pixels</param>
        /// <param name="color">The color used to paint the interior of the rounded rectangle</param>
        void FillRoundedRectangle(AtfDrawingRoundedRect roundedRect, Color color);

        /// <summary>
        /// Draws a path defined by an enumeration of EdgeStyleData structures</summary>
        /// <param name="path">The enumeration of drawing primitives that describes the contents of the path</param>
        /// <param name="brush">The brush used to paint the path's stroke</param>
        /// <param name="strokeWidth">A value greater than or equal to 0.0f that specifies the width of the stroke</param>
        /// <remarks>Assume the end point of one primitive be coincident with the start point of the following primitive in a path</remarks>
        /// <param name="strokeStyle">The style of the rounded rectangle's stroke, or null to paint a solid stroke</param>
        void DrawPath(IEnumerable<EdgeStyleData> path, IAtfBrush brush, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws a path defined by an enumeration of EdgeStyleData structures</summary>
        /// <param name="path">The enumeration of drawing primitives that describes the contents of the path</param>
        /// <param name="color">The color used to paint the path's stroke</param>
        /// <param name="strokeWidth">A value greater than or equal to 0.0f that specifies the width of the stroke</param>
        /// <remarks>Assume the end point of one primitive be coincident with the start point of the following primitive in a path</remarks>
        /// <param name="strokeStyle">The style of the rounded rectangle's stroke, or null to paint a solid stroke</param>
        void DrawPath(IEnumerable<EdgeStyleData> path, Color color, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Fills the interior of a path defined by an enumeration of EdgeStyleData structures</summary>
        /// <param name="path">The enumeration of drawing primitives that describes the contents of the path</param>
        /// <param name="brush">The brush used to paint the path's stroke</param>
        /// <remarks>Assume the end point of one primitive be coincident with the start point of the following primitive in a path</remarks>
        void FillPath(IEnumerable<EdgeStyleData> path, IAtfBrush brush);

        /// <summary>
        /// Fills the interior of a path defined by an enumeration of EdgeStyleData structures</summary>
        /// <param name="path">The enumeration of drawing primitives that describes the contents of the path</param>
        /// <param name="color">The color used to paint the path's stroke</param>
        /// <remarks>Assume the end point of one primitive be coincident with the start point of the following primitive in a path</remarks>
        void FillPath(IEnumerable<EdgeStyleData> path, Color color);

        /// <summary>
        /// Draws a geometry previosly defined</summary>
        /// <param name="geometry">The geometry to draw</param>
        /// <param name="brush">The brush used to paint the geometry's stroke</param>
        /// <param name="strokeWidth">A value greater than or equal to 0.0f that specifies the width of the stroke</param>
        /// <param name="strokeStyle">The style of the geometry's stroke, or null to paint a solid stroke</param>
        void DrawGeometry(IAtfGeometry geometry, IAtfBrush brush, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws a geometry previosly defined</summary>
        /// <param name="geometry">The geometry to draw</param>
        /// <param name="color">The color used to paint the geometry's stroke</param>
        /// <param name="strokeWidth">A value greater than or equal to 0.0f that specifies the width of the stroke</param>
        /// <param name="strokeStyle">The style of the geometry's stroke, or null to paint a solid stroke</param>
        void DrawGeometry(IAtfGeometry geometry, Color color, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);

        /// <summary>
        /// Draws a geometry previosly defined</summary>
        /// <param name="geometry">The geometry to draw</param>
        /// <param name="brush">The brush used to fill.</param>
        void FillGeometry(IAtfGeometry geometry, IAtfBrush brush);

        /// <summary>
        /// Draws a geometry previosly defined</summary>
        /// <param name="geometry">The geometry to draw</param>
        /// <param name="color">The color used to fill.</param>
        void FillGeometry(IAtfGeometry geometry, Color color);

        /// <summary>    
        /// Specifies a rectangle to which all subsequent drawing operations are clipped.
        /// Use current antialiasing mode to draw the edges of clip rects.</summary>    
        /// <remarks>The clipRect is transformed by the current world transform set on the render target.
        /// After the transform is applied to the clipRect that is passed in, the axis-aligned 
        /// bounding box for the clipRect is computed. For efficiency, the contents are clipped 
        /// to this axis-aligned bounding box and not to the original clipRect that is passed in.</remarks>    
        /// <param name="clipRect">The size and position of the clipping area, in pixels</param>
        void PushAxisAlignedClip(RectangleF clipRect);

        /// <summary>    
        /// Specifies a rectangle to which all subsequent drawing operations are clipped</summary>
        /// <remarks>The clipRect is transformed by the current world transform set on the render target.
        /// After the transform is applied to the clipRect that is passed in, the axis-aligned 
        /// bounding box for the clipRect is computed. For efficiency, the contents are clipped 
        /// to this axis-aligned bounding box and not to the original clipRect that is passed in.</remarks>    
        /// <param name="clipRect">The size and position of the clipping area, in pixels</param>
        /// <param name="antialiasMode">The antialiasing mode that is used to draw the edges of clip rects that have subpixel 
        /// boundaries, and to blend the clip with the scene contents. The blending is performed 
        /// once when the PopAxisAlignedClip method is called, and does not apply to each 
        /// primitive within the layer.</param>        
        void PushAxisAlignedClip(RectangleF clipRect, AtfDrawingAntialiasMode antialiasMode);

        /// <summary>    
        /// Removes the last axis-aligned clip from the render target. 
        /// After this method is called, the clip is no longer applied to subsequent 
        /// drawing operations.</summary>        
        void PopAxisAlignedClip();

        /// <summary>
        /// Creates a new IAtfSolidColorBrush that has the specified color.
        /// It is recommended to create IAtfSolidColorBrush and reuse them instead
        /// of creating and disposing of them for each method call.</summary>
        /// <param name="color">The red, green, blue, and alpha values of the brush's color</param>
        /// <returns>A new IAtfSolidColorBrush</returns>
        IAtfSolidColorBrush CreateSolidBrush(Color color);

        /// <summary>
        /// Creates a IAtfLinearGradientBrush with the specified points and colors</summary>        
        /// <param name="gradientStops">An array of AtfDrawingGradientStop structures that describe the colors in the brush's gradient 
        /// and their locations along the gradient line</param>
        /// <returns>A new instance of IAtfLinearGradientBrush</returns>
        IAtfLinearGradientBrush CreateLinearGradientBrush(params AtfDrawingGradientStop[] gradientStops);

        /// <summary>
        /// Creates a IAtfLinearGradientBrush that contains the specified gradient stops,
        /// extend mode, and gamma interpolation. has no transform, and has a base opacity of 1.0.</summary>
        /// <param name="pt1">A System.Drawing.PointF structure that represents the starting point of the
        /// linear gradient</param>
        /// <param name="pt2">A System.Drawing.PointF structure that represents the endpoint of the linear gradient</param>
        /// <param name="gradientStops">An array of AtfDrawingGradientStop structures that describe the colors in the brush's gradient 
        /// and their locations along the gradient line</param>
        /// <param name="extendMode">The behavior of the gradient outside the [0,1] normalized range</param>
        /// <param name="gamma">The space in which color interpolation between the gradient stops is performed</param>
        /// <returns>A new instance of IAtfLinearGradientBrush</returns>
        IAtfLinearGradientBrush CreateLinearGradientBrush(
            PointF pt1,
            PointF pt2,
            AtfDrawingGradientStop[] gradientStops,
            AtfDrawingExtendMode extendMode,
            AtfDrawingGamma gamma);

        /// <summary>
        /// Creates a IAtfRadialGradientBrush that contains the specified gradient stops</summary>
        /// <param name="gradientStops">An array of AtfDrawingGradientStop structures that describe the
        /// colors in the brush's gradient and their locations along the gradient</param>
        /// <returns>A new instance of IAtfRadialGradientBrush</returns>
        IAtfRadialGradientBrush CreateRadialGradientBrush(params AtfDrawingGradientStop[] gradientStops);

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
        IAtfRadialGradientBrush CreateRadialGradientBrush(
            PointF center,
            PointF gradientOriginOffset,
            float radiusX,
            float radiusY,
            params AtfDrawingGradientStop[] gradientStops);

        /// <summary>
        /// Creates a new IAtfBitmapBrush from the specified bitmap</summary>
        /// <param name="bitmap">The bitmap contents of the new brush</param>
        /// <returns>A new instance of IAtfBitmapBrush</returns>
        IAtfBitmapBrush CreateBitmapBrush(IAtfBitmap bitmap);

        /// <summary>
        /// Creates a new instance of the IAtfBitmap class from a specified resource</summary>
        /// <param name="type">The type used to extract the resource</param>
        /// <param name="resource">The name of the resource</param>
        /// <returns>A new IAtfBitmap</returns>
        IAtfBitmap CreateBitmap(Type type, string resource);

        /// <summary>
        /// Creates a new instance of the IAtfBitmap from the specified stream</summary>
        /// <param name="stream">The data stream used to load the image</param>
        /// <returns>A new IAtfBitmap</returns>
        /// <remarks>It is safe to close the stream after the function call.</remarks>
        IAtfBitmap CreateBitmap(System.IO.Stream stream);

        /// <summary>
        /// Creates a new instance of the IAtfBitmap from the specified file</summary>
        /// <param name="filename">The name of the bitmap file</param>
        /// <returns>A new IAtfBitmap</returns>
        IAtfBitmap CreateBitmap(string filename);

        /// <summary>
        /// Creates a Direct2D Bitmap by copying the specified GDI image</summary>
        /// <param name="img">A GDI bitmap from which to create new IAtfBitmap</param>
        /// <returns>A new IAtfBitmap</returns>
        IAtfBitmap CreateBitmap(System.Drawing.Image img);

        /// <summary>
        /// Creates a Direct2D Bitmap by copying the specified GDI bitmap</summary>
        /// <param name="bmp">A GDI bitmap from which to create new IAtfBitmap</param>
        /// <returns>A new IAtfBitmap</returns>
        IAtfBitmap CreateBitmap(System.Drawing.Bitmap bmp);

        /// <summary>
        /// Creates an empty Direct2D Bitmap from specified width and height
        /// Pixel format is set to 32 bit ARGB with premultiplied alpha</summary>      
        /// <param name="width">Width of the bitmap in pixels</param>
        /// <param name="height">Height of the bitmap in pixels</param>
        /// <returns>A new IAtfBitmap</returns>
        IAtfBitmap CreateBitmap(int width, int height);

        /// <summary>
        /// Creates a new IAtfBitmapGraphics for use during intermediate offscreen drawing 
        /// that is compatible with the current IAtfGraphics and has the same size, DPI, and pixel format
        /// as the current IAtfGraphics</summary>
        /// <returns>IAtfBitmapGraphics for use during intermediate offscreen drawing</returns>
        IAtfBitmapGraphics CreateCompatibleGraphics();

        /// <summary>
        /// Creates a new IAtfBitmapGraphics for use during intermediate offscreen drawing 
        /// that is compatible with the current IAtfGraphics and has the same size, DPI, and pixel format
        /// as the current IAtfGraphics and with the specified options</summary>        
        /// <param name="options">Whether the new IAtfBitmapGraphics must be compatible with GDI</param>    
        /// <returns>IAtfBitmapGraphics for use during intermediate offscreen drawing</returns>
        IAtfBitmapGraphics CreateCompatibleGraphics(AtfDrawingCompatibleGraphicsOptions options);

        /// <summary>
        /// Creates a new IAtfBitmapGraphics with specified pixel size for use during intermediate offscreen drawing
        /// that is compatible with the current IAtfGraphics and has the same DPI, and pixel format
        /// as the current IAtfGraphics and with the specified options</summary>        
        /// <param name="pixelSize">The desired size of the new IAtfGraphics in pixels</param>
        /// <param name="options">Whether the new IAtfBitmapGraphics must be compatible with GDI</param>   
        /// <returns>IAtfBitmapGraphics with specified pixel size for use during intermediate offscreen drawing</returns>
        IAtfBitmapGraphics CreateCompatibleGraphics(Size pixelSize, AtfDrawingCompatibleGraphicsOptions options);

        /// <summary>
        /// Creates a new IAtfBitmapGraphics with specified DIP size for use during intermediate offscreen drawing
        /// that is compatible with the current IAtfGraphics and has the same DPI, and pixel format
        /// as the current IAtfGraphics and with the specified options</summary>        
        /// <param name="size">The desired size of the new IAtfGraphics in pixels</param>
        /// <param name="options">Whether the new IAtfBitmapGraphics must be compatible with GDI</param> 
        /// <returns>IAtfBitmapGraphics with specified DIP size for use during intermediate offscreen drawing</returns>
        IAtfBitmapGraphics CreateCompatibleGraphics(SizeF size, AtfDrawingCompatibleGraphicsOptions options);

        /// <summary>
        /// Gets the current clipping rectangle, in pixels. Is analagous to
        /// System.Drawing.Graphics' ClipBounds property. Works with PushAxisAlignedClip and
        /// PopAxisAlignedClip. If no clip rectangle has been pushed, this property gets the
        /// size of the D2D surface.</summary>
        RectangleF ClipBounds { get; }

        /// <summary>
        /// Event that is raised after render target is recreated.
        /// All the resources are automatically recreated except resources 
        /// of type IAtfBitmapGraphics. User must recreate all the 
        /// resources of type IAtfBitmapGraphics that are been created 
        /// from this IAtfGraphics</summary>
        event EventHandler RecreateResources;

        /// <summary>
        /// Gets the serial number for the underlining render target.
        /// Each time the render target is recreated the number will be incremented.
        /// Cient code can compare this number with previous one to determine if the render target has 
        /// been recreated since last time a call made to EndDraw().</summary>
        uint RenderTargetNumber { get; }
    }

    /// <summary>
    /// Specifies how the edges of nontext primitives are rendered</summary>
    public enum AtfDrawingAntialiasMode
    {
        /// <summary>
        /// Edges are antialiased using the Direct2D per-primitive method of high-quality
        /// antialiasing</summary>
        PerPrimitive = 0,

        /// <summary>
        /// Objects are aliased in most cases. Objects are antialiased only when they
        /// are drawn to a IAtfGraphics created by the {{CreateDxgiSurfaceRenderTarget}}
        /// method and Direct3D multisampling has been enabled on the backing DirectX
        /// Graphics Infrastructure (DXGI) surface.</summary>
        Aliased = 1,
    }

    /// <summary>
    /// Describes the antialiasing mode used for drawing text</summary>
    public enum AtfDrawingTextAntialiasMode
    {
        /// <summary>
        /// Use the system default</summary>
        Default = 0,

        /// <summary>
        /// Use ClearType antialiasing</summary>
        Cleartype = 1,

        /// <summary>
        /// Use grayscale antialiasing</summary>
        Grayscale = 2,

        /// <summary>
        /// Do not use antialiasing</summary>
        Aliased = 3,
    }
}
