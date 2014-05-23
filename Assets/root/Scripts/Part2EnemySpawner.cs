using UnityEngine;
using System.Collections;

public class Part2EnemySpawner : MonoBehaviour {

    float lastSpawnTime = 0f;
	public GameObject EnemyDroidPrefab;

	// Use this for initialization
	void Start () {
		this.Spawn();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - lastSpawnTime > 3)
        {
            this.Spawn();
        }
	}
	
	void Spawn() {
		float x = Random.Range(-40, 40);
            float z = Random.Range(30, 300);
            float y = 0f;
			
			GameObject enemy = Instantiate(EnemyDroidPrefab, new Vector3(x, y, z), transform.rotation) as GameObject;
			lastSpawnTime = Time.time;
	}
}
