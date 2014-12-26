//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System.Drawing;

namespace Sce.Atf.Drawing
{
    /// <summary>
    /// Brush that paints an area with a solid color
    /// </summary>
    public interface IAtfSolidColorBrush : IAtfBrush
    {
        /// <summary>
        /// Gets and sets the color of the solid color brush
        /// </summary>
        Color Color
        {
            get;
            set;
        }
    }
}
