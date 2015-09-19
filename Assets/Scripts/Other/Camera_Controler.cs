using UnityEngine;
using System.Collections;

public class Camera_Controler : MonoBehaviour {
	public Rigidbody2D rb;
	bool _canClimb;	
	bool _GoingUp;
	GameObject playerpos;
	Camera_Controler _instance;
	public float hightadjuster;
	// Use this for initialization
	void Start () {
		 playerpos = GameObject.FindWithTag ("Player");
		GameManager.getInstance().mainCamera = this.gameObject;
	}
	// Update is called once per frame
	void Update () {

		if (_canClimb && GoingUp) {
			transform.position = new Vector3 (playerpos.transform.position.x,
			                                  (playerpos.transform.position.y + 0.6f),
			                                  this.transform.position.z);
		} else if (_canClimb) {
			transform.position = new Vector3 (playerpos.transform.position.x,
			                                  (playerpos.transform.position.y - 0.1f),
			                                  this.transform.position.z);
		}
		else {
			transform.position = new Vector3 (playerpos.transform.position.x,
		                                  this.transform.position.y,
		                                  this.transform.position.z);
//			Debug.Log ("not climbing");
		} 
		}
	/*	if (canClimb) { //this code is broken but will be fixed for the next milestone
			if(Input.GetKey(KeyCode.UpArrow) && canClimb)
			{
				rb.velocity = new Vector2 (rb.velocity.x, ClimbValue);
			}
			if(Input.GetKey(KeyCode.DownArrow) && canClimb)
			{
				rb.velocity = new Vector2 (rb.velocity.x, ClimbValue);
			}
		} */
		

		public bool canClimb
	{
		get{ return _canClimb;}
		set{_canClimb = value;}
	}

	public bool GoingUp
	{
		get{ return _GoingUp;}
		set{_GoingUp = value;}
	}
	public static Camera_Controler instance{
		get;
		set;
	}
		   }

