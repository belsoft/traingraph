namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class LayoutView : Form
    {
        protected ArrayList alMoveDelegates = new ArrayList();
        public Connection m_ActiveConnection = null;
        public Element m_ActiveElement = null;
        public TrainBase m_ActiveTrain = null;
        protected ArrayList m_alConncetionsSelected = new ArrayList();
        protected ArrayList m_alElementsSelected = new ArrayList();
        protected ArrayList m_alElTypes = null;
        protected bool m_bAllowToolTip;
        private bool m_bCanClose;
        protected bool m_bIsDragingNow;
        public bool m_bReadOnly = true;
        protected bool m_bSelectionStarted = false;
        protected Bitmap m_btCursor;
        protected Cursor m_crDropAllowed;
        protected Cursor m_crDropNotAllowed;
        private Size m_crSize = new Size(64.0, 64.0);
        protected TrainBase m_draggedTrain;
        protected Element m_ElToAdd = null;
        protected Element m_ElToConnect = null;
        protected Font m_fntTrainBigImpact = new Font();
        protected Font m_fntTrainBigNormal = new Font();
        protected Font m_fntTrainSmall = new Font();
        protected Grid m_grCursor;
        protected Layout m_Layout;
        protected MouseEventArgs m_MouseEventArgs = null;
        protected Point m_MovePoint = new Point(0.0, 0.0);
        protected int m_nSourceToConnect;
        protected Rectangle m_rtDraggedSource;
        protected Rectangle m_SelectionRect = new Rectangle(0, 0, 0, 0);
        protected Point m_StartMovePoint = new Point(0.0, 0.0);

        public event TrainPropEventHandler TrainProp;

        public void AddElement(Element e)
        {
            Debug.Assert(this.m_alElTypes.Contains(e.GetType()));
            this.m_ElToAdd = e;
            this.m_ElToAdd.m_Layout = this.m_Layout;
        }

        protected bool AddMoveDelegate(OnElementMoveDelegate d)
        {
            if (!((d == null) || this.alMoveDelegates.Contains(d)))
            {
                this.alMoveDelegates.Add(d);
                return true;
            }
            return false;
        }

        protected void CancelSelection()
        {
            this.m_alElementsSelected.Clear();
            this.m_alConncetionsSelected.Clear();
            if (this.m_bSelectionStarted)
            {
                this.m_bSelectionStarted = false;
                this.Invalidate();
            }
        }

        public virtual void ClearContent()
        {
            this.m_Layout.ClearContent();
            this.SetScrollSize();
            this.Invalidate();
        }

        protected void CursorDragLogic()
        {
            if (this.m_bIsDragingNow)
            {
                this.InitDraggedCursors();
                this.m_bIsDragingNow = false;
            }
        }

        public void DeleteCurrentConnection()
        {
            if (this.m_ActiveConnection != null)
            {
                if (!this.m_ActiveConnection.HasElement(typeof(Light)))
                {
                    OnElementMoveDelegate delegate2 = this.m_ActiveConnection.Element1.CanMove(this.m_ActiveConnection.PointCenter);
                    if (delegate2 != null)
                    {
                        this.m_ActiveConnection.PointCenter = this.m_ActiveConnection.Element1.Center;
                    }
                    else
                    {
                        delegate2 = this.m_ActiveConnection.Element2.CanMove(this.m_ActiveConnection.PointCenter);
                        this.m_ActiveConnection.PointCenter = this.m_ActiveConnection.Element2.Center;
                    }
                    delegate2(this.m_ActiveConnection.PointCenter);
                }
                this.m_ActiveConnection.Element1.Disconnect(this.m_ActiveConnection);
                this.m_ActiveConnection.Element2.Disconnect(this.m_ActiveConnection);
                this.m_Layout.Connections.Remove(this.m_ActiveConnection);
                this.m_ActiveConnection = null;
                this.Invalidate();
            }
        }

        public void DeleteCurrentElement()
        {
            if (this.m_ActiveElement != null)
            {
                ArrayList list = new ArrayList();
                foreach (Connection connection in this.m_ActiveElement.m_Connections)
                {
                    list.Add(connection);
                    connection.GetConnectedElement(this.m_ActiveElement).Disconnect(connection);
                }
                this.m_ActiveElement.Disconnect();
                foreach (Connection connection in list)
                {
                    this.m_Layout.Connections.Remove(connection);
                }
                this.m_Layout.Elements.Remove(this.m_ActiveElement);
                this.m_ActiveElement = null;
                this.SetScrollSize();
                this.Invalidate();
            }
        }

        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr inconHandler);
        private void DisposeDraggedCursors()
        {
        }

        protected virtual bool FindActiveContext(Point p)
        {
            if (this.GetLayout() == null)
            {
                return false;
            }
            string toolTipText = "";
            this.m_ActiveConnection = null;
            this.m_ActiveElement = null;
            if (!this.m_bReadOnly)
            {
                foreach (Connection connection in this.GetLayout().Connections)
                {
                    if (connection.HasElement(this.m_alElTypes) && (connection.CanMove(p) != null))
                    {
                        this.m_ActiveConnection = connection;
                        toolTipText = this.m_ActiveConnection.GetToolTipText(true, p);
                        break;
                    }
                }
            }
            foreach (Element element in this.GetLayout().Elements)
            {
                if (this.m_alElTypes.Contains(element.GetType()) && (Element.IsPtCaptured(p, element.GetCenterPoint()) || (element.CanMove(p) != null)))
                {
                    this.m_ActiveElement = element;
                    if (toolTipText.Length == 0)
                    {
                        toolTipText = this.m_ActiveElement.GetToolTipText(!this.m_bReadOnly, p);
                    }
                    break;
                }
            }
            return ((this.m_ActiveConnection != null) || (this.m_ActiveElement != null));
        }

        public virtual LayoutBase GetLayout()
        {
            return this.m_Layout;
        }

        public virtual ArrayList GetSupportedElementTypes()
        {
            return new ArrayList();
        }

        protected void InitDraggedCursors()
        {
        }

        private void InitializeComponent()
        {
        }

        private void Invalidate()
        {
        }

        private void LayoutView_Click(object sender, EventArgs e)
        {
            this.OnClick(sender, e);
        }

        private void LayoutView_DoubleClick(object sender, EventArgs e)
        {
            this.OnDoubleClick(sender, e);
        }

        private void LayoutView_MouseHover(object sender, EventArgs e)
        {
            this.OnMouseHover(sender, e);
        }

        public virtual void MoveContent(Size size)
        {
            foreach (Element element in this.m_Layout.Elements)
            {
                element.Move(size);
            }
            foreach (Connection connection in this.m_Layout.Connections)
            {
                connection.Move(size);
            }
            this.SetScrollSize();
            this.Invalidate();
        }

        public void MoveElementsSelected(Size size)
        {
            foreach (Element element in this.m_alElementsSelected)
            {
                element.Move(size);
            }
            foreach (Connection connection in this.m_alConncetionsSelected)
            {
                connection.Move(size);
            }
            this.SetScrollSize();
            this.Invalidate();
        }

        protected virtual void OnClick(object sender, EventArgs e)
        {
        }

        protected virtual void OnDoubleClick(object sender, EventArgs e)
        {
            if ((this.m_ActiveElement != null) || (this.m_ActiveTrain != null))
            {
                this.ShowCurrentProperties();
            }
        }

        protected virtual void OnMouseHover(object sender, EventArgs e)
        {
        }

        protected void ResetTimerToolTip()
        {
        }

        public virtual bool SaveLayout()
        {
            return true;
        }

        public virtual bool SetCursor4CurrentElement()
        {
            return true;
        }

        public bool SetElementBaseWidth(int nBaseWidth)
        {
            if (this.m_Layout != null)
            {
                foreach (Element element in this.m_Layout.Elements)
                {
                    element.BaseWidth = nBaseWidth;
                }
                this.Invalidate();
                return true;
            }
            return false;
        }

        public virtual void SetLayout(Layout l)
        {
            this.m_Layout = l;
            this.SetScrollSize();
            this.Invalidate();
        }

        public void SetReadOnly(bool bReadOnly)
        {
            this.m_bReadOnly = bReadOnly;
            this.CancelSelection();
            this.Invalidate();
        }

        public void SetScrollSize()
        {
        }

        public virtual void ShowCurrentProperties()
        {
            if ((this.m_ActiveTrain != null) && (this.TrainProp != null))
            {
                this.TrainProp(this, new TrainPropEventArgs(this.m_ActiveTrain));
            }
            else if (!(this.m_bReadOnly || (this.m_ActiveElement == null)))
            {
            }
        }

        public void TimerToolTip_Tick(object sender, EventArgs e)
        {
            this.m_bAllowToolTip = true;
        }

        public void UpdateElement(Element e)
        {
            this.Invalidate();
        }

        public bool CanClose
        {
            get
            {
                return this.m_bCanClose;
            }
            set
            {
                if (value != this.m_bCanClose)
                {
                    this.m_bCanClose = value;
                }
            }
        }
    }
}

