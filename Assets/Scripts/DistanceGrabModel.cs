using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceGrabModel : MonoBehaviour
{

    private void Update()
    {
        
    }

    //StartCoroutine(EnableHandModelVisibilityAfterDelay(true));
    private void ToggleHandModelVisibility(bool visible)
    {
        // ��� ���� ��ü���� SkinnedMeshRenderer ������Ʈ�� ã�� Ȱ��ȭ/��Ȱ��ȭ
        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer renderer in renderers)
        {
            renderer.enabled = visible;
        }
    }
    IEnumerator EnableHandModelVisibilityAfterDelay(bool enable)
    {
        yield return new WaitForFixedUpdate(); // GrabBegin�� ������ ���� ������ ���

        // �� �� ���ü� Ȱ��ȭ
        ToggleHandModelVisibility(true);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Item")
        {
            Debug.Log("�׷� ������Ʈ");
            if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            {
                ToggleHandModelVisibility(false);
            }
            else if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
            {
                StartCoroutine(EnableHandModelVisibilityAfterDelay(true));
            }
            else if(OVRInput.GetDown(OVRInput.Button.Two))
            {
                StartCoroutine(EnableHandModelVisibilityAfterDelay(true));
            }
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Item")
        {
            StartCoroutine(EnableHandModelVisibilityAfterDelay(true));
        }
    }
}



