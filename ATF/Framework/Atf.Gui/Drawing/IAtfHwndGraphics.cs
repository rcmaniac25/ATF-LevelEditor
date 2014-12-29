//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System;

namespace Sce.Atf.Drawing
{
    /// <summary>
    /// Renders drawing instructions to a control</summary>
    public interface IAtfHwndGraphics : IAtfGraphics
    {
        /// <summary>
        /// Changes the size of the IAtfGraphics to the specified pixel size</summary>
        /// <param name="pixelSize">The new size of the IAtfGraphics in device pixels</param>
        void Resize(System.Drawing.Size pixelSize);

        /// <summary>
        /// Gets the HWND associated with this IAtfGraphics</summary>
        IntPtr Hwnd { get; }

        /// <summary>
        /// Indicates whether the HWND associated with this IAtfGraphics is occluded</summary>        
        /// <returns>AtfDrawingWindowState value that indicates whether the HWND associated with
        /// this IAtfGraphics is occluded</returns>
        /// <remarks>Note that if the window was occluded the last time EndDraw was called, the
        /// next time the IAtfGraphics calls CheckWindowState, it returns AtfDrawingWindowState.Occluded
        /// regardless of the current window state. If you want to use CheckWindowState
        /// to determine the current window state, you should call CheckWindowState after
        /// every EndDraw call and ignore its return value. This ensures that your
        /// next call to CheckWindowState state returns the actual window state.</remarks>
        AtfDrawingWindowState CheckWindowState();
    }

    /// <summary>
    /// Describes whether a window is occluded.</summary>
    /// <remarks>
    /// If the window was occluded the last time EndDraw was called, the next
    /// time the IAtfGraphics calls CheckWindowState, it returns Occluded
    /// regardless of the current window state. If you want to use CheckWindowState
    /// to determine the current window state, you should call CheckWindowState after
    /// every EndDraw call and ignore its return value. This ensures that your
    /// next call to CheckWindowState state returns the actual window state.</remarks>    
    public enum AtfDrawingWindowState
    {
        /// <summary>
        /// The window is not occluded.</summary>
        None = 0,

        /// <summary>
        /// The window is occluded.</summary>
        Occluded = 1,
    }
}
