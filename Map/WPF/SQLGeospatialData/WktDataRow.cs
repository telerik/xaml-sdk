namespace SQLGeospatialData
{
    public class WktDataRow
    {
        private string _name;
        private string _geometry;

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public string Geometry
        {
            get
            {
                return this._geometry;
            }
            set
            {
                this._geometry = value;
            }
        }
    }
}
