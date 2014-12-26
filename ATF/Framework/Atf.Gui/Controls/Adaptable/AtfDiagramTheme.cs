//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System;
using System.Collections.Generic;
using System.Drawing;

using Sce.Atf.Drawing;

namespace Sce.Atf.Controls.Adaptable
{
    /// <summary>
    /// Diagram rendering theme class
    /// </summary>
    public class AtfDiagramTheme : AtfDrawingResource
    {
        /// <summary>
        /// Constructor with no parameters
        /// </summary>
        public AtfDiagramTheme()
            : this("Microsoft Sans Serif", 10)
        {

        }
        
        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="fontFamilyName">Font family name for theme</param>
        /// <param name="fontSize">Font size</param>
        public AtfDiagramTheme(string fontFamilyName, float fontSize)
        {
            m_d2dTextFormat = AtfDrawingFactory.CreateTextFormat(fontFamilyName, fontSize);
            m_fillBrush = AtfDrawingFactory.CreateSolidBrush(SystemColors.Window);
            m_fillTitleBrush = AtfDrawingFactory.CreateSolidBrush(Color.YellowGreen);
            m_textBrush = AtfDrawingFactory.CreateSolidBrush(SystemColors.WindowText);
            m_outlineBrush = AtfDrawingFactory.CreateSolidBrush(SystemColors.ControlDark);
            m_highlightBrush = AtfDrawingFactory.CreateSolidBrush(SystemColors.Highlight);
            m_lastHighlightBrush = AtfDrawingFactory.CreateSolidBrush(SystemColors.Highlight);
            m_textHighlightBrush = AtfDrawingFactory.CreateSolidBrush(SystemColors.Highlight);
            m_hotBrush = AtfDrawingFactory.CreateSolidBrush(SystemColors.HotTrack);
            m_dragSourceBrush = AtfDrawingFactory.CreateSolidBrush(Color.SlateBlue);
            m_dropTargetBrush = AtfDrawingFactory.CreateSolidBrush(Color.Chartreuse);
            m_ghostBrush = AtfDrawingFactory.CreateSolidBrush(Color.White);
            m_hiddenBrush = AtfDrawingFactory.CreateSolidBrush(Color.LightGray);
            m_templatedInstance = AtfDrawingFactory.CreateSolidBrush(Color.Yellow);
            m_copyInstance = AtfDrawingFactory.CreateSolidBrush(Color.Green);
            m_errorBrush = AtfDrawingFactory.CreateSolidBrush(Color.Tomato);
            m_infoBrush = AtfDrawingFactory.CreateSolidBrush(SystemColors.Info);
            m_hoverBorderBrush = AtfDrawingFactory.CreateSolidBrush(SystemColors.ControlDarkDark);

            int fontHeight = (int)TextFormat.FontHeight;
            m_rowSpacing = fontHeight + PinMargin;
            m_pinOffset = (fontHeight - m_pinSize) / 2;

            AtfGraphicsGradientStop[] gradstops = 
            { 
                new AtfGraphicsGradientStop(Color.White, 0),
                new AtfGraphicsGradientStop(Color.LightSteelBlue, 1.0f),
            };
            m_fillLinearGradientBrush = AtfDrawingFactory.CreateLinearGradientBrush(gradstops);
            StrokeWidth = 2;
        }

        /// <summary>
        /// Gets row spacing in pixels between pins on element</summary>
        public virtual int RowSpacing { get { return m_rowSpacing; } }
        /// <summary>
        /// Gets pin offset in pixels from pin location</summary>
        public virtual int PinOffset { get { return m_pinOffset; } }
        /// <summary>
        /// Gets pin size in pixels</summary>
        public int PinSize { get { return m_pinSize; } }
        /// <summary>
        /// Gets margin in pixels around pins between pin and other markings, such as labels</summary>
        public int PinMargin { get { return m_pinMargin; } }

        private int m_rowSpacing;
        private int m_pinOffset;
        private int m_pinSize = 8;
        private int m_pinMargin = 2;

        /// <summary>
        /// Registers a brush (pen) with a unique key</summary>
        /// <param name="key">Key to access brush</param>
        /// <param name="brush">Custom brush</param>
        public void RegisterCustomBrush(object key, IAtfBrush brush)
        {
            IAtfBrush oldBrush;
            m_brushes.TryGetValue(key, out oldBrush);
            if (brush != oldBrush)
            {
                if (oldBrush != null)
                    oldBrush.Dispose();

                m_brushes[key] = brush;
                OnRedraw();
            }
        }

        /// <summary>
        /// Gets the custom brush (pen) corresponding to the key, or null</summary>
        /// <param name="key">Key identifying brush</param>
        /// <returns>Custom brush corresponding to the key, or null</returns>
        public IAtfBrush GetCustomBrush(object key)
        {
            IAtfBrush pen;
            m_brushes.TryGetValue(key, out pen);
            return pen;
        }

        /// <summary>
        /// Registers a bitmap with a unique key</summary>
        /// <param name="key">Key to access bitmap</param>
        /// <param name="image">Custom bitmap</param>
        public void RegisterBitmap(object key, Image image)
        {
            var bitmap = AtfDrawingFactory.CreateBitmap(image);
            m_bitmaps[key] = bitmap;
        }

        /// <summary>
        /// Gets the custom bitmap corresponding to the key, or null</summary>
        /// <param name="key">Key identifying bitmap</param>
        /// <returns>Bitmap corresponding to the key, or null</returns>
        public IAtfBitmap GetBitmap(object key)
        {
            IAtfBitmap bitmap;
            if (m_bitmaps.TryGetValue(key, out bitmap))
                return bitmap;
            return null;
        }

        /// <summary>
        /// Gets the custom title background fill brush</summary>
        /// <param name="key">Key identifying bitmap</param>
        /// <returns>Custom brush corresponding to the key, or the default title fill brush</returns>
        public IAtfBrush GetFillTitleBrush(object key)
        {
            IAtfBrush brush;
            m_brushes.TryGetValue(key, out brush);
            return brush ?? m_fillTitleBrush;
        }

        /// <summary>
        /// Gets or sets default Stroke width used for drawing outline</summary>        
        public float StrokeWidth
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the diagram text format</summary>
        public IAtfTextFormat TextFormat
        {
            get { return m_d2dTextFormat; }
            set { SetDisposableField(value, ref m_d2dTextFormat); }
        }

        /// <summary>
        /// Gets or sets the brush (pen) used to draw diagram outlines</summary>
        public IAtfBrush OutlineBrush
        {
            get { return m_outlineBrush; }
            set { SetDisposableField(value, ref m_outlineBrush); }
        }

        /// <summary>
        /// Gets or sets the gradient brush (pen) used to fill diagram items</summary>
        public IAtfLinearGradientBrush FillGradientBrush
        {
            get { return m_fillLinearGradientBrush; }
            set { SetDisposableField(value, ref m_fillLinearGradientBrush); }
        }

        /// <summary>
        /// Gets or sets the brush (pen) used to fill diagram items</summary>
        public IAtfBrush FillBrush
        {
            get { return m_fillBrush; }
            set { SetDisposableField(value, ref m_fillBrush); }
        }

        /// <summary>
        /// Gets or sets the brush (pen) used to fill title area</summary>
        public IAtfBrush FillTitleBrush
        {
            get { return m_fillTitleBrush; }
            set { SetDisposableField(value, ref m_fillTitleBrush); }
        }

        /// <summary>
        /// Gets or sets the brush (pen) used to draw diagram text</summary>
        public IAtfBrush TextBrush
        {
            get { return m_textBrush; }
            set { SetDisposableField(value, ref m_textBrush); }
        }

        /// <summary>
        /// Gets or sets the brush (pen) used to outline highighted (selected) diagram items</summary>
        public IAtfBrush HighlightBrush
        {
            get { return m_highlightBrush; }
            set { SetDisposableField(value, ref m_highlightBrush); }
        }

        /// <summary>
        /// Gets or sets the brush (pen) used to outline the last highighted (selected) diagram item</summary>
        public IAtfBrush LastHighlightBrush
        {
            get { return m_lastHighlightBrush; }
            set { SetDisposableField(value, ref m_lastHighlightBrush); }
        }

        /// <summary>
        /// Gets or sets the brush that paints the background of selected text</summary>
        public IAtfBrush TextHighlightBrush
        {
            get { return m_textHighlightBrush; }
            set { SetDisposableField(value, ref m_textHighlightBrush); }
        }

        /// <summary>
        /// Gets or sets the brush (pen) used to outline ghosted diagram items</summary>
        public IAtfBrush GhostBrush
        {
            get { return m_ghostBrush; }
            set { SetDisposableField(value, ref m_ghostBrush); }
        }

        /// <summary>
        /// Gets or sets the brush (pen) used to outline hidden diagram items</summary>
        public IAtfBrush HiddenBrush
        {
            get { return m_hiddenBrush; }
            set { SetDisposableField(value, ref m_hiddenBrush); }
        }

        /// <summary>
        /// Gets or sets the brush (pen) used to outline templated subgroup diagram items</summary>
        public IAtfBrush TemplatedInstance
        {
            get { return m_templatedInstance; }
            set { SetDisposableField(value, ref m_templatedInstance); }
        }

        /// <summary>
        /// Gets or sets the brush (pen) used to outline copy instance subgroup diagram items</summary>
        public IAtfBrush CopyInstance
        {
            get { return m_copyInstance; }
            set { SetDisposableField(value, ref m_copyInstance); }
        }

        /// <summary>
        /// Gets or sets the brush (pen) used to fill hot-tracked diagram items</summary>
        public IAtfBrush HotBrush
        {
            get { return m_hotBrush; }
            set { SetDisposableField(value, ref m_hotBrush); }
        }

        /// <summary>
        /// Gets or sets the brush (pen) used to fill hot-tracked diagram sub-items</summary>
        public IAtfBrush DragSourceBrush
        {
            get { return m_dragSourceBrush; }
            set { SetDisposableField(value, ref m_dragSourceBrush); }
        }

        /// <summary>
        /// Gets or sets the brush (pen) used to fill hot-tracked diagram sub-items</summary>
        public IAtfBrush DropTargetBrush
        {
            get { return m_dropTargetBrush; }
            set { SetDisposableField(value, ref m_dropTargetBrush); }
        }

        /// <summary>
        /// Gets or sets the brush (pen) used to outline diagram items with errors</summary>
        public IAtfBrush ErrorBrush
        {
            get { return m_errorBrush; }
            set { SetDisposableField(value, ref m_errorBrush); }
        }

        /// <summary>
        /// Gets or sets the brush (pen) used to draw the info/tool tip background</summary>
        public IAtfBrush InfoBrush
        {
            get { return m_infoBrush; }
            set { SetDisposableField(value, ref m_infoBrush); }
        }

        /// <summary>
        /// Gets or sets the brush (pen) used to hover border</summary>
        public IAtfBrush HoverBorderBrush
        {
            get { return m_hoverBorderBrush; }
            set { SetDisposableField(value, ref m_hoverBorderBrush); }
        }

        /// <summary>
        /// Returns the custom brush (pen) corresponding to the key, or null</summary>
        /// <param name="key">Key identifying brush</param>
        /// <returns>Custom brush corresponding to the key, or null</returns>
        public IAtfBrush GetCustomOrDefaultBrush(object key)
        {
            // try to use custom brush (pen) if registered
            IAtfBrush brush;
            m_brushes.TryGetValue(key, out brush);
            if (brush == null)  // use a default brush
                brush = m_fillLinearGradientBrush;
            return brush;
        }

        /// <summary>
        /// Event that is raised after any user property of the theme changes</summary>
        public event EventHandler Redraw;

        /// <summary>
        /// Raises the Redraw event</summary>
        protected virtual void OnRedraw()
        {
            Redraw.Raise(this, EventArgs.Empty);
        }

        /// <summary>
        /// Disposes of resources</summary>
        /// <param name="disposing">True to release both managed and unmanaged resources;
        /// false to release only unmanaged resources</param>
        protected override void Dispose(bool disposing)
        {
            if (IsDisposed) return;

            if (disposing)
            {
                m_fillBrush.Dispose();
                m_fillTitleBrush.Dispose();
                m_textBrush.Dispose();
                m_d2dTextFormat.Dispose();
                m_highlightBrush.Dispose();
                m_lastHighlightBrush.Dispose();
                m_ghostBrush.Dispose();
                m_hiddenBrush.Dispose();
                m_templatedInstance.Dispose();
                m_copyInstance.Dispose();
                m_hotBrush.Dispose();
                m_errorBrush.Dispose();
                m_highlightBrush.Dispose();

                foreach (IAtfBrush brush in m_brushes.Values)
                    brush.Dispose();
                m_brushes.Clear();

                foreach (IAtfBitmap bitmap in m_bitmaps.Values)
                    bitmap.Dispose();
                m_bitmaps.Clear();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Gets or sets the diagram picking tolerance</summary>
        public int PickTolerance
        {
            get { return m_pickTolerance; }
            set { m_pickTolerance = value; }
        }

        /// <summary>
        /// Gets the theme's brush (pen) for the given drawing style</summary>
        /// <param name="style">Drawing style</param>
        /// <returns>Brush for the given drawing style</returns>
        public IAtfBrush GetOutLineBrush(DiagramDrawingStyle style)
        {
            switch (style)
            {
                case DiagramDrawingStyle.Normal:
                    return m_outlineBrush;
                case DiagramDrawingStyle.Selected:
                    return m_highlightBrush;
                case DiagramDrawingStyle.LastSelected:
                    return m_lastHighlightBrush;
                case DiagramDrawingStyle.Hot:
                    return m_hotBrush;
                case DiagramDrawingStyle.DragSource:
                    return m_dragSourceBrush;
                case DiagramDrawingStyle.DropTarget:
                    return m_dropTargetBrush;
                case DiagramDrawingStyle.Ghosted:
                    return m_ghostBrush;
                case DiagramDrawingStyle.Hidden:
                    return m_hiddenBrush;
                case DiagramDrawingStyle.TemplatedInstance:
                    return m_templatedInstance;
                case DiagramDrawingStyle.CopyInstance:
                    return m_copyInstance;
                case DiagramDrawingStyle.Error:
                default:
                    return m_errorBrush;
            }
        }

        private void SetDisposableField<T>(T value, ref T field)
        where T : class, IDisposable
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (field != value)
            {
                field.Dispose();
                field = value;
                OnRedraw();
            }
        }

        private readonly Dictionary<object, IAtfBrush> m_brushes = new Dictionary<object, IAtfBrush>();
        private readonly Dictionary<object, IAtfBitmap> m_bitmaps = new Dictionary<object, IAtfBitmap>();
        private IAtfTextFormat m_d2dTextFormat;
        private IAtfBrush m_fillBrush;
        private IAtfBrush m_fillTitleBrush;
        private IAtfBrush m_textBrush;
        private IAtfBrush m_highlightBrush;
        private IAtfBrush m_textHighlightBrush;
        private IAtfBrush m_lastHighlightBrush;
        private IAtfBrush m_ghostBrush;
        private IAtfBrush m_hiddenBrush;
        private IAtfBrush m_templatedInstance;
        private IAtfBrush m_copyInstance;
        private IAtfBrush m_hotBrush;
        private IAtfBrush m_dropTargetBrush;
        private IAtfBrush m_dragSourceBrush;
        private IAtfBrush m_errorBrush;
        private IAtfBrush m_infoBrush;
        private IAtfBrush m_outlineBrush;
        private IAtfBrush m_hoverBorderBrush;
        private IAtfLinearGradientBrush m_fillLinearGradientBrush;
        private int m_pickTolerance = 3;
    }
}
