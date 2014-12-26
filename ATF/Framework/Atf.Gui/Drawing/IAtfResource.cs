//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System;

namespace Sce.Atf.Drawing
{
    /// <summary>
    /// Base interface for all drawing resources
    /// </summary>
    public interface IAtfResource : IDisposable
    {
        /// <summary>
        /// Gets whether the object has been disposed of
        /// </summary>
        bool IsDisposed { get; }
    }
}
