﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace DemoCommon
{
    /// <summary>
    /// Interaction logic for DocumentNullSwitcher.xaml.
    /// </summary>
    public partial class DocumentNullSwitcher : UserControl
    {
        public DocumentNullSwitcher() => InitializeComponent();

        private class FlowControlDocument
        {
            public Func<FlowDocument> GetDocument { get; }

            public Action<FlowDocument> SetDocument { get; }

            public FlowControlDocument(Func<FlowDocument> getDocument, Action<FlowDocument> setDocument)
            {
                GetDocument = getDocument;
                SetDocument = setDocument;
            }
        }

        private FlowControlDocument _flowControlDocument;

        public Control FlowControl
        {
            get => (Control)GetValue(FlowControlProperty);
            set => SetValue(FlowControlProperty, value);
        }

        // Using a DependencyProperty as the backing store for FlowControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FlowControlProperty =
            DependencyProperty.Register(nameof(FlowControl), typeof(Control), typeof(DocumentNullSwitcher), new PropertyMetadata(null, FlowControlChanged));

        private static void FlowControlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var nullDocumentSwitcher = d as DocumentNullSwitcher;
            object flowControl = e.NewValue;
            if (flowControl is FlowDocumentReader flowDocumentReader)
            {
                nullDocumentSwitcher._flowControlDocument = new FlowControlDocument(
                    () => flowDocumentReader.Document,
                    (flowDocument) => flowDocumentReader.Document = flowDocument);
            }
            else if (flowControl is FlowDocumentScrollViewer flowDocumentScrollViewer)
            {
                nullDocumentSwitcher._flowControlDocument = new FlowControlDocument(
                    () => flowDocumentScrollViewer.Document,
                    (flowDocument) => flowDocumentScrollViewer.Document = flowDocument);
            }
            else if (flowControl is FlowDocumentPageViewer flowDocumentPageViewer)
            {
                nullDocumentSwitcher._flowControlDocument = new FlowControlDocument(
                    () => flowDocumentPageViewer.Document as FlowDocument,
                    (flowDocument) => flowDocumentPageViewer.Document = flowDocument);
            }
            else
            {
                throw new Exception("Not a flow control");
            }
        }

        private FlowDocument _flowDocument;

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (_flowControlDocument == null)
            {
                return;
            }

            _flowControlDocument.SetDocument(_flowDocument);
        }

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {

            if (_flowDocument == null)
            {
                _flowDocument = _flowControlDocument.GetDocument();
            }

            _flowControlDocument.SetDocument(null);
        }
    }
}
