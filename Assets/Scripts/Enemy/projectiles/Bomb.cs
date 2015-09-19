using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag == "Player") {
			c.gameObject.GetComponent<Character_Controler_player> ().loseLife ();
		}
		Destroy (gameObject);
	}
}
