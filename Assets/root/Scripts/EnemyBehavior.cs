using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour{

	public float MoveSpeed= 15f;
	public float AttackRange = 2000f;
	public float TouchRange = 10f;
	public float DamagePerSecond = 10f;
	public AudioClip DamageSound;

	private PlayerBehavior pState;
    private GameObject gamePlayer;
	
	private bool audioPlaying = false;
    private bool isActive = true;
	
	// Use this for initialization
	void Start () 
	{
		gamePlayer = GameObject.FindWithTag("Player");
        if (gamePlayer != null)
            pState = gamePlayer.GetComponent<PlayerBehavior>();
        else
        {
            Debug.Log("no player");
            pState = null;
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (this.isActive)
        {
            //if close to player, damage him
            if (isTouching() && pState.currentGesture != PlayerBehavior.Gesture.HEAL)
            {
               pState.healthRemaining  = Mathf.Clamp(pState.healthRemaining - DamagePerSecond*Time.deltaTime, 0.0f, 100.0f);
                /*if(!audioPlaying)
                {
                    audio.clip = DamageSound;
                    audio.loop = true;
                    audio.volume = 0.6f;
                    audio.Play();

                    audioPlaying = true;
                }*/
            }
            else if (isClose())
            {
                //if player in range, orient and move towards him
                Vector3 playerPos = gamePlayer.transform.position;
                //Debug.Log(playerPos);
                transform.LookAt(playerPos);
                Vector3 moveDirection = playerPos - transform.position;
                moveDirection.Normalize();

                transform.position = transform.position + moveDirection * MoveSpeed * Time.deltaTime;

                if (audioPlaying)
                {
                    audio.Stop();
                    audioPlaying = false;
                }
            }
        }
	}
	
	void OnCollisionEnter(Collision collision)
    {
		Debug.Log("enemy" + Time.deltaTime + " " + pState);
		if (pState.currentGesture == PlayerBehavior.Gesture.PUSH){
			Debug.Log("pushing");
	            this.GetPushed(transform.position);
	 
		} else if (pState.currentGesture == PlayerBehavior.Gesture.LIGHTNING) {
			Debug.Log("lightning" + Time.deltaTime);
			this.lightining(collision);
		}
        
    }
	
	public bool isClose()
	{
        //Debug.Log(Vector3.Distance(gamePlayer.transform.position, transform.position));
        return Vector3.Distance(gamePlayer.transform.position, transform.position) < AttackRange;
	}
	
	public bool isTouching()
	{
        return Vector3.Distance(gamePlayer.transform.position, transform.position) < TouchRange;
	}
	
	public void GetThrown() {
		this.isActive = false;
	}

    public void GetPushed(Vector3 handPos)
    {
        this.isActive = false;
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
        Vector3 awayVec = handPos - transform.position;
        Debug.Log(awayVec);
        awayVec.z *= -1;
        rb.AddForce(awayVec.normalized * 5800);
    }
	public void lightining(Collision hit)
    {
		Debug.Log("DESTROY " + Time.deltaTime);
        Destroy(gameObject);
    }
}
