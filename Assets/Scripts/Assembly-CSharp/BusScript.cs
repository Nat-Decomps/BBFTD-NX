using UnityEngine;
using UnityEngine.SceneManagement;

public class BusScript : MonoBehaviour
{
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
			SceneManager.LoadScene("SampleScene");
		}
	}
}
