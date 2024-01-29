using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
	public float delay;

	private void Start()
	{
		delay = 1f;
	}

	private void Update()
	{
		if (delay > 0f)
		{
			delay -= Time.unscaledDeltaTime;
		}
		if (Input.anyKeyDown & (delay <= 0f) & !Input.GetKey(KeyCode.Escape))
		{
			SceneManager.LoadScene("School");
		}
	}
}
