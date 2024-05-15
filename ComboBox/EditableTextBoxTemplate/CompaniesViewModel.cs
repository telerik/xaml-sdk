using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace EditableTextBoxTemplate
{
public class CompaniesViewModel
{
    private ObservableCollection<Company> companies;
    public ObservableCollection<Company> Companies
    {
        get
        {
            if (companies == null)
            {
                companies = new ObservableCollection<Company>();

                companies.Add(new Company("Globex Corporation", "France, Marseille", "(100) 555-4822", "Images/Image3.png"));
                companies.Add(new Company("Atlantic Northern", "Brazil, São Paulo", "(11) 555-1189", "Images/Image5.png"));
                companies.Add(new Company("Roboto Industries", "Spain, Madrid", "(91) 745 6200", "Images/Image6.png"));
                companies.Add(new Company("Galaxy Corp", "Sweden, Luleå", "0921-12 34 65", "Images/Image7.png"));
                companies.Add(new Company("Wayne Enterprises", "USA, Portland", "(503) 555-3612", "Images/Image8.png"));
                companies.Add(new Company("Acme, inc.", "Finland, Helsinki", "90-224 8858", "Images/Image9.png"));
                companies.Add(new Company("Consolidated Holdings", "UK, London", "(171) 555-2282", "Images/Image4.png"));
            }

            return companies;
        }
    }
}
}
