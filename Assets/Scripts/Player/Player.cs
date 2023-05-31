using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Object/Player", order = int.MaxValue)]
public class Player : ScriptableObject
{
    /*
    [Header("플레이어 HP")]
    [SerializeField]
    private int zlayerHP = 0;
    public int PlayerHP { get { return zlayerHP; } }
    */
    [Header("회복 아이템 갯수")]
    [SerializeField]
    private int HealingItem = 0;
    public int InventoryHealingItem { get { return HealingItem; } }
}



