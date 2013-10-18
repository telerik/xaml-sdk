using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PixelsToPixelLengthConversion
{
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
        }

        private void ButtonConvertPixelLengthToPixels_Click(object sender, RoutedEventArgs e)
        {
            this.SetPixelsAndPixelLength();
        }

        private void ButtonConvertPixelsToPixelLength_Click(object sender, RoutedEventArgs e)
        {
            this.SetPixelLength();
        }

        private void PixelsWatermarkTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.SetPixelLength();
            }
        }

        private void PixelLengthWatermarkTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.SetPixelsAndPixelLength();
            }
        }

        private void SetPixelLength()
        {
            var pixelLength = GetPixelLengthFromPixels();
            if (pixelLength != null)
            {
                this.GanttView.PixelLength = (TimeSpan)pixelLength;
                var result = string.Format("{0}:{1}:{2}.{3}", pixelLength.Value.Hours.ToString(), pixelLength.Value.Minutes.ToString(), pixelLength.Value.Seconds.ToString(), pixelLength.Value.Milliseconds.ToString());
                this.PixelLengthWatermarkTextBox.Text = result;
                this.PixelLengthResultTextBlock.Text = result;
                this.PixelsResultTextBlock.Text = "---";
            }
        }

        private void SetPixelsAndPixelLength()
        {
            var pixelLength = this.GetPixelLengthFromString();
            if (pixelLength != null)
            {
                this.GanttView.PixelLength = (TimeSpan)pixelLength;
                this.PixelsWatermarkTextBox.Text = GetPixelsFromPixelLength((TimeSpan)pixelLength).ToString();
                this.PixelsResultTextBlock.Text = GetPixelsFromPixelLength((TimeSpan)pixelLength).ToString();
                this.PixelLengthResultTextBlock.Text = "---";
            }
        }

        /// <summary>
        /// Gets PixelLength from Pixels. 
        /// The coversion formula that is use is: PixelLength = TimeSpan.FromTicks(VisibleRange.Ticks / Pixels)
        /// </summary>
        /// <returns>TimeSpan? object</returns>
        private TimeSpan? GetPixelLengthFromPixels()
        {
            int pixels;

            if (int.TryParse(this.PixelsWatermarkTextBox.Text, out pixels) && pixels > 0)
            {
                var maxTicks = this.GanttView.VisibleRange.End.Subtract(this.GanttView.VisibleRange.Start).Ticks / pixels;

                return TimeSpan.FromTicks(maxTicks);
            }

            return null;
        }

        /// <summary>
        /// Gets Pixels From PixelLength. 
        /// The formula that is used is: Pixels = VisibleRange.Ticks / pixelLength.Ticks
        /// </summary>
        /// <param name="pixelLength"></param>
        /// <returns></returns>
        private double GetPixelsFromPixelLength(TimeSpan pixelLength)
        {
            var maxTicks = pixelLength.Ticks;
            var pixels = this.GanttView.VisibleRange.End.Subtract(this.GanttView.VisibleRange.Start).Ticks / maxTicks;

            return pixels;
        }

        private TimeSpan? GetPixelLengthFromString()
        {
            TimeSpan? pixelLength;

            if (!string.IsNullOrEmpty(this.PixelLengthWatermarkTextBox.Text))
            {
                pixelLength = this.GetTimeSpanFromString(this.PixelLengthWatermarkTextBox.Text);

                return pixelLength;
            }

            return null;
        }

        private TimeSpan? GetTimeSpanFromString(string input)
        {
            List<string> values = input.Split(new char[] { ':', '.' }).ToList();
            while (values.Count < 4)
            {
                values.Add("0");
            }

            var result = new TimeSpan(0, int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3].Substring(0, 1)));

            if (result.Ticks > 0)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
