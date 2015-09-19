using UnityEngine;
using System.Collections;

public class snakeScript : MonoBehaviour {

	//where the projectile spawns in the scene
	public Transform projectileSpawnPoint;
	//what prefab is instantiated
	public Projectile projectilePrefab;
	public Transform targetPlayer;
	public float fireRate;
	float countDown = 0;
	float countStart = 0;
	// Use this for initialization
	void Start () {
	if (!targetPlayer) {
			Debug.Log("ERROR! SNAKE MISSING TARGET!");
		}
	}
	
	// Update is called once per frame
	void Update () {

		if ((fireRate + countStart) < countDown) {
			Projectile temp = Instantiate (projectilePrefab, projectileSpawnPoint.position,
		                              projectileSpawnPoint.rotation)
			as Projectile;
			temp.GetComponent<Projectile> ().setSeek (new Vector3 (targetPlayer.transform.position.x,
			                                                       targetPlayer.transform.position.y,
			                                                       0));
			countStart = Time.time;
		}
		countDown = Time.time;

	}

}
