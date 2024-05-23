using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Identity.DataAccess.Models.Entities;

public class UserEntity
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[Required]
	public required string Username { get; set; }

    [Required]
    public required byte[] PasswordHash { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required List<string> Roles { get; set; }
}