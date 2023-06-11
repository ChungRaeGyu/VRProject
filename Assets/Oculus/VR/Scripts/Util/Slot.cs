using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public GameObject ItemInSlot;
    public Image slotImage;
    Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        slotImage = GetComponentInChildren<Image>();
        originalColor = slotImage.color;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (ItemInSlot != null) return;
        GameObject obj = coll.gameObject;
        if (!IsItem(obj)) return;
        if(OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            InsertItem(obj);
        }
    }

    bool IsItem(GameObject obj)
    {
        return obj.GetComponent<Inventory>();
    }

    void InsertItem(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.transform.SetParent(gameObject.transform, true);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = obj.GetComponent<Inventory>().sloRotation;
        obj.GetComponent<Inventory>().inSlot = true;
        obj.GetComponent<Inventory>().currentSlot = this;
        ItemInSlot = obj;
        slotImage.color = Color.gray;
    }

    public void ResetColor()
    {
        slotImage.color = originalColor;
    }

}
