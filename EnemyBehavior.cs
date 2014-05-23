using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour{

	public float MoveSpeed= 15f;
	public float AttackRange = 200f;
	public float TouchRange = 2f;
	public float DamagePerSecond = 10f;
	public AudioClip DamageSound;

	private PlayerState pState;
	
	private bool audioPlaying = false;
	
	// Use this for initialization
	void Start () 
	{
		GameObject gamePlayer = GameObject.FindWithTag("Player1");
		if(gamePlayer != null)
			pState = gamePlayer.GetComponent<PlayerState>();
		else 
			pState = null;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if close to player, damage him
		if(isTouching())
		{
			pState.Health  = Mathf.Clamp(pState.Health - DamagePerSecond*Time.deltaTime, 0.0f, 100.0f);
			if(!audioPlaying)
			{
				audio.clip = DamageSound;
				audio.loop = true;
				audio.volume = 0.6f;
				audio.Play();

				audioPlaying = true;
			}
		}
		else if(isClose())
		{
			//if player in range, orient and move towards him
			
			transform.LookAt(Camera.main.transform.position);
			Vector3 moveDirection = Camera.main.transform.position - transform.position;
			moveDirection.Normalize();
			
			transform.position = transform.position + moveDirection * MoveSpeed * Time.deltaTime;
			
			if(audioPlaying)
			{
				audio.Stop();
				audioPlaying = false;
			}
		}
	}
	
	public bool isClose()
	{
		return Vector3.Distance(Camera.main.transform.position, transform.position) < AttackRange;
	}
	
	public bool isTouching()
	{
		return Vector3.Distance(Camera.main.transform.position, transform.position) < TouchRange;
	}
}
