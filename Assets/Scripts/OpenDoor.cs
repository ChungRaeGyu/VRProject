using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

    public float smooth = 2.0f;
    public float DoorOpenAngle = 90.0f;

    public AudioClip OpenAudio;
    public AudioClip CloseAudio;
    private bool AudioS;

    private Vector3 defaultRot;
    private Vector3 openRot;
    public bool open;
    private bool enter;
    private bool click = false;

    private bool doorStatus;

    // Use this for initialization
    private void Start()
    {

        defaultRot = transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);

        open = false;
        doorStatus = false;
    }

    // Update is called once per frame
    private void Update()
    {
        /*
        if (open) {
           //if (AudioS == false) {
           //   gameObject.GetComponent<AudioSource> ().PlayOneShot (OpenAudio);
           //   AudioS = true;
           //}
           transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, openRot, Time.deltaTime * smooth);

        } else {
           //if (AudioS == true) {
           //   gameObject.GetComponent<AudioSource> ().PlayOneShot (CloseAudio);
           //   AudioS = false;
           //}
           transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, defaultRot, Time.deltaTime * smooth);

        }


        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && enter || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && enter)
        {
           open = !open;
           click = true;
           PlayerScript.attention_level += 25;
           Debug.Log("어그로 수치: " + PlayerScript.attention_level);
        }
        */

        if (click == true)
        {
            click = false;
        }


        if (enter == true)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.Two))
            {
                if (doorStatus == false)
                {
                    doorStatus = true;
                }
                else if (doorStatus == true)
                {
                    doorStatus = false;
                }
                click = true;
                PlayerScript.attention_level += 25;
                Debug.Log("어그로 수치: " + PlayerScript.attention_level);
            }
        }
        if (doorStatus == false)
        {
            //if (AudioS == true)
            //{
            //   gameObject.GetComponent<AudioSource>().PlayOneShot(CloseAudio);
            //   AudioS = false;
            //}
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
        }
        else if (doorStatus == true)
        {
            //if (AudioS == false)
            //{
            //   gameObject.GetComponent<AudioSource>().PlayOneShot(OpenAudio);
            //   AudioS = true;
            //}
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (col.tag == "Player")
            {
                enter = true;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            enter = false;
        }
    }


    //  IEnumerator ButtonClick()
    //  {
    //      open = !open;
    //PlayerScript.attention_level += 25;
    //      yield return null;
    //  }

}