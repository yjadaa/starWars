using UnityEngine;
using System.Collections;

public class SpellDamage : MonoBehaviour {

	public float EnemyHealth = 100.0f;
	
	public GameObject ExplosionPrefab;
	
	public float Spell1Damage;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	void Update()
	{
		if(EnemyHealth == 0.0f)
		{
			Destroy(gameObject);
			Instantiate(ExplosionPrefab, transform.position, transform.rotation);
		}
		
		
	}
	
	void OnCollisionEnter(Collision hit)
	{
		if(hit.gameObject.tag == "fireball")
		{
			EnemyHealth = Mathf.Clamp(EnemyHealth - Spell1Damage, 0f,100f);
			Destroy(hit.gameObject);
			
			Debug.Log("EnemyHealth = " + EnemyHealth);
		}
	}
}
