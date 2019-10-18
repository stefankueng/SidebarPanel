using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace SidebarPanel.UserControls
{
    public class SidebarPanelItem
    {
        public UIElement UIItem { get; set; }
    }

    public sealed partial class SidebarPanel : UserControl
    {
        private int leftIsPinned = 0;
        private int rightIsPinned = 0;
        private TappedEventHandler MainContentTappedHandler = null;

        public SidebarPanel()
        {
            this.InitializeComponent();
        }

        public UIElement MainContent
        {
            get { return (UIElement)GetValue(MainContentProperty); }
            set { SetValue(MainContentProperty, value); }
        }

        public static UIElement GetMainContent(DependencyObject obj)
        {
            return (UIElement)obj.GetValue(MainContentProperty);
        }

        public static void SetMainContent(DependencyObject obj, UIElement value)
        {
            obj.SetValue(MainContentProperty, value);
        }

        public static readonly DependencyProperty MainContentProperty =
            DependencyProperty.RegisterAttached("MainContent", typeof(UIElement), typeof(SidebarPanel), new PropertyMetadata(0));

        public List<SidebarPanelItem> LeftContentItems
        {
            get
            {
                var spi = new List<SidebarPanelItem>();
                foreach (var s in LeftContent)
                {
                    if (s is SidebarItem sbi)
                    {
                        sbi.PropertyChanged += LeftItemPropertyChanged;
                        sbi.IsPinned = false;
                    }

                    spi.Add(new SidebarPanelItem()
                    {
                        UIItem = s
                    });
                }
                return spi;
            }
        }

        public Collection<UIElement> LeftContent
        {
            get { return (Collection<UIElement>)GetValue(LeftContentProperty); }
            set
            {
                SetValue(LeftContentProperty, value);
            }
        }

        public static Collection<UIElement> GetLeftContent(DependencyObject obj)
        {
            return (Collection<UIElement>)obj.GetValue(LeftContentProperty);
        }

        public static void SetLeftContent(DependencyObject obj, Collection<UIElement> value)
        {
            obj.SetValue(LeftContentProperty, value);
        }

        public static readonly DependencyProperty LeftContentProperty =
            DependencyProperty.RegisterAttached("LeftContent", typeof(Collection<UIElement>), typeof(SidebarPanel), new PropertyMetadata(new Collection<UIElement>(), OnLeftContentPropertyChanged));

        private static void OnLeftContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                if (e.OldValue is Collection<UIElement> c)
                {
                    c.Clear();
                }
            }
        }

        public List<SidebarPanelItem> RightContentItems
        {
            get
            {
                var spi = new List<SidebarPanelItem>();
                foreach (var s in RightContent)
                {
                    if (s is SidebarItem sbi)
                    {
                        sbi.PropertyChanged += RightItemPropertyChanged;
                        sbi.IsPinned = false;
                    }

                    spi.Add(new SidebarPanelItem()
                    {
                        UIItem = s
                    });
                }
                return spi;
            }
        }

        public Collection<UIElement> RightContent
        {
            get { return (Collection<UIElement>)GetValue(RightContentProperty); }
            set { SetValue(RightContentProperty, value); }
        }

        public static Collection<UIElement> GetRightContent(DependencyObject obj)
        {
            return (Collection<UIElement>)obj.GetValue(RightContentProperty);
        }

        public static void SetRightContent(DependencyObject obj, Collection<UIElement> value)
        {
            obj.SetValue(RightContentProperty, value);
        }

        public static readonly DependencyProperty RightContentProperty =
            DependencyProperty.RegisterAttached("RightContent", typeof(Collection<UIElement>), typeof(SidebarPanel), new PropertyMetadata(new Collection<UIElement>(), OnRightContentPropertyChanged));

        private static void OnRightContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                if (e.OldValue is Collection<UIElement> c)
                {
                    c.Clear();
                }
            }
        }

        private void leftSidebarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.DataContext is SidebarPanelItem sbp)
                {
                    LeftSidebarItem.Children.Clear();
                    LeftSidebarItem.Children.Add(sbp.UIItem);
                    LeftSidebarItem.Visibility = Visibility.Visible;
                    gridSplitterLeftPanel.Visibility = Visibility.Visible;
                }
            }
            leftContent.Visibility = Visibility.Visible;
        }

        private void LeftItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsPinned")
            {
                if (sender is SidebarItem sbi)
                {
                    if (sbi.IsPinned)
                    {
                        columnWithPinnedLeftSidebarPanel.Width = new GridLength(columnWithLeftSidebarPanel.Width.Value + 2);
                        ++leftIsPinned;
                    }
                    else
                    {
                        LeftSidebarItem.Visibility = Visibility.Collapsed;
                        gridSplitterLeftPanel.Visibility = Visibility.Collapsed;
                        columnWithPinnedLeftSidebarPanel.Width = new GridLength(0);
                        --leftIsPinned;
                        if (leftIsPinned < 0)
                            leftIsPinned = 0;
                    }
                }
            }
        }

        private void RightItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsPinned")
            {
                if (sender is SidebarItem sbi)
                {
                    if (sbi.IsPinned)
                    {
                        columnWithPinnedRightSidebarPanel.Width = new GridLength(columnWithRightSidebarPanel.Width.Value + 2);
                        ++rightIsPinned;
                    }
                    else
                    {
                        RightSidebarItem.Visibility = Visibility.Collapsed;
                        gridSplitterRightPanel.Visibility = Visibility.Collapsed;
                        columnWithPinnedRightSidebarPanel.Width = new GridLength(0);
                        --rightIsPinned;
                        if (rightIsPinned < 0)
                            rightIsPinned = 0;
                    }
                }
            }
        }

        private void rightSidebarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.DataContext is SidebarPanelItem sbp)
                {
                    RightSidebarItem.Children.Clear();
                    RightSidebarItem.Children.Add(sbp.UIItem);
                    RightSidebarItem.Visibility = Visibility.Visible;
                    gridSplitterRightPanel.Visibility = Visibility.Visible;
                }
            }
            rightContent.Visibility = Visibility.Visible;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MainContentTappedHandler = new TappedEventHandler(MainContent_Tapped);
            MainContent.AddHandler(UIElement.TappedEvent, MainContentTappedHandler, true);
        }

        private void MainContent_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (rightIsPinned == 0 && RightSidebarItem.Visibility == Visibility.Visible)
            {
                RightSidebarItem.Visibility = Visibility.Collapsed;
                gridSplitterRightPanel.Visibility = Visibility.Collapsed;
            }
            if (leftIsPinned == 0 && LeftSidebarItem.Visibility == Visibility.Visible)
            {
                LeftSidebarItem.Visibility = Visibility.Collapsed;
                gridSplitterLeftPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            MainContent.RemoveHandler(UIElement.TappedEvent, MainContentTappedHandler);
            MainContentTappedHandler = null;
            foreach (var c in LeftContent)
            {
                if (c is SidebarItem sb)
                {
                    sb.PropertyChanged -= LeftItemPropertyChanged;
                }
            }
            foreach (var c in RightContent)
            {
                if (c is SidebarItem sb)
                {
                    sb.PropertyChanged -= RightItemPropertyChanged;
                }
            }
            LeftContent = null;
            RightContent = null;
        }

        private void gridSplitterLeftPanel_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (leftIsPinned > 0)
                columnWithPinnedLeftSidebarPanel.Width = new GridLength(columnWithLeftSidebarPanel.Width.Value + 2);
        }

        private void gridSplitterRightPanel_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (rightIsPinned > 0)
                columnWithPinnedRightSidebarPanel.Width = new GridLength(columnWithRightSidebarPanel.Width.Value + 2);
        }
    }
}
