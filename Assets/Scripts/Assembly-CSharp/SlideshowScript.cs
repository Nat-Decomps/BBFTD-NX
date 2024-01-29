using UnityEngine;

public class SlideshowScript : MonoBehaviour
{
	public GameObject[] slides;

	private int i;

	private void Start()
	{
		slides[0].SetActive(true);
	}

	private void Update()
	{
		if (Input.anyKeyDown & !Input.GetKey(KeyCode.Escape))
		{
			i++;
			if (i < slides.Length)
			{
				slides[i - 1].SetActive(false);
				slides[i].SetActive(true);
			}
		}
	}
}
