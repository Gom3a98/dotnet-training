using System.ComponentModel.DataAnnotations;


namespace TodoApi.Models
{
    public class TodoItem
    {
        [Required]
        [Range(1,100)]
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:20 , MinimumLength = 5)]
        public string Description { get; set; }
        public bool isCompeleted { get; set; }
    }
}
