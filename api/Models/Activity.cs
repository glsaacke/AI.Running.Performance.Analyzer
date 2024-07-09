namespace api.Models
{
    public class Activity
    {
        public int activityID { get; set; }
        public string name { get; set;}
        public string type { get; set;}
        public int distance { get; set; }
        public int duration { get; set; }
        public int pace { get; set; }
        public int heartrate { get; set; }

        
    }
}