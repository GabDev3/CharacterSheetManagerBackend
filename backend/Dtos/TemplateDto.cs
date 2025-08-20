using System.ComponentModel.DataAnnotations;

namespace CharacterSheetManager.DTOs.ItemTemplate
{
    public class ItemTemplateDto
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
        public string Category { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateItemTemplateDto
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
        
        [StringLength(50)]
        public string Category { get; set; } = "Miscellaneous";
        
        public bool IsActive { get; set; } = true;
    }

    public class UpdateItemTemplateDto
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
        
        [StringLength(50)]
        public string Category { get; set; } = "Miscellaneous";
        
        public bool IsActive { get; set; } = true;
    }
}

namespace CharacterSheetManager.DTOs.SpellTemplate
{
    public class SpellTemplateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Effect { get; set; } = string.Empty;
        public string? Damage { get; set; }
        public int Level { get; set; }
        public string School { get; set; } = string.Empty;
        public string CastingTime { get; set; } = string.Empty;
        public string Range { get; set; } = string.Empty;
        public string Components { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateSpellTemplateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Effect { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string? Damage { get; set; }
        
        [Range(0, 9)]
        public int Level { get; set; } = 1;
        
        [StringLength(50)]
        public string School { get; set; } = "Evocation";
        
        [StringLength(50)]
        public string CastingTime { get; set; } = "1 action";
        
        [StringLength(50)]
        public string Range { get; set; } = "Touch";
        
        [StringLength(100)]
        public string Components { get; set; } = "V, S";
        
        [StringLength(50)]
        public string Duration { get; set; } = "Instantaneous";
        
        public bool IsActive { get; set; } = true;
    }

    public class UpdateSpellTemplateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Effect { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string? Damage { get; set; }
        
        [Range(0, 9)]
        public int Level { get; set; }
        
        [StringLength(50)]
        public string School { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string CastingTime { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Range { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string Components { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Duration { get; set; } = string.Empty;
        
        public bool IsActive { get; set; } = true;
    }
}