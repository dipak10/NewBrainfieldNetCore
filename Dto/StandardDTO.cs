using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Dto
{
    public class StandardDTO
    {        
        [Key]
        public int StandardID { get; set; }

        [Display(Name = "Standard Name")]

        public string StandardName { get; set; }
    }
}
