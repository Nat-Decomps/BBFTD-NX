using UnityEngine;

public class PrincipalScript : MonoBehaviour
{
	public AudioSource audioDevice;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		audioDevice.Play();
	}
}
