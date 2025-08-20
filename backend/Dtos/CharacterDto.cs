using System.ComponentModel.DataAnnotations;

namespace CharacterSheetManager.DTOs.Character
{
    public class CharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public int Level { get; set; }
        public int ArmorClass { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public string? ImageBase64 { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public List<ItemDto> Items { get; set; } = new();
        public List<SpellDto> Spells { get; set; } = new();
    }

    public class CreateCharacterDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Class { get; set; } = string.Empty;
        
        [Range(1, 20)]
        public int Level { get; set; } = 1;
        
        [Range(1, 30)]
        public int ArmorClass { get; set; } = 10;
        
        [Range(1, 20)]
        public int Strength { get; set; } = 10;
        
        [Range(1, 20)]
        public int Dexterity { get; set; } = 10;
        
        [Range(1, 20)]
        public int Constitution { get; set; } = 10;
        
        [Range(1, 20)]
        public int Intelligence { get; set; } = 10;
        
        [Range(1, 20)]
        public int Wisdom { get; set; } = 10;
        
        [Range(1, 20)]
        public int Charisma { get; set; } = 10;
        
        public string? ImageBase64 { get; set; }
    }

    
    public class UpdateCharacterDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Class { get; set; } = string.Empty;
        
        [Range(1, 20)]
        public int Level { get; set; }
        
        [Range(1, 30)]
        public int ArmorClass { get; set; }
        
        [Range(1, 20)]
        public int Strength { get; set; }
        
        [Range(1, 20)]
        public int Dexterity { get; set; }
        
        [Range(1, 20)]
        public int Constitution { get; set; }
        
        [Range(1, 20)]
        public int Intelligence { get; set; }
        
        [Range(1, 20)]
        public int Wisdom { get; set; }
        
        [Range(1, 20)]
        public int Charisma { get; set; }
        
        public string? ImageBase64 { get; set; }
    }

    
    public class PatchCharacterDto
    {
        [StringLength(100, MinimumLength = 1)]
        public string? Name { get; set; }
        
        [StringLength(50, MinimumLength = 1)]
        public string? Class { get; set; }
        
        [Range(1, 20)]
        public int? Level { get; set; }
        
        [Range(1, 30)]
        public int? ArmorClass { get; set; }
        
        [Range(1, 20)]
        public int? Strength { get; set; }
        
        [Range(1, 20)]
        public int? Dexterity { get; set; }
        
        [Range(1, 20)]
        public int? Constitution { get; set; }
        
        [Range(1, 20)]
        public int? Intelligence { get; set; }
        
        [Range(1, 20)]
        public int? Wisdom { get; set; }
        
        [Range(1, 20)]
        public int? Charisma { get; set; }
        
        public string? ImageBase64 { get; set; }
    }

    
    public class CharacterSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public int Level { get; set; }
        public int ArmorClass { get; set; }
        public string? ImageBase64 { get; set; } 
        public DateTime CreatedAt { get; set; }
    }

    
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
    }

    public class SpellDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Effect { get; set; } = string.Empty;
        public string? Damage { get; set; }
        public int? SpellTemplateId { get; set; }
        public string? SpellTemplateName { get; set; }
    }
}