using UnityEngine;
using System.Collections;

public class PottonScript : MonoBehaviour {
	public Rigidbody2D Rb;
	public int speed;
	public Transform targetPlayer;
	bool movingRight = false;
	public Animator anim;
	public double Animlength;
	double counter;
	double startTime;

	public Transform projectileSpawnPoint;
	//what prefab is instantiated
	public Bomb projectilePrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (movingRight) {
			Rb.velocity = (new Vector2 (speed, 0));
		}
		else {
			Rb.velocity = (new Vector2 (speed * -1, 0));
		}

		if (targetPlayer.transform.position.x - 0.09 < this.transform.position.x &&
		    targetPlayer.transform.position.x + 0.09 > this.transform.position.x &&
		    !anim.GetBool("Empty")) {
			anim.SetBool ("Dropping", true);
			startTime = Time.time;
		}

		if (startTime + Animlength < counter && anim.GetBool("Dropping") && !anim.GetBool("Empty")) {
			anim.SetBool ("Empty", true);
			Bomb temp = Instantiate(projectilePrefab, projectileSpawnPoint.position,
			                              projectileSpawnPoint.rotation)
				as Bomb;
		}
		counter = Time.time;
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		movingRight = !movingRight;
	}
}
