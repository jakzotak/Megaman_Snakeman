using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	
	public float lifeTime;
	public float speed;
	public Rigidbody2D Rd;
	Vector3 playerpos;
	public bool seek = false;

	// Use this for initialization
	void Start () {
		//destroy gameobject after some time
		Destroy (gameObject, lifeTime);
		
		//make projectile move when instantiated

	}
	
	// Update is called once per frame
	void Update () {
		if (seek) {
			//change to a normal translation
			//transform.Translate (Vector3.forward * speed * Time.deltaTime);
			/*
			Quaternion myrotation = Quaternion.LookRotation(playerpos, playerpos);
			transform.rotation = myrotation;*/
			//transform.Translate (playerpos, Space.Self);
			transform.position = Vector3.MoveTowards(transform.position, playerpos, 0.03f);
		}
		else {
			this.Rd.velocity = (new Vector2 (speed, 0));
		}
		//transform.position = new Vector3 (this.transform.position.x,
		 //                                this.transform.position.y,
		 //                                0);
		transform.rotation = Quaternion.Euler(0,0,this.transform.rotation.z);

	}
	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag == "Enemy" && !seek) {
			c.gameObject.GetComponent<LivesManager> ().loseLife ();
		} else if (c.gameObject.tag == "EnemyBullet" && !seek) {
			c.gameObject.GetComponent<LivesManager> ().loseLife ();
			c.gameObject.GetComponent<BulletScript> ().hit ();
		} else if (c.gameObject.tag == "Snake_Gient") {
			c.gameObject.GetComponent<LivesManager> ().loseLife ();
		} else if (c.gameObject.tag == "Player") {
			c.gameObject.GetComponent<Character_Controler_player> ().loseLife ();
		}
		else {
			//Destroy(gameObject);
		}

		Destroy(gameObject);
	}
	public void setSeek(Vector3 pos){
		playerpos = pos;
		seek = true;
	}
}
