﻿using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace StylableFindFlowDocumentReader
{
    /// <summary>
    /// Interaction logic for FindNextPreviousButtons.xaml
    /// </summary>
    public partial class FindNextPreviousButtons : UserControl
    {
        public bool ShowTooltips
        {
            get { return (bool)GetValue(ShowTooltipsProperty); }
            set { SetValue(ShowTooltipsProperty, value); }
        }

        public static readonly DependencyProperty ShowTooltipsProperty =
            DependencyProperty.Register(nameof(ShowTooltips), typeof(bool), typeof(FindNextPreviousButtons), new PropertyMetadata(true));

        public string FindNextTooltip
        {
            get { return (string)GetValue(FindNextTooltipProperty); }
            set { SetValue(FindNextTooltipProperty, value); }
        }

        public static readonly DependencyProperty FindNextTooltipProperty =
            DependencyProperty.Register(nameof(FindNextTooltip), typeof(string), typeof(FindNextPreviousButtons), new PropertyMetadata("Find Next"));

        public string FindPreviousTooltip
        {
            get { return (string)GetValue(FindPreviousTooltipProperty); }
            set { SetValue(FindPreviousTooltipProperty, value); }
        }

        public static readonly DependencyProperty FindPreviousTooltipProperty =
            DependencyProperty.Register(nameof(FindPreviousTooltip), typeof(string), typeof(FindNextPreviousButtons), new PropertyMetadata("Find Previous"));

        public FindNextPreviousButtons()
        {
            InitializeComponent();
        }
    }
}
