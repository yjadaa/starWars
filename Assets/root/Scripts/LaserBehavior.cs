using UnityEngine;
using System.Collections;

public class LaserBehavior : MonoBehaviour {
	
	public float DAMAGE = 20f;
    public AudioClip LaserDeflect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision) {

		if(collision.gameObject.name == "First Person Controller") {
			collision.gameObject.GetComponent<PlayerBehavior>().TakeDamage(DAMAGE);
			Destroy(this.gameObject);
		}

        else if ( collision.gameObject.name == "SaberCollisionBox" )
        {
            ContactPoint contact = collision.contacts[0];
            Debug.DrawRay( contact.point, contact.normal, Color.red );
            contact.thisCollider.rigidbody.AddForce( contact.normal * 400 );
            audio.PlayOneShot( LaserDeflect );

            GameObject gamePlayer = GameObject.FindWithTag( "Player" );
            if ( gamePlayer != null )
            {
                PlayerBehavior playerState = gamePlayer.GetComponent<PlayerBehavior>();
                playerState.LaserDeflected();
            }
        
        }
	}
}
