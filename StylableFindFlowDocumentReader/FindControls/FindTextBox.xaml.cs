﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StylableFindFlowDocumentReader.FindControls
{
    /// <summary>
    /// Interaction logic for FindTextBox.xaml
    /// </summary>
    public partial class FindTextBox : UserControl
    {
        static FindTextBox()
        {
            SelectionBrushProperty = TextBox.SelectionBrushProperty.AddOwner(typeof(FindTextBox));

            SelectionOpacityProperty = TextBox.SelectionOpacityProperty.AddOwner(typeof(FindTextBox));
        }

        // SelectionBrush
        public static readonly DependencyProperty SelectionBrushProperty;
        public Brush SelectionBrush
        {
            get => (Brush)GetValue(SelectionBrushProperty);
            set => SetValue(SelectionBrushProperty, value);
        }

        // SelectionOpacity
        public static readonly DependencyProperty SelectionOpacityProperty;
        public double SelectionOpacity
        {
            get => (double)GetValue(SelectionOpacityProperty);
            set => SetValue(SelectionOpacityProperty, value);
        }

        public bool ShowTooltip
        {
            get => (bool)GetValue(ShowTooltipProperty);
            set => SetValue(ShowTooltipProperty, value);
        }

        public static readonly DependencyProperty ShowTooltipProperty =
            DependencyProperty.Register(nameof(ShowTooltip), typeof(bool), typeof(FindTextBox), new PropertyMetadata(true));

        public string Tooltip
        {
            get => (string)GetValue(TooltipProperty);
            set => SetValue(TooltipProperty, value);
        }

        public static readonly DependencyProperty TooltipProperty =
            DependencyProperty.Register(nameof(Tooltip), typeof(string), typeof(FindTextBox), new PropertyMetadata("Search for a word or phrase in this document."));

        public string HintText
        {
            get => (string)GetValue(HintTextProperty);
            set => SetValue(HintTextProperty, value);
        }

        public static readonly DependencyProperty HintTextProperty =
            DependencyProperty.Register(nameof(HintText), typeof(string), typeof(FindTextBox), new PropertyMetadata("Search…"));

        public double HintOpacity
        {
            get => (double)GetValue(HintOpacityProperty);
            set => SetValue(HintOpacityProperty, value);
        }

        public static readonly DependencyProperty HintOpacityProperty =
            DependencyProperty.Register(nameof(HintOpacity), typeof(double), typeof(FindTextBox), new PropertyMetadata(0.7));

        public FontStyle HintFontStyle
        {
            get => (FontStyle)GetValue(HintFontStyleProperty);
            set => SetValue(HintFontStyleProperty, value);
        }

        public static readonly DependencyProperty HintFontStyleProperty =
            DependencyProperty.Register(nameof(HintFontStyle), typeof(FontStyle), typeof(FindTextBox), new PropertyMetadata(FontStyles.Italic));

        public double TextBoxWidth
        {
            get => (double)GetValue(TextBoxWidthProperty);
            set => SetValue(TextBoxWidthProperty, value);
        }

        public static readonly DependencyProperty TextBoxWidthProperty =
            DependencyProperty.Register(nameof(TextBoxWidth), typeof(double), typeof(FindTextBox), new PropertyMetadata((double)183));

        public FindTextBox()
        {
            Foreground = Brushes.Black;
            InitializeComponent();
        }
    }
}
