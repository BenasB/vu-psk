using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Models.Entities;

public class UserEntity
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[Required]
	public string Username { get; set; }

	public IList<int> AssociatedRecipes { get; set; } = new List<int>();	

	public DateTime DateRegistered { get; set; }
}
