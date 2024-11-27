using System.ComponentModel.DataAnnotations;

namespace DriftRefactoringApp.Models
{
    public class Person
    {
        [Required]
        public int PersonId { get; set; }

        [MinLength(2)]
        public string Name { get; set; }

        [MinLength(2)]
        public string UserHandle { get; set; }

        public override string ToString()
        {
            return "[Id, Name, Handle] = [" + PersonId + ", " + Name + ", " + UserHandle + "]";
        }
    }
}