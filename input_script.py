import requests
import json
import time
import random

BASE_URL = "http: 
HEADERS = {"Content-Type": "application/json"}

def make_request(method, endpoint, data=None):
    """Make HTTP request with error handling"""
    url = f"{BASE_URL}/{endpoint}"
    try:
        if method == "POST":
            response = requests.post(url, headers=HEADERS, json=data)
        elif method == "GET":
            response = requests.get(url, headers=HEADERS)
        
        response.raise_for_status()
        return response.json()
    except requests.exceptions.RequestException as e:
        print(f"Error making {method} request to {endpoint}: {e}")
        if hasattr(e.response, 'text'):
            print(f"Response: {e.response.text}")
        return None

def seed_item_templates():
    """Create item templates"""
    print("Creating item templates...")
    
    item_templates = [
         
        {
            "name": "Longsword of Might",
            "effect": "A finely crafted longsword that enhances the wielder's strength.",
            "strengthBonus": 2,
            "category": "Weapon",
            "isActive": True
        },
        {
            "name": "Dagger of Stealth",
            "effect": "A sharp dagger that improves the wielder's dexterity and stealth.",
            "dexterityBonus": 3,
            "category": "Weapon",
            "isActive": True
        },
        {
            "name": "Staff of Wisdom",
            "effect": "An ancient staff that boosts the wielder's wisdom and magical insight.",
            "wisdomBonus": 2,
            "intelligenceBonus": 1,
            "category": "Weapon",
            "isActive": True
        },
        {
            "name": "War Hammer",
            "effect": "A heavy war hammer that increases strength significantly.",
            "strengthBonus": 3,
            "category": "Weapon",
            "isActive": True
        },
        
         
        {
            "name": "Plate Mail of Protection",
            "effect": "Heavy armor that provides excellent protection.",
            "armorBonus": 8,
            "constitutionBonus": 1,
            "category": "Armor",
            "isActive": True
        },
        {
            "name": "Leather Armor of Agility",
            "effect": "Light leather armor that enhances mobility.",
            "armorBonus": 3,
            "dexterityBonus": 2,
            "category": "Armor",
            "isActive": True
        },
        {
            "name": "Robe of the Arcane",
            "effect": "Magical robes that enhance spellcasting abilities.",
            "armorBonus": 1,
            "intelligenceBonus": 3,
            "wisdomBonus": 1,
            "category": "Armor",
            "isActive": True
        },
        {
            "name": "Chainmail Vest",
            "effect": "Medium armor providing balanced protection.",
            "armorBonus": 5,
            "category": "Armor",
            "isActive": True
        },
        
         
        {
            "name": "Ring of Charisma",
            "effect": "A beautiful ring that enhances the wearer's charm and presence.",
            "charismaBonus": 3,
            "category": "Accessory",
            "isActive": True
        },
        {
            "name": "Amulet of Health",
            "effect": "A protective amulet that boosts constitution and vitality.",
            "constitutionBonus": 2,
            "category": "Accessory",
            "isActive": True
        },
        {
            "name": "Boots of Speed",
            "effect": "Enchanted boots that increase movement speed and agility.",
            "dexterityBonus": 2,
            "category": "Accessory",
            "isActive": True
        },
        {
            "name": "Cloak of Resistance",
            "effect": "A mystical cloak that provides minor bonuses to multiple attributes.",
            "strengthBonus": 1,
            "dexterityBonus": 1,
            "constitutionBonus": 1,
            "category": "Accessory",
            "isActive": True
        },
        {
            "name": "Gauntlets of Power",
            "effect": "Heavy gauntlets that significantly boost strength.",
            "strengthBonus": 4,
            "category": "Accessory",
            "isActive": True
        }
    ]
    
    created_templates = []
    for template in item_templates:
        result = make_request("POST", "itemtemplates", template)
        if result:
            created_templates.append(result)
            print(f"✓ Created item template: {template['name']}")
        else:
            print(f"✗ Failed to create item template: {template['name']}")
        time.sleep(0.1)   
    
    return created_templates

def seed_spell_templates():
    """Create spell templates"""
    print("\nCreating spell templates...")
    
    spell_templates = [
         
        {
            "name": "Fire Bolt",
            "effect": "A mote of fire streaks toward a creature or object within range.",
            "damage": "1d10",
            "level": 0,
            "school": "Evocation",
            "castingTime": "1 action",
            "range": "120 feet",
            "components": "V, S",
            "duration": "Instantaneous",
            "isActive": True
        },
        {
            "name": "Mage Hand",
            "effect": "A spectral, floating hand appears at a point you choose within range.",
            "level": 0,
            "school": "Conjuration",
            "castingTime": "1 action",
            "range": "30 feet",
            "components": "V, S",
            "duration": "1 minute",
            "isActive": True
        },
        {
            "name": "Minor Illusion",
            "effect": "You create a sound or an image of an object within range.",
            "level": 0,
            "school": "Illusion",
            "castingTime": "1 action",
            "range": "30 feet",
            "components": "S, M",
            "duration": "1 minute",
            "isActive": True
        },
        {
            "name": "Eldritch Blast",
            "effect": "A crackling beam of energy streaks toward a creature within range.",
            "damage": "1d10",
            "level": 0,
            "school": "Evocation",
            "castingTime": "1 action",
            "range": "120 feet",
            "components": "V, S",
            "duration": "Instantaneous",
            "isActive": True
        },
        
         
        {
            "name": "Magic Missile",
            "effect": "Three glowing darts of magical force strike your target.",
            "damage": "1d4+1",
            "level": 1,
            "school": "Evocation",
            "castingTime": "1 action",
            "range": "120 feet",
            "components": "V, S",
            "duration": "Instantaneous",
            "isActive": True
        },
        {
            "name": "Healing Word",
            "effect": "A creature you can see within range regains hit points.",
            "damage": "1d4+mod",
            "level": 1,
            "school": "Evocation",
            "castingTime": "1 bonus action",
            "range": "60 feet",
            "components": "V",
            "duration": "Instantaneous",
            "isActive": True
        },
        {
            "name": "Shield",
            "effect": "An invisible barrier of magical force appears and protects you.",
            "level": 1,
            "school": "Abjuration",
            "castingTime": "1 reaction",
            "range": "Self",
            "components": "V, S",
            "duration": "1 round",
            "isActive": True
        },
        {
            "name": "Cure Light Wounds",
            "effect": "You touch a creature and restore hit points.",
            "damage": "1d8+mod",
            "level": 1,
            "school": "Evocation",
            "castingTime": "1 action",
            "range": "Touch",
            "components": "V, S",
            "duration": "Instantaneous",
            "isActive": True
        },
        
         
        {
            "name": "Fireball",
            "effect": "A bright streak flashes from your pointing finger to a point within range and blossoms into an explosion of flame.",
            "damage": "8d6",
            "level": 3,
            "school": "Evocation",
            "castingTime": "1 action",
            "range": "150 feet",
            "components": "V, S, M",
            "duration": "Instantaneous",
            "isActive": True
        },
        {
            "name": "Lightning Bolt",
            "effect": "A stroke of lightning forming a line 100 feet long and 5 feet wide blasts out from you.",
            "damage": "8d6",
            "level": 3,
            "school": "Evocation",
            "castingTime": "1 action",
            "range": "Self (100-foot line)",
            "components": "V, S, M",
            "duration": "Instantaneous",
            "isActive": True
        },
        {
            "name": "Invisibility",
            "effect": "A creature you touch becomes invisible until the spell ends.",
            "level": 2,
            "school": "Illusion",
            "castingTime": "1 action",
            "range": "Touch",
            "components": "V, S, M",
            "duration": "Concentration, up to 1 hour",
            "isActive": True
        }
    ]
    
    created_templates = []
    for template in spell_templates:
        result = make_request("POST", "spelltemplates", template)
        if result:
            created_templates.append(result)
            print(f"✓ Created spell template: {template['name']}")
        else:
            print(f"✗ Failed to create spell template: {template['name']}")
        time.sleep(0.1)
    
    return created_templates

def seed_characters():
    """Create characters"""
    print("\nCreating characters...")
    
    characters = [
        {
            "name": "Gandalf the Grey",
            "class": "Wizard",
            "level": 15,
            "armorClass": 12,
            "strength": 10,
            "dexterity": 11,
            "constitution": 12,
            "intelligence": 20,
            "wisdom": 18,
            "charisma": 16
        },
        {
            "name": "Aragorn",
            "class": "Ranger",
            "level": 12,
            "armorClass": 18,
            "strength": 16,
            "dexterity": 18,
            "constitution": 15,
            "intelligence": 14,
            "wisdom": 16,
            "charisma": 14
        },
        {
            "name": "Legolas",
            "class": "Ranger",
            "level": 10,
            "armorClass": 16,
            "strength": 12,
            "dexterity": 20,
            "constitution": 14,
            "intelligence": 13,
            "wisdom": 18,
            "charisma": 15
        },
        {
            "name": "Gimli",
            "class": "Fighter",
            "level": 11,
            "armorClass": 20,
            "strength": 18,
            "dexterity": 10,
            "constitution": 18,
            "intelligence": 10,
            "wisdom": 12,
            "charisma": 11
        },
        {
            "name": "Merlin",
            "class": "Wizard",
            "level": 20,
            "armorClass": 14,
            "strength": 8,
            "dexterity": 12,
            "constitution": 14,
            "intelligence": 20,
            "wisdom": 19,
            "charisma": 17
        },
        {
            "name": "Robin Hood",
            "class": "Rogue",
            "level": 8,
            "armorClass": 15,
            "strength": 12,
            "dexterity": 20,
            "constitution": 13,
            "intelligence": 14,
            "wisdom": 16,
            "charisma": 15
        },
        {
            "name": "Conan",
            "class": "Barbarian",
            "level": 9,
            "armorClass": 16,
            "strength": 20,
            "dexterity": 16,
            "constitution": 18,
            "intelligence": 10,
            "wisdom": 11,
            "charisma": 12
        },
        {
            "name": "Elara Moonwhisper",
            "class": "Cleric",
            "level": 7,
            "armorClass": 17,
            "strength": 13,
            "dexterity": 11,
            "constitution": 15,
            "intelligence": 14,
            "wisdom": 20,
            "charisma": 16
        }
    ]
    
    created_characters = []
    for character in characters:
        result = make_request("POST", "characters", character)
        if result:
            created_characters.append(result)
            print(f"✓ Created character: {character['name']} (Level {character['level']} {character['class']})")
        else:
            print(f"✗ Failed to create character: {character['name']}")
        time.sleep(0.1)
    
    return created_characters

def seed_items(characters, item_templates):
    """Create items for characters"""
    print("\nCreating items...")
    
    if not characters or not item_templates:
        print("No characters or item templates available for item creation")
        return []
    
    created_items = []
    
     
    template_items = [
        {"character_idx": 0, "template_name": "Staff of Wisdom"},   
        {"character_idx": 0, "template_name": "Robe of the Arcane"},
        {"character_idx": 1, "template_name": "Longsword of Might"},   
        {"character_idx": 1, "template_name": "Leather Armor of Agility"},
        {"character_idx": 2, "template_name": "Dagger of Stealth"},   
        {"character_idx": 2, "template_name": "Boots of Speed"},
        {"character_idx": 3, "template_name": "War Hammer"},   
        {"character_idx": 3, "template_name": "Plate Mail of Protection"},
        {"character_idx": 4, "template_name": "Staff of Wisdom"},   
        {"character_idx": 4, "template_name": "Cloak of Resistance"},
        {"character_idx": 5, "template_name": "Dagger of Stealth"},   
        {"character_idx": 5, "template_name": "Leather Armor of Agility"},
        {"character_idx": 6, "template_name": "Gauntlets of Power"},   
        {"character_idx": 6, "template_name": "Chainmail Vest"},
        {"character_idx": 7, "template_name": "Amulet of Health"},   
        {"character_idx": 7, "template_name": "Ring of Charisma"}
    ]
    
    template_lookup = {t['name']: t['id'] for t in item_templates}
    
    for item_data in template_items:
        if item_data['character_idx'] >= len(characters):
            continue
            
        template_name = item_data['template_name']
        if template_name not in template_lookup:
            continue
            
        character_id = characters[item_data['character_idx']]['id']
        template_id = template_lookup[template_name]
        
        item_request = {
            "itemTemplateId": template_id,
            "characterId": character_id
        }
        
        result = make_request("POST", "items/from-template", item_request)
        if result:
            created_items.append(result)
            print(f"✓ Created item: {template_name} for {characters[item_data['character_idx']]['name']}")
        else:
            print(f"✗ Failed to create item: {template_name}")
        time.sleep(0.1)
    
     
    custom_items = [
        {
            "name": "Ancient Spellbook",
            "effect": "A weathered tome containing ancient magical knowledge.",
            "intelligenceBonus": 2,
            "wisdomBonus": 1,
            "characterId": characters[0]['id']   
        },
        {
            "name": "Elven Bow",
            "effect": "A masterfully crafted elven longbow.",
            "dexterityBonus": 3,
            "characterId": characters[2]['id']   
        },
        {
            "name": "Dwarven Ale Mug",
            "effect": "A sturdy mug that boosts morale and constitution.",
            "constitutionBonus": 1,
            "charismaBonus": 1,
            "characterId": characters[3]['id']   
        }
    ]
    
    for item in custom_items:
        result = make_request("POST", "items", item)
        if result:
            created_items.append(result)
            print(f"✓ Created custom item: {item['name']}")
        else:
            print(f"✗ Failed to create custom item: {item['name']}")
        time.sleep(0.1)
    
    return created_items

def seed_spells(characters, spell_templates):
    """Create spells for characters"""
    print("\nCreating spells...")
    
    if not characters or not spell_templates:
        print("No characters or spell templates available for spell creation")
        return []
    
    created_spells = []
    template_lookup = {t['name']: t['id'] for t in spell_templates}
    
     
    character_spells = [
         
        {"character_idx": 0, "spells": ["Fire Bolt", "Mage Hand", "Magic Missile", "Shield", "Fireball", "Lightning Bolt", "Invisibility"]},
         
        {"character_idx": 4, "spells": ["Fire Bolt", "Minor Illusion", "Eldritch Blast", "Magic Missile", "Fireball", "Lightning Bolt"]},
         
        {"character_idx": 7, "spells": ["Minor Illusion", "Healing Word", "Cure Light Wounds"]},
         
        {"character_idx": 1, "spells": ["Healing Word"]},
         
        {"character_idx": 2, "spells": ["Cure Light Wounds"]}
    ]
    
    for char_spell_data in character_spells:
        if char_spell_data['character_idx'] >= len(characters):
            continue
            
        character = characters[char_spell_data['character_idx']]
        character_id = character['id']
        
        for spell_name in char_spell_data['spells']:
            if spell_name not in template_lookup:
                continue
                
            template_id = template_lookup[spell_name]
            
            spell_request = {
                "spellTemplateId": template_id,
                "characterId": character_id
            }
            
            result = make_request("POST", "spells/from-template", spell_request)
            if result:
                created_spells.append(result)
                print(f"✓ Created spell: {spell_name} for {character['name']}")
            else:
                print(f"✗ Failed to create spell: {spell_name} for {character['name']}")
            time.sleep(0.1)
    
     
    custom_spells = [
        {
            "name": "Gandalf's Light",
            "effect": "Creates a brilliant light that banishes darkness and fear.",
            "damage": "3d6",
            "characterId": characters[0]['id']   
        },
        {
            "name": "Nature's Blessing",
            "effect": "Calls upon nature to heal and protect allies.",
            "damage": "2d8+mod",
            "characterId": characters[1]['id']   
        }
    ]
    
    for spell in custom_spells:
        result = make_request("POST", "spells", spell)
        if result:
            created_spells.append(result)
            print(f"✓ Created custom spell: {spell['name']}")
        else:
            print(f"✗ Failed to create custom spell: {spell['name']}")
        time.sleep(0.1)
    
    return created_spells

def main():
    """Main seeding function"""
    print("🚀 Starting API data seeding...")
    print(f"Target URL: {BASE_URL}")
    print("-" * 50)
    
    try:
         
        test_response = make_request("GET", "characters")
        if test_response is None:
            print("❌ Cannot connect to API. Make sure your server is running at http: 
            return
        
        print("✅ API connection successful")
        
         
        item_templates = seed_item_templates()
        spell_templates = seed_spell_templates()
        characters = seed_characters()
        items = seed_items(characters, item_templates)
        spells = seed_spells(characters, spell_templates)
        
         
        print("\n" + "="*50)
        print("📊 SEEDING SUMMARY")
        print("="*50)
        print(f"Item Templates Created: {len(item_templates)}")
        print(f"Spell Templates Created: {len(spell_templates)}")
        print(f"Characters Created: {len(characters)}")
        print(f"Items Created: {len(items)}")
        print(f"Spells Created: {len(spells)}")
        print(f"Total Records Created: {len(item_templates) + len(spell_templates) + len(characters) + len(items) + len(spells)}")
        print("\n✅ Data seeding completed successfully!")
        print(f"🌐 You can now visit http: 
        
    except Exception as e:
        print(f"\n❌ An error occurred during seeding: {e}")
        print("Please check that your API is running and accessible.")

if __name__ == "__main__":
    main()