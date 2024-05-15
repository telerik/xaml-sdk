
namespace CustomDragDropBehavior
{
    public class Meeting
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public Meeting Copy()
        {
            return new Meeting
            {
                Name = this.Name,
                ID = this.ID
            };
        }
    }
}
