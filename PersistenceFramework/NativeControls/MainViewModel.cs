using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Persistence;

namespace NativeControls
{
    public class MainViewModel : ViewModelBase
    {
        private Draft selectedDraft;
        private ObservableCollection<Draft> drafts = new ObservableCollection<Draft>();
        PersistenceManager manager = new PersistenceManager()
                                        .AllowCoreControls()
                                        .AllowInputControls()
                                        .AllowTypes(new Type[]
                                        {
                                            typeof(UIElementCollection),
                                            typeof(Grid),
                                            typeof(ColumnDefinitionCollection),
                                            typeof(ColumnDefinition),
                                            typeof(RowDefinitionCollection),
                                            typeof(RowDefinition),
                                            typeof(StackPanel),
                                            typeof(TextBlock),
                                            typeof(TextBox),
                                            typeof(GridLength),
                                            typeof(DependencyObject),
                                            typeof(InlineCollection),
                                            typeof(Run),
                                            typeof(Thickness),
                                            typeof(ThicknessConverter),
                                            typeof(Size),
                                            typeof(SizeConverter),
                                            typeof(LengthConverter),
                                            typeof(ItemCollection),
                                            typeof(CornerRadius),
                                            typeof(System.Windows.CornerRadiusConverter),
                                            typeof(FontWeight),
                                            typeof(FontStyle),
                                            typeof(SolidColorBrush),
                                            typeof(Brush),
                                            typeof(BrushConverter),
                                            typeof(FontFamily),
                                            typeof(FontSizeConverter),
                                            typeof(ListBox),
                                            typeof(Border),
                                            typeof(UIElement),
                                            typeof(NullableBoolConverter)
                                        });

        public DelegateCommand SaveDraft { get; set; }
        public DelegateCommand LoadDraft { get; set; }
        public DelegateCommand DeleteDraft { get; set; }
        public ObservableCollection<Draft> Drafts
        {
            get { return drafts; }
            set
            {
                drafts = value;
                this.OnPropertyChanged("Drafts");
            }
        }
        public Draft SelectedDraft
        {
            get { return selectedDraft; }
            set
            {
                selectedDraft = value;
                this.OnPropertyChanged("SelectedDraft");
                this.LoadDraft.InvalidateCanExecute();
                this.DeleteDraft.InvalidateCanExecute();
            }
        }

        public MainViewModel()
        {
            this.SaveDraft = new DelegateCommand(new Action<object>(OnSaveDraft));
            this.LoadDraft = new DelegateCommand(new Action<object>(OnLoadDraft), new Predicate<object>(HasSelectedDraft));
            this.DeleteDraft = new DelegateCommand(new Action<object>(OnDeleteDraft), new Predicate<object>(HasSelectedDraft));
        }

        private void OnSaveDraft(object param)
        {
            Draft draft = new Draft();
            draft.Title = this.GetDraftName();
            draft.Stream = manager.Save(param);
            this.Drafts.Add(draft);
        }

        private void OnLoadDraft(object param)
        {
            if (this.SelectedDraft != null)
            {
                this.SelectedDraft.Stream.Position = 0L;
                manager.Load(param, this.SelectedDraft.Stream);
            }
        }

        private void OnDeleteDraft(object param)
        {
            if (this.SelectedDraft != null)
            {
                this.Drafts.Remove(this.SelectedDraft);
            }
        }

        private bool HasSelectedDraft(object param)
        {
            return this.SelectedDraft != null;
        }

        private string GetDraftName()
        {
            string baseName = "Draft";
            string newName = string.Empty;

            if (this.Drafts.Count == 0)
            {
                newName = baseName + 1.ToString();
            }
            else
            {
                Draft lastDraft = this.Drafts[this.Drafts.Count - 1];
                int index = int.Parse(lastDraft.Title.Replace(baseName, string.Empty));
                newName = baseName + ++index;
            }

            return newName;
        }
    }

    public class Draft
    {
        public string Title { get; set; }
        public Stream Stream { get; set; }
    }
}
