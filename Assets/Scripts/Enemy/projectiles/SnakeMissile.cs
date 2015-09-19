using UnityEngine;
using System.Collections;

public class SnakeMissile : MonoBehaviour {

	public float lifeTime;
	public float speed;
	public Rigidbody2D Rb;
	public bool faceingRight = false;
	bool faceingUp = false;
	public Animator anim;
	bool falling = true;
	//the last trigger that the snake collided with
	string lastTrigger;
	void Start () {
		Destroy (gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		if (!falling) {
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (lastTrigger == c.gameObject.name) {
			return;
		}
		if (c.gameObject.tag == "SlitherUp") {
			lastTrigger = c.gameObject.name;
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, (transform.eulerAngles.z + 90f));
		}
		else if (c.gameObject.tag == "SlitherDown") {
			lastTrigger = c.gameObject.name;
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, (transform.eulerAngles.z - 90f));
		}
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag == "Player") {
			c.gameObject.GetComponent<Character_Controler_player> ().loseLife ();
		}
		if (c.gameObject.tag == "Ground" && falling) {
			Debug.Log("snakeMissile just hit the ground");
			falling = false;
			Rb.gravityScale = 0;
//			Rb.velocity = new Vector2(-Vector3.right * speed * Time.deltaTime);
		}
	}

	void flipHorizontal()
	{
		//toggle faceingRight
		faceingRight = !faceingRight;
		Vector3 scaleFacter = transform.localScale; //scale is 1,1,1
		//flip the scale
		scaleFacter.x *= -1; //scale changed to -1,1,1
		//update scale to flipped value
		transform.localScale = scaleFacter;
	}

	void flipVirtical()
	{
		//toggle faceingRight
		faceingUp = !faceingUp;
		Vector3 scaleFacter = transform.localScale; //scale is 1,1,1
		//flip the scale
		scaleFacter.y *= -1; //scale changed to -1,1,1
		//update scale to flipped value
		transform.localScale = scaleFacter;
	}
	public void invertFaceingRight ()
	{
		faceingRight = !faceingRight;
	}
	public bool getFaceingRight()
	{
		return faceingRight;
	}
}
