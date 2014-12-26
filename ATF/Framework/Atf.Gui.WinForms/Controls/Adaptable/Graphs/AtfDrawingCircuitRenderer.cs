//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
//Copyright © 2014 Vincent Simonetti

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Sce.Atf.Adaptation;
using Sce.Atf.Applications;
using Sce.Atf.Controls.PropertyEditing;
using Sce.Atf.Rendering;
using Sce.Atf.VectorMath;
using Sce.Atf.Drawing;

namespace Sce.Atf.Controls.Adaptable.Graphs
{
    /// <summary>
    /// Graph renderer that draws graph nodes as circuit elements, and edges as wires.
    /// Elements have 0 or more output pins, where wires originate, and 0 or more input
    /// pins, where wires end. Output pins are on the right and input pins are on the left
    /// side of elements.</summary>
    /// <typeparam name="TElement">Element type, must implement IElementType, and IGraphNode</typeparam>
    /// <typeparam name="TWire">Wire type, must implement IGraphEdge</typeparam>
    /// <typeparam name="TPin">Pin type, must implement ICircuitPin, IEdgeRoute</typeparam>
    public class AtfDrawingCircuitRenderer<TElement, TWire, TPin> : AtfDrawingGraphRenderer<TElement, TWire, TPin>, IDisposable
        where TElement : class, ICircuitElement
        where TWire : class, IGraphEdge<TElement, TPin>
        where TPin : class, ICircuitPin
    {
        //TODO: constructor

        /// <summary>
        /// Specifies the pin drawing style </summary>
        public enum PinStyle
        {
            Default, // non-filled, location adjacent to border but internal
            OnBorderFilled,
        }

        /// <summary>
        /// Gets or sets the pin drawing style</summary>
        public PinStyle PinDrawStyle
        {
            get { return m_pinDrawStyle; }
            set { m_pinDrawStyle = value; }
        }

        /// <summary>
        /// Get the string to use on the title bar of the circuit element</summary>
        /// <param name="element">Circuit element</param>
        /// <returns>String to use on title bar of circuit element</returns>
        protected virtual string GetElementTitle(TElement element)
        {
            return element.Type.Name;
        }

        /// <summary>
        /// Get the display name of the circuit element, which is drawn below and outside the circuit element. 
        /// Returns null or the empty string to not draw anything in this place</summary>
        /// <param name="element">Circuit element</param>
        /// <returns>Display name of the circuit element</returns>
        protected virtual string GetElementDisplayName(TElement element)
        {
            return element.Name;
        }

        /// <summary>
        /// Get element type cache</summary>
        protected Dictionary<ICircuitElementType, ElementTypeInfo> ElementTypeCache
        {
            get { return m_elementTypeCache; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the background area of the title should be filled</summary>
        public bool TitleBackgroundFilled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether edges of elements have a rounded 
        /// rather than a square or sharp appearance</summary>
        public bool RoundedBorder { set; get; }

        /// <summary>
        /// Gets or sets the threshold size that shows the details in a circuit element</summary>
        /// <remarks>Title, pins, and labels(after transformation) smaller than the threshold won’t be displayed for speed optimization.
        /// Default is 6. Set value 0 effectively turns off this optimization.</remarks>
        public int DetailsThresholdSize { get; set; }

        /// <summary>
        /// Gets or sets a Boolean that determines if wires should be selected as a fallback
        /// if no elements are selected, in a marquee selection</summary>
        public bool RectangleSelectsWires { get; set; }

        /// <summary>
        /// Gets or sets D2dDiagramTheme</summary>
        public AtfDiagramTheme Theme
        {
            get { return m_theme; }
            set
            {
                if (m_theme != value)
                {
                    if (m_theme != null)
                        m_theme.Redraw -= new EventHandler(theme_Redraw);
                    SetPinSpacing();
                    m_theme = value;
                }
            }
        }


        /// <summary>
        /// Disposes of resources</summary>
        /// <param name="disposing">True to release both managed and unmanaged resources;
        /// false to release only unmanaged resources</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_theme.Redraw -= new EventHandler(theme_Redraw);
                m_subGraphPinBrush.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Gets or sets D2dBrush for subgraph pins</summary>
        public IAtfBrush SubGraphPinBrush
        {
            get { return m_subGraphPinBrush; }
            set
            {
                m_subGraphPinBrush.Dispose();
                m_subGraphPinBrush = value;
            }
        }

        /// <summary>
        /// Called when content of graph object changes</summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event args</param>
        public override void OnGraphObjectChanged(object sender, ItemChangedEventArgs<object> e)
        {
            if (e.Item.Is<ICircuitElementType>())
                Invalidate(e.Item.Cast<ICircuitElementType>());
        }

        /// <summary>
        /// Invalidates cached info for element type name</summary>
        /// <param name="elementType">Element type to invalidate</param>
        public void Invalidate(ICircuitElementType elementType)
        {
            m_elementTypeCache.Remove(elementType);
            OnRedraw();
        }

        //TODO: 185-298

        /// <summary>
        /// Gets pin position in element local space</summary>
        /// <param name="element">Element</param>
        /// <param name="pinIndex">Pin index</param>
        /// <param name="inputSide">True if pin side is input, false for output side</param>
        /// <param name="g">Graphics object</param>
        /// <returns>Pin position</returns>
        public Point GetPinPosition(TElement element, int pinIndex, bool inputSide, IAtfGraphics g)
        {
            if (inputSide) // group pin box on the left edge
            {
                Point op = element.Bounds.Location;
                op.Y += GetPinOffset(element, pinIndex, true);
                if (m_pinDrawStyle == PinStyle.OnBorderFilled)
                    op.X -= m_pinSize / 2;
                return op;
            }
            else
            {
                ElementTypeInfo info = GetElementTypeInfo(element, g);
                Point ip = element.Bounds.Location;
                ip.X += info.Size.Width;
                if (m_pinDrawStyle == PinStyle.OnBorderFilled)
                    ip.X += m_pinSize / 2;
                ip.Y += GetPinOffset(element, pinIndex, false);
                return ip;

            }
        }

        //TODO: 330-682

        private void theme_Redraw(object sender, EventArgs e)
        {
            m_elementTypeCache.Clear(); // invalidate cached info
            OnRedraw();
        }

        //TODO: 691-895

        /// <summary>
        /// Truncates very long pin names into the form "prefix...suffix" where prefix and suffix are the 
        /// beginning and ending substrings of the original name.
        /// </summary>
        /// <param name="pinText">Original pin name</param>
        /// <returns>Original pin name if it is less than MaxCollapsedGroupPinNameLength, otherwise the 
        /// truncated version</returns>
        private string TruncatePinText(string pinText)
        {
            if (pinText.Length < MaxCollapsedGroupPinNameLength) return pinText;

            var sb = new StringBuilder();
            sb.Append(pinText.Substring(0, m_truncatedPinNameSubstringLength));
            sb.Append("...");
            sb.Append(pinText.Substring(pinText.Length - m_truncatedPinNameSubstringLength));
            return sb.ToString();
        }

        //TODO: 915-1381

        /// <summary>
        /// Obtains ElementTypeInfo for element</summary>
        /// <param name="element">Element to obtain ElementTypeInfo</param>
        /// <param name="g">IAtfGraphics object</param>
        /// <returns>ElementTypeInfo for element</returns>
        protected ElementTypeInfo GetElementTypeInfo(TElement element, IAtfGraphics g)
        {
            // look it up in the cache
            ICircuitElementType type = element.Type;
            string title = GetElementTitle(element);
            ElementTypeInfo cachedInfo;
            if (m_elementTypeCache.TryGetValue(type, out cachedInfo))
            {
                if (cachedInfo.numInputs != type.Inputs.Count || cachedInfo.numOutputs != type.Outputs.Count ||
                    cachedInfo.Title != title)
                {
                    m_elementTypeCache.Remove(type);
                    Invalidate(type);
                }
                else
                    return cachedInfo;
            }


            // not in cache, recompute
            if (element.Is<ICircuitGroupType<TElement, TWire, TPin>>())
            {
                var group = element.Cast<ICircuitGroupType<TElement, TWire, TPin>>();
                if (m_elementTypeCache.TryGetValue(group, out cachedInfo))
                {
                    if (cachedInfo.numInputs != group.Inputs.Count || cachedInfo.numOutputs != group.Outputs.Count ||
                        cachedInfo.Title != title)
                    {
                        m_elementTypeCache.Remove(group);
                        Invalidate(group);
                    }
                    else
                        return cachedInfo;
                }
                return GetHierarchicalElementTypeInfo(group, g);

            }

            ElementSizeInfo sizeInfo = GetElementSizeInfo(type, g, title);
            var info = new ElementTypeInfo
            {
                Title = title,
                Size = sizeInfo.Size,
                Interior = sizeInfo.Interior,
                OutputLeftX = sizeInfo.OutputLeftX.ToArray(),
                numInputs = type.Inputs.Count,
                numOutputs = type.Outputs.Count,
            };

            m_elementTypeCache.Add(type, info);

            return info;
        }

        /// <summary>Computes interior and exterior size as well as the x positions of output pins</summary>
        /// <param name="type">Circuit element type</param>
        /// <param name="g">Graphics that can be used for measuring strings</param>
        /// <param name="title">The string to use on the title bar of the circuit element</param>
        /// <returns>Element size info; cannot be null</returns>
        /// <remarks>Clients using customized rendering should override this method
        /// to adjust sizes accordingly. These sizes are used by drag-from picking.</remarks>
        protected virtual ElementSizeInfo GetElementSizeInfo(ICircuitElementType type, IAtfGraphics g, string title = null)
        {
            SizeF typeNameSize = new SizeF();
            if (title != null)
                typeNameSize = g.MeasureText(title, m_theme.TextFormat);
            else
                g.MeasureText(type.Name, m_theme.TextFormat);

            int width = (int)typeNameSize.Width + 2 * m_pinMargin + 4 * ExpanderSize + 1;

            IList<ICircuitPin> inputPins = type.Inputs;
            IList<ICircuitPin> outputPins = type.Outputs;
            int inputCount = inputPins.Count;
            int outputCount = outputPins.Count;
            int minRows = Math.Min(inputCount, outputCount);
            int maxRows = Math.Max(inputCount, outputCount);

            bool isCollapsedGroup = false;
            if (type.Is<ICircuitGroupType<TElement, TWire, TPin>>())
            {
                // If this is a group, it must be collapsed, because expanded groups
                // are handled by GetHierarchicalElementSizeInfo.
                isCollapsedGroup = true;
            }

            int[] outputLeftX = new int[outputCount];

            int height = m_rowSpacing + 2 * m_pinMargin;
            height += Math.Max(
                maxRows * m_rowSpacing,
                minRows * m_rowSpacing + type.InteriorSize.Height - m_pinMargin);

            bool imageRight = true;
            for (int i = 0; i < maxRows; i++)
            {
                double rowWidth = 2 * m_pinMargin;
                if (inputCount > i)
                {
                    var pinText = inputPins[i].Name;
                    if (isCollapsedGroup)
                        pinText = TruncatePinText(pinText);
                    SizeF labelSize = g.MeasureText(pinText, m_theme.TextFormat);
                    rowWidth += labelSize.Width + m_pinSize + m_pinMargin;
                }
                else
                {
                    imageRight = false;
                }


                if (outputCount > i)
                {
                    var pinText = outputPins[i].Name;
                    if (isCollapsedGroup)
                        pinText = TruncatePinText(pinText);
                    SizeF labelSize = g.MeasureText(pinText, m_theme.TextFormat);
                    outputLeftX[i] = (int)labelSize.Width;
                    rowWidth += labelSize.Width + m_pinSize + m_pinMargin;
                }

                rowWidth += type.InteriorSize.Width;

                width = Math.Max(width, (int)rowWidth);
            }

            if (inputCount == outputCount)
                width = Math.Max(width, type.InteriorSize.Width + 2);

            width = Math.Max(width, MinElementWidth);
            height = Math.Max(height, MinElementHeight);

            Size size = new Size(width, height);
            Rectangle interior = new Rectangle(
                imageRight ? width - type.InteriorSize.Width : 1,
                height - type.InteriorSize.Height,
                type.InteriorSize.Width,
                type.InteriorSize.Height);

            for (int i = 0; i < outputLeftX.Length; i++)
                outputLeftX[i] = width - m_pinMargin - m_pinSize - outputLeftX[i];

            return new ElementSizeInfo(size, interior, outputLeftX);
        }

        private ElementTypeInfo GetHierarchicalElementTypeInfo(ICircuitGroupType<TElement, TWire, TPin> group, IAtfGraphics g)
        {
            ElementSizeInfo sizeInfo = GetHierarchicalElementSizeInfo(group, g);

            var info = new ElementTypeInfo
            {
                Title = GetElementTitle(group.As<TElement>()),
                Size = sizeInfo.Size,
                Interior = sizeInfo.Interior,
                OutputLeftX = sizeInfo.OutputLeftX.ToArray(),
                numInputs = group.Inputs.Count,
                numOutputs = group.Outputs.Count,
            };

            m_elementTypeCache.Add(group, info);
            return info;
        }

        /// <summary>Computes interior and exterior size as well as the x positions of output pins for expanded group</summary>
        /// <param name="group">Group</param>
        /// <param name="g">Graphics object that can be used for measuring strings</param>
        /// <returns>Element size info; cannot be null</returns>
        protected virtual ElementSizeInfo GetHierarchicalElementSizeInfo(ICircuitGroupType<TElement, TWire, TPin> group, IAtfGraphics g)
        {
            if (!group.Expanded) // group element collapsed, treat like non-Hierarchical node
                return GetElementSizeInfo(group, g, GetElementTitle(group.As<TElement>()));



            Rectangle grpBounds;
            if (group.AutoSize)
            {
                // measure size of the expanded group element 
                grpBounds = new Rectangle(); // always start with origin for sub-contents area
                foreach (var subNode in group.SubNodes)
                {

                    ElementSizeInfo sizeInfo = subNode.Is<ICircuitGroupType<TElement, TWire, TPin>>()
                                                   ? GetHierarchicalElementSizeInfo(
                                                       subNode.Cast
                                                           <ICircuitGroupType<TElement, TWire, TPin>>(),
                                                       g)
                                                   : GetElementSizeInfo(subNode.Type, g, GetElementTitle(subNode));

                    //if this is the first time through, then lets just set the location and size to
                    //be the current node. Then the box will take the minimum space to hold all of the subnodes.
                    if (grpBounds.Width == 0 && grpBounds.Height == 0)
                    {
                        grpBounds.Location = subNode.Bounds.Location;
                        grpBounds.Size = sizeInfo.Size;
                    }
                    else
                    {
                        grpBounds = Rectangle.Union(grpBounds, new Rectangle(subNode.Bounds.Location, sizeInfo.Size));
                    }
                }

                // compensate group pin positions
                int yMin = int.MaxValue;
                int yMax = int.MinValue;
                foreach (var pin in group.Inputs)
                {
                    var grpPin = pin.Cast<ICircuitGroupPin<TElement>>();
                    if (grpPin.Bounds.Location.Y < yMin)
                        yMin = grpPin.Bounds.Location.Y;
                    if (grpPin.Bounds.Location.Y > yMax)
                        yMax = grpPin.Bounds.Location.Y;
                }
                if (yMin == int.MaxValue) // if no visible input pins
                    yMin = grpBounds.Y;

                foreach (var pin in group.Outputs)
                {
                    var grpPin = pin.Cast<ICircuitGroupPin<TElement>>();
                    if (grpPin.Bounds.Location.Y < yMin)
                        yMin = grpPin.Bounds.Location.Y;
                    if (grpPin.Bounds.Location.Y > yMax)
                        yMax = grpPin.Bounds.Location.Y;
                }
                if (yMax == int.MinValue) // if no visible output pins
                    yMax = grpBounds.Y + grpBounds.Height;

                int width = grpBounds.Width + m_subContentOffset.X;
                int height = Math.Max(yMax - yMin, grpBounds.Height);
                height += LabelHeight; // compensate that the  bottom of sub-nodes may have non-empty names

                if (group.Info.MinimumSize.Width > width)
                    width = group.Info.MinimumSize.Width;
                if (group.Info.MinimumSize.Height > height)
                    height = group.Info.MinimumSize.Height;


                grpBounds = Rectangle.Union(grpBounds,
                                            new Rectangle(grpBounds.Location.X, yMin, width, height));

            }
            else
            {
                grpBounds = group.Bounds;
            }


            // measure offset of right pins
            int outputCount = group.Outputs.Count();
            int[] grpOutputLeftX = new int[outputCount];
            for (int i = 0; i < grpOutputLeftX.Length; i++)
            {
                SizeF labelSize = g.MeasureText(group.Outputs[i].Name, m_theme.TextFormat);
                grpOutputLeftX[i] = grpBounds.Size.Width - m_pinMargin - m_pinSize - (int)labelSize.Width;

            }

            if (group.AutoSize)
            {
                bool includeHMargin = group.Info.MinimumSize.IsEmpty;
                bool includeVMargin = includeHMargin;
                if (!group.Info.MinimumSize.IsEmpty) // MinimumSize is set, 
                {
                    if (group.Info.MinimumSize.Width >= grpBounds.Size.Width)
                        includeHMargin = false;
                    if (group.Info.MinimumSize.Height >= grpBounds.Size.Height)
                        includeVMargin = false;
                }
                if (includeHMargin || includeVMargin)
                {
                    return new ElementSizeInfo(new Size(grpBounds.Size.Width + (includeHMargin ? m_subContentOffset.X : 0),
                                                        grpBounds.Size.Height + (includeVMargin ? m_subContentOffset.Y : 0)),
                                               grpBounds,
                                               grpOutputLeftX);
                }

                return new ElementSizeInfo(grpBounds.Size,
                                               grpBounds,
                                               grpOutputLeftX);
            }

            return new ElementSizeInfo(
                grpBounds.Size,
                new Rectangle(0, 0, 0, 0),
                grpOutputLeftX);

        }
        
        //TODO: 1676-1780

        /// <summary>
        /// Gets pin offset</summary>
        /// <param name="element">Element containing pin</param>
        /// <param name="pinIndex">Pin index</param>
        /// <param name="inputSide">True if pin side is input, false for output side</param>
        /// <returns>Pin offset</returns>
        public virtual int GetPinOffset(ICircuitElement element, int pinIndex, bool inputSide)
        {
            if (inputSide)
            {
                if (pinIndex < element.Type.Inputs.Count())
                {
                    var pin = element.Type.Inputs[pinIndex];
                    if (pin.Is<ICircuitGroupPin<TElement>>())
                    {
                        var group = element.Cast<ICircuitGroupType<TElement, TWire, TPin>>();
                        if (group.Expanded)
                        {
                            var grpPin = pin.Cast<ICircuitGroupPin<TElement>>();
                            return grpPin.Bounds.Location.Y + m_groupPinExpandedOffset + group.Info.Offset.Y;
                        }
                    }
                }
            }
            else
            {
                if (pinIndex < element.Type.Outputs.Count())
                {
                    var pin = element.Type.Outputs[pinIndex];
                    if (pin.Is<ICircuitGroupPin<TElement>>())
                    {
                        var group = element.Cast<ICircuitGroupType<TElement, TWire, TPin>>();
                        if (group.Expanded)
                        {
                            var grpPin = pin.Cast<ICircuitGroupPin<TElement>>();
                            return grpPin.Bounds.Location.Y + m_groupPinExpandedOffset + group.Info.Offset.Y;
                        }
                    }
                }
            }

            return m_rowSpacing + 2 * m_pinMargin + pinIndex * m_rowSpacing + m_pinOffset + m_pinSize / 2;
        }

        private void SetPinSpacing()
        {
            m_pinMargin = m_theme.PinMargin;
            m_rowSpacing = m_theme.RowSpacing;
            m_pinOffset = m_theme.PinOffset;
            m_pinSize = m_theme.PinSize;
            m_groupPinExpandedOffset = 2 * m_rowSpacing;
            if (!m_subContentOffseExternalSet)
                m_subContentOffset = new Point(m_rowSpacing + 4 * m_pinMargin, m_rowSpacing + 4 * m_pinMargin);
        }

        // Get the element bounds, not including the space for the label below the element.
        private RectangleF GetElementBounds(TElement element, IAtfGraphics g)
        {
            ElementTypeInfo info = GetElementTypeInfo(element, g);
            return new RectangleF(element.Bounds.Location, info.Size);
        }

        //TODO: 1845-1933

        private RectangleF GetPointsBounds(IEnumerable<PointF> points)
        {
            var pts = points.ToArray();
            float minX = points.Min(p => p.X);
            float minY = points.Min(p => p.Y);
            float maxX = points.Max(p => p.X);
            float maxY = points.Max(p => p.Y);

            return new RectangleF(new PointF(minX, minY), new SizeF(maxX - minX, maxY - minY));
        }

        /// <summary>
        /// Gets expander rectangle</summary>
        /// <param name="p">Upper-left corner point of expander</param>
        /// <returns>Expander rectangle</returns>
        protected RectangleF GetExpanderRect(PointF p)
        {
            return new RectangleF(p.X + m_pinMargin + 1, p.Y + 2 * m_pinMargin + 1, ExpanderSize, ExpanderSize);
        }

        /// <summary>
        /// Gets or sets margin offset to be added to draw all sub-elements when the group is expanded inline</summary>
        public Point SubContentOffset
        {
            get { return m_subContentOffset; }
            set
            {
                m_subContentOffset = value;
                m_subContentOffseExternalSet = true;
            }
        }

        /// <summary>
        /// Gets or sets vertical (Y) offset to be added to draw group pins on the border of the group 
        /// when the group is expanded inline</summary>
        public int GroupPinExpandedOffset
        {
            get { return m_groupPinExpandedOffset; }
            set { m_groupPinExpandedOffset = value; }
        }

        /// <summary>
        /// Gets title height at the top of an element</summary>
        public int TitleHeight
        {
            get { return m_rowSpacing + m_pinMargin; }
        }

        /// <summary>
        /// Gets label height at the bottom of an element</summary>
        public int LabelHeight
        {
            get { return m_rowSpacing + m_pinMargin; }
        }

        // The upper-left corner of the sub-nodes and floating pinYs defines the origin offset of sub-contents
        // relative to the containing group. This offset is used when expanding groups, 
        // so that the contained sub-nodes are drawn within the expanded space
        private Point SubGraphOffset(TElement element)
        {
            Point offset = Point.Empty;
            if (element.Is<ICircuitGroupType<TElement, TWire, TPin>>())
            {
                var group = element.Cast<ICircuitGroupType<TElement, TWire, TPin>>();
                offset = group.Info.Offset;
            }

            return offset;
        }

        /// <summary>
        /// Accumulated world offset of given drawing stack</summary>
        /// <param name="graphPath">Elements in drawing stack</param>
        /// <returns>Accumulated world offset of elements</returns>
        public Point WorldOffset(IEnumerable<TElement> graphPath)
        {

            Point offset = new Point();
            Point nodeDelta = Point.Empty;
            foreach (var element in graphPath)
            {
                nodeDelta = SubGraphOffset(element);

                offset.X += element.Bounds.Location.X + nodeDelta.X;
                offset.Y += element.Bounds.Location.Y + nodeDelta.Y;

                // nested subgraph contents  offset
                offset.Offset(m_subContentOffset);
            }

            return offset;

        }

        /// <summary>
        /// Accumulated world offset of current drawing stack</summary>
        public Point CurrentWorldOffset
        {
            get { return WorldOffset(m_graphPath); }
        }

        /// <summary>
        /// Gets or sets the maximum length of a pin name on a collapsed circuit group. If a pin name is longer than
        /// this when the group is collapsed, then the pin name will be truncated and shown with an ellipsis. The
        /// default is 25 characters and the minimum is 5 characters.</summary>
        public int MaxCollapsedGroupPinNameLength
        {
            get { return m_maxCollapsedGroupPinNameLength; }
            set
            {
                if (value < 5)
                    throw new ArgumentOutOfRangeException("the minimum value is 5");
                m_maxCollapsedGroupPinNameLength = value;
                m_truncatedPinNameSubstringLength = (value - 3) / 2;
            }
        }

        /// <summary>
        /// accumulated offset of next to top drawing stack
        /// </summary>
        private Point ParentWorldOffset(IEnumerable<TElement> graphPath)
        {

            var offset = new Point();
            Point node_delta = Point.Empty;

            foreach (var element in graphPath.Skip(1)) // m_graphPath is a stack, top item is the most nested 
            {
                node_delta = SubGraphOffset(element);

                offset.X += element.Bounds.Location.X + node_delta.X;
                offset.Y += element.Bounds.Location.Y + node_delta.Y;

                // nested subgraph contents  offset
                int margin = m_rowSpacing + 4 * m_pinMargin;
                offset.Offset(margin, margin);
            }

            return offset;

        }

        private void DocumentRegistryOnDocumentRemoved(object sender, ItemRemovedEventArgs<IDocument> itemRemovedEventArgs)
        {
            m_cachePerDocument.Remove(itemRemovedEventArgs.Item);
        }

        private void DocumentRegistryOnActiveDocumentChanging(object sender, EventArgs eventArgs)
        {
            if (m_documentRegistry.ActiveDocument != null)
                m_cachePerDocument[m_documentRegistry.ActiveDocument] = m_elementTypeCache;
        }

        private void DocumentRegistryOnActiveDocumentChanged(object sender, EventArgs eventArgs)
        {
            if (m_documentRegistry.ActiveDocument == null ||
                !m_cachePerDocument.TryGetValue(m_documentRegistry.ActiveDocument, out m_elementTypeCache))
            {
                m_elementTypeCache = new Dictionary<ICircuitElementType, ElementTypeInfo>();
            }
        }

        #region Private Classes

        /// <summary>
        /// Class to hold cached element type layout, in pixels (or DIPs)</summary>
        protected class ElementTypeInfo
        {
            /// <summary>Element title</summary>
            public string Title;
            /// <summary>Element size</summary>
            public Size Size;
            /// <summary>Element interior size</summary>
            public Rectangle Interior;
            /// <summary>Array of horizontal offsets of output pins in pixels</summary>
            public int[] OutputLeftX;

            // The following are properties of the ICircuitElementType that reflect info in the cached object
            // If any of these differ from the cached object the cached object should be invalidated so it can be re-drawn to reflect these changes.
            /// <summary>
            /// Number of input pins. Property of the ICircuitElementType that reflects info in the cached object.</summary>
            public int numInputs;
            /// <summary>
            /// Number of output pins. Property of the ICircuitElementType that reflects info in the cached object.</summary>
            public int numOutputs;
        }

        /// <summary>
        /// Size info for a CircuitElement type</summary>
        public class ElementSizeInfo
        {
            /// <summary>
            /// Constructor for gathering size information, in pixels (or DIPs)</summary>
            /// <param name="size">Element size in pixels</param>
            /// <param name="interior">Element interior rectangle in pixels</param>
            /// <param name="outputLeftX">Horizontal offset of output pins in pixels</param>
            public ElementSizeInfo(Size size, Rectangle interior, IEnumerable<int> outputLeftX)
            {
                m_size = size;
                m_interior = interior;
                m_outputLeftX = outputLeftX;
            }

            /// <summary>
            /// Gets the size in pixels. The size includes the whole visual circuit element
            /// but not the label below it.</summary>
            public Size Size { get { return m_size; } }

            /// <summary>
            /// Gets the interior rectangle in pixels</summary>
            public Rectangle Interior { get { return m_interior; } }

            /// <summary>
            /// Gets the horizontal offset of output pins in pixels</summary>
            public IEnumerable<int> OutputLeftX { get { return m_outputLeftX; } }

            private readonly Size m_size;
            private readonly Rectangle m_interior;
            private readonly IEnumerable<int> m_outputLeftX;
        }

        #endregion

        /// <summary>
        /// Size of category expanders in pixels</summary>
        protected const int ExpanderSize = GdiUtil.ExpanderSize;
        private IAtfBrush m_subGraphPinBrush;

        //TODO: m_elementBody
        private AtfDiagramTheme m_theme;

        private int m_rowSpacing;
        private int m_pinSize = 8;
        private int m_pinOffset;
        private int m_pinMargin = 2;
        private Point m_subContentOffset;
        private bool m_subContentOffseExternalSet;


        private int m_groupPinExpandedOffset; // the y offset to be added to draw group pins on the border of the group when the group is expanded inline


        private Dictionary<ICircuitElementType, ElementTypeInfo> m_elementTypeCache = new Dictionary<ICircuitElementType, ElementTypeInfo>();
        private readonly Stack<TElement> m_graphPath = new Stack<TElement>(); // current drawing stack
        private readonly Dictionary<IDocument, Dictionary<ICircuitElementType, ElementTypeInfo>> m_cachePerDocument =
            new Dictionary<IDocument, Dictionary<ICircuitElementType, ElementTypeInfo>>();
        private IDocumentRegistry m_documentRegistry;
        private PinStyle m_pinDrawStyle;

        private int m_maxCollapsedGroupPinNameLength;
        private int m_truncatedPinNameSubstringLength;

        private const int MinElementWidth = 4;
        private const int MinElementHeight = 4;
        //private const int HighlightingWidth = 3;
        private const int MaxNameOverhang = 64;
    }
}
