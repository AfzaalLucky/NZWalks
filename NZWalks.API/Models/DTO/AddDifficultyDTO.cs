using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddDifficultyDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
