//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System;
using Sce.Atf.DirectWrite;

namespace Sce.Atf.Drawing
{
    /// <summary>
    /// The IAtfTextLayout object represents a block of text after it has been
    /// fully analyzed and formatted</summary>
    public interface IAtfTextLayout : IAtfTextFormat
    {
        /// <summary>
        /// Gets text that is formatted using this object</summary>
        /// <remarks>This property cannot be set. A new instance of 
        /// IAtfTextFormat must be created per unique string.</remarks>
        string Text { get; }

        /// <summary>    
        /// Sets strikethrough for text within a specified text range</summary>    
        /// <param name="hasStrikethrough">A Boolean flag that indicates whether 
        /// strikethrough takes place in the range specified by textRange</param>
        /// <param name="startPosition">The start position of the text range</param>
        /// <param name="length">The number of positions in the text range</param>                
        void SetStrikethrough(bool hasStrikethrough, int startPosition, int length);

        /// <summary>    
        /// Sets underlining for text within a specified text range</summary>    
        /// <param name="hasUnderline">A Boolean flag that indicates whether 
        /// underlining takes place within a specified text range</param>        
        /// <param name="startPosition">The start position of the text range</param>
        /// <param name="length">The number of positions in the text range</param>                
        void SetUnderline(bool hasUnderline, int startPosition, int length);

        /// <summary>
        /// Gets or sets layout width</summary>
        float LayoutWidth { get; set; }

        /// <summary>
        /// Gets or sets layout height</summary>
        float LayoutHeight { get; set; }

        /// <summary>
        /// Gets width of the formatted text, while ignoring trailing whitespace at the end of each line</summary>
        float Width { get; }

        /// <summary>
        /// Gets height of the formatted text</summary>
        /// <remarks>The height of an empty string is set to the same value as that of the default font.</remarks>
        float Height { get; }

        /// <summary>
        /// Gets total number of lines</summary>
        int LineCount { get; }

        /// <summary>
        /// The application calls this function passing in a specific pixel location relative to the top-left location 
        /// of the layout box and obtains the information about the correspondent hit-test metrics of the text string 
        /// where the hit-test has occurred. When the specified pixel location is outside the text string, 
        /// the function sets the output value IsInside to False.</summary>
        /// <param name="x">The pixel location X to hit-test, relative to the top-left location of the layout box.</param>
        /// <param name="y">The pixel location Y to hit-test, relative to the top-left location of the layout box.</param>
        /// <returns>HitTestMetrics of text string where hit test occurs</returns>
        HitTestMetrics HitTestPoint(float x, float y);

        /// <summary>
        /// The application calls this function to get the pixel location relative to the top-left of the layout box 
        /// given the text position and the logical side of the position. 
        /// This function is normally used as part of caret positioning of text where the caret is drawn 
        /// at the location corresponding to the current text editing position. 
        /// It may also be used as a way to programmatically obtain the geometry of a particular text position in UI automation.</summary>
        /// <param name="textPosition">The text position used to get the pixel location</param>
        /// <param name="isTrailingHit">A Boolean flag that whether the pixel location is of the leading or 
        /// the trailing side of the specified text position</param>
        /// <returns>HitTestMetrics of given text position</returns>
        HitTestMetrics HitTestTextPosition(int textPosition, bool isTrailingHit);

        /// <summary>
        /// The application calls this function to get a set of hit-test metrics corresponding to a range of text positions. 
        /// One of the main usages is to implement highlight selection of the text string.</summary>
        /// <param name="textPosition">The first text position of the specified range.</param>
        /// <param name="textLength">The number of positions of the specified range.</param>
        /// <param name="originX">The origin pixel location X at the left of the layout box. 
        /// This offset is added to the hit-test metrics returned. </param>
        /// <param name="originY">The origin pixel location Y at the top of the layout box. 
        /// This offset is added to the hit-test metrics returned. </param>
        /// <returns>An array of HitTestMetrics fully enclosing the specified position range.</returns>
        HitTestMetrics[] HitTestTextRange(int textPosition, int textLength, float originX, float originY);

        /// <summary>
        /// Retrieves information about each individual text line of the text string</summary>
        /// <returns>Array of LineMetrics for each text line</returns>
        LineMetrics[] GetLineMetrics();

        /// <summary>
        /// Retrieves logical properties and measurements of each glyph cluster</summary>
        /// <returns>Metrics, such as line-break or total advance width, for a glyph cluster</returns>
        ClusterMetrics[] GetClusterMetrics();
    }
}
