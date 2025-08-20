using System.ComponentModel.DataAnnotations;

namespace CharacterSheetManager.Models
{
    public class SpellTemplate
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
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
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}