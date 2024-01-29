using UnityEngine;

public class QuitScript : MonoBehaviour
{
	private float exitTime;

	private void Start()
	{
		exitTime = 1f;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			exitTime -= Time.unscaledDeltaTime;
			if (exitTime <= 0f)
			{
				Application.Quit();
			}
		}
		else
		{
			exitTime = 1f;
		}
	}
}
