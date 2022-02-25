using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplayHandler : MonoBehaviour
{
    [SerializeField] private GameObject EntirePanel = null;
    [SerializeField] private Text NameField;
    [SerializeField] private Text DamageField;
    [SerializeField] private Text HealthField;
    [SerializeField] private List<Image> ResistancesImage = new List<Image>();

    private CharacterUIStats currentStats;
    
    public void DisplayCharacterStats(CharacterUIStats displayStats)
    {
        EntirePanel.SetActive(true);
        if (currentStats.CharacterName != displayStats.CharacterName)
        {
            currentStats = displayStats;
        }
        NameField.text = displayStats.CharacterName;
        DamageField.text = "" + displayStats.CharacterDamage;
        HealthField.text = "" + displayStats.CharacterHealth + "/" + displayStats.CharacterMaxHealth;
        foreach (Image image in ResistancesImage)
        {
            image.enabled = false;
        }
        foreach (DamageType type in displayStats.CharacterResistances.Keys)
        {
            if(type != DamageType.NotDefined)
                ResistancesImage[(int)type - 1].enabled = true;
        }
    }

    public void DontShow()
    {
        EntirePanel.SetActive(false);
    }
}
