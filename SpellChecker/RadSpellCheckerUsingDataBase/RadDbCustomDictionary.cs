using System;
using System.Collections.ObjectModel;
using RadSpellCheckerUsingDataBase.DataServiceReference;
using Telerik.Windows.Documents.Proofing;

namespace RadSpellCheckerUsingDataBase
{
    public class RadDbCustomDictionary : RadDictionary, ICustomWordDictionary, IDisposable
    {
        DataServiceClient dataServiceClient;
        public bool AutoUpdateDataBase { get; set; }

        public RadDbCustomDictionary()
        {
            this.AutoUpdateDataBase = true;
            this.dataServiceClient = new DataServiceClient();
            this.dataServiceClient.GetAllWordsCompleted += OnGetAllWordsCompleted;
            this.dataServiceClient.UpdateWordsCompleted += OnUpdateWordsCompleted;
            this.dataServiceClient.GetAllWordsAsync();
        }

        void OnGetAllWordsCompleted(object sender, GetAllWordsCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                foreach (Word word in e.Result)
                {
                    if (!string.IsNullOrEmpty(word.@string))
                    {
                        this.AddWordInternal(word.@string);
                    }
                }
            }
        }

        public void UpdateDataBase()
        {
            dataServiceClient.UpdateWordsAsync(new ObservableCollection<string>(this.WordsList));
        }

        void OnUpdateWordsCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            this.dataServiceClient.GetAllWordsAsync();
        }

        private bool AddWordInternal(string word)
        {
            if (!this.ContainsWord(word))
            {
                DoubleMetaphone metaphone = new DoubleMetaphone();
                string primaryKey = metaphone.Encode(word, false);
                string alternateKey = metaphone.Encode(word, true);

                this.WordsList.Add(word);
                this.AddWordToMetaphoneDictionaries(word, primaryKey, alternateKey);

                return true;
            }
            return false;
        }

        public void AddWord(string word)
        {
            string properWord = word.ToLower();
            if (this.AddWordInternal(properWord))
            {
                if (this.AutoUpdateDataBase)
                {
                    this.UpdateDataBase();
                }

                this.OnDataChanged();
            }
        }

        public void RemoveWord(string word)
        {
            string properWord = word.ToLower();
            if (this.ContainsWord(properWord))
            {
                string primaryKey = this.metaphoneByWord[properWord].Item1;
                string alternateKey = this.metaphoneByWord[properWord].Item2;

                this.WordsList.Remove(properWord);
                this.RemoveWordFromMetaphoneDictionaries(properWord, primaryKey, alternateKey);

                if (this.AutoUpdateDataBase)
                {
                    this.UpdateDataBase();
                }

                this.OnDataChanged();
            }
        }

        public void ClearWords()
        {
            if (this.WordsList.Count > 0)
            {
                this.WordsList.Clear();
                this.wordsByMetaphone.Clear();
                this.wordsByMetaphoneAlternate.Clear();
                this.metaphoneByWord.Clear();

                if (this.AutoUpdateDataBase)
                {
                    this.UpdateDataBase();
                }

                this.OnDataChanged();
            }
        }

        public void Dispose()
        {
            this.dataServiceClient.GetAllWordsCompleted -= OnGetAllWordsCompleted;
            this.dataServiceClient.UpdateWordsCompleted -= OnUpdateWordsCompleted;
        }
    }
}
