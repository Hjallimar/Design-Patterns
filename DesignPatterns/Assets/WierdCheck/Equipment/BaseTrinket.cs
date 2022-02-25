using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Trinket")]
public class BaseTrinket : ScriptableObject
{
    public ClassType ClassSpecific = ClassType.NotSpecified;
    public int DamageStatIncrease = 0;
    public int HealthStatIncrease = 0;

    public DamageType ResistanceType = DamageType.NotDefined;
    public int ResistanceStatIncrease = 0;
    
    public DamageType BonusAgainstType = DamageType.NotDefined;
    public int  BonusDamage = 0;
}