using System.ComponentModel.DataAnnotations.Schema;

namespace LibaryManagementSystem.Core.Models.Entities
{
    public class Author
    {
        [Column("AuthorId")]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
