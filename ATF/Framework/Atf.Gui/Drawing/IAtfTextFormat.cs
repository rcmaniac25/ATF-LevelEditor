//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System;

namespace Sce.Atf.Drawing
{
    /// <summary>
    /// The IAtfTextFormat describes the font and paragraph properties used to format text, 
    /// and it describes locale information</summary>
    public interface IAtfTextFormat : IDisposable
    {
        //TODO

        /// <summary>
        /// Gets and sets whether the text is underlined</summary>
        bool Underlined
        {
            get;
            set;
        }

        /// <summary>
        /// Gets and sets whether the text has the strike-out effect</summary>
        bool Strikeout
        {
            get;
            set;
        }

        //TODO
    }
}
