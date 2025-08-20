using AutoMapper;
using CharacterSheetManager.Models;
using CharacterSheetManager.DTOs.Character;
using CharacterSheetManager.DTOs.Item;
using CharacterSheetManager.DTOs.Spell;
using CharacterSheetManager.DTOs.ItemTemplate;
using CharacterSheetManager.DTOs.SpellTemplate;

namespace CharacterSheetManager.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Character, CharacterDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.Spells, opt => opt.MapFrom(src => src.Spells));
                
            CreateMap<Character, CharacterSummaryDto>();
            
            CreateMap<CreateCharacterDto, Character>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Items, opt => opt.Ignore())
                .ForMember(dest => dest.Spells, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
                
            CreateMap<UpdateCharacterDto, Character>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Items, opt => opt.Ignore())
                .ForMember(dest => dest.Spells, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            
            CreateMap<Item, DTOs.Item.ItemDto>()
                .ForMember(dest => dest.ItemTemplateName, opt => opt.MapFrom(src => src.ItemTemplate != null ? src.ItemTemplate.Name : null))
                .ForMember(dest => dest.CharacterName, opt => opt.MapFrom(src => src.Character.Name));
                
            CreateMap<Item, DTOs.Character.ItemDto>()
                .ForMember(dest => dest.ItemTemplateName, opt => opt.MapFrom(src => src.ItemTemplate != null ? src.ItemTemplate.Name : null));
                
            CreateMap<CreateItemDto, Item>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Character, opt => opt.Ignore())
                .ForMember(dest => dest.ItemTemplate, opt => opt.Ignore());
                
            CreateMap<UpdateItemDto, Item>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CharacterId, opt => opt.Ignore())
                .ForMember(dest => dest.Character, opt => opt.Ignore())
                .ForMember(dest => dest.ItemTemplate, opt => opt.Ignore());

            
            CreateMap<Spell, DTOs.Spell.SpellDto>()
                .ForMember(dest => dest.SpellTemplateName, opt => opt.MapFrom(src => src.SpellTemplate != null ? src.SpellTemplate.Name : null))
                .ForMember(dest => dest.CharacterName, opt => opt.MapFrom(src => src.Character.Name))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.SpellTemplate != null ? src.SpellTemplate.Level : (int?)null))
                .ForMember(dest => dest.School, opt => opt.MapFrom(src => src.SpellTemplate != null ? src.SpellTemplate.School : null))
                .ForMember(dest => dest.CastingTime, opt => opt.MapFrom(src => src.SpellTemplate != null ? src.SpellTemplate.CastingTime : null))
                .ForMember(dest => dest.Range, opt => opt.MapFrom(src => src.SpellTemplate != null ? src.SpellTemplate.Range : null))
                .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.SpellTemplate != null ? src.SpellTemplate.Components : null))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.SpellTemplate != null ? src.SpellTemplate.Duration : null));
                
            CreateMap<Spell, DTOs.Character.SpellDto>()
                .ForMember(dest => dest.SpellTemplateName, opt => opt.MapFrom(src => src.SpellTemplate != null ? src.SpellTemplate.Name : null));
                
            CreateMap<CreateSpellDto, Spell>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Character, opt => opt.Ignore())
                .ForMember(dest => dest.SpellTemplate, opt => opt.Ignore());
                
            CreateMap<UpdateSpellDto, Spell>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CharacterId, opt => opt.Ignore())
                .ForMember(dest => dest.Character, opt => opt.Ignore())
                .ForMember(dest => dest.SpellTemplate, opt => opt.Ignore());

            
            CreateMap<ItemTemplate, ItemTemplateDto>();
            CreateMap<CreateItemTemplateDto, ItemTemplate>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<UpdateItemTemplateDto, ItemTemplate>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            
            CreateMap<SpellTemplate, SpellTemplateDto>();
            CreateMap<CreateSpellTemplateDto, SpellTemplate>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<UpdateSpellTemplateDto, SpellTemplate>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}