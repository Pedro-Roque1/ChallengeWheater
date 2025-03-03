namespace WheaterAppApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Login { get; set; }
        public string? Email { get; set; }
        public required string Password { get; set; }
        public List<FavoriteCity>? FavoriteCities { get; set; }
    }
}
