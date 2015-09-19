using UnityEngine;
using System.Collections;

public class SpawnTrigger : MonoBehaviour {

	public GameObject enemytoSpawn;
	public Transform spawnPoint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

public void activate()
	{
		GameObject temp = Instantiate(enemytoSpawn, spawnPoint.position,
		                              spawnPoint.rotation)
			as GameObject;
		Destroy (gameObject);
	}
}
