namespace DataBinding
{
    public class Task
    {
        public Employee Assignee { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public bool ShowIndicator { get; set; }
        public string CategoryName { get; set; }
        public List<object> Tags { get; set; }
    }
}
