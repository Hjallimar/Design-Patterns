using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public struct CharacterUIStats
{
    public string CharacterName;
    public int CharacterHealth;
    public int CharacterDamage;
    public int CharacterMaxHealth;
    public Dictionary<DamageType, int> CharacterResistances;
}

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private ClassType CharacterClass = ClassType.NotSpecified;
    [SerializeField] private float CharacterHealth = 10.0f;
    [SerializeField] private float CharacterDamage = 2.0f;
    [SerializeField] private string CharacterName = "Garrosh Hellscream";
    [SerializeField] private BaseWeapon Weapon = null;
    [SerializeField] private BaseTrinket PrimaryTrinket = null;
    [SerializeField] private BaseTrinket SecondaryTrinket = null;
    
    [SerializeField] private TalentTree talentTree = null;

    [SerializeField] private Dictionary<DamageType, int> Resistances = new Dictionary<DamageType, int>();
    [SerializeField] private Dictionary<DamageType, int> DamageBonus = new Dictionary<DamageType, int>();

    private float CurrentHealth = 0.0f;
    private float CurrentMaxHealth = 0.0f;

    private float BonusHealth = 0.0f;
    private float BonusDamage = 0.0f;
    
    private float CurrentDamage = 0.0f;

    [SerializeField] private CharacterDisplayHandler displayHandler;

    private void Start()
    {
        gameObject.name = CharacterName;
        UpdateCharacterStats();
    }

    private void OnMouseEnter()
    {
        DisplayStats();
    }

    private void OnMouseExit()
    {
        displayHandler.DontShow();
    }

    private void DisplayStats()
    {
        CharacterUIStats myStats = new CharacterUIStats();
        myStats.CharacterHealth = (int)CurrentHealth;
        myStats.CharacterMaxHealth = (int)CurrentMaxHealth;
        myStats.CharacterDamage = (int)CurrentDamage;
        myStats.CharacterName = CharacterName;
        myStats.CharacterResistances = Resistances;
        displayHandler.DisplayCharacterStats(myStats);
    }

    private void UpdateCharacterStats()
    {
        CurrentHealth = CharacterHealth;
        CurrentDamage = CharacterDamage;
        
        if (Weapon != null)
        {
            CurrentHealth += Weapon.HealthStatIncrease;
            CurrentDamage += Weapon.DamageStatIncrease;
            
            if (Weapon.ResistanceType != DamageType.NotDefined)
            {
                AddResistance(Weapon.ResistanceType, Weapon.ResistanceStatIncrease);
            }
            if (Weapon.BonusAgainstType != DamageType.NotDefined)
            {
                AddDamageBonus(Weapon.BonusAgainstType, Weapon.BonusDamage);
            }
        }

        if (PrimaryTrinket != null)
            IncludeTrinket(PrimaryTrinket);
        if (SecondaryTrinket != null)
            IncludeTrinket(SecondaryTrinket);
        
        if (CurrentDamage < 0)
            CurrentDamage = 0.0f;
        
        CurrentMaxHealth = CurrentHealth;
        BonusDamage = CurrentDamage - CharacterDamage;
        BonusHealth = CurrentMaxHealth - CharacterHealth;
    }

    private void EquipPrimaryTrinket(BaseTrinket newTrinket)
    {
        PrimaryTrinket = newTrinket;
        UpdateCharacterStats();
    }
    
    private void EquipSecondaryTrinket(BaseTrinket newTrinket)
    {
        SecondaryTrinket = newTrinket;
        UpdateCharacterStats();
    }
    
    private void TakeDamage(DamageType dmgtype, int Ammount)
    {
        int damageTaken = Ammount;
        if (Resistances.ContainsKey(dmgtype))
        {
            damageTaken -= Resistances[dmgtype];
        }

        CurrentHealth -= damageTaken;
        DisplayStats();
    }

    private void ReciveHealing(int Ammount)
    {
        CurrentHealth += Ammount;
        if (CurrentHealth > CurrentMaxHealth)
        {
            CurrentHealth = CurrentMaxHealth;
        }
        DisplayStats();
    }
    
    private void IncludeTrinket(BaseTrinket trinket)
    {
        CurrentHealth += trinket.HealthStatIncrease;
        CurrentDamage += trinket.DamageStatIncrease;
            
        if (trinket.ResistanceType != DamageType.NotDefined)
        {
            AddResistance(trinket.ResistanceType, trinket.ResistanceStatIncrease);
        }
        if (trinket.BonusAgainstType != DamageType.NotDefined)
        {
            AddDamageBonus(trinket.BonusAgainstType, trinket.BonusDamage);
        }
    }
    
    private void AddResistance(DamageType type, int ammount)
    {
        if (Resistances.ContainsKey(type))
        {
            Resistances[type] += ammount;
        }
        else
        {
            Resistances.Add(type, ammount);
        }
    }
    
    private void AddDamageBonus(DamageType type, int ammount)
    {
        if (DamageBonus.ContainsKey(type))
        {
            DamageBonus[type] += ammount;
        }
        else
        {
            DamageBonus.Add(type, ammount);
        }
    }
}
