using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Object/Player", order = int.MaxValue)]
public class Player : ScriptableObject
{
    /*
    [Header("�÷��̾� HP")]
    [SerializeField]
    private int zlayerHP = 0;
    public int PlayerHP { get { return zlayerHP; } }
    */
    [Header("ȸ�� ������ ����")]
    [SerializeField]
    private int HealingItem = 0;
    public int InventoryHealingItem { get { return HealingItem; } }
}



