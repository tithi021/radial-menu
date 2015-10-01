﻿namespace RadialMenuControl.UserControl
{
    using RadialMenuControl.UserControl;
    using System;
    using System.Diagnostics;
    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;

    public sealed partial class PieSlice : UserControl
    {
        // Inner Arc Colors
        public static readonly DependencyProperty InnerNormalColorProperty =
            DependencyProperty.Register("InnerNormalColor", typeof(Color), typeof(PieSlice), null);

        public static readonly DependencyProperty InnerHoverColorProperty =
            DependencyProperty.Register("InnerHoverColor", typeof(Color), typeof(PieSlice), null);

        public static readonly DependencyProperty InnerTappedColorProperty =
            DependencyProperty.Register("InnerTappedColor", typeof(Color), typeof(PieSlice), null);

        public static readonly DependencyProperty InnerReleasedColorProperty =
            DependencyProperty.Register("InnerReleasedColor", typeof(Color), typeof(PieSlice), null);

        public Color InnerHoverColor
        {
            get { return (Color)GetValue(InnerHoverColorProperty); }
            set { SetValue(InnerHoverColorProperty, value); }
        }

        public Color InnerNormalColor
        {
            get { return (Color)GetValue(InnerNormalColorProperty); }
            set { SetValue(InnerNormalColorProperty, value); }
        }

        public Color InnerTappedColor
        {
            get { return (Color)GetValue(InnerTappedColorProperty); }
            set { SetValue(InnerTappedColorProperty, value); }
        }
        public Color InnerReleasedColor
        {
            get { return (Color)GetValue(InnerReleasedColorProperty); }
            set { SetValue(InnerReleasedColorProperty, value); }
        }

        // Outer Arc Colors
        public static readonly DependencyProperty OuterNormalColorProperty =
            DependencyProperty.Register("OuterNormalColor", typeof(Color), typeof(PieSlice), null);

        public static readonly DependencyProperty OuterHoverColorProperty =
            DependencyProperty.Register("OuterHoverColor", typeof(Color), typeof(PieSlice), null);

        public static readonly DependencyProperty OuterTappedColorProperty =
            DependencyProperty.Register("OuterTappedColor", typeof(Color), typeof(PieSlice), null);

        public static readonly DependencyProperty OuterDisabledColorProperty =
            DependencyProperty.Register("OuterDisabledColor", typeof(Color), typeof(PieSlice), null);

        public static readonly DependencyProperty IconImageProperty =
            DependencyProperty.Register("IconImage", typeof(ImageSource), typeof(PieSlice), null);

        public static readonly DependencyProperty IconImageSideLengthProperty =
            DependencyProperty.Register("IconImageSideLength", typeof(ImageSource), typeof(PieSlice), null);

        public Color OuterHoverColor
        {
            get { return (Color)GetValue(OuterHoverColorProperty); }
            set { SetValue(OuterHoverColorProperty, value); }
        }

        public Color OuterNormalColor
        {
            get { return (Color)GetValue(OuterNormalColorProperty); }
            set { SetValue(OuterNormalColorProperty, value); }
        }

        public Color OuterDisabledColor
        {
            get { return (Color)GetValue(OuterDisabledColorProperty); }
            set { SetValue(OuterDisabledColorProperty, value); }
        }

        public Color OuterTappedColor
        {
            get { return (Color)GetValue(OuterTappedColorProperty); }
            set { SetValue(OuterTappedColorProperty, value); }
        }

        // Angles & Radius
        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof(double), typeof(PieSlice), null);

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(PieSlice), null);

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(double), typeof(PieSlice), null);

        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        // Label
        public static readonly DependencyProperty HideLabelProperty =
            DependencyProperty.Register("HideLabel", typeof(bool), typeof(PieSlice), null);

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(PieSlice), null);

        public static readonly DependencyProperty LabelSizeProperty =
            DependencyProperty.Register("LabelSize", typeof(int), typeof(PieSlice), null);

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(PieSlice), null);

        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(int), typeof(PieSlice), null);

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public int LabelSize
        {
            get { return (int)GetValue(LabelSizeProperty); }
            set { SetValue(LabelSizeProperty, value); }
        }

        public bool HideLabel
        {
            get { return (bool)GetValue(HideLabelProperty); }
            set { SetValue(HideLabelProperty, value); }
        }

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public int IconSize
        {
            get { return (int)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        public ImageSource IconImage
        {
            get { return (ImageSource)GetValue(IconImageProperty); }
            set { SetValue(IconImageProperty, value); }
        }

        public double IconImageSideLength
        {
            get
            {
                return (double)GetValue(IconImageSideLengthProperty);
            }
            set
            {
                SetValue(IconImageSideLengthProperty, value);
            }
        }

        public Visibility TextBlockVisibility => this.IconImage == null ? Visibility.Visible : Visibility.Collapsed;

        // Pass in the original RadialMenuButton
        public Components.RadialMenuButton OriginalRadialMenuButton;
        public static readonly DependencyProperty OriginalRadialMenuButtonProperty =
            DependencyProperty.Register("OriginalRadialMenuButton", typeof(Components.RadialMenuButton), typeof(PieSlice), null);

        // Change Menu
        public delegate void ChangeMenuRequestHandler(object sender, RadialMenu submenu);
        public event ChangeMenuRequestHandler ChangeMenuRequestEvent;

        private void OnChangeMenuRequest(object s, RadialMenu sm)
        {
            ChangeMenuRequestEvent?.Invoke(s, sm);
        }

        public PieSlice()
        {
            this.InitializeComponent();
            this.DataContext = this;

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            // Setup outer arc
            OuterPieSlicePath.Radius = this.Radius;
            OuterPieSlicePath.StartAngle = this.StartAngle;
            OuterPieSlicePath.Angle = this.Angle;
            var middleRadian = (Math.PI / 180) * (this.StartAngle + (this.Angle / 2));

            if (OriginalRadialMenuButton.Submenu == null && !OriginalRadialMenuButton.HasOuterArcEvents())
            {
                OuterPieSlicePath.Fill = new SolidColorBrush((Color)this.OuterDisabledColor);
            }
            else
            {
                OuterPieSlicePath.Fill = new SolidColorBrush((Color)this.OuterNormalColor);
                OuterPieSlicePath.PointerPressed += outerPieSlicePath_PointerPressed;
                OuterPieSlicePath.PointerReleased += outerPieSlicePath_PointerReleased;
                OuterPieSlicePath.PointerEntered += outerPieSlicePath_PointerEntered;
                OuterPieSlicePath.PointerExited += outerPieSlicePath_PointerExited;
                // setup caret
                CaretRotateTransform.Angle = (this.StartAngle + (this.Angle / 2));
                CaretTranslate.X = this.Radius * Math.Sin(middleRadian);
                CaretTranslate.Y = -this.Radius * Math.Cos(middleRadian);
            }
            
            // Setup inner arc
            InnerPieSlicePath.Radius = this.Radius - 20;
            InnerPieSlicePath.StartAngle = this.StartAngle;
            InnerPieSlicePath.Angle = this.Angle;
            InnerPieSlicePath.Fill = new SolidColorBrush((Color)this.InnerNormalColor);

            // Setup icon and text
            IconTranslate.X = 85 * Math.Sin(middleRadian);
            IconTranslate.Y = -85 * Math.Cos(middleRadian);

        }
        // Change Selected Menu
        public delegate void ChangeSelectedHandler(object sender, PieSlice slice);
        public event ChangeSelectedHandler ChangeSelectedEvent;

        private void outerPieSlicePath_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "OuterPressed", true);

            // Check for Submenu
            if (OriginalRadialMenuButton.Submenu != null)
            {
                OnChangeMenuRequest(OriginalRadialMenuButton, OriginalRadialMenuButton.Submenu);
            }

            OriginalRadialMenuButton.OnOuterArcPressed(e);
        }

        private void outerPieSlicePath_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            // TODO: Check if we're actually still hovering
            VisualStateManager.GoToState(this, "OuterHover", true);
            OriginalRadialMenuButton.OnOuterArcReleased(e);
        }

        private void outerPieSlicePath_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "OuterHover", true);
        }

        private void outerPieSlicePath_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "OuterNormal", true);
        }

        private void innerPieSlicePath_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (OriginalRadialMenuButton.Type == Components.RadialMenuButton.ButtonType.Toggle)
            {
                VisualStateManager.GoToState(this, OriginalRadialMenuButton.Value ? "InnerReleased" : "InnerHover", true);
            }
            else if (OriginalRadialMenuButton.Type == Components.RadialMenuButton.ButtonType.Radio)
            {
                VisualStateManager.GoToState(this, OriginalRadialMenuButton.MenuSelected ? "InnerReleased" : "InnerHover", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "InnerHover", true);
            }
        }

        private void innerPieSlicePath_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            switch (OriginalRadialMenuButton.Type)
            {
                case Components.RadialMenuButton.ButtonType.Toggle:
                    VisualStateManager.GoToState(this, OriginalRadialMenuButton.Value ? "InnerReleased" : "InnerNormal", true);
                    break;
                case Components.RadialMenuButton.ButtonType.Radio:
                    UpdateSliceForRadio();
                    break;
                default:
                    VisualStateManager.GoToState(this, "InnerNormal", true);
                    break;
            }
        }

        private void innerPieSlicePath_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "InnerPressed", true);
            OriginalRadialMenuButton.OnInnerArcPressed(e);
        }

        private void innerPieSlicePath_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            OriginalRadialMenuButton.OnInnerArcReleased(e);
            switch (OriginalRadialMenuButton.Type)
            {
                case Components.RadialMenuButton.ButtonType.Toggle:
                    VisualStateManager.GoToState(this, OriginalRadialMenuButton.Value ? "InnerReleased" : "InnerNormal", true);
                    break;
                case Components.RadialMenuButton.ButtonType.Radio:
                    VisualStateManager.GoToState(this, "InnerReleased", true);
                    // get all other menus to release now that this menu has been selected
                    ChangeSelectedEvent?.Invoke(sender, this);
                    break;
                default:
                    VisualStateManager.GoToState(this, "InnerNormal", true);
                    break;
            }
        }

        public void UpdateSliceForRadio()
        {
            VisualStateManager.GoToState(this, OriginalRadialMenuButton.MenuSelected ? "InnerReleased" : "InnerNormal", true);
        }
    }
}
