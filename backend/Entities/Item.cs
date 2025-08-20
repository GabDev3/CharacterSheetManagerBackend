using System.ComponentModel.DataAnnotations;

namespace CharacterSheetManager.Models
{
    public class Item
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
        
        
        public int? ItemTemplateId { get; set; }
        public virtual ItemTemplate? ItemTemplate { get; set; }
        
        
        public int CharacterId { get; set; }
        public virtual Character Character { get; set; } = null!;
    }
}