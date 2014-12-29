//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System;

namespace Sce.Atf.Drawing
{
    /// <summary>
    /// Represents a bitmap that has been bound to a IAtfGraphics object
    /// </summary>
    public interface IAtfBitmap : IAtfResource
    {
        /// <summary>
        /// Gets the size, in (pixels), of the bitmap</summary>
        System.Drawing.Size PixelSize { get; }

        /// <summary>
        /// Gets the size, in device-independent pixels (DIPs), of the bitmap.
        /// A DIP is 1/96  of an inch. To retrieve the size in device pixels, use the
        /// IAtfBitmap.PixelSize.</summary>
        System.Drawing.SizeF Size { get; }

        /// <summary>
        /// Gets the GdiBitmap that is used as backup copy
        /// or null if there is none.</summary>
        /// <remarks>
        /// Do not dispose the returned bitmap.
        /// If the bitmap is modified then call update()
        /// </remarks>
        System.Drawing.Bitmap GdiBitmap { get; }

        /// <summary>
        /// Updates native bitmap from GdiBitmap.
        /// Call this method to force update when GdiBitmap is changed
        /// outside this class.</summary>
        void Update();

        /// <summary>
        /// Copy raw memory into the bitmap.</summary>
        /// <param name="bytes">Data to copy</param>
        /// <param name="stride">Stride, or pitch, of source bitmap</param>
        /// <remarks>Pitch = pixel width * bytes per pixel + memory padding</remarks>
        void CopyFromMemory(byte[] bytes, int stride);
    }

    /// <summary>
    /// Specifies the algorithm that is used when images are scaled or rotated</summary>
    public enum AtfDrawingBitmapInterpolationMode
    {
        /// <summary>
        /// Use the exact color of the nearest bitmap pixel to the current rendering
        /// pixel.</summary>
        NearestNeighbor,

        /// <summary>        
        /// Interpolate a color from the four bitmap pixels that are the nearest to the
        /// rendering pixel.</summary>
        Linear
    }
}
