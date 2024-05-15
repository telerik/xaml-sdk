using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace DropDownWithHeaders
{
    public class ViewModel: ViewModelBase
    {
        public ObservableCollection<Material> Materials { get; set; }
        public ViewModel()
        {
            this.Materials = new ObservableCollection<Material>
            {
                new Material { Id = 1, Name = "Item 1", Type = "Material Type 1", Description="Description 1" },
                new Material { Id = 2, Name = "Item 2", Type = "Material Type 2", Description="Description 2" },
                new Material { Id = 3, Name = "Item 3", Type = "Material Type 3", Description="Description 3" },
                new Material { Id = 4, Name = "Item 4", Type = "Material Type 4", Description="Description 4" },
                new Material { Id = 5, Name = "Item 5", Type = "Material Type 5", Description="Description 5" },
            };
        }

        private Material selectedMaterial;

        public Material SelectedMaterial
        {
            get
            {
                return this.selectedMaterial;
            }

            set
            {
                if (this.selectedMaterial != value)
                {
                    this.selectedMaterial = value;
                    this.OnPropertyChanged(() => this.SelectedMaterial);
                }
            }
        }
    }
}
