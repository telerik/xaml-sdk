using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections.Generic;

namespace RadSpellCheckerUsingDataBase.Web
{
    [ServiceContract(Namespace = "")]
    [SilverlightFaultBehavior]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DataService
    {
        [OperationContract]
        public List<Word> GetAllWords()
        {
            SpellCheckerDataBaseDataContext db = new SpellCheckerDataBaseDataContext();
            return db.Words.ToList();
        }

        [OperationContract]
        public void UpdateWords(List<string> words)
        {
            SpellCheckerDataBaseDataContext db = new SpellCheckerDataBaseDataContext();
            db.ExecuteCommand("DELETE FROM Word");

            foreach (string word in words)
            {
                Word newRecord = new Word();
                newRecord.@string = word;
                db.Words.InsertOnSubmit(newRecord);
            }
            db.SubmitChanges();
        }
    }
}
