using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AbilityType
{
    Passive = 1,
    Activate = 2,
    StartOfGame = 3,
    EndOfTurn = 4,
    Reactive = 5
};

public enum ClassType
{
    Warrior = 1,
    Paladin = 2,
    Priest = 3,
    Rouge = 4,
    Mage = 5,
    Warlock = 6,
    Druid = 7,
    Hunter = 8,
    NotSpecified = 9
};

public enum DamageType
{
    Physical = 1, 
    Shadow = 2,
    Holy = 3, 
    Fire = 4,
    Frost = 5,
    Arcane = 6,
    Nature = 7,
    NotDefined = 12
}