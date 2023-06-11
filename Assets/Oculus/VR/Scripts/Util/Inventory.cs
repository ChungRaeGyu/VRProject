using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static int HelingItemNum = 0;//치료제 갯수

    public bool inSlot;
    public Vector3 sloRotation = Vector3.zero;
    public Slot currentSlot;
}
