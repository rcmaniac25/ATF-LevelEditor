//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System.Drawing;

namespace Sce.Atf.Drawing
{
    /// <summary>
    /// Paints an area with a bitmap</summary>
    public interface IAtfBitmapBrush : IAtfBrush
    {
        /// <summary>    
        /// Gets the bitmap source that this brush uses to paint</summary>            
        IAtfBitmap Bitmap { get; set; }

        /// <summary>    
        /// Gets the method by which the brush horizontally tiles those areas that extend past its bitmap</summary>    
        /// <remarks>    
        /// Like all brushes, IAtfBitmapBrush defines an infinite plane of content. Because bitmaps are finite,
        /// it relies on an extend mode to determine how the plane is filled horizontally and vertically.</remarks>            
        AtfDrawingExtendMode ExtendModeX { get; set; }

        /// <summary>    
        /// Gets the method by which the brush vertically tiles those areas that extend past its bitmap</summary>    
        /// <remarks>    
        /// Like all brushes, IAtfBitmapBrush defines an infinite plane of content. Because bitmaps are finite,
        /// it relies on an extend mode to determine how the plane is filled horizontally and vertically.</remarks>            
        AtfDrawingExtendMode ExtendModeY { get; set; }

        /// <summary>    
        /// Gets the interpolation method used when the brush bitmap is scaled or rotated</summary>        
        AtfDrawingBitmapInterpolationMode InterpolationMode { get; set; }

        /// <summary>
        /// Gets and sets location</summary>
        PointF Location { get; set; }
    }
}
