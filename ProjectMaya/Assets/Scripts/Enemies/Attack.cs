using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Random = UnityEngine.Random;
using System;

public class Attack : MonoBehaviour
{
    [FoldoutGroup("Parameters")]
    [SerializeField]
    [EnumToggleButtons]
    private LayerMask collisionLayerMask;

    [FoldoutGroup("Parameters")]
    [SerializeField]
    public float attackRange = 1f;

    [FoldoutGroup("Parameters")]
    [SerializeField]
    public float attackSpeed = 1f;

    [FoldoutGroup("Parameters")]
    [SerializeField]
    [AssetsOnly]
    [InlineEditor]
    private WeaponBase[] availableAttacks;
    public WeaponBase selectedWeapon {get; private set;}

    private EnemyStateMachine _controller;
    //private int attackChancesSum = 1;
    private int selectedAttackIndex;

    [HideInInspector] public bool isInRange;

    public Action OnAttack;

    private void Awake()
    {
        _controller = GetComponent<EnemyStateMachine>();

        if (availableAttacks.Length > 0)
        {
            selectedWeapon = availableAttacks[0];
        }
    }

    private void Start()
    {
        // for (int i = 0; i < avalibleAttacks.Length; i++)
        // {
        //     attackChancesSum += avalibleAttacks[i].attackChance;
        // }
    }

    public void SelectAttack()
    {
        // int assortedChance = Random.Range(0, (attackChancesSum));
        // int accumulatedChance = 0;

        // for (int i = 0; i < avalibleAttacks.Length; i++)
        // {
        //     accumulatedChance += avalibleAttacks[i].attackChance;
        //     if (assortedChance <= accumulatedChance)
        //     {
        //         selectedAttackIndex = i;
        //         break;
        //     }
        // }

        for (int i = 0; i < availableAttacks.Length; i++)
        {
            if (/*(selectedWeapon.WeaponPriority < availableAttacks[i].WeaponPriority) &&*/ (availableAttacks[i].magazineRemaning > 0))
            {
                selectedWeapon = availableAttacks[i];

            }
        }

        selectedWeapon.Shoot();
        OnAttack?.Invoke();
    }
}
