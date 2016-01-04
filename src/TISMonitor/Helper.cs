namespace TISMonitor
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class Helper
    {
        public static void RegisterForNotification(string property, FrameworkElement frameworkElement, PropertyChangedCallback OnCallBack)
        {
            Binding binding2 = new Binding(property);
            binding2.set_Source(frameworkElement);
            Binding binding = binding2;
            DependencyProperty property2 = DependencyProperty.RegisterAttached("ListenAttached" + property, typeof(object), typeof(UserControl), new PropertyMetadata(OnCallBack));
            frameworkElement.SetBinding(property2, binding);
        }
    }
}

