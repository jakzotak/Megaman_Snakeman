using UnityEngine;
using System.Collections;

public class Dada_Script : MonoBehaviour {

	//tracks how many times Dada has jumped
	int jumps = 0;
	public int jumpHight;
	public int SuperJumpHight;
	public Rigidbody2D rd;
	public int movespeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.rd.velocity = new Vector2 (movespeed, this.rd.velocity.y);
	}
	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag == "Ground") {
		//	Debug.Log("jump");
			if(jumps < 2)
			{
				jumps ++;
				this.rd.AddForce (new Vector2 (0, jumpHight));
			}
			else{
				jumps = 0;
				this.rd.AddForce (new Vector2 (0, SuperJumpHight));
			}
		}
	}
}
