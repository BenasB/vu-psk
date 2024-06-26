namespace Identity.Public;

public class User
{
    public required int Id { get; set; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public string ImageUrl => $"https://api.dicebear.com/8.x/thumbs/svg?seed={Username}";
}