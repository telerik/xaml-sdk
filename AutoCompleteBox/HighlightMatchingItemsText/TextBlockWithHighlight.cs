using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace HighlightMatchingItemsText
{
    public class TextBlockWithHighlight : Control
    {
        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register("SearchText", typeof(string), typeof(TextBlockWithHighlight), new PropertyMetadata(string.Empty, OnSearchTextPropertyChanged));

        public static readonly DependencyProperty ItemTextProperty =
            DependencyProperty.Register("ItemText", typeof(string), typeof(TextBlockWithHighlight), new PropertyMetadata(string.Empty, OnItemTextPropertyChanged));

        private string _SearchText;
        private string _ItemText;

        private TextBlock textBoxPart;

        static TextBlockWithHighlight()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBlockWithHighlight), new FrameworkPropertyMetadata(typeof(TextBlockWithHighlight)));
        }

        public string SearchText
        {
            get
            {
                return this._SearchText;
            }
            set
            {
                if (this._SearchText != value)
                {
                    this.SetValue(SearchTextProperty, value);
                }
            }
        }

        public string ItemText
        {
            get
            {
                return this._ItemText;
            }
            set
            {
                if (this._ItemText != value)
                {
                    this.SetValue(ItemTextProperty, value);
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.textBoxPart = this.GetTemplateChild("PART_TextBlock") as TextBlock;

            this.BuildText();
        }

        private static void OnSearchTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = d as TextBlockWithHighlight;
            if (target != null)
            {
                target._SearchText = (string)e.NewValue;
                target.BuildText();
            }
        }

        private static void OnItemTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = d as TextBlockWithHighlight;
            if (target != null)
            {
                target._ItemText = (string)e.NewValue;
                target.BuildText();
            }
        }

        private void BuildText()
        {
            if (!string.IsNullOrEmpty(this.SearchText) && !string.IsNullOrEmpty(this.ItemText) && this.ItemText.ToLower().Contains(this.SearchText.ToLower()))
            {
                int matchIndex = this.ItemText.ToLower().IndexOf(this.SearchText.ToLower());

                string preMatchText = this.ItemText.Substring(0, matchIndex);
                string matchText = this.ItemText.Substring(matchIndex, this.SearchText.Length);
                string afterMatchText = this.ItemText.Substring(matchIndex + this.SearchText.Length);

                this.AddTextToTextBlockPart(preMatchText, false);
                this.AddTextToTextBlockPart(matchText, true);
                this.AddTextToTextBlockPart(afterMatchText, false);
            }
        }

        private void AddTextToTextBlockPart(string text, bool isBold)
        {
            var run = new Run();
            run.FontWeight = isBold ? FontWeights.Bold : FontWeights.Normal;
            run.Text = text;

            if (this.textBoxPart != null)
            {
                this.textBoxPart.Inlines.Add(run);
            }
        }
    }
}
