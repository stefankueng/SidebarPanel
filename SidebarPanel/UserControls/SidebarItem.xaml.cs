using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace SidebarPanel.UserControls
{
    [ContentProperty(Name=nameof(Child))]
    public sealed partial class SidebarItem : UserControl, INotifyPropertyChanged
    {
        public SidebarItem()
        {
            this.InitializeComponent();
            Child = new Grid();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public UIElement Child
        {
            get { return (UIElement)GetValue(ChildProperty); }
            set { SetValue(ChildProperty, value); }
        }
        public static UIElement GetChild(DependencyObject obj)
        {
            return (UIElement)obj.GetValue(ChildProperty);
        }

        public static void SetChild(DependencyObject obj, UIElement value)
        {
            obj.SetValue(ChildProperty, value);
        }

        public static readonly DependencyProperty ChildProperty =
            DependencyProperty.RegisterAttached("Child", typeof(UIElement), typeof(SidebarItem), new PropertyMetadata(0));


        private bool _isPinned = true;
        public bool IsPinned
        {
            get { return _isPinned; }
            set
            {
                _isPinned = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsPinned)));
            }
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }

        private void buttonPin_Click(object sender, RoutedEventArgs e)
        {
            IsPinned = !IsPinned;
            imgPinned.Visibility = IsPinned ? Visibility.Visible : Visibility.Collapsed;
            imgUnpinned.Visibility = !IsPinned ? Visibility.Visible : Visibility.Collapsed;
        }
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            IsPinned = false;
            imgPinned.Visibility = Visibility.Collapsed;
            imgUnpinned.Visibility = Visibility.Visible;
        }
    }
}
