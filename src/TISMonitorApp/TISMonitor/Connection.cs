namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    [Serializable]
    public class Connection : Element
    {
        private Element m_Element1;
        private Element m_Element2;
        private Point m_PointCenter;
        public int Source1;
        public int Source2;

        public override bool AllowAddToSchema()
        {
            return false;
        }

        public override OnElementMoveDelegate CanMove(Point p)
        {
            if (Element.IsPtCaptured(p, this.PointCenter))
            {
                return new OnElementMoveDelegate(this.OnMove);
            }
            return null;
        }

        public override void CopyLocationInfoFrom(Element e)
        {
            Debug.Assert(e is Connection);
            if (e is Connection)
            {
                this.m_PointCenter = (e as Connection).PointCenter;
            }
        }

        public override void Draw(Grid g)
        {
        }

        public Element GetConnectedElement(Element e)
        {
            if (this.Element1 == e)
            {
                return this.Element2;
            }
            if (this.Element2 == e)
            {
                return this.Element1;
            }
            return null;
        }

        public bool GetConnectedElement(Element eFor, out Element e, out int nElementSource)
        {
            int num;
            return this.GetConnectedElement(eFor, out num, out e, out nElementSource);
        }

        public bool GetConnectedElement(Element eFor, out int nForElementSource, out Element e, out int nElementSource)
        {
            e = null;
            nElementSource = nForElementSource = -1;
            if (this.Element1 == eFor)
            {
                nForElementSource = this.Source1;
                e = this.Element2;
                nElementSource = this.Source2;
                return true;
            }
            if (this.Element2 == eFor)
            {
                nForElementSource = this.Source2;
                e = this.Element1;
                nElementSource = this.Source1;
                return true;
            }
            return false;
        }

        public override string GetName()
        {
            return XMLResourceLoaderUtils.GetString("CONNECTION");
        }

        public override string GetToolTipText(bool bDetailed)
        {
            string format = "{0} {1} {2} / {3} {4} ";
            return string.Format(format, new object[] { this.GetName(), this.Element1.ID, this.Element1.GetConnectionSourceName(this.Source1), this.Element2.ID, this.Element2.GetConnectionSourceName(this.Source2) });
        }

        public bool HasBothElements(ArrayList alTypes)
        {
            return (alTypes.Contains(this.Element1.GetType()) && alTypes.Contains(this.Element2.GetType()));
        }

        public bool HasElement(Type t)
        {
            return ((this.Element1.GetType() == t) || (this.Element2.GetType() == t));
        }

        public bool HasElement(ArrayList alTypes)
        {
            return (alTypes.Contains(this.Element1.GetType()) || alTypes.Contains(this.Element2.GetType()));
        }

        public bool IsConnected(Element el1, Element el2)
        {
            return (((this.Element1 == el1) && (this.Element2 == el2)) || ((this.Element1 == el2) && (this.Element2 == el1)));
        }

        public PathElement IsConnectedPath(Element e, int nSource)
        {
            PathElement element = null;
            element = (PathElement) this.IsConnectedTo(e, nSource, typeof(Track));
            if (element != null)
            {
                return element;
            }
            element = (PathElement) this.IsConnectedTo(e, nSource, typeof(PointSwitch));
            if (element != null)
            {
                return element;
            }
            return (PathElement) this.IsConnectedTo(e, nSource, typeof(PointSimple));
        }

        public bool IsConnectedSource(Element e, int nSource)
        {
            return (((this.Element1 == e) && (this.Source1 == nSource)) || ((this.Element2 == e) && (this.Source2 == nSource)));
        }

        public Element IsConnectedTo(Element e, int nSource, Type t)
        {
            if (((this.Element1 == e) && (this.Source1 == nSource)) && (this.Element2.GetType() == t))
            {
                return this.Element2;
            }
            if (((this.Element2 == e) && (this.Source2 == nSource)) && (this.Element1.GetType() == t))
            {
                return this.Element1;
            }
            return null;
        }

        public bool IsSegmentsBorder()
        {
            return (((this.Element1 is PathElement) && (this.Element2 is PathElement)) && ((this.Element1 as PathElement).Segment != (this.Element2 as PathElement).Segment));
        }

        public override void Move(Size size)
        {
        }

        private Rectangle OnMove(Point p)
        {
            this.PointCenter = p;
            return new Rectangle(0, 0, 0, 0);
        }

        public override void Reflect(int width, bool h)
        {
            this.PointCenter = h ? Element.GetPointRotatedByY(width, this.PointCenter) : Element.GetPointRotatedByX(width, this.PointCenter);
        }

        public override void SetLocation(Rectangle r)
        {
            this.PointCenter = new Point((double) r.X, (double) r.Y);
        }

        public override void UpdateBounds()
        {
            base.Center = this.m_PointCenter;
        }

        private void UpdateID()
        {
            base.ID = "";
            if (this.Element1 != null)
            {
                base.ID = this.Element1.ID;
            }
            if (this.Element2 != null)
            {
                base.ID = base.ID + "/" + this.Element2.ID;
            }
        }

        public Element Element1
        {
            get
            {
                return this.m_Element1;
            }
            set
            {
                if (value != this.m_Element1)
                {
                    this.m_Element1 = value;
                    this.UpdateID();
                }
            }
        }

        public Element Element2
        {
            get
            {
                return this.m_Element2;
            }
            set
            {
                if (value != this.m_Element2)
                {
                    this.m_Element2 = value;
                    this.UpdateID();
                }
            }
        }

        public Point PointCenter
        {
            get
            {
                return this.m_PointCenter;
            }
            set
            {
                if (this.m_PointCenter != value)
                {
                    this.m_PointCenter = value;
                    this.UpdateBounds();
                }
            }
        }
    }
}

