using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentTree : MonoBehaviour
{
   [SerializeField] private ClassType ClassName = ClassType.NotSpecified;
   [Space, SerializeField] private string SpecName1 = "Spec 1";
   [SerializeField] private TalentAbility[] SpecTree1 = new TalentAbility[4];
   [Space, SerializeField] private string SpecName2 = "Spec 2";
   [SerializeField] private TalentAbility[] SpecTree2 = new TalentAbility[4];
   [Space, SerializeField] private string SpecName3 = "Spec 3";
   [SerializeField] private TalentAbility[] SpecTree3 = new TalentAbility[4];
  
}
