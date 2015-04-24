using System.Collections.Generic;

namespace LightweightComboBoxColumn
{
    public class ViewModel
    {
        public ViewModel()
        {
            CreateTestData();
        }

        public List<CrossReference> CrossReferences { get; set; }
        public List<Item> Items { get; set; }

        public void CreateTestData()
        {
            Items = new List<Item>(5500);
            CrossReferences = new List<CrossReference>(50);

            for (int i = 0; i < 5500; i++) {
                Items.Add(new Item() { ItemKey = i, ItemNumber = i.ToString("x") });
            }

            for (int i = 0; i < 50; i++) {
                CrossReferences.Add(new CrossReference() { ItemKey = i, CustomerItem = (i + 20).ToString("x") });
            }
        }
    }
}
