using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class FinishDoor : MonoBehaviour
{
	Text FinishLockTxt;
	public float smooth = 2.0f;
	public float DoorOpenAngle = 90.0f;

	public AudioClip OpenAudio;
	public AudioClip CloseAudio;
	private bool AudioS;

	private Vector3 defaultRot;
	private Vector3 openRot;
	private bool open;
	private bool enter;


	// Use this for initialization
	void Start()
	{
		FinishLockTxt = GameObject.Find("FinishLock").GetComponent<Text>();
		open = false;
		enter = false;
		defaultRot = transform.eulerAngles;
		openRot = new Vector3(defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);
	}

	// Update is called once per frame
	void Update()
	{
		if (open)
		{
			if (AudioS == false)
			{
				gameObject.GetComponent<AudioSource>().PlayOneShot(OpenAudio);
				AudioS = true;
			}
			transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
            SceneManager.LoadScene("Scenes/GameClear");
		}
		else
		{
			if (AudioS == true)
			{
				gameObject.GetComponent<AudioSource>().PlayOneShot(CloseAudio);
				AudioS = false;
			}
			transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaultRot, Time.deltaTime * smooth);

		}
		if(enter == true && open == false)
        {
			if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
				FinishLockTxt.GetComponent<Text>().enabled = true;
			}
        }
	}

	void OnTriggerEnter(Collider col)
	{
		//if (col.gameObject.tag == "Key")
		//{
		//	open = true;
		//	Destroy(col.gameObject);
		//}
		if(col.gameObject.CompareTag("Key"))
        {
			open = true;
			Destroy(col.gameObject);
		}
		if(col.gameObject.CompareTag("Player"))
        {
			enter = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
		if(col.gameObject.CompareTag("Player"))
        {
			enter = false;
			if(FinishLockTxt.GetComponent<Text>().enabled == true)
            {
				FinishLockTxt.GetComponent<Text>().enabled = false;
			}
        }

    }



}