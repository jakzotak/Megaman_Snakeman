using UnityEngine;
using System.Collections;

public class SpiderScript : MonoBehaviour {
	//how fast the spider moves
	public float speed;
	//how long the spider will go a direction before switching
	public float distance;

	public Rigidbody2D Rb;
	public Animator anim;
	
	bool goingUp = false;
	float counter;
	float timeStart = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (goingUp) {
				Rb.velocity = (new Vector2(0, speed));
			}

		 else
			{
				Rb.velocity = (new Vector2(0, -speed));
			}
		if (counter > distance + timeStart) {
			timeStart = Time.time;
			goingUp = !goingUp;
		}
		counter = Time.time;
	}
}
