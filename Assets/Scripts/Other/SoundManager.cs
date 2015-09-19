using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	public static SoundManager instance = null;
	
	public AudioSource sfxSource;
	// Use this for initialization
	void Start () {
		
		if (!instance) {
			instance = this;
		}
		else{
			Destroy(gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}
	
	public void playSingle(AudioClip clip){
		
		sfxSource.clip = clip;
		sfxSource.Play ();
	}
	
}
