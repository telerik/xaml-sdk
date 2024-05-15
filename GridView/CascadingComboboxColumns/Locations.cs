using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CascadingComboBoxColumns
{
    public static class Locations
    {
        public static List<Country> Countries;
        public static List<Continent> Continents;

        static Locations()
        {
            Countries = new List<Country>();
            Continents = new List<Continent>();
            //http://en.wikipedia.org/wiki/List_of_countries_by_continent_(data_file)
            #region  generated code
            Countries.Add(new Country() { ID = 1, ContinentCode = "AS", Name = "Afghanistan, Islamic Republicof" });
            Countries.Add(new Country() { ID = 3, ContinentCode = "EU", Name = "Albania, Republic of" });
            Countries.Add(new Country() { ID = 4, ContinentCode = "AF", Name = "Algeria, Peoples DemocraticRepublic of" });
            Countries.Add(new Country() { ID = 5, ContinentCode = "OC", Name = "American Samoa" });
            Countries.Add(new Country() { ID = 6, ContinentCode = "EU", Name = "Andorra, Principality of" });
            Countries.Add(new Country() { ID = 7, ContinentCode = "AF", Name = "Angola, Republic of" });
            Countries.Add(new Country() { ID = 8, ContinentCode = "NA", Name = "Anguilla" });
            Countries.Add(new Country() { ID = 9, ContinentCode = "AN", Name = "Antarctica (the territory South of 60 deg S)" });
            Countries.Add(new Country() { ID = 10, ContinentCode = "NA", Name = "Antigua and Barbuda" });
            Countries.Add(new Country() { ID = 11, ContinentCode = "SA", Name = "Argentina, Argentine Republic" });
            Countries.Add(new Country() { ID = 12, ContinentCode = "AS", Name = "Armenia, Republic of" });
            Countries.Add(new Country() { ID = 13, ContinentCode = "NA", Name = "Aruba" });
            Countries.Add(new Country() { ID = 14, ContinentCode = "OC", Name = "Australia, Commonwealth of" });
            Countries.Add(new Country() { ID = 15, ContinentCode = "EU", Name = "Austria, Republic of" });
            Countries.Add(new Country() { ID = 16, ContinentCode = "AS", Name = "Azerbaijan, Republic of" });
            Countries.Add(new Country() { ID = 17, ContinentCode = "NA", Name = "Bahamas, Commonwealth of the" });
            Countries.Add(new Country() { ID = 18, ContinentCode = "AS", Name = "Bahrain, Kingdom of" });
            Countries.Add(new Country() { ID = 19, ContinentCode = "AS", Name = "Bangladesh, Peoples Republic of" });
            Countries.Add(new Country() { ID = 20, ContinentCode = "NA", Name = "Barbados" });
            Countries.Add(new Country() { ID = 21, ContinentCode = "EU", Name = "Belarus, Republic of" });
            Countries.Add(new Country() { ID = 22, ContinentCode = "EU", Name = "Belgium, Kingdom of" });
            Countries.Add(new Country() { ID = 23, ContinentCode = "NA", Name = "Belize" });
            Countries.Add(new Country() { ID = 24, ContinentCode = "AF", Name = "Benin, Republic of" });
            Countries.Add(new Country() { ID = 25, ContinentCode = "NA", Name = "Bermuda" });
            Countries.Add(new Country() { ID = 26, ContinentCode = "AS", Name = "Bhutan, Kingdom of" });
            Countries.Add(new Country() { ID = 27, ContinentCode = "SA", Name = "Bolivia, Republic of" });
            Countries.Add(new Country() { ID = 28, ContinentCode = "EU", Name = "Bosnia and Herzegovina" });
            Countries.Add(new Country() { ID = 29, ContinentCode = "AF", Name = "Botswana, Republic of" });
            Countries.Add(new Country() { ID = 30, ContinentCode = "AN", Name = "Bouvet Island (Bouvetoya)" });
            Countries.Add(new Country() { ID = 31, ContinentCode = "SA", Name = "Brazil, Federative Republic of" });
            Countries.Add(new Country() { ID = 32, ContinentCode = "AS", Name = "British Indian Ocean Territory (Chagos Archipelago)" });
            Countries.Add(new Country() { ID = 33, ContinentCode = "NA", Name = "British Virgin Islands" });
            Countries.Add(new Country() { ID = 34, ContinentCode = "AS", Name = "Brunei Darussalam" });
            Countries.Add(new Country() { ID = 35, ContinentCode = "EU", Name = "Bulgaria, Republic of" });
            Countries.Add(new Country() { ID = 36, ContinentCode = "AF", Name = "Burkina Faso" });
            Countries.Add(new Country() { ID = 37, ContinentCode = "AF", Name = "Burundi, Republic of" });
            Countries.Add(new Country() { ID = 38, ContinentCode = "AS", Name = "Cambodia, Kingdom of" });
            Countries.Add(new Country() { ID = 39, ContinentCode = "AF", Name = "Cameroon, Republic of" });
            Countries.Add(new Country() { ID = 40, ContinentCode = "NA", Name = "Canada" });
            Countries.Add(new Country() { ID = 41, ContinentCode = "AF", Name = "Cape Verde, Republic of" });
            Countries.Add(new Country() { ID = 42, ContinentCode = "NA", Name = "Cayman Islands" });
            Countries.Add(new Country() { ID = 43, ContinentCode = "AF", Name = "Central African Republic" });
            Countries.Add(new Country() { ID = 44, ContinentCode = "AF", Name = "Chad, Republic of" });
            Countries.Add(new Country() { ID = 45, ContinentCode = "SA", Name = "Chile, Republic of" });
            Countries.Add(new Country() { ID = 46, ContinentCode = "AS", Name = "China, Peoples Republic of" });
            Countries.Add(new Country() { ID = 47, ContinentCode = "AS", Name = "Christmas Island" });
            Countries.Add(new Country() { ID = 48, ContinentCode = "AS", Name = "Cocos (Keeling) Islands" });
            Countries.Add(new Country() { ID = 49, ContinentCode = "SA", Name = "Colombia, Republic of" });
            Countries.Add(new Country() { ID = 50, ContinentCode = "AF", Name = "Comoros, Union of the" });
            Countries.Add(new Country() { ID = 51, ContinentCode = "AF", Name = "Congo, Democratic Republic ofthe" });
            Countries.Add(new Country() { ID = 52, ContinentCode = "AF", Name = "Congo, Republic of the" });
            Countries.Add(new Country() { ID = 53, ContinentCode = "OC", Name = "Cook Islands" });
            Countries.Add(new Country() { ID = 54, ContinentCode = "NA", Name = "Costa Rica, Republic of" });
            Countries.Add(new Country() { ID = 55, ContinentCode = "AF", Name = "Cote d'Ivoire, Republic of" });
            Countries.Add(new Country() { ID = 56, ContinentCode = "EU", Name = "Croatia, Republic of" });
            Countries.Add(new Country() { ID = 57, ContinentCode = "NA", Name = "Cuba, Republic of" });
            Countries.Add(new Country() { ID = 58, ContinentCode = "AS", Name = "Cyprus, Republic of" });
            Countries.Add(new Country() { ID = 59, ContinentCode = "EU", Name = "Czech Republic" });
            Countries.Add(new Country() { ID = 60, ContinentCode = "EU", Name = "Denmark, Kingdom of" });
            Countries.Add(new Country() { ID = 61, ContinentCode = "AF", Name = "Djibouti, Republic of" });
            Countries.Add(new Country() { ID = 62, ContinentCode = "NA", Name = "Dominica, Commonwealth of" });
            Countries.Add(new Country() { ID = 63, ContinentCode = "NA", Name = "Dominican Republic" });
            Countries.Add(new Country() { ID = 64, ContinentCode = "SA", Name = "Ecuador, Republic of" });
            Countries.Add(new Country() { ID = 65, ContinentCode = "AF", Name = "Egypt, Arab Republic of" });
            Countries.Add(new Country() { ID = 66, ContinentCode = "NA", Name = "El Salvador, Republic of" });
            Countries.Add(new Country() { ID = 67, ContinentCode = "AF", Name = "Equatorial Guinea, Republic of" });
            Countries.Add(new Country() { ID = 68, ContinentCode = "AF", Name = "Eritrea, State of" });
            Countries.Add(new Country() { ID = 69, ContinentCode = "EU", Name = "Estonia, Republic of" });
            Countries.Add(new Country() { ID = 70, ContinentCode = "AF", Name = "Ethiopia, Federal DemocraticRepublic of" });
            Countries.Add(new Country() { ID = 71, ContinentCode = "EU", Name = "Faroe Islands" });
            Countries.Add(new Country() { ID = 72, ContinentCode = "SA", Name = "Falkland Islands (Malvinas)" });
            Countries.Add(new Country() { ID = 73, ContinentCode = "OC", Name = "Fiji, Republic of the Fiji Islands" });
            Countries.Add(new Country() { ID = 74, ContinentCode = "EU", Name = "Finland, Republic of" });
            Countries.Add(new Country() { ID = 75, ContinentCode = "EU", Name = "France, French Republic" });
            Countries.Add(new Country() { ID = 76, ContinentCode = "SA", Name = "French Guiana" });
            Countries.Add(new Country() { ID = 77, ContinentCode = "OC", Name = "French Polynesia" });
            Countries.Add(new Country() { ID = 78, ContinentCode = "AN", Name = "French Southern Territories" });
            Countries.Add(new Country() { ID = 79, ContinentCode = "AF", Name = "Gabon, Gabonese Republic" });
            Countries.Add(new Country() { ID = 80, ContinentCode = "AF", Name = "Gambia, Republic of the" });
            Countries.Add(new Country() { ID = 81, ContinentCode = "AS", Name = "Georgia" });
            Countries.Add(new Country() { ID = 82, ContinentCode = "EU", Name = "Germany, Federal Republic of" });
            Countries.Add(new Country() { ID = 83, ContinentCode = "AF", Name = "Ghana, Republic of" });
            Countries.Add(new Country() { ID = 84, ContinentCode = "EU", Name = "Gibraltar" });
            Countries.Add(new Country() { ID = 85, ContinentCode = "EU", Name = "Greece, Hellenic Republic" });
            Countries.Add(new Country() { ID = 86, ContinentCode = "NA", Name = "Greenland" });
            Countries.Add(new Country() { ID = 87, ContinentCode = "NA", Name = "Grenada" });
            Countries.Add(new Country() { ID = 88, ContinentCode = "NA", Name = "Guadeloupe" });
            Countries.Add(new Country() { ID = 89, ContinentCode = "OC", Name = "Guam" });
            Countries.Add(new Country() { ID = 90, ContinentCode = "NA", Name = "Guatemala, Republic of" });
            Countries.Add(new Country() { ID = 91, ContinentCode = "EU", Name = "Guernsey, Bailiwick of" });
            Countries.Add(new Country() { ID = 92, ContinentCode = "AF", Name = "Guinea, Republic of" });
            Countries.Add(new Country() { ID = 93, ContinentCode = "AF", Name = "Guinea-Bissau, Republic of" });
            Countries.Add(new Country() { ID = 94, ContinentCode = "SA", Name = "Guyana, Co-operative Republicof" });
            Countries.Add(new Country() { ID = 95, ContinentCode = "NA", Name = "Haiti, Republic of" });
            Countries.Add(new Country() { ID = 96, ContinentCode = "AN", Name = "Heard Island and McDonald Islands" });
            Countries.Add(new Country() { ID = 97, ContinentCode = "EU", Name = "Holy See (Vatican City State)" });
            Countries.Add(new Country() { ID = 98, ContinentCode = "NA", Name = "Honduras, Republic of" });
            Countries.Add(new Country() { ID = 99, ContinentCode = "AS", Name = "Hong Kong, Special Administrative Region of China" });
            Countries.Add(new Country() { ID = 100, ContinentCode = "EU", Name = "Hungary, Republic of" });
            Countries.Add(new Country() { ID = 101, ContinentCode = "EU", Name = "Iceland, Republic of" });
            Countries.Add(new Country() { ID = 102, ContinentCode = "AS", Name = "India, Republic of" });
            Countries.Add(new Country() { ID = 103, ContinentCode = "AS", Name = "Indonesia, Republic of" });
            Countries.Add(new Country() { ID = 104, ContinentCode = "AS", Name = "Iran, Islamic Republic of" });
            Countries.Add(new Country() { ID = 105, ContinentCode = "AS", Name = "Iraq, Republic of" });
            Countries.Add(new Country() { ID = 106, ContinentCode = "EU", Name = "Ireland" });
            Countries.Add(new Country() { ID = 107, ContinentCode = "EU", Name = "Isle of Man" });
            Countries.Add(new Country() { ID = 108, ContinentCode = "AS", Name = "Israel, State of" });
            Countries.Add(new Country() { ID = 109, ContinentCode = "EU", Name = "Italy, Italian Republic" });
            Countries.Add(new Country() { ID = 110, ContinentCode = "NA", Name = "Jamaica" });
            Countries.Add(new Country() { ID = 111, ContinentCode = "AS", Name = "Japan" });
            Countries.Add(new Country() { ID = 112, ContinentCode = "EU", Name = "Jersey, Bailiwick of" });
            Countries.Add(new Country() { ID = 113, ContinentCode = "AS", Name = "Jordan, Hashemite Kingdom of" });
            Countries.Add(new Country() { ID = 114, ContinentCode = "AS", Name = "Kazakhstan, Republic of" });
            Countries.Add(new Country() { ID = 115, ContinentCode = "AF", Name = "Kenya, Republic of" });
            Countries.Add(new Country() { ID = 116, ContinentCode = "OC", Name = "Kiribati, Republic of" });
            Countries.Add(new Country() { ID = 117, ContinentCode = "AS", Name = "Korea, Democratic People's Republic of" });
            Countries.Add(new Country() { ID = 118, ContinentCode = "AS", Name = "Korea, Republic of" });
            Countries.Add(new Country() { ID = 119, ContinentCode = "AS", Name = "Kuwait, State of" });
            Countries.Add(new Country() { ID = 120, ContinentCode = "AS", Name = "Kyrgyz Republic" });
            Countries.Add(new Country() { ID = 121, ContinentCode = "AS", Name = "Lao People's Democratic Republic" });
            Countries.Add(new Country() { ID = 122, ContinentCode = "EU", Name = "Latvia, Republic of" });
            Countries.Add(new Country() { ID = 123, ContinentCode = "AS", Name = "Lebanon, Lebanese Republic" });
            Countries.Add(new Country() { ID = 124, ContinentCode = "AF", Name = "Lesotho, Kingdom of" });
            Countries.Add(new Country() { ID = 125, ContinentCode = "AF", Name = "Liberia, Republic of" });
            Countries.Add(new Country() { ID = 126, ContinentCode = "AF", Name = "Libyan Arab Jamahiriya" });
            Countries.Add(new Country() { ID = 127, ContinentCode = "EU", Name = "Liechtenstein, Principality of" });
            Countries.Add(new Country() { ID = 128, ContinentCode = "EU", Name = "Lithuania, Republic of" });
            Countries.Add(new Country() { ID = 129, ContinentCode = "EU", Name = "Luxembourg, Grand Duchy of" });
            Countries.Add(new Country() { ID = 130, ContinentCode = "AS", Name = "Macao, Special AdministrativeRegion of China" });
            Countries.Add(new Country() { ID = 131, ContinentCode = "EU", Name = "Macedonia, Republic of" });
            Countries.Add(new Country() { ID = 132, ContinentCode = "AF", Name = "Madagascar, Republic of" });
            Countries.Add(new Country() { ID = 133, ContinentCode = "AF", Name = "Malawi, Republic of" });
            Countries.Add(new Country() { ID = 134, ContinentCode = "AS", Name = "Malaysia" });
            Countries.Add(new Country() { ID = 135, ContinentCode = "AS", Name = "Maldives, Republic of" });
            Countries.Add(new Country() { ID = 136, ContinentCode = "AF", Name = "Mali, Republic of" });
            Countries.Add(new Country() { ID = 137, ContinentCode = "EU", Name = "Malta, Republic of" });
            Countries.Add(new Country() { ID = 138, ContinentCode = "OC", Name = "Marshall Islands, Republic ofthe" });
            Countries.Add(new Country() { ID = 139, ContinentCode = "NA", Name = "Martinique" });
            Countries.Add(new Country() { ID = 140, ContinentCode = "AF", Name = "Mauritania, Islamic Republicof" });
            Countries.Add(new Country() { ID = 141, ContinentCode = "AF", Name = "Mauritius, Republic of" });
            Countries.Add(new Country() { ID = 142, ContinentCode = "AF", Name = "Mayotte" });
            Countries.Add(new Country() { ID = 143, ContinentCode = "NA", Name = "Mexico, United Mexican States" });
            Countries.Add(new Country() { ID = 144, ContinentCode = "OC", Name = "Micronesia, Federated Statesof" });
            Countries.Add(new Country() { ID = 145, ContinentCode = "EU", Name = "Moldova, Republic of" });
            Countries.Add(new Country() { ID = 146, ContinentCode = "EU", Name = "Monaco, Principality of" });
            Countries.Add(new Country() { ID = 147, ContinentCode = "AS", Name = "Mongolia" });
            Countries.Add(new Country() { ID = 148, ContinentCode = "EU", Name = "Montenegro, Republic of" });
            Countries.Add(new Country() { ID = 149, ContinentCode = "NA", Name = "Montserrat" });
            Countries.Add(new Country() { ID = 150, ContinentCode = "AF", Name = "Morocco, Kingdom of" });
            Countries.Add(new Country() { ID = 151, ContinentCode = "AF", Name = "Mozambique, Republic of" });
            Countries.Add(new Country() { ID = 152, ContinentCode = "AS", Name = "Myanmar, Union of" });
            Countries.Add(new Country() { ID = 153, ContinentCode = "AF", Name = "Namibia, Republic of" });
            Countries.Add(new Country() { ID = 154, ContinentCode = "OC", Name = "Nauru, Republic of" });
            Countries.Add(new Country() { ID = 155, ContinentCode = "AS", Name = "Nepal, State of" });
            Countries.Add(new Country() { ID = 156, ContinentCode = "NA", Name = "Netherlands Antilles" });
            Countries.Add(new Country() { ID = 157, ContinentCode = "EU", Name = "Netherlands, Kingdom of the" });
            Countries.Add(new Country() { ID = 158, ContinentCode = "OC", Name = "New Caledonia" });
            Countries.Add(new Country() { ID = 159, ContinentCode = "OC", Name = "New Zealand" });
            Countries.Add(new Country() { ID = 160, ContinentCode = "NA", Name = "Nicaragua, Republic of" });
            Countries.Add(new Country() { ID = 161, ContinentCode = "AF", Name = "Niger, Republic of" });
            Countries.Add(new Country() { ID = 162, ContinentCode = "AF", Name = "Nigeria, Federal Republic of" });
            Countries.Add(new Country() { ID = 163, ContinentCode = "OC", Name = "Niue" });
            Countries.Add(new Country() { ID = 164, ContinentCode = "OC", Name = "Norfolk Island" });
            Countries.Add(new Country() { ID = 165, ContinentCode = "OC", Name = "Northern Mariana Islands, Commonwealth of the" });
            Countries.Add(new Country() { ID = 166, ContinentCode = "EU", Name = "Norway, Kingdom of" });
            Countries.Add(new Country() { ID = 167, ContinentCode = "AS", Name = "Oman, Sultanate of" });
            Countries.Add(new Country() { ID = 168, ContinentCode = "AS", Name = "Pakistan, Islamic Republic of" });
            Countries.Add(new Country() { ID = 169, ContinentCode = "OC", Name = "Palau, Republic of" });
            Countries.Add(new Country() { ID = 170, ContinentCode = "AS", Name = "Palestinian Territory, Occupied" });
            Countries.Add(new Country() { ID = 171, ContinentCode = "NA", Name = "Panama, Republic of" });
            Countries.Add(new Country() { ID = 172, ContinentCode = "OC", Name = "Papua New Guinea, IndependentState of" });
            Countries.Add(new Country() { ID = 173, ContinentCode = "SA", Name = "Paraguay, Republic of" });
            Countries.Add(new Country() { ID = 174, ContinentCode = "SA", Name = "Peru, Republic of" });
            Countries.Add(new Country() { ID = 175, ContinentCode = "AS", Name = "Philippines, Republic of the" });
            Countries.Add(new Country() { ID = 176, ContinentCode = "OC", Name = "Pitcairn Islands" });
            Countries.Add(new Country() { ID = 177, ContinentCode = "EU", Name = "Poland, Republic of" });
            Countries.Add(new Country() { ID = 178, ContinentCode = "EU", Name = "Portugal, Portuguese Republic" });
            Countries.Add(new Country() { ID = 179, ContinentCode = "NA", Name = "Puerto Rico, Commonwealth of" });
            Countries.Add(new Country() { ID = 180, ContinentCode = "AS", Name = "Qatar, State of" });
            Countries.Add(new Country() { ID = 181, ContinentCode = "AF", Name = "Reunion" });
            Countries.Add(new Country() { ID = 182, ContinentCode = "EU", Name = "Romania" });
            Countries.Add(new Country() { ID = 183, ContinentCode = "EU", Name = "Russian Federation" });
            Countries.Add(new Country() { ID = 184, ContinentCode = "AF", Name = "Rwanda, Republic of" });
            Countries.Add(new Country() { ID = 185, ContinentCode = "NA", Name = "Saint Barthelemy" });
            Countries.Add(new Country() { ID = 186, ContinentCode = "AF", Name = "Saint Helena" });
            Countries.Add(new Country() { ID = 187, ContinentCode = "NA", Name = "Saint Kitts and Nevis, Federation of" });
            Countries.Add(new Country() { ID = 188, ContinentCode = "NA", Name = "Saint Lucia" });
            Countries.Add(new Country() { ID = 189, ContinentCode = "NA", Name = "Saint Martin" });
            Countries.Add(new Country() { ID = 190, ContinentCode = "NA", Name = "Saint Pierre and Miquelon" });
            Countries.Add(new Country() { ID = 191, ContinentCode = "NA", Name = "Saint Vincent and the Grenadines" });
            Countries.Add(new Country() { ID = 192, ContinentCode = "OC", Name = "Samoa, Independent State of" });
            Countries.Add(new Country() { ID = 193, ContinentCode = "EU", Name = "San Marino, Republic of" });
            Countries.Add(new Country() { ID = 194, ContinentCode = "AF", Name = "Sao Tome and Principe, Democratic Republic of" });
            Countries.Add(new Country() { ID = 195, ContinentCode = "AS", Name = "Saudi Arabia, Kingdom of" });
            Countries.Add(new Country() { ID = 196, ContinentCode = "AF", Name = "Senegal, Republic of" });
            Countries.Add(new Country() { ID = 197, ContinentCode = "EU", Name = "Serbia, Republic of" });
            Countries.Add(new Country() { ID = 198, ContinentCode = "AF", Name = "Seychelles, Republic of" });
            Countries.Add(new Country() { ID = 199, ContinentCode = "AF", Name = "Sierra Leone, Republic of" });
            Countries.Add(new Country() { ID = 200, ContinentCode = "AS", Name = "Singapore, Republic of" });
            Countries.Add(new Country() { ID = 201, ContinentCode = "EU", Name = "Slovakia (Slovak Republic)" });
            Countries.Add(new Country() { ID = 202, ContinentCode = "EU", Name = "Slovenia, Republic of" });
            Countries.Add(new Country() { ID = 203, ContinentCode = "OC", Name = "Solomon Islands" });
            Countries.Add(new Country() { ID = 204, ContinentCode = "AF", Name = "Somalia, Somali Republic" });
            Countries.Add(new Country() { ID = 205, ContinentCode = "AF", Name = "South Africa, Republic of" });
            Countries.Add(new Country() { ID = 206, ContinentCode = "AN", Name = "South Georgia and the South Sandwich Islands" });
            Countries.Add(new Country() { ID = 207, ContinentCode = "EU", Name = "Spain, Kingdom of" });
            Countries.Add(new Country() { ID = 208, ContinentCode = "AS", Name = "Sri Lanka, Democratic Socialist Republic of" });
            Countries.Add(new Country() { ID = 209, ContinentCode = "AF", Name = "Sudan, Republic of" });
            Countries.Add(new Country() { ID = 210, ContinentCode = "SA", Name = "Suriname, Republic of" });
            Countries.Add(new Country() { ID = 211, ContinentCode = "EU", Name = "Svalbard & Jan Mayen Islands" });
            Countries.Add(new Country() { ID = 212, ContinentCode = "AF", Name = "Swaziland, Kingdom of" });
            Countries.Add(new Country() { ID = 213, ContinentCode = "EU", Name = "Sweden, Kingdom of" });
            Countries.Add(new Country() { ID = 214, ContinentCode = "EU", Name = "Switzerland, Swiss Confederation" });
            Countries.Add(new Country() { ID = 215, ContinentCode = "AS", Name = "Syrian Arab Republic" });
            Countries.Add(new Country() { ID = 216, ContinentCode = "AS", Name = "Taiwan" });
            Countries.Add(new Country() { ID = 217, ContinentCode = "AS", Name = "Tajikistan, Republic of" });
            Countries.Add(new Country() { ID = 218, ContinentCode = "AF", Name = "Tanzania, United Republic of" });
            Countries.Add(new Country() { ID = 219, ContinentCode = "AS", Name = "Thailand, Kingdom of" });
            Countries.Add(new Country() { ID = 220, ContinentCode = "AS", Name = "Timor-Leste, Democratic Republic of" });
            Countries.Add(new Country() { ID = 221, ContinentCode = "AF", Name = "Togo, Togolese Republic" });
            Countries.Add(new Country() { ID = 222, ContinentCode = "OC", Name = "Tokelau" });
            Countries.Add(new Country() { ID = 223, ContinentCode = "OC", Name = "Tonga, Kingdom of" });
            Countries.Add(new Country() { ID = 224, ContinentCode = "NA", Name = "Trinidad and Tobago, Republicof" });
            Countries.Add(new Country() { ID = 225, ContinentCode = "AF", Name = "Tunisia, Tunisian Republic" });
            Countries.Add(new Country() { ID = 226, ContinentCode = "AS", Name = "Turkey, Republic of" });
            Countries.Add(new Country() { ID = 227, ContinentCode = "AS", Name = "Turkmenistan" });
            Countries.Add(new Country() { ID = 228, ContinentCode = "NA", Name = "Turks and Caicos Islands" });
            Countries.Add(new Country() { ID = 229, ContinentCode = "OC", Name = "Tuvalu" });
            Countries.Add(new Country() { ID = 230, ContinentCode = "AF", Name = "Uganda, Republic of" });
            Countries.Add(new Country() { ID = 231, ContinentCode = "EU", Name = "Ukraine" });
            Countries.Add(new Country() { ID = 232, ContinentCode = "AS", Name = "United Arab Emirates" });
            Countries.Add(new Country() { ID = 233, ContinentCode = "EU", Name = "United Kingdom of Great Britain & Northern Ireland" });
            Countries.Add(new Country() { ID = 234, ContinentCode = "NA", Name = "United States of America" });
            Countries.Add(new Country() { ID = 235, ContinentCode = "OC", Name = "United States Minor OutlyingIslands" });
            Countries.Add(new Country() { ID = 236, ContinentCode = "NA", Name = "United States Virgin Islands" });
            Countries.Add(new Country() { ID = 237, ContinentCode = "SA", Name = "Uruguay, Eastern Republic of" });
            Countries.Add(new Country() { ID = 238, ContinentCode = "AS", Name = "Uzbekistan, Republic of" });
            Countries.Add(new Country() { ID = 239, ContinentCode = "OC", Name = "Vanuatu, Republic of" });
            Countries.Add(new Country() { ID = 240, ContinentCode = "SA", Name = "Venezuela, Bolivarian Republic of" });
            Countries.Add(new Country() { ID = 241, ContinentCode = "AS", Name = "Vietnam, Socialist Republic of" });
            Countries.Add(new Country() { ID = 242, ContinentCode = "OC", Name = "Wallis and Futuna" });
            Countries.Add(new Country() { ID = 243, ContinentCode = "AF", Name = "Western Sahara" });
            Countries.Add(new Country() { ID = 244, ContinentCode = "AS", Name = "Yemen" });
            Countries.Add(new Country() { ID = 245, ContinentCode = "AF", Name = "Zambia, Republic of" });
            Countries.Add(new Country() { ID = 246, ContinentCode = "AF", Name = "Zimbabwe, Republic of" });
            #endregion
            #region  generated code
            Continents.Add(new Continent() { Code = "AS", Name = "Asia" });
            Continents.Add(new Continent() { Code = "EU", Name = "Europe" });
            Continents.Add(new Continent() { Code = "AF", Name = "Africa" });
            Continents.Add(new Continent() { Code = "OC", Name = "Oceania" });
            Continents.Add(new Continent() { Code = "NA", Name = "North America" });
            Continents.Add(new Continent() { Code = "SA", Name = "South America" });
            Continents.Add(new Continent() { Code = "AN", Name = "Antarctica" });
            #endregion
        }
    }

    public class Continent
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ContinentCode { get; set; }
    }
}
