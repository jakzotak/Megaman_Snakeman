using UnityEngine;
using System.Collections;

public class Snake_PlatformScript : MonoBehaviour {
	public GameObject Master;
	public Animator anim;
	public float movementSpeed;
	public float intervel;
	public float distance;
	public Rigidbody2D rb;
	float startpos;
	// Use this for initialization
	void Start () {
	if (!Master) {
			Debug.Log ("ERROR! MASTER NOT SET!");
		}
		startpos = this.transform.position.y;
		this.transform.position = new Vector3(this.transform.position.x,
		                                      this.transform.position.y + intervel);
	}
	
	// Update is called once per frame
	void Update () {
	if (!Master) {
			movementSpeed = 0;
		}
		if (this.transform.position.y > distance + startpos && movementSpeed > 0 ||
		    this.transform.position.y < -distance +startpos && movementSpeed < 0) {
			movementSpeed = -movementSpeed;
		}
		rb.velocity = new Vector2 (0, movementSpeed);
	}
}
