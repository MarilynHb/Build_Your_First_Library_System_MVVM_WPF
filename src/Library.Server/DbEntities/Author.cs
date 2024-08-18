using System.ComponentModel.DataAnnotations;

namespace Library.Server.DbEntities;
internal class Author
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public required string FirstName { get; set; }
    [MaxLength(50)]
    public required string LastName { get; set; }
}
