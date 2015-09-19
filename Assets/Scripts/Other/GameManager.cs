using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	static GameManager _instance = null;
	
	public Canvas ScreenTitle;
	public Canvas ScreenHUD;
	public Canvas PauseMenu;
	public Canvas levelSelect;
	public AudioSource pauseSound;
	GameObject _mainCamera;
	GameObject _Player;
	bool gameStarted = false;
	// Use this for initialization
	void Start () {
		if (instance) {
			DestroyImmediate (gameObject);
		} else {
			_instance = this;
			DontDestroyOnLoad (this);
			int _score = 0;
		}
		PauseMenu.GetComponent<CanvasGroup>().alpha = 0f;
		PauseMenu.GetComponent<CanvasGroup> ().interactable = false;
		PauseMenu.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && gameStarted) {
			pauseGame();		
		}
		if (!_mainCamera) {
			Debug.Log("camera missing");
		}
		if (!_Player) {
			Debug.Log("player missing");
		}
	}

	public static GameManager instance 
	{
		get{ return _instance;}
		set{ _instance = value;}
		//get; set;
	}

	public void pauseGame()
	{		
		//	SoundManager.instance.playSingle(pauseSound);
		pauseSound.Play ();
		//PauseMenu.enabled = true;
		PauseMenu.GetComponent<CanvasGroup>().alpha = 1f;
		PauseMenu.GetComponent<CanvasGroup> ().interactable = true;
		PauseMenu.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		
		_mainCamera.transform.position = new Vector3(this.transform.position.x, 
		                                        this.transform.position.y,
		                                        this.transform.position.z);
		Time.timeScale = 0;
	}
	public void unPauseGame()
	{
		_mainCamera.transform.position = new Vector3(_Player.transform.position.x, 
		                                        _Player.transform.position.y,
		                                        -1);
		Time.timeScale = 1;
		PauseMenu.GetComponent<CanvasGroup>().alpha = 0f;
		PauseMenu.GetComponent<CanvasGroup> ().interactable = false;
		PauseMenu.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		
	}
	public void quitGame()
	{
		Application.Quit ();
	}
	public void startGame(){
		Application.LoadLevel ("SnakeManLevel");
		//disable screenTitle Canvas
		ScreenTitle.enabled = false;
		
		levelSelect.GetComponent<CanvasGroup>().alpha = 0f;
		levelSelect.GetComponent<CanvasGroup> ().interactable = false;
		levelSelect.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		gameStarted = true;
	}
	public void goToLevelSelect()
	{
		Application.LoadLevel ("SnakeMan_LevelSelect");
		//disable screenTitle Canvas
		ScreenTitle.enabled = false;
		
		ScreenTitle.GetComponent<CanvasGroup>().alpha = 0f;
		ScreenTitle.GetComponent<CanvasGroup> ().interactable = false;
		ScreenTitle.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	public GameObject mainCamera
	{
		get{ return _mainCamera;}
		set{_mainCamera = value;}
	}

	public GameObject Player
	{
		get{ return _Player;}
		set{_Player = value;}
	}

	public static GameManager getInstance()
	{
		return _instance;
	}
	public void Victory()
	{
		Application.LoadLevel ("Victory");
	}
}
