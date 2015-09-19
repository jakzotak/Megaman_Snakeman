using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]//must have an animator for character animating
[RequireComponent(typeof(BoxCollider2D))]//collision with everything including the floor is required
[RequireComponent(typeof(Rigidbody2D))]//gravity and other important components require this

public class Character_Controler_player : MonoBehaviour {
	//variable decleration section
	public Rigidbody2D rb;
	public float jumpForce; //lets user update value through inspector
	public bool canClimb = false;
	public float normalGrav;
	public Camera_Controler MainCamera;
	float animTimer = 0;
	public float animLength;
	public float slideSpeed;
	public float slideDistance;
	public BoxCollider2D megaCollider;
	public int lives;
	public AudioClip ShootSound;
	public AudioClip JumpSound;
	public AudioClip HurtSound;
	float collisionHight = 0.2409284f;
	// Use this for initialization
	void Start () {
		float normalGrav = rb.gravityScale;
		if (!rb) {
			Debug.Log ("no rigidbody2d attached");
		}
		GameManager.getInstance().Player = this.gameObject;
	}
	
	bool isGrounded; 				//if character is on the ground
	public LayerMask isGroundLayer; //used to list what is ground
	public Transform groundCheck;	//used to check ground colision
	public Animator anim;
	int state;
	//when the player started to slide
	float slideStart;
	
	//controls facing direction
	public bool faceingRight;
	
	//where the projectile spawns in the scene
	public Transform projectileSpawnPoint;
	//what prefab is instantiated
	public Projectile projectilePrefab;

	// Update is called once per frame
	void Update () {
		if (lives < 1) {
			Application.LoadLevel("GameOver");
		}
		if (slideStart + slideDistance < Time.time) {
			megaCollider.size = new Vector2(megaCollider.size.x, 0.2409284f);
			anim.SetBool ("Sliding", false);
		} 
		if (canClimb) {
			rb.gravityScale = 0;
		} else {
			rb.gravityScale = normalGrav;
		}
		//check if character is on ground
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, isGroundLayer);
		if (!isGrounded) {
			anim.SetBool ("Jumping", true);
		} else {
			anim.SetBool ("Jumping", false);
		}
		//check if space is pressed
		if(Input.GetButtonDown ("Jump") && isGrounded)
		{
			//use rigidbody to move character
			SoundManager.instance.playSingle(JumpSound);
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}

		//check if slide pressed

		if (Input.GetKeyDown (KeyCode.LeftAlt)) {
			anim.SetBool("Sliding", true);
			megaCollider.size = new Vector2(megaCollider.size.x, 0.1409284f);
			slideStart = Time.time;
			if(faceingRight == true)
			{
				rb.velocity = (new Vector2(slideSpeed, rb.velocity.y));
				Debug.Log("sliding to the right");
			}
			else
			{
				rb.velocity = (new Vector2(-slideSpeed, rb.velocity.y));
				Debug.Log("sliding to the left");
			}
		}

		//check if fire pressed
		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			SoundManager.instance.playSingle(ShootSound);
			Projectile temp = Instantiate(projectilePrefab, projectileSpawnPoint.position,
			                              projectileSpawnPoint.rotation)
				as Projectile;
			if(faceingRight && temp.GetComponent<Projectile>().speed < 0
			   || !faceingRight && temp.GetComponent<Projectile>().speed > 0)
			{
				temp.GetComponent<Projectile>().speed *= -1;
			}
			anim.SetBool("Shooting", true);
			animTimer = Time.time;
		}

		if (!anim) {
			Debug.Log("there is no anim");
		}
		//make character move left or right if grounded
		//get axisraw returns -1, 0, 1
		//get axis returns -1->1
		float moveValue = Input.GetAxisRaw ("Horizontal");
		float ClimbValue = Input.GetAxisRaw ("Vertical");
		//Debug.Log (moveValue);
		if (!anim.GetBool ("Sliding")) {
			rb.velocity = new Vector2 (moveValue, rb.velocity.y);
		}
		anim.SetFloat ("Move", Mathf.Abs (moveValue));

		if (moveValue > 0 && !faceingRight || moveValue < 0 && faceingRight) {
			flip ();
		}
		// Change state parameter
		anim.SetFloat ("Move", Mathf.Abs (moveValue));

		if(Input.GetKey(KeyCode.UpArrow) && canClimb)
		{
			rb.velocity = new Vector2 (rb.velocity.x, ClimbValue);
		}
		if(Input.GetKey(KeyCode.DownArrow) && canClimb)
		{
			rb.velocity = new Vector2 (rb.velocity.x, ClimbValue);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {

		}
		if (Time.time > (animLength + animTimer)) {
			anim.SetBool("Shooting", false);
		}

	}
	
	//check collisions/triggers
	//check when character first collides
	//c is an object that character collides with
	//one of the gameObjects must have a rigidbody2D
	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag == "Enemy") {
			loseLife();
		}
	}
	//check when character first uncollides
	void OnCollisionExit2D(Collision2D c)
	{

	}
	//check when character stays colliding
	void OnCollisionStay2D(Collision2D c)
	{
		
	}
	
	//for triggers
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag == "Collectible") {
			//do something with the GameObject
			//destroy game object
			Destroy (c.gameObject);
		}
		if (c.gameObject.tag == "climbUp") {
			canClimb = true;
			anim.SetBool ("Climbing", true);
			rb.velocity = new Vector2 (0, 0);
			Debug.Log("climbing");
			MainCamera.canClimb = true;
			MainCamera.GoingUp = true;
		}
		if (c.gameObject.tag == "climbDown") {
			canClimb = true;
			anim.SetBool ("Climbing", true);
			rb.velocity = new Vector2 (0, 0);
			Debug.Log("climbing");
			MainCamera.canClimb = true;
			MainCamera.GoingUp = false;
		}
		if (c.gameObject.tag == "cameraOveride") {
			MainCamera.canClimb = true;
			MainCamera.GoingUp = false;
		}
		if(c.gameObject.tag == ("Spawner"))
		   {
			c.gameObject.GetComponent<SpawnTrigger> ().activate();
		}
		if (c.gameObject.tag == "DeathZone") {
			lives = 0;
		}
	}
	//check when character first uncollides
	void OnTriggerExit2D(Collider2D c)
	{
		canClimb = false;
		anim.SetBool ("Climbing", false);
		MainCamera.canClimb = false;
	}
	//check when character stays colliding
	void OnTriggerStay2D(Collider2D c)
	{
		
	}
	
	//makes character flip
	void flip()
	{
		//toggle faceingRight
		faceingRight = !faceingRight;
		Vector3 scaleFacter = transform.localScale; //scale is 1,1,1
		//flip the scale
		scaleFacter.x *= -1; //scale changed to -1,1,1
		//update scale to flipped value
		transform.localScale = scaleFacter;
	}

	public void loseLife()
	{
		SoundManager.instance.playSingle(HurtSound);
		lives --;
	}
	
}
