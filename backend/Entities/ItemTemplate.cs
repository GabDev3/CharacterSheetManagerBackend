using System.ComponentModel.DataAnnotations;

namespace CharacterSheetManager.Models
{
    public class ItemTemplate
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Effect { get; set; } = string.Empty;
        
        
        public int? StrengthBonus { get; set; }
        public int? DexterityBonus { get; set; }
        public int? ConstitutionBonus { get; set; }
        public int? IntelligenceBonus { get; set; }
        public int? WisdomBonus { get; set; }
        public int? CharismaBonus { get; set; }
        
        
        public int? ArmorBonus { get; set; }
        
        [StringLength(50)]
        public string Category { get; set; } = "Miscellaneous"; 
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}