using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Identity.API.Models.Entities;

public class UserEntity
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[Required]
	public string Username { get; set; }

	[Required]
	public string PasswordHash { get; set; }

	[Required]
	public string Email { get; set; }

	[Required]
	public UserRole Roles { get; set; }
}

public enum UserRole
{
	Admin = 1,
	Member = 2
}
