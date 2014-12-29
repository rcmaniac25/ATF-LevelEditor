//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System;

namespace Sce.Atf.Drawing
{
    /// <summary>
    /// Defines an object that paints an area
    /// </summary>
    public interface IAtfBrush : IAtfResource
    {
        /// <summary>
        /// Gets and sets Opacity. Opacity ranges from 0.0 to 1.0.</summary>
        float Opacity { get; set; }

        /// <summary>
        /// Gets the IAtfGraphics object that was used to create this D2dBrush</summary>
        IAtfGraphics Owner { get; }
    }

    /// <summary>
    /// Specifies how a brush paints areas outside of its normal content area</summary>
    /// <remarks>
    /// For a IAtfBitmapBrush, the brush's content is the brush's
    /// bitmap. For an IAtfLinearGradientBrush, the brush's content
    /// area is the gradient axis. For a IAtfRadialGradientBrush,
    /// the brush's content is the area within the gradient ellipse.</remarks>
    public enum AtfDrawingExtendMode
    {
        /// <summary>
        /// Repeat the edge pixels of the brush's content for all regions outside the
        /// normal content area.</summary>
        Clamp = 0,

        /// <summary>
        /// Repeat the brush's content.</summary>
        Wrap = 1,

        /// <summary>
        /// The same as Wrap, except that alternate tiles of the brush's
        /// content are flipped. (The brush's normal content is drawn untransformed.)</summary>
        Mirror = 2,
    }
}
