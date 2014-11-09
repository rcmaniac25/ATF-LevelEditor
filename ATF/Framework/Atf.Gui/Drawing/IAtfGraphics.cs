//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System;

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
    public interface IAtfGraphics : IDisposable
    {
    }
}
