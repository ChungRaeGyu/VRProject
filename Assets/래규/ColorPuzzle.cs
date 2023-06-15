using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPuzzle : MonoBehaviour
{
    Image[] ImageColor;
    Color[] Sample= new Color[4];
    bool quest1=false;
    bool quest2 = false;
    bool quest3 = false;
    int a=0,b=1,c=2;
    // Start is called before the first frame update
    void Start()
    {
        ImageColor = gameObject.GetComponentsInChildren<Image>();
        for(int i= 0; i<ImageColor.Length;i++){
            Sample[i]= ImageColor[i].color;
        }   

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changeColor1(){
        Debug.Log("첫번쨰 블럭");
        ImageColor[0].color = Sample[a];
        if(a==0){
            quest1 = true;
        }
        else{
            quest1 = false;
        }
        a++;
        if(a>2){
            a=0;
        }
    }
    public void changeColor2()
    {Debug.Log("두번쨰 블럭");
        ImageColor[1].color = Sample[b];
        
        if(b==1){
            quest2 = true;
        }else{
            quest2 = false;
        }
        b++;
        if (b > 2)
        {
            b = 0;
        }
    }
    public void changeColor3()
    {
        Debug.Log("세번쨰 블럭");
        ImageColor[2].color = Sample[c];
        if (c == 1)
        {
            quest3 = true;
        }
        else
        {
            quest3 = false;
        }
        c++;
        if (c > 2)
        {
            c = 0;  
        }
    }
    public void EnterBtn(){
        PlayerScript.attention_level+=10;
        if(quest1&&quest2&&quest3){
            gameObject.GetComponentInParent<GameStartDoor>().opendoor=true;
            //딸깍 소리 내기
            Debug.Log("열림");
        }
        else{
            Debug.Log("잠김");
            Debug.Log("1" +quest1);
            Debug.Log("2" + quest2);
            Debug.Log("3" + quest3);
            
        }
    }
}
