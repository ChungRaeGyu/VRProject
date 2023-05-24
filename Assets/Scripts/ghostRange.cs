using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostRange : MonoBehaviour
{
    RaycastHit hit;
    public float Distance = 7f;
    int layerMask;
    GameObject Light;
    Transform ghost;    
    // Start is called before the first frame update
    void Start()
    {
        ghost = GetComponent<Transform>();
        Light = GameObject.Find("handlight");
    }

    // Update is called once per frame
    void Update()
    {
    }
    //트리거 침입 -> 칩입자 식별-> LightON여부 확인 -> Ray를 쏴서 플레이어와 유령사이의 장애물 확인
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name=="Player"){
            if (Light.GetComponent<FlashLight>().LightON)
            {
                Vector3 direction = other.gameObject.transform.position-ghost.position;
                float ah = direction.magnitude; //거리 확인용
                if (Ray(direction,ah))
                {   
                    //장애물이 없을 때 
                    //목표를 플레이어로 지정
                    GetComponentInParent<GhostScript_NoPoint>().setDirection(other.gameObject.transform.position);
                }
            }
        }
    }
    //Light가 켜져 있을때 
    public bool Ray(Vector3 playerposition,float ah)
    {
            int layerMask = ~LayerMask.GetMask("Range");
            Physics.Raycast(gameObject.transform.position, playerposition, out RaycastHit hit, Distance,layerMask);
            Debug.DrawRay(gameObject.transform.position, playerposition, Color.red);
            if (hit.collider.gameObject.name == "Player")
            {
                return true;
            }
            else
            {
                return false;
            }
    }
}
