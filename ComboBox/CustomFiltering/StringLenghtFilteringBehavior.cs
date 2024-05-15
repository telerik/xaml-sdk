using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace CustomFiltering
{
    public class StringLenghtFilteringBehavior : ComboBoxFilteringBehavior
    {
        private int charLenght;

        public override int FindFullMatchIndex(ReadOnlyCollection<int> matchIndexes)
        {
            var fullMatch = this.ComboBox.Items.OfType<DataItem>().FirstOrDefault(i => i.Title.Length == charLenght);
            if (fullMatch == null)
            {
                return -1;
            }

            var fullMatchIndex = this.ComboBox.Items.IndexOf(fullMatch);
            if (matchIndexes.Contains(fullMatchIndex))
            {
                return fullMatchIndex;
            }

            return -1;
        }

        public override List<int> FindMatchingIndexes(string text)
        {
            if (int.TryParse(text, out this.charLenght))
            {
                return this.ComboBox.Items.OfType<DataItem>().Where(i => i.Title.Length >= this.charLenght).Select(i => this.ComboBox.Items.IndexOf(i)).ToList();
            }

            return new List<int>();
        }
    }
}