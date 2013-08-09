using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using TimelineTimeBarSpecialSlots;

namespace SpecialSlots
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            FirstDayComboBox.SelectedIndex = 2;
            DaysCountComboBox.SelectedIndex = 1;
            radioButton1.IsChecked = true;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.UpdateGenerator();
        }

        private void DaysCountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.UpdateGenerator();
        }

        private void FirstDayComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.UpdateGenerator();
        }

        private void UpdateGenerator()
        {
            if (radioButton1.IsChecked == true)
            {
                timeline.SpecialSlotsGenerator = new WeekDaysGenerator()
                {
                    DaysCount = (DaysCountComboBox.SelectedIndex + 1),
                    FirstDay = (DayOfWeek)(FirstDayComboBox.SelectedIndex + 1),
                };
            }
            else if (radioButton2.IsChecked == true)
            {
                timeline.SpecialSlotsGenerator = new WeekDaysGenerator()
                {
                    DaysCount = 5,
                    FirstDay = DayOfWeek.Monday,
                };
            }
            else if (radioButton3.IsChecked == true)
            {
                timeline.SpecialSlotsGenerator = new WeekDaysGenerator()
                {
                    DaysCount = 2,
                    FirstDay = DayOfWeek.Saturday,
                };
            }
            else if (radioButton4.IsChecked == true)
            {
                timeline.SpecialSlotsGenerator = new MonthDayGenerator() { DayOfTheMonth = int.Parse(DayOfTheMonthTextBox.Text) };
            }
            else if (radioButton5.IsChecked == true)
            {
                timeline.SpecialSlotsGenerator = null;
            }
        }
    }
}
