using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public GameObject ItemInSlot;
    // Start is called before the first frame update

    private void OnTriggerStay(Collider coll)
    {
        if (ItemInSlot != null) return;
        GameObject obj = coll.gameObject;
        if (!IsItem(obj)) return;
        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
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
        if (ItemInSlot != null)
        {
            ItemInSlot.GetComponentInParent<Slot>().ItemInSlot = null;
            ItemInSlot.transform.SetParent(null);
            ItemInSlot.GetComponent<Inventory>().inSlot = false;
            ItemInSlot.GetComponent<Inventory>().currentSlot = null;
        }
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.transform.SetParent(gameObject.transform, true);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = obj.GetComponent<Inventory>().sloRotation;
        obj.GetComponent<Inventory>().inSlot = true;
        obj.GetComponent<Inventory>().currentSlot = this;
        ItemInSlot = obj;
    }


}
