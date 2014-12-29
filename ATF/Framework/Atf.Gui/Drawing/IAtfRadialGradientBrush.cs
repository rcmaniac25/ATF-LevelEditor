//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System;
using System.Drawing;

namespace Sce.Atf.Drawing
{
    /// <summary>
    /// Paints an area with a radial gradient</summary>
    public interface IAtfRadialGradientBrush
    {
        /// <summary>    
        /// Gets and sets the center of the gradient ellipse</summary>            
        PointF Center { get; set; }

        /// <summary>    
        /// Gets and sets the offset of the gradient origin relative to the gradient 
        /// ellipse's center</summary>            
        PointF GradientOriginOffset { get; set; }

        /// <summary>    
        /// Gets and sets the x-radius of the gradient ellipse</summary>
        float RadiusX { get; set; }

        /// <summary>    
        /// Gets and sets the y-radius of the gradient ellipse</summary>            
        float RadiusY { get; set; }
    }
}
