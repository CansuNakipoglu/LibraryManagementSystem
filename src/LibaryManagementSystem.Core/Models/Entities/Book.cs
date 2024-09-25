using System.ComponentModel.DataAnnotations.Schema;

namespace LibaryManagementSystem.Core.Models.Entities
{
    public class Book
    {
        [Column("BookId")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }
        public int ISBN { get; set; }
        public int CopiesAvailable { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

    }
}
