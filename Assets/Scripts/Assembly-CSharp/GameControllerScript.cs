using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
	public bool debugMode;

	public bool gameOver;

	public bool pause;

	public float gameOverTime;

	public AudioSource audioDevice;

	public AudioClip screech;

	public GameObject pauseText;

	private void Start()
	{
		Debug.Log("Game Controller Start");
		LockMouse();
		Time.timeScale = 1f;
	}

	private void Update()
	{
		if (gameOver)
		{
			Time.timeScale = 0f;
			gameOverTime -= Time.unscaledDeltaTime;
			audioDevice.PlayOneShot(screech);
			if (gameOverTime < 0f)
			{
				Time.timeScale = 1f;
				SceneManager.LoadScene("End");
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!pause)
			{
				Time.timeScale = 0f;
				pause = true;
				pauseText.SetActive(true);
			}
			else
			{
				Time.timeScale = 1f;
				pause = false;
				pauseText.SetActive(false);
			}
		}
	}

	public void LockMouse()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void UnlockMouse()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}
