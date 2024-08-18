using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Server.DbEntities;
internal class Book
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public required string Title { get; set; }
    [Display(Name = "Author")]
    public virtual int AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public virtual required Author Author { get; set; }
    public int Year { get; set; }
    public BookGenre Genre { get; set; }
}
