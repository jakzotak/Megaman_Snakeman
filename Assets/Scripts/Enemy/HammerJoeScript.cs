using UnityEngine;
using System.Collections;

public class HammerJoeScript : MonoBehaviour {

	public float fireRate;
	float countDown = 0;
	float countStart = 0;
	public double throwAnimLength;
	public float waitTime;
	public Animator anim;

	//wheather or not this character is in the middle of throwing
	bool throwing = false;

	public Transform projectileSpawnPoint;
	//what prefab is instantiated
	public Projectile projectilePrefab;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((fireRate + countStart) < countDown && !anim.GetBool("throwing") && !anim.GetBool("thrown")) {
			anim.SetBool("throwing", true);
			countStart = Time.time;
			throwing = true;
			Debug.Log ("start");
		}
		else if(countStart + throwAnimLength < countDown && !anim.GetBool("thrown") && anim.GetBool("throwing"))
		{
			Projectile temp = Instantiate(projectilePrefab, projectileSpawnPoint.position,
			                              projectileSpawnPoint.rotation)
				as Projectile;
			temp.GetComponent<Projectile>().speed *= -1;
			anim.SetBool("thrown", true);
			countStart = Time.time;
			Debug.Log ("middle");
		}

		else if (waitTime + countStart < countDown && anim.GetBool("thrown") && anim.GetBool("throwing")) {

			anim.SetBool("thrown", false);
			anim.SetBool("throwing", false);
			countStart = Time.time;
			Debug.Log ("end");
		}

		countDown = Time.time;

	}


}
