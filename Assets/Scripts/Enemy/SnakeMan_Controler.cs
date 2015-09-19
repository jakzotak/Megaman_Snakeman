using UnityEngine;
using System.Collections;

public class SnakeMan_Controler : MonoBehaviour {

	public Rigidbody2D rb;
	public Animator anim;
	public int shotProbability;
	public float jumpForce;
	//the length of time it takes to reach the peak of a jump
	public float jumplength;
	//time between the two snakeMissiles
	public float betweenTime;
	//how long snakeman waits before moving
	public float waitTime;
	float startTime;
	bool faceingRight = true;
	bool shooting;
	bool waiting;
	public float movementSpeed;
	bool isColliding;
	//where the projectile spawns in the scene
	public Transform projectileSpawnPoint;
	//what prefab is instantiated
	public SnakeMissile projectilePrefab;
	// Use this for initialization

	string previusSide;

	bool isGrounded; 				//if character is on the ground
	bool hasShot;
	public LayerMask isGroundLayer; //used to list what is ground
	public Transform groundCheck;	//used to check ground colision
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<LivesManager>().lives < 1) {
			Application.LoadLevel("Victory");
		}
		if (!shooting && isGrounded) {
			if (faceingRight && !waiting) {
		//		Debug.Log ("snakeman is moving to the right");
				rb.velocity = new Vector2 (movementSpeed, rb.velocity.y);
			} else if (!waiting){
		//		Debug.Log ("snakeman is moving to the left");
				rb.velocity = new Vector2 (-movementSpeed, rb.velocity.y);
			}
		}

		//check if character is on ground
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, isGroundLayer);


		if (startTime + jumplength < Time.time && shooting && !hasShot) {
			anim.SetBool("falling", true);
			if( shooting){
				anim.SetBool("shooting", true);
				startTime = Time.time;
				SnakeMissile temp = Instantiate(projectilePrefab, projectileSpawnPoint.position,
			                              projectileSpawnPoint.rotation)
					as SnakeMissile;
				Debug.Log("snakeman is firing his first shot");
				hasShot = true;
				if(faceingRight && temp.GetComponent<SnakeMissile>().getFaceingRight() == false
				   || !faceingRight && temp.GetComponent<SnakeMissile>().getFaceingRight())
				{
					temp.GetComponent<SnakeMissile>().invertFaceingRight();
				}
			}
		}
		if (startTime + betweenTime < Time.time && shooting && hasShot) {

			SnakeMissile temp2 = Instantiate(projectilePrefab, projectileSpawnPoint.position,
			                                projectileSpawnPoint.rotation)
				as SnakeMissile;
			Debug.Log("snakeman is firing his second shot");
			if(faceingRight && !temp2.GetComponent<SnakeMissile>().getFaceingRight()  
			   || !faceingRight && temp2.GetComponent<SnakeMissile>().getFaceingRight())
			{
				temp2.GetComponent<SnakeMissile>().invertFaceingRight();
			}
			shooting = false;
			hasShot = false;
		}
		if (isGrounded) {
			anim.SetBool("shooting", false);
			anim.SetBool("falling", false);
			anim.SetBool("jumping", false);
		//	Debug.Log ("snakeman is grounded");
			if(rb.velocity.x > 0 || rb.velocity.x < 0)
			{
		//		Debug.Log("velocity is " + rb.velocity.x);
				anim.SetBool("running", true);
			}
		}

	}
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.name == previusSide) {
			return;
		}
		if (c.gameObject.tag == "navFlip") {
			waiting = true;
			StartCoroutine(waiter(true));
		//	Debug.Log ("snakeman has entered a navFlip " + this.transform.position.x); 
			previusSide = c.gameObject.name;
		}
		else if (c.gameObject.tag == "navJump") {
		//	Debug.Log ("snakeman is inside a navJump");
			jump ();
		}
		else if (c.gameObject.name == "BossMidPoint") {
		//	Debug.Log ("snakeman has entered BossMidPoint");
			waiting = true;
			StartCoroutine(waiter(false));
			previusSide = c.gameObject.name;
		}
	}
	
	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag == "stopWalking") {
			rb.velocity = new Vector2(0, rb.velocity.y);
		}
	}

	//makes character flip
	void flip()
	{
	//	Debug.Log ("snakeman is flipping");
		//toggle faceingRight
		faceingRight = !faceingRight;
		Vector3 scaleFacter = transform.localScale; //scale is 1,1,1
		//flip the scale
		scaleFacter.x *= -1; //scale changed to -1,1,1
		//update scale to flipped value
		transform.localScale = scaleFacter;
		projectileSpawnPoint.transform.eulerAngles = new Vector3
			(projectileSpawnPoint.transform.eulerAngles.x, (projectileSpawnPoint.transform.eulerAngles.y + 180f),
			 projectileSpawnPoint.transform.eulerAngles.z);
	}

	void jump()
	{
		anim.SetBool("jumping", true);
		rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
	//	Debug.Log ("snakeman is jumping");
	}
	void fire()
	{
		int choice = Random.Range (0, 10);
		Debug.Log ("snakeman is deciding whether or not to shoot");
			if(choice < shotProbability)
		{
			Debug.Log ("snakeman is shooting");
			jump ();
			startTime = Time.time;
			shooting = true;
		}
	}

	IEnumerator waiter(bool flipcharacter)
	{
		Debug.Log("starting wait");
		if (flipcharacter) {
			flip ();
		}
		rb.velocity = new Vector2 (0, rb.velocity.y);
		anim.SetBool("running", false);
		yield return new WaitForSeconds(waitTime);
		Debug.Log ("wait finished");
		anim.SetBool("running", true);
		fire ();
		waiting = false;
	}
}
