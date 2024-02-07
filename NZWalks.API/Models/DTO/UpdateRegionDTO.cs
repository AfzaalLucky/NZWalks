using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionDTO
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Code must be minimum 3 characters.")]
        [MaxLength(3, ErrorMessage = "Code must be maximum 3 characters.")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
