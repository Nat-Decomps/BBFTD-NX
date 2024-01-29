using UnityEngine;

public class TutorBaldiScript : MonoBehaviour
{
	public AudioSource audioDevice;

	private void Start()
	{
	}

	private void Update()
	{
		if (!audioDevice.isPlaying)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
