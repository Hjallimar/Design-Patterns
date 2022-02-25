using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TalentAbility")]
public class TalentAbility : ScriptableObject
{
    [SerializeField] private string AbilityName = "Placeholder";
    [SerializeField] private string AbilityDescription = "Does X";
    [SerializeField] private AbilityType Type = AbilityType.Passive;
}
