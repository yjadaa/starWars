using UnityEngine;
using System.Collections;

public class LeftArmForce : MonoBehaviour {
	
	private GameObject gamePlayer;
	private PlayerBehavior pState;
	Collider lightningCollider;
	Collider pushCollider;
	GameObject lightningModel;
	public AudioClip lightningSound;
	bool audioIsPlaying = false;
	float lastPlayTime = 0f;
	
	// Use this for initialization
	void Start () {
		lightningCollider = GameObject.FindWithTag("LightningBox").collider;
		pushCollider = GameObject.FindWithTag("PushBox").collider;
		lightningModel = GameObject.FindGameObjectWithTag("LightningModel");
		lastPlayTime = Time.time + 3;
		Debug.Log(lightningCollider + " " + pushCollider);
		this.DisableLightning();
		this.DisablePush();
	}
	
	// Update is called once per frame
	void Update () {
		gamePlayer = GameObject.FindWithTag("Player");
        if (gamePlayer != null)
            pState = gamePlayer.GetComponent<PlayerBehavior>();
        else
        {
            pState = null;
        }
		
		if(Time.time - lastPlayTime > 3) {
			audioIsPlaying = false;
		}
	}

    /*void OnCollisionEnter(Collision collision)
    {
		Debug.Log(collision.gameObject.name);
		if (collision.gameObject.name == "EnemyDroid(Clone)")
	        {
			if (pState.currentGesture == PlayerBehavior.Gesture.PUSH){
				Debug.Log("pushing");
		            collision.gameObject.GetComponent<EnemyBehavior>().GetPushed(transform.position);
		 
			} else if (pState.currentGesture == PlayerBehavior.Gesture.LIGHTNING) {
				Debug.Log("lightning" + Time.deltaTime);
				collision.gameObject.GetComponent<EnemyBehavior>().lightining(collision);
			}
		}

        Debug.Log(collision.gameObject.name);
        
    }*/
	
	public void DisableLightning() {
		lightningCollider.enabled = false;
		Debug.Log("lightning off");
		//lightningModel.renderer.enabled = false;
	}
	
	public void EnableLightning() {
		lightningCollider.enabled = true;
		Debug.Log("lightning on");
		//lightningModel.renderer.enabled = true;
		if(!audioIsPlaying) {
			audio.PlayOneShot( lightningSound );
			audioIsPlaying = true;
			lastPlayTime = Time.time;
		}
	}
	
	public void DisablePush() {
		pushCollider.enabled = false;
	}
	
	public void EnablePush() {
		pushCollider.enabled = true;
		Debug.Log("push on");
	}
}
