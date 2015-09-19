using UnityEngine;
using System.Collections;

public class snakeGientScript : MonoBehaviour {

	
	//where the projectile spawns in the scene
	public Transform projectileSpawnPoint;
	//what prefab is instantiated
	public Projectile projectilePrefab;
	public Transform targetPlayer;
	public float fireRate;
	float countDown = 0;
	float countStart = 0;
	public int lives;

	//these are where the four projectiles are meant to aim for
	public Transform target0;
	public Transform target1;
	public Transform target2;
	public Transform target3;
	// Use this for initialization
	void Start () {
		if (!targetPlayer) {
			Debug.Log("ERROR! SNAKE MISSING player!");
		}
		if (!target0) {
			Debug.Log("ERROR! SNAKE MISSING target0!");
		}
		if (!target1) {
			Debug.Log("ERROR! SNAKE MISSING target1!");
		}
		if (!target2) {
			Debug.Log("ERROR! SNAKE MISSING target2!");
		}
		if (!target3) {
			Debug.Log("ERROR! SNAKE MISSING target3!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if ((fireRate + countStart) < countDown) {
			//first projectile
			Projectile fire0 = Instantiate (projectilePrefab, projectileSpawnPoint.position,
			                               projectileSpawnPoint.rotation)
				as Projectile;
			fire0.GetComponent<Projectile> ().setSeek (new Vector3 (target0.transform.position.x,
			                                                        target0.transform.position.y,
			                                                       0));
			//second projectile
			Projectile fire1 = Instantiate (projectilePrefab, projectileSpawnPoint.position,
			                                projectileSpawnPoint.rotation)
				as Projectile;
			fire1.GetComponent<Projectile> ().setSeek (new Vector3 (target1.transform.position.x,
			                                                        target1.transform.position.y,
			                                                        0));
			//third projectile
			Projectile fire2 = Instantiate (projectilePrefab, projectileSpawnPoint.position,
			                                projectileSpawnPoint.rotation)
				as Projectile;
			fire2.GetComponent<Projectile> ().setSeek (new Vector3 (target2.transform.position.x,
			                                                        target2.transform.position.y,
			                                                        0));
			//fourth projectile
			Projectile fire3 = Instantiate (projectilePrefab, projectileSpawnPoint.position,
			                                projectileSpawnPoint.rotation)
				as Projectile;
			fire3.GetComponent<Projectile> ().setSeek (new Vector3 (target3.transform.position.x,
			                                                        target3.transform.position.y,
			                                                        0));


			countStart = Time.time;
		}
		countDown = Time.time;
		if (lives < 1) {
			Destroy(gameObject);
		}
	}
	public void reduceLives()
	{
		lives--;
	}
}
