using UnityEngine;
using System.Collections;

public class LivesManager : MonoBehaviour {
	public int lives;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (lives < 1) {
			Destroy(gameObject);
		}
	}
	public void loseLife()
	{
		lives --;
	}
}
