using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Identity.API.Models.Entities;

public class UserEntity
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[Required]
	public required string Username { get; set; }

    [Required]
    public required string PasswordHash { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required UserRole Roles { get; set; }
}

public enum UserRole
{
	Admin = 1,
	Member = 2
}
