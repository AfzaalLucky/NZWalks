using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddWalkDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }
    }
}
