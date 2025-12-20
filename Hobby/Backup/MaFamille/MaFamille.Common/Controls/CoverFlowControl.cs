using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.Generic;

namespace MaFamille.Common.Controls
{
    public delegate void SelectedItemChangedEvent(CoverFlowEventArgs e);
    public delegate void ItemsLoadedEvent();
    public class CoverFlowControl : ItemsControl, INotifyPropertyChanged
    {
        public event SelectedItemChangedEvent SelectedItemChanged;
        public event ItemsLoadedEvent ItemsLoaded;
        private FrameworkElement LayoutRoot;
        private ItemsPresenter ItemsPresenter;
        private Dictionary<object, CoverFlowItemControl> _objectToItemContainer;
        public List<CoverFlowItemControl> items;

        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                IndexSelected(value, false);
            }
        }

        private void IndexSelected(int index, bool mouseclick)
        {
            IndexSelected(index, mouseclick, true);
        }

        private void IndexSelected(int index, bool mouseclick, bool layoutChildren)
        {
            if (items.Count > 0)
            {
                selectedIndex = index;
                if (layoutChildren)
                    LayoutChildren();

                CoverFlowEventArgs e = new CoverFlowEventArgs() { Index = index, Item = items[index].Content, MouseClick = mouseclick };

                if (SelectedItemChanged != null && mouseclick)
                    SelectedItemChanged(e);
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedIndex"));
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedItem"));
                }
            }
        }

        public object SelectedItem
        {
            get
            {

                return items.Count > 0 ? items[SelectedIndex].Content : null;
            }
            set
            {
                CoverFlowItemControl o = GetItemContainerForObject(value);
                if (o != null)
                    SelectedIndex = items.IndexOf(o);
            }
        }

        #region SpaceBetweenItems (DependencyProperty) (l)

        public double SpaceBetweenItems
        {
            get { return (double)GetValue(SpaceBetweenItemsProperty); }
            set { SetValue(SpaceBetweenItemsProperty, value); }
        }
        public static readonly DependencyProperty SpaceBetweenItemsProperty =
            DependencyProperty.Register("SpaceBetweenItems", typeof(double), typeof(CoverFlowControl), new PropertyMetadata(60d, new PropertyChangedCallback(CoverFlowControl.OnValuesChanged)));

        #endregion

        #region SpaceBetweenSelectedItemAndItems (DependencyProperty) (k)

        public double SpaceBetweenSelectedItemAndItems
        {
            get { return (double)GetValue(SpaceBetweenSelectedItemAndItemsProperty); }
            set { SetValue(SpaceBetweenSelectedItemAndItemsProperty, value); }
        }
        public static readonly DependencyProperty SpaceBetweenSelectedItemAndItemsProperty =
            DependencyProperty.Register("SpaceBetweenSelectedItemAndItems", typeof(double), typeof(CoverFlowControl), new PropertyMetadata(140d, new PropertyChangedCallback(CoverFlowControl.OnValuesChanged)));

        #endregion

        #region RotationAngle (DependencyProperty) (r)

        public double RotationAngle
        {
            get { return (double)GetValue(RotationAngleProperty); }
            set { SetValue(RotationAngleProperty, value); }
        }
        public static readonly DependencyProperty RotationAngleProperty =
            DependencyProperty.Register("RotationAngle", typeof(double), typeof(CoverFlowControl), new PropertyMetadata(45d, new PropertyChangedCallback(CoverFlowControl.OnValuesChanged)));

        #endregion

        #region ZDistance (DependencyProperty) (z)

        public double ZDistance
        {
            get { return (double)GetValue(ZDistanceProperty); }
            set { SetValue(ZDistanceProperty, value); }
        }
        public static readonly DependencyProperty ZDistanceProperty =
            DependencyProperty.Register("ZDistance", typeof(double), typeof(CoverFlowControl), new PropertyMetadata(0.0d, new PropertyChangedCallback(CoverFlowControl.OnValuesChanged)));

        #endregion

        #region Scale (DependencyProperty) (s)

        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }
        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register("Scale", typeof(double), typeof(CoverFlowControl), new PropertyMetadata(.7d, new PropertyChangedCallback(CoverFlowControl.OnValuesChanged)));

        #endregion

        #region SingleItemDuration (DependencyProperty)

        public Duration SingleItemDuration
        {
            get { return (Duration)GetValue(SingleItemDurationProperty); }
            set { SetValue(SingleItemDurationProperty, value); }
        }
        public static readonly DependencyProperty SingleItemDurationProperty =
            DependencyProperty.Register("SingleItemDuration", typeof(Duration), typeof(CoverFlowControl), null);

        #endregion

        #region SingleItemDuration (DependencyProperty)

        public Duration PageDuration
        {
            get { return (Duration)GetValue(PageDurationProperty); }
            set { SetValue(PageDurationProperty, value); }
        }
        public static readonly DependencyProperty PageDurationProperty =
            DependencyProperty.Register("PageDuration", typeof(Duration), typeof(CoverFlowControl), null);

        #endregion

        #region EasingFunction (DependencyProperty)

        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }
        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(CoverFlowControl), null);

        #endregion

        private double k { get { return SpaceBetweenSelectedItemAndItems; } }
        private double l { get { return SpaceBetweenItems; } }
        private double r { get { return RotationAngle; } }
        private double z { get { return ZDistance; } }
        private Duration duration;


        public CoverFlowControl()
        {
            DefaultStyleKey = typeof(CoverFlowControl);
            items = new List<CoverFlowItemControl>();
            SingleItemDuration = new Duration(TimeSpan.FromMilliseconds(600));
            PageDuration = new Duration(TimeSpan.FromMilliseconds(900));
            duration = SingleItemDuration;
            EasingFunction = new CubicEase();
        }

        #region ItemContainer methods

        protected CoverFlowItemControl GetItemContainerForObject(object value)
        {
            CoverFlowItemControl item = value as CoverFlowItemControl;
            if (item == null)
            {
                this.ObjectToItemContainer.TryGetValue(value, out item);
            }
            return item;
        }

        protected Dictionary<object, CoverFlowItemControl> ObjectToItemContainer
        {
            get
            {
                if (this._objectToItemContainer == null)
                {
                    this._objectToItemContainer = new Dictionary<object, CoverFlowItemControl>();
                }
                return this._objectToItemContainer;
            }
        }

        #endregion

        private static void OnValuesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CoverFlowControl).LayoutChildren();
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            CoverFlowItemControl item = new CoverFlowItemControl();
            return item;
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is CoverFlowItemControl);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            LayoutRoot = (FrameworkElement)GetTemplateChild("LayoutRoot");
            ItemsPresenter = (ItemsPresenter)GetTemplateChild("ItemsPresenter");
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right || e.Key == Key.D)
            {
                NextItem();
                e.Handled = true;
            }
            else if (e.Key == Key.Left || e.Key == Key.A)
            {
                PreviousItem();
                e.Handled = true;
            }
            else if (e.Key == Key.PageDown || e.Key == Key.S || e.Key == Key.Down)
            {
                NextPage();
                e.Handled = true;
            }
            else if (e.Key == Key.PageUp || e.Key == Key.W || e.Key == Key.Up)
            {
                PreviousPage();
                e.Handled = true;
            }
            else if (e.Key == Key.Home || e.Key == Key.Q)
            {
                First();
                e.Handled = true;
            }
            else if (e.Key == Key.End || e.Key == Key.E)
            {
                Last();
                e.Handled = true;
            }
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            CoverFlowItemControl item2 = element as CoverFlowItemControl;
            if (item2 != item)
            {
                this.ObjectToItemContainer[item] = item2;
            }
            if (!items.Contains(item2))
            {
                items.Add(item2);                
                //item2.ItemSelected += new EventHandler(item2_ItemSelected);
                item2.ItemSelected += new CoverFlowItemControl.ItemSelectedHandler(item2_ItemSelected);
                item2.SizeChanged += new SizeChangedEventHandler(item2_SizeChanged);
            }
            if (items.Count == 1)
            {
                IndexSelected(0, false, false);
                if(ItemsLoaded!=null)
                    ItemsLoaded();
            }
            
        }

        void item2_ItemSelected(object sender, CoverFlowEventArgs e)
        {
            CoverFlowItemControl item = sender as CoverFlowItemControl;
            if (item == null)
                return;
            int index = items.IndexOf(item);
            if (index >= 0)
                IndexSelected(index, e.MouseClick);
            //SelectedIndex = index;
        }

        void item2_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CoverFlowItemControl item = sender as CoverFlowItemControl;
            int index = items.IndexOf(item);
            LayoutChild(item, index);
        }

        //void item2_ItemSelected(object sender, EventArgs e)
        //{
        //    CoverFlowItemControl item = sender as CoverFlowItemControl;
        //    if (item == null)
        //        return;
        //    int index = items.IndexOf(item);
        //    if (index >= 0)
        //        IndexSelected(index, true);
        //    //SelectedIndex = index;

        //}

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            base.ClearContainerForItemOverride(element, item);
            CoverFlowItemControl item2 = element as CoverFlowItemControl;
            if (item2 != item)
            {
                this.ObjectToItemContainer.Remove(item);
            }
            items.Remove(item2);
        }


        protected void LayoutChildren()
        {
            for (int i = 0; i < items.Count; i++)
            {
                LayoutChild(items[i], i);
            }
        }

        protected void LayoutChild(CoverFlowItemControl item, int index)
        {
            double m = ItemsPresenter.ActualWidth / 2;

            int b = index - SelectedIndex;
            double mu = 0;
            if (b < 0)
                mu = -1;
            else if (b > 0)
                mu = 1;
            double x = (m + ((double)b * l + (k * mu))) - (item.ActualWidth / 2);

            double s = mu == 0 ? 1 : Scale;

            int zindex = items.Count - Math.Abs(b);

            if (((x + item.ActualWidth) < 0 || x > ItemsPresenter.ActualWidth)
                && ((item.X + item.ActualWidth) < 0 || item.X > ItemsPresenter.ActualWidth)
                && !((x + item.ActualWidth) < 0 && item.X > ItemsPresenter.ActualWidth)
                && !((item.X + item.ActualWidth) < 0 && x > ItemsPresenter.ActualWidth))
            {
                item.SetValues(x, zindex, r * mu, z * Math.Abs(mu), s, duration, EasingFunction, false);
            }
            else
            {
                item.SetValues(x, zindex, r * mu, z * Math.Abs(mu), s, duration, EasingFunction, true);
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Size size = base.ArrangeOverride(finalSize);
            RectangleGeometry visibleArea = new RectangleGeometry();
            Rect clip = new Rect(0, 0, ItemsPresenter.ActualWidth, ItemsPresenter.ActualHeight);
            foreach (CoverFlowItemControl item in items)
            {
                item.Height = ItemsPresenter.ActualHeight;
            }
            visibleArea.Rect = clip;
            ItemsPresenter.Clip = visibleArea;

            double m = ItemsPresenter.ActualWidth / 2;

            for (int index = 0; index < items.Count; index++)
            {
                CoverFlowItemControl item = items[index];
                int b = index - SelectedIndex;
                double mu = 0;
                if (b < 0)
                    mu = -1;
                else if (b > 0)
                    mu = 1;
                double x = (m + ((double)b * l + (k * mu))) - (item.ActualWidth / 2);

                double s = mu == 0 ? 1 : Scale;

                int zindex = items.Count - Math.Abs(b);

                item.X = x;
                item.YRotation = r * mu;
                item.ZOffset = z * Math.Abs(mu);
                item.Scale = s;
            }

            return size;
        }

        public void NextItem()
        {
            if (SelectedIndex < items.Count - 1)
            {
                duration = SingleItemDuration;
                SelectedIndex++;
            }
        }
        public void PreviousItem()
        {
            if (SelectedIndex > 0)
            {
                duration = SingleItemDuration;
                SelectedIndex--;
            }
        }

        public void NextPage()
        {
            if (SelectedIndex == items.Count - 1)
                return;

            duration = PageDuration;
            int i = GetPageCount();
            if (SelectedIndex + i >= items.Count)
                SelectedIndex = items.Count - 1;
            else
                SelectedIndex += i;
        }
        public void PreviousPage()
        {
            if (SelectedIndex == 0)
                return;
            duration = PageDuration;
            int i = GetPageCount();
            if (SelectedIndex - i < 0)
                SelectedIndex = 0;
            else
                SelectedIndex -= i;
        }

        protected int GetPageCount()
        {
            double m = ItemsPresenter.ActualWidth / 2;
            m -= k;
            return (int)(m / l);
        }

        public void First()
        {
            if (items.Count == 0)
                return;
            duration = PageDuration;
            SelectedIndex = 0;
        }
        public void Last()
        {
            if (items.Count == 0)
                return;
            duration = PageDuration;
            SelectedIndex = items.Count - 1;
        }

        public void UpdatePositions()
        {
            LayoutChildren();
        }

        public void UpdatePositions(object value)
        {
            CoverFlowItemControl item = GetItemContainerForObject(value);
            if (item == null)
                return;

            int index = items.IndexOf(item);
            LayoutChild(item, index);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

}
