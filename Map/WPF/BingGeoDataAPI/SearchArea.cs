namespace BingGeoDataAPI
{
    internal class SearchArea
    {
        public SearchArea(string name, Telerik.Windows.Controls.Map.Location loc, string key, string type = null)
        {
            this.Location = loc;

            this.EntityType = type != null ? type : "'PopulatedPlace'";
            this.LevelOfDetail = 2;
            this.GetAllPolygons = true;
            this.GetEntityMetadata = true;
            this.Culture = "'en-us'";
            this.UserRegion = "'US'";
            this.BingKey = key;
            this.Name = name;
        }

        // {0}
        public Telerik.Windows.Controls.Map.Location Location { get; set; }

        // {0}
        public string Address { get; set; }

        // {1}
        public int LevelOfDetail { get; set; }

        // {2}
        public string EntityType { get; set; }

        // {3} bool -> 0 or 1
        public bool GetAllPolygons { get; set; }

        // {4} bool -> 0 or 1
        public bool GetEntityMetadata { get; set; }

        // {5} 
        public string Culture { get; set; }

        // {6} 
        public string UserRegion { get; set; }

        // {7}
        public string BingKey { get; set; }

        public string Name { get; set; }
    }
}
