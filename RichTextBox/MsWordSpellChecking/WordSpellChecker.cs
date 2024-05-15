using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Office.Interop.Word;
using Telerik.Windows.Documents.Proofing;

namespace WordSpellCheck
{
    public class WordSpellChecker : ISpellChecker, IDisposable
    {
        private Application application = new Application();
        private Document document;
        private bool alreadyDisposed;
        private SpellCheckerSettings settings;
        private readonly HashSet<string> customWords;

        public WordSpellChecker()
        {
            this.settings = new SpellCheckerSettings()
            {
                SpellCheckUppercaseWords = true,
                SpellCheckWordsWithNumbers = true
            };

            this.customWords = new HashSet<string>();

            this.document = this.application.Documents.Add();
            document.LanguageDetected = true;
        }

        public bool CheckWordIsCorrect(string word)
        {
            if (!this.Settings.SpellCheckUppercaseWords && char.IsUpper(word[0]))
            {
                return true;
            }

            if (!this.Settings.SpellCheckWordsWithNumbers && word.Any(c => char.IsDigit(c)))
            {
                return true;
            }

            Range content = this.document.Content;
            content.Text = word;

            bool result = content.SpellingErrors.Count == 0;

            if (!result)
            {
                return this.customWords.Contains(word);
            }

            return result;
        }

        public bool CheckWordIsCorrect(string word, CultureInfo culture)
        {
            return this.CheckWordIsCorrect(word);
        }

        public ICollection<string> GetSuggestions(string word)
        {
            var appSuggestions = this.document.Content.GetSpellingSuggestions();

            string[] strArray = new string[appSuggestions.Count];
            for (int i = 0; i < appSuggestions.Count; i++)
            {
                SpellingSuggestion suggestion = appSuggestions[i + 1];
                strArray[i] = suggestion.Name;
            }

            return strArray;
        }

        public ICollection<string> GetSuggestions(string word, CultureInfo culture)
        {
            return this.GetSuggestions(word);
        }

        public bool CanAddWord()
        {
            return true;
        }

        public bool CanAddWord(CultureInfo culture)
        {
            return true;
        }

        public void AddWord(string word)
        {
            this.customWords.Add(word);

            this.OnDataChanged();
        }

        public void AddWord(string word, CultureInfo culture)
        {
            this.AddWord(word);
        }

        public void RemoveWord(string word)
        {
            this.customWords.Remove(word);

            this.OnDataChanged();
        }

        public void RemoveWord(string word, CultureInfo culture)
        {
            this.RemoveWord(word);
        }

        public IWordDictionary GetDictionary()
        {
            return null;
        }

        public IWordDictionary GetDictionary(CultureInfo culture)
        {
            return null;
        }

        public ICustomWordDictionary GetCustomDictionary()
        {
            return null;
        }

        public ICustomWordDictionary GetCustomDictionary(CultureInfo culture)
        {
            return null;
        }

        public CultureInfo SpellCheckingCulture
        {
            get
            {
                return new CultureInfo("en-us");
            }
            set
            {
            }
        }

        public SpellCheckerSettings Settings
        {
            get
            {
                return this.settings;
            }
            set
            {
                this.settings = value;
            }
        }

        public event EventHandler DataChanged;

        protected virtual void OnDataChanged()
        {
            if (this.DataChanged != null)
            {
                this.DataChanged(this, EventArgs.Empty);
            }
        }

        ~WordSpellChecker()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.alreadyDisposed)
            {
                if (disposing)
                {
                    if (this.application != null)
                    {
                        this.document = null;
                        object saveChanges = null;
                        this.application.Quit(ref saveChanges, ref saveChanges, ref saveChanges);
                        this.application = null;
                    }
                }

                this.alreadyDisposed = true;
            }
        }
    }
}