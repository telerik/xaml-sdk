using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using NHunspell;
using Telerik.Windows.Documents.Proofing;

namespace NHunspellSpellChecking
{
    public class HunspellSpellChecker : ISpellChecker, IDisposable
    {
        private Hunspell spell = new Hunspell();
        private bool alreadyDisposed;
        private SpellCheckerSettings settings;

        public HunspellSpellChecker(Stream affFile, Stream dicFile)
        {
            this.settings = new SpellCheckerSettings()
            {
                SpellCheckUppercaseWords = true,
                SpellCheckWordsWithNumbers = true
            };

            byte[] affBytes = ReadAllBytes(affFile);
            byte[] dicBytes = ReadAllBytes(dicFile);

            this.spell.Load(affBytes, dicBytes);
        }

        private static byte[] ReadAllBytes(Stream stream)
        {
            byte[] result = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(result, 0, result.Length);

            return result;
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

            bool result = this.spell.Spell(word);

            return result;
        }

        public bool CheckWordIsCorrect(string word, CultureInfo culture)
        {
            return this.CheckWordIsCorrect(word);
        }

        public ICollection<string> GetSuggestions(string word)
        {
            List<string> result = this.spell.Suggest(word);

            return result;
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
            this.spell.Add(word);

            this.OnDataChanged();
        }

        public void AddWord(string word, CultureInfo culture)
        {
            this.AddWord(word);
        }

        public void RemoveWord(string word)
        {
            throw new NotSupportedException();
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

        ~HunspellSpellChecker()
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
                    if (this.spell != null)
                    {
                        this.spell.Dispose();
                        this.spell = null;
                    }
                }

                this.alreadyDisposed = true;
            }
        }
    }
}