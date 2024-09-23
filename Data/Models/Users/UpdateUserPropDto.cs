namespace WorkoutTrackerApi.Data.Models.Users
{
    public class UpdateUserPropDto
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public double BodyWeight { get; set; }
        public double BodyHeight { get; set; }
    }
}
