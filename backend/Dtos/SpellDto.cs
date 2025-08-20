using System.ComponentModel.DataAnnotations;

namespace CharacterSheetManager.DTOs.Spell
{
    public class SpellDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Effect { get; set; } = string.Empty;
        public string? Damage { get; set; }
        public int? SpellTemplateId { get; set; }
        public string? SpellTemplateName { get; set; }
        public int CharacterId { get; set; }
        public string CharacterName { get; set; } = string.Empty;
        
        
        public int? Level { get; set; }
        public string? School { get; set; }
        public string? CastingTime { get; set; }
        public string? Range { get; set; }
        public string? Components { get; set; }
        public string? Duration { get; set; }
    }

    public class CreateSpellDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Effect { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string? Damage { get; set; }
        
        public int? SpellTemplateId { get; set; }
        
        [Required]
        public int CharacterId { get; set; }
    }

    public class UpdateSpellDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Effect { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string? Damage { get; set; }
        
        public int? SpellTemplateId { get; set; }
    }

    public class CreateSpellFromTemplateDto
    {
        [Required]
        public int SpellTemplateId { get; set; }
        
        [Required]
        public int CharacterId { get; set; }
        
        public string? CustomName { get; set; }
        public string? CustomEffect { get; set; }
        public string? CustomDamage { get; set; }
    }
}