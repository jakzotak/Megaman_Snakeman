using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	//for when hidden in a cloud
	public float speed;
	//for when not hiding
	public float fastSpeed;

	public Rigidbody2D Rb;
	public Animator anim;

	bool hiding = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (hiding) {
			Rb.velocity = (new Vector2 (speed, 0));
		} 
		else {
			Rb.velocity = (new Vector2 (fastSpeed, 0));
			anim.SetBool("Hiding", true);
		}
	}
	public void hit()
	{
		hiding = false;
		//consider making the bullet hold still for a moment
	}
	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag == "Player") {
			Debug.Log("touch");
			c.gameObject.GetComponent<Character_Controler_player>().loseLife();
			Destroy(this);
		}
	}
}
