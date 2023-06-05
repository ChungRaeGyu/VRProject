using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool setPlayerScreen = false;

    public GameObject player;

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {    
            if (setPlayerScreen == false)
            {
                setPlayerScreen = true;
            }
            else if (setPlayerScreen == true)
            {
                setPlayerScreen = false;
            }
            setPause(setPlayerScreen);
            GameManager.GM.UM.SwitchPlayerScreen(setPlayerScreen);
        }
    }

    public void setPause(bool sP)
    {
        if (sP == true)
        {
            player.GetComponent<OVRPlayerController>().enabled = false;
            Time.timeScale = 0;
        }
        else if (sP == false)
        {
            player.GetComponent<OVRPlayerController>().enabled = true;
            Time.timeScale = 1;  
        }
    }
}
