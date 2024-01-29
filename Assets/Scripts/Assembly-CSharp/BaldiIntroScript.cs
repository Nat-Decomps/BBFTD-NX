using UnityEngine;

public class BaldiIntroScript : MonoBehaviour
{
	public AudioSource audioDevice;

	public Animator animatic;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player")
		{
			animatic.SetTrigger("closwe");
			audioDevice.Play();
		}
	}
}
