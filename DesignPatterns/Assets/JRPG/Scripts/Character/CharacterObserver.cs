using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterObserver : MonoBehaviour
{
    private static CharacterObserver instance = null;

    public static Action Execute = delegate { };

    protected int currentIndex { get; set; } = 0;

    [SerializeField] private List<CharacterController> controllableCharacters = new List<CharacterController>();

    protected float speed { get; set; } = 1f;

    [SerializeField] private Image targetIndicator = null;

    private Coroutine indicator = default;

    private int characterIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        instance.characterIndex = -1;
    }

    private void Start()
    {
        ChangeCharacter();
    }

    public void PerformAbility(int i)
    {
        Debug.Log(instance.controllableCharacters[instance.characterIndex].transform.name + "is getting its action called");
        instance.controllableCharacters[instance.characterIndex].UseAction(i);
        ChangeCharacter();
    }

    public void UndoAction()
    {
        instance.characterIndex -= 1;
        if (instance.characterIndex < 0)
        {
            instance.characterIndex = 0;
        }
        instance.controllableCharacters[instance.characterIndex].UndoAction();
        instance.characterIndex -= 1;
        if (instance.characterIndex < -1)
        {
            instance.characterIndex = -1;
        }
        ChangeCharacter();
    }

    public void ExecuteCommands()
    {
        Execute();
        PlayerCommand.ExecuteCommands();
        PlaningPhase.ClearList();
    }

    public static void ChangeCharacter()
    {
        instance.characterIndex++;
        if(instance.characterIndex > instance.controllableCharacters.Count - 1)
        {
            PlaningPhase.ChangeToExecute();
            instance.StopCoroutine(instance.indicator);
            instance.targetIndicator.enabled = false;
        }
        else if (!instance.controllableCharacters[instance.characterIndex].Alive())
        {
            ChangeCharacter();
        }
        else
        {
            instance.targetIndicator.enabled = true;
            if (instance.indicator != null)
            {
                instance.StopCoroutine(instance.indicator);
            }
            Transform target = instance.controllableCharacters[instance.characterIndex].transform;
            instance.indicator = instance.StartCoroutine(instance.Indicator(target));
            PlaningPhase.ChangeToNextCharacter(instance.controllableCharacters[instance.characterIndex]);
        }
    }

    private IEnumerator Indicator(Transform target)
    {
        Vector3 start = Vector3.up * 1.5f;
        Vector3 end = Vector3.up * 2.5f;
        float timer = 0f;
        float adjust = 1f;
        while (true)
        {
            timer += Time.deltaTime * adjust;
            instance.targetIndicator.transform.position = target.position + Vector3.Lerp(start, end, timer * 2);
            yield return new WaitForSeconds(Time.deltaTime);
            if(timer > 0.5f || timer < 0)
            {
                adjust *= -1;
            }
        }
    }

    public static void ResetTurn()
    {
        Debug.Log("Turn reset");
        instance.characterIndex = -1;
        ChangeCharacter();
    }
}
