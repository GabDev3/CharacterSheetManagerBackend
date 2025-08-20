using System.ComponentModel.DataAnnotations;

namespace CharacterSheetManager.Models
{
    public class Spell
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Effect { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string? Damage { get; set; }
        
        
        public int? SpellTemplateId { get; set; }
        public virtual SpellTemplate? SpellTemplate { get; set; }
        
        
        public int CharacterId { get; set; }
        public virtual Character Character { get; set; } = null!;
    }
}