﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;

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
    private int actionIndex = 0;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DealDamage(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DealDamage(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DealDamage(2);
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(HelpComponent.GetMouseRay(), out hit))
            {
                if(hit.collider.GetComponent<IDamageable>() != null)
                {
                    HelpComponent.TargetAssigner(hit.transform);
                }
            }
        }
    }

    private void DealDamage(int i) 
    {
        CharacterController target = instance.controllableCharacters[i];
        target.TakeDamage(10f);
    }

    public void PerformAbility(int i)
    {
        instance.actionIndex = i;
        HelpComponent.TargetAssigner += instance.controllableCharacters[instance.characterIndex].GetNewTarget;
        HelpComponent.TargetAssigner += ActionRegistered;
    }

    private static void ActionRegistered(Transform trans)
    {
        HelpComponent.TargetAssigner -= instance.controllableCharacters[instance.characterIndex].GetNewTarget;
        HelpComponent.TargetAssigner -= ActionRegistered;

        instance.controllableCharacters[instance.characterIndex].UseAction(instance.actionIndex);
        ChangeCharacter();
    }

    public static Transform GetEnemyTarget()
    {
        List<Transform> active = new List<Transform>();
        foreach(CharacterController character in instance.controllableCharacters)
        {
            if (character.Alive())
            {
                active.Add(character.transform);
            }
        }

        if(active.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, active.Count);
            return active[rand];
        }
        else
        {
            return null;
        }
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
        PlaningUI.ClearList();
    }

    public static void ChangeCharacter()
    {
        instance.characterIndex++;
        int count = 0;
        if(instance.characterIndex > instance.controllableCharacters.Count - 1)
        {
            PlaningUI.ChangeToExecute();
            instance.StopCoroutine(instance.indicator);
            instance.targetIndicator.enabled = false;
        }
        else if (!instance.controllableCharacters[instance.characterIndex].Alive())
        {
            count++;
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
            PlaningUI.ChangeToNextCharacter(instance.controllableCharacters[instance.characterIndex]);
        }
        if(count >= instance.controllableCharacters.Count - 1)
        {
            Debug.Log("you died");
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
