//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System.Drawing;

namespace Sce.Atf.Drawing
{
    /// <summary>
    /// Path geometry class.</summary>
    public interface IAtfGeometry : IAtfResource
    {
        /// <summary>
        /// Retrieves the number of figures in the path geometry.</summary>
        int FigureCount { get; }

        /// <summary>
        /// Retrieves the number of segments in the path geometry.</summary>
        int SegmentCount { get; }

        /// <summary>
        /// Retrieves the bounds of the geometry.</summary>
        RectangleF Bounds { get; }

        /// <summary>
        /// Opens the mesh for population</summary>
        /// <returns>IAtfGeometrySink object</returns>
        IAtfGeometrySink Open();

        /// <summary>
        /// Indicates whether the area filled by the geometry would contain the specified point given the specified flattening tolerance</summary>
        /// <param name="point">Specified point</param>
        /// <returns>True iff area filled by the geometry contains point</returns>
        bool FillContainsPoint(PointF point);

        /// <summary>	
        /// Determines whether the geometry's stroke contains the specified point given the specified stroke thickness and style.</summary>
        /// <param name="point">Specified point</param>
        /// <param name="strokeWidth">Stroke width</param>
        /// <param name="strokeStyle">IAtfStrokeStyle</param>
        /// <returns>True iff geometry's stroke contains specified point</returns>
        bool StrokeContainsPoint(PointF point, float strokeWidth = 1.0f, IAtfStrokeStyle strokeStyle = null);
    }
}
