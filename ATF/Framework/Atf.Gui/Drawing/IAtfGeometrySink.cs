//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System.Drawing;

namespace Sce.Atf.Drawing
{
    /// <summary>
    /// Enumeration indicating whether path is filled or hollow</summary>
    public enum AtfDrawingFigureBegin
    {
        /// <summary>Path should be filled</summary>
        Filled = 0,
        /// <summary>Path should be hollow</summary>
        Hollow = 1
    }

    /// <summary>
    /// Enumeration indicating whether path is open or closed</summary>
    public enum AtfDrawingFigureEnd
    {
        /// <summary>Path is open</summary>
        Open = 0,
        /// <summary>Path is closed</summary>
        Closed = 1
    }

    /// <summary>
    /// Class with operations on a GeometrySink, which is a path that can contain lines, arcs, 
    /// cubic Bezier curves, and quadratic Bezier curves</summary>
    public interface IAtfGeometrySink
    {
        /// <summary>
        /// Starts a path at a point, optionally filling it</summary>
        /// <param name="startPoint">Path starting point</param>
        /// <param name="figureBegin">Whether path is open or closed</param>
        void BeginFigure(PointF startPoint, AtfDrawingFigureBegin figureBegin);

        /// <summary>
        /// Ends the current path, optionally closing it</summary>
        /// <param name="figureEnd">Whether path is open or closed</param>
        void EndFigure(AtfDrawingFigureEnd figureEnd);

        /// <summary>
        /// Adds point to the current path from the current point to the given point</summary>
        /// <param name="point">Point to extend path to</param>
        void AddLine(PointF point);

        /// <summary>
        /// Creates and adds set of Cubic Bezier curves to the current path</summary>
        /// <param name="beziers">Array of Cubic Bezier curves to add</param>
        void AddBeziers(params AtfDrawingBezierSegment[] beziers);

        /// <summary>
        /// Adds set of lines to the current path from a given list of points</summary>
        /// <param name="points">Array of points to add lines for</param>
        void AddLines(params PointF[] points);

        /// <summary>
        /// Adds arc to the current path</summary>
        /// <param name="arc">Arc to add</param>
        void AddArc(AtfDrawingArcSegment arc);

        /// <summary>
        /// Closes current path, indicating whether it is in an error state and resets error state</summary>
        void Close();
    }
}
