//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System;

namespace Sce.Atf.Drawing
{
    /// <summary>
    /// Renders to an intermediate bitmap created by the CreateCompatibleRenderTarget method</summary>
    public interface IAtfBitmapGraphics : IAtfGraphics
    {
        /// <summary>
        /// Retrieves the bitmap for this IAtfGraphics. The returned bitmap can be used
        /// for drawing operations.</summary>     
        /// <returns>IAtfBitmap for this IAtfGraphics</returns>
        IAtfBitmap GetBitmap();
    }

    /// <summary>
    /// Specifies additional features supportable by a compatible IAtfGraphics when
    /// it is created. This enumeration allows a bitwise combination of its member values.</summary>
    /// <remarks>    
    /// The GdiCompatible option may only be requested if the parent IAtfGraphics was created with the
    /// GdiCompatible option</remarks>
    public enum AtfDrawingCompatibleGraphicsOptions
    {
        /// <summary>
        /// The IAtfGraphics supports no additional features</summary>
        None = 0,

        /// <summary>
        /// The IAtfGraphics supports interoperability with the Windows Graphics Device Interface (GDI)</summary>
        GdiCompatible = 1,
    }
}
