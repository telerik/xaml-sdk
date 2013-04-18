using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SQLGeospatialData
{
    public class WktDataViewModel
    {
        private Collection<WktDataRow> _wktDataCollection;

        public WktDataViewModel()
        {
            this.WktDataCollection = new Collection<WktDataRow>();

            WktDataRow northAreaDataRow = new WktDataRow();
            northAreaDataRow.Name = "North Area";
            northAreaDataRow.Geometry = "Polygon (("
                    + "-84.3932461670301, 33.7967217961125"
                    + ", -84.418995373573 33.808989109452"
                    + ", -84.4303250244518 33.8377961143588"
                    + ", -84.4303250244518 33.8480616114576"
                    + ", -84.4320416382213 33.8563301427474"
                    + ", -84.4413113525767 33.8668784860621"
                    + ", -84.4605374267954 33.8908212383562"
                    + ", -84.447147839393 33.8996555568394"
                    + ", -84.4169354370492 33.9161818236152"
                    + ", -84.384663098182 33.9127628588944"
                    + ", -84.3788266113656 33.9133326958734"
                    + ", -84.3633770874397 33.9101985453388"
                    + ", -84.3616604736702 33.9101985453388"
                    + ", -84.3300747803108 33.9204553366171"
                    + ", -84.2957425049201 33.9198855472676"
                    + ", -84.2596936157599 33.8919612018418"
                    + ", -84.2713665893927 33.8831260863279"
                    + ", -84.2768597534552 33.8745751033359"
                    + ", -84.2905926636114 33.8640277109635"
                    + ", -84.3039822510137 33.8480616114574"
                    + ", -84.3125653198613 33.8397922798236"
                    + ", -84.3489575317754 33.8249625088656"
                    + ", -84.3589138916386 33.8226807773054"
                    + ", -84.3688702515018 33.8124122318747"
                    + ", -84.3822598389041 33.8058511269131"
                    + ", -84.3932461670301 33.7967217961125"
                    + ", -84.3932461670301 33.7967217961125"
                    + "))";

            this.WktDataCollection.Add(northAreaDataRow);

            WktDataRow store1DataRow = new WktDataRow();
            store1DataRow.Name = "Mr. Dorrell";
            store1DataRow.Geometry = "Point (-84.3827345898996 33.858244576749)";
            this.WktDataCollection.Add(store1DataRow);

            WktDataRow store2DataRow = new WktDataRow();
            store2DataRow.Name = "Family Store";
            store2DataRow.Geometry = "Point (-84.3842473557835 33.840489590028)";
            this.WktDataCollection.Add(store2DataRow);

            WktDataRow store3DataRow = new WktDataRow();
            store3DataRow.Name = "Greenings";
            store3DataRow.Geometry = "Point (-84.308161132657 33.8936528837044)";
            this.WktDataCollection.Add(store3DataRow);

            WktDataRow store4DataRow = new WktDataRow();
            store4DataRow.Name = "Ted's";
            store4DataRow.Geometry = "Point (-84.413727515064 33.906362188132)";
            this.WktDataCollection.Add(store4DataRow);

            WktDataRow eastAreaDataRow = new WktDataRow();
            eastAreaDataRow.Name = "East Area";
            eastAreaDataRow.Geometry = "Polygon (("
                    + "-84.2596936157599 33.8919612018418"
                    + ", -84.2535138061902 33.8825560474539"
                    + ", -84.2480206421277 33.8437844708754"
                    + ", -84.2562603882215 33.8300961822591"
                    + ", -84.2493939331434 33.7913007952029"
                    + ", -84.2274212768934 33.7650468834483"
                    + ", -84.2301678589246 33.7422108552654"
                    + ", -84.2411541870496 33.7159419049292"
                    + ", -84.261753552284 33.7182264806728"
                    + ", -84.2892193725965 33.7102301996174"
                    + ", -84.3249249390027 33.7239376540615"
                    + ", -84.3249249390027 33.7399269179405"
                    + ", -84.3317913940808 33.7467785474731"
                    + ", -84.3523907593152 33.743352801123"
                    + ", -84.3743634155652 33.745636647227"
                    + ", -84.3935894897839 33.7462075992506"
                    + ", -84.3784832886121 33.7593384466376"
                    + ", -84.3908429077527 33.7661885251864"
                    + ", -84.3904995849988 33.7807431241315"
                    + ", -84.3908429077527 33.7901594881121"
                    + ", -84.3932461670301 33.7967217961125"
                    + ", -84.3822598389041 33.8058511269131"
                    + ", -84.3688702515018 33.8124122318747"
                    + ", -84.3589138916386 33.8226807773054"
                    + ", -84.3489575317754 33.8249625088656"
                    + ", -84.3125653198613 33.8397922798236"
                    + ", -84.3039822510137 33.8480616114574"
                    + ", -84.2905926636114 33.8640277109635"
                    + ", -84.2768597534552 33.8745751033359"
                    + ", -84.2713665893927 33.8831260863279"
                    + ", -84.2596936157599 33.8919612018418"
                    + "))";

            this.WktDataCollection.Add(eastAreaDataRow);

            store1DataRow = new WktDataRow();
            store1DataRow.Name = "Fresh & Green";
            store1DataRow.Geometry = "Point (-84.3137723139137 33.815569818206)";
            this.WktDataCollection.Add(store1DataRow);

            store2DataRow = new WktDataRow();
            store2DataRow.Name = "Dominos";
            store2DataRow.Geometry = "Point (-84.3658608129834 33.7727478963652)";
            this.WktDataCollection.Add(store2DataRow);

            store3DataRow = new WktDataRow();
            store3DataRow.Name = "Ellinor";
            store3DataRow.Geometry = "Point (-84.2466419866888 33.7779395401149)";
            this.WktDataCollection.Add(store3DataRow);

            store4DataRow = new WktDataRow();
            store4DataRow.Name = "NearBy";
            store4DataRow.Geometry = "Point (-84.2415028742161 33.7766999578522)";
            this.WktDataCollection.Add(store4DataRow);

            WktDataRow store5DataRow = new WktDataRow();
            store5DataRow.Name = "Perfecto";
            store5DataRow.Geometry = "Point (-84.2783510616587 33.7223916381193)";
            this.WktDataCollection.Add(store5DataRow);

            WktDataRow southEastAreaDataRow = new WktDataRow();
            southEastAreaDataRow.Name = "South-East Area";
            southEastAreaDataRow.Geometry = "Polygon (("
                    + "-84.2411541870496 33.7159419049292"
                    + ", -84.2562603882214 33.7028044147553"
                    + ", -84.2796063354871 33.6970918357038"
                    + ", -84.3043255737683 33.6868082362484"
                    + ", -84.3208050659558 33.6765234063122"
                    + ", -84.367496960487 33.6548069475491"
                    + ", -84.3839764526745 33.6342284039869"
                    + ", -84.3990826538464 33.6250808058726"
                    + ", -84.4869732788464 33.6182194698272"
                    + ", -84.4622540405652 33.6307981685331"
                    + ", -84.4526410034558 33.6422317554189"
                    + ", -84.4389080932996 33.6525206856264"
                    + ", -84.4196820190808 33.6673803024105"
                    + ", -84.4059491089245 33.6959492743123"
                    + ", -84.4059491089245 33.7085166139405"
                    + ", -84.3922161987683 33.7210821148631"
                    + ", -84.3935894897839 33.7462075992506"
                    + ", -84.3743634155652 33.745636647227"
                    + ", -84.3523907593152 33.743352801123"
                    + ", -84.3317913940808 33.7467785474731"
                    + ", -84.3249249390027 33.7399269179405"
                    + ", -84.3249249390027 33.7239376540615"
                    + ", -84.2892193725965 33.7102301996174"
                    + ", -84.261753552284 33.7182264806728"
                    + ", -84.2411541870496 33.7159419049292"
                    + "))";

            this.WktDataCollection.Add(southEastAreaDataRow);

            store1DataRow = new WktDataRow();
            store1DataRow.Name = "LollyHolly";
            store1DataRow.Geometry = "Point (-84.3795239856981 33.7394093095038)";
            this.WktDataCollection.Add(store1DataRow);

            store2DataRow = new WktDataRow();
            store2DataRow.Name = "The Favourites";
            store2DataRow.Geometry = "Point (-84.3926614454527 33.7063308812285)";
            this.WktDataCollection.Add(store2DataRow);

            store3DataRow = new WktDataRow();
            store3DataRow.Name = "Quality Food";
            store3DataRow.Geometry = "Point (-84.2943745783107 33.6978423497645)";
            this.WktDataCollection.Add(store3DataRow);

            store4DataRow = new WktDataRow();
            store4DataRow.Name = "Marrie and Jack";
            store4DataRow.Geometry = "Point (-84.3917763164772 33.6519384707607)";
            this.WktDataCollection.Add(store4DataRow);

            WktDataRow southWestAreaDataRow = new WktDataRow();
            southWestAreaDataRow.Name = "South-West Area";
            southWestAreaDataRow.Geometry = "Polygon (("
                    + "-84.4869732788464 33.6182194698272"
                    + ", -84.4979596069714 33.6456615354618"
                    + ", -84.4993328979871 33.6753805714828"
                    + ", -84.5034527710339 33.6868082362484"
                    + ", -84.4965863159558 33.7062317798524"
                    + ", -84.5034527710339 33.7233665538214"
                    + ", -84.4952130249402 33.7530587270419"
                    + ", -84.4938397339246 33.7667593403523"
                    + ", -84.4608807495496 33.7530587270419"
                    + ", -84.4292950561902 33.7462075992506"
                    + ", -84.4169354370495 33.7427818300946"
                    + ", -84.3935894897839 33.7462075992506"
                    + ", -84.3922161987683 33.7210821148631"
                    + ", -84.4059491089245 33.7085166139405"
                    + ", -84.4059491089245 33.6959492743123"
                    + ", -84.4196820190808 33.6673803024105"
                    + ", -84.4389080932996 33.6525206856264"
                    + ", -84.4526410034558 33.6422317554189"
                    + ", -84.4622540405652 33.6307981685331"
                    + ", -84.4869732788464 33.6182194698272"
                    + ", -84.4869732788464 33.6182194698272"
                    + "))";

            this.WktDataCollection.Add(southWestAreaDataRow);

            store1DataRow = new WktDataRow();
            store1DataRow.Name = "ShopAtOnes";
            store1DataRow.Geometry = "Point (-84.4330930641421 33.7374893033587)";
            this.WktDataCollection.Add(store1DataRow);

            store2DataRow = new WktDataRow();
            store2DataRow.Name = "GoShopping!";
            store2DataRow.Geometry = "Point (-84.4366067579503 33.6845986928175)";
            this.WktDataCollection.Add(store2DataRow);

            store3DataRow = new WktDataRow();
            store3DataRow.Name = "Variety";
            store3DataRow.Geometry = "Point (-84.4933301141971 33.6885377258407)";
            this.WktDataCollection.Add(store3DataRow);

            WktDataRow northWestAreaDataRow = new WktDataRow();
            northWestAreaDataRow.Name = "North-West Area";
            northWestAreaDataRow.Geometry = "Polygon (("
                    + "-84.4938397339246 33.7667593403523"
                    + ", -84.4965863159558 33.7861647934865"
                    + ", -84.4965863159558 33.805565850067"
                    + ", -84.4979596069714 33.821539888694"
                    + ", -84.4897198608777 33.8272441795402"
                    + ", -84.4897198608777 33.8454953528023"
                    + ", -84.4828534057996 33.8637426282186"
                    + ", -84.4759869507214 33.8728648039164"
                    + ", -84.4732403686902 33.8842661526508"
                    + ", -84.4622540405652 33.8899662557435"
                    + ", -84.4413113525767 33.8668784860621"
                    + ", -84.4320416382213 33.8563301427474"
                    + ", -84.4303250244518 33.8480616114576"
                    + ", -84.4303250244518 33.8377961143588"
                    + ", -84.418995373573 33.808989109452"
                    + ", -84.3932461670301 33.7967217961125"
                    + ", -84.3908429077527 33.7901594881121"
                    + ", -84.3904995849988 33.7807431241315"
                    + ", -84.3908429077527 33.7661885251864"
                    + ", -84.3784832886121 33.7593384466376"
                    + ", -84.3935894897839 33.7462075992506"
                    + ", -84.4169354370495 33.7427818300946"
                    + ", -84.4292950561902 33.7462075992506"
                    + ", -84.4608807495496 33.7530587270419"
                    + ", -84.4938397339246 33.7667593403523"
                    + "))";

            this.WktDataCollection.Add(northWestAreaDataRow);

            store1DataRow = new WktDataRow();
            store1DataRow.Name = "The Hit";
            store1DataRow.Geometry = "Point (-84.3932595780585 33.7492138045389)";
            this.WktDataCollection.Add(store1DataRow);

            store2DataRow = new WktDataRow();
            store2DataRow.Name = "Mrs. Smith";
            store2DataRow.Geometry = "Point (-84.4314542344275 33.7952549107929)";
            this.WktDataCollection.Add(store2DataRow);

            store3DataRow = new WktDataRow();
            store3DataRow.Name = "Your Store";
            store3DataRow.Geometry = "Point (-84.4860425522968 33.8410824657857)";
            this.WktDataCollection.Add(store3DataRow);
        }

        public Collection<WktDataRow> WktDataCollection
        {
            get
            {
                return this._wktDataCollection;
            }
            set
            {
                this._wktDataCollection = value;
            }
        }
    }
}
