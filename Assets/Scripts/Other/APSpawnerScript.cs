using UnityEngine;
using System.Collections;

public class APSpawnerScript : MonoBehaviour {

	public AscensionPlatformScript platformPrefab;
	public float spawnRate;
	//the time the last playform was spawned
	float timerStart;
	// Use this for initialization
	void Start () {
		timerStart = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (timerStart + spawnRate < Time.time) {
			timerStart = Time.time;
			AscensionPlatformScript temp = Instantiate(platformPrefab, this.transform.position,
			                        this.transform.rotation)
				as AscensionPlatformScript;
		}
	}
}
