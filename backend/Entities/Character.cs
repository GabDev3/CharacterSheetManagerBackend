using System.ComponentModel.DataAnnotations;

namespace CharacterSheetManager.Models
{
    public class Character
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Class { get; set; } = string.Empty;
        
        [Range(1, 20)]
        public int Level { get; set; } = 1;
        
        [Range(1, 30)]
        public int ArmorClass { get; set; }
        
        
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
        
        
        public virtual ICollection<Item> Items { get; set; } = new List<Item>();
        public virtual ICollection<Spell> Spells { get; set; } = new List<Spell>();
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}