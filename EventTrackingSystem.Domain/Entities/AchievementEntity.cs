using System.ComponentModel.DataAnnotations;

namespace EventTrackingSystem.Domain.Entities
{
    public class AchievementEntity
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;
        public string PhotoPath { get; set; } = null!;
    }
}
