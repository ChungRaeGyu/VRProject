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
	private bool open;
	private bool enter;

	private bool click = false;

	// Use this for initialization
	void Start () {
		
			defaultRot = transform.eulerAngles;
			openRot = new Vector3 (defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);
		}
	
	// Update is called once per frame
	void Update () {
		if (open) {
			if (AudioS == false) {
				gameObject.GetComponent<AudioSource> ().PlayOneShot (OpenAudio);
				AudioS = true;
			}
			transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, openRot, Time.deltaTime * smooth);
		} else {
			if (AudioS == true) {
				gameObject.GetComponent<AudioSource> ().PlayOneShot (CloseAudio);
				AudioS = false;
			}
			transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, defaultRot, Time.deltaTime * smooth);

		}
		if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && enter)
		{
			open = !open;
			click = true;
		}
		
		if (Input.GetKeyDown(KeyCode.F) && enter)
		{
			open = !open;
			click = true;
		}
		if (click == true)
        {
			click = false;
			PlayerMove.attention_level += 25;
			Debug.Log("어그로 수치: " + PlayerMove.attention_level);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			enter = true;
		}
		}

    void OnTriggerExit(Collider col)
{
	if (col.tag == "Player") {
		enter = false;
	}
}


}