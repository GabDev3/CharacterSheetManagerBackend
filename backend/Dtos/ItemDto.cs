using System.ComponentModel.DataAnnotations;

namespace CharacterSheetManager.DTOs.Item
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Effect { get; set; } = string.Empty;
        public int? StrengthBonus { get; set; }
        public int? DexterityBonus { get; set; }
        public int? ConstitutionBonus { get; set; }
        public int? IntelligenceBonus { get; set; }
        public int? WisdomBonus { get; set; }
        public int? CharismaBonus { get; set; }
        public int? ArmorBonus { get; set; }
        public int? ItemTemplateId { get; set; }
        public string? ItemTemplateName { get; set; }
        public int CharacterId { get; set; }
        public string CharacterName { get; set; } = string.Empty;
    }

    public class CreateItemDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Effect { get; set; } = string.Empty;
        
        [Range(-10, 10)]
        public int? StrengthBonus { get; set; }
        
        [Range(-10, 10)]
        public int? DexterityBonus { get; set; }
        
        [Range(-10, 10)]
        public int? ConstitutionBonus { get; set; }
        
        [Range(-10, 10)]
        public int? IntelligenceBonus { get; set; }
        
        [Range(-10, 10)]
        public int? WisdomBonus { get; set; }
        
        [Range(-10, 10)]
        public int? CharismaBonus { get; set; }
        
        [Range(-10, 20)]
        public int? ArmorBonus { get; set; }
        
        public int? ItemTemplateId { get; set; }
        
        [Required]
        public int CharacterId { get; set; }
    }

    public class UpdateItemDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Effect { get; set; } = string.Empty;
        
        [Range(-10, 10)]
        public int? StrengthBonus { get; set; }
        
        [Range(-10, 10)]
        public int? DexterityBonus { get; set; }
        
        [Range(-10, 10)]
        public int? ConstitutionBonus { get; set; }
        
        [Range(-10, 10)]
        public int? IntelligenceBonus { get; set; }
        
        [Range(-10, 10)]
        public int? WisdomBonus { get; set; }
        
        [Range(-10, 10)]
        public int? CharismaBonus { get; set; }
        
        [Range(-10, 20)]
        public int? ArmorBonus { get; set; }
        
        public int? ItemTemplateId { get; set; }
    }

    public class CreateItemFromTemplateDto
    {
        [Required]
        public int ItemTemplateId { get; set; }
        
        [Required]
        public int CharacterId { get; set; }
        
        public string? CustomName { get; set; }
        public string? CustomEffect { get; set; }
        
        
        public int? StrengthBonus { get; set; }
        public int? DexterityBonus { get; set; }
        public int? ConstitutionBonus { get; set; }
        public int? IntelligenceBonus { get; set; }
        public int? WisdomBonus { get; set; }
        public int? CharismaBonus { get; set; }
        public int? ArmorBonus { get; set; }
    }
}