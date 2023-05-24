//Shady
using UnityEngine;

[ExecuteInEditMode]
public class RevealLight : MonoBehaviour
{
    //UV라이트를 사용한 단서오브젝트에 넣어주면 된다.
    //SerializeField란 : Inspector창에서는 조작이 가능하지만, 다른 스크립트에서는 불러올 수 없다.
    [SerializeField] Material Mat; 
    [SerializeField] Light UVLight;
	
	void Update ()
    {
        if(UVLight.enabled){
        //방향과 각도 만 가지고 생각한다. 사이에 장애물이 있던 말던 상관없음, 거리 상관없음
        Mat.SetVector("MyLightPosition",  UVLight.transform.position);
        Mat.SetVector("MyLightDirection", -UVLight.transform.forward );
        Mat.SetFloat ("MyLightAngle", UVLight.spotAngle);
        }
        else{
            Mat.SetVector("MyLightDirection", UVLight.transform.forward);
        }
    }//Update() end
}//class end