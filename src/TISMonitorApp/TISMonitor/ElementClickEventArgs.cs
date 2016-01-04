namespace TISMonitor
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class ElementClickEventArgs : EventArgs
    {
        public TISMonitor.Element Element;
        public System.Windows.Input.MouseEventArgs MouseEventArgs;
        public System.Windows.Point Point;

        public ElementClickEventArgs(TISMonitor.Element el, System.Windows.Point pt, System.Windows.Input.MouseEventArgs mea)
        {
            this.Element = el;
            this.Point = pt;
            this.MouseEventArgs = mea;
        }
    }
}

