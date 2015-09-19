using UnityEngine;
using System.Collections;

public class AscensionPlatformScript : MonoBehaviour {

	public Rigidbody2D rb;
	public float movementSpeed;
	public float lifeTime;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = (new Vector2(0, movementSpeed));
	}
}
