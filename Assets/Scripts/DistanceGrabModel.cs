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
        // 모든 하위 객체에서 SkinnedMeshRenderer 컴포넌트를 찾아 활성화/비활성화
        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer renderer in renderers)
        {
            renderer.enabled = visible;
        }
    }
    IEnumerator EnableHandModelVisibilityAfterDelay(bool enable)
    {
        yield return new WaitForFixedUpdate(); // GrabBegin이 완전히 끝날 때까지 대기

        // 손 모델 가시성 활성화
        ToggleHandModelVisibility(true);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Item")
        {
            Debug.Log("그랩 오브젝트");
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



