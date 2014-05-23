using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
	
	public AudioClip music;

	// Use this for initialization
	void Start () {
		audio.volume = 0.2f;
		audio.PlayOneShot(music);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
