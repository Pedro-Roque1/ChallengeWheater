namespace WheaterAppApi.Models
{
    public class FavoriteCity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
