using System.Collections;
using UnityEngine;

public class CloudyCopterScript : MonoBehaviour
{
	public FireScript fire;

	public AudioSource audioDevice;

	public AudioClip blowing;

	public AudioClip pah;

	public bool bBlowing;

	public float dropSpeed;

	public float minWait;

	public float maxWait;

	public float wait;

	private Vector3 initialLocation;

	private void Start()
	{
		initialLocation = base.transform.position;
		wait = Random.Range(minWait, maxWait);
		StartCoroutine("Wait");
	}

	private void Update()
	{
	}

	private IEnumerator Wait()
	{
		while (wait > 0f)
		{
			wait -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		StartCoroutine("Drop");
	}

	private IEnumerator Drop()
	{
		while (base.transform.position.y > 0f)
		{
			base.transform.position -= Vector3.up * dropSpeed * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		bBlowing = true;
		if (fire.isActiveAndEnabled)
		{
			fire.blowMultiplier = 2f;
		}
		audioDevice.clip = blowing;
		audioDevice.loop = true;
		audioDevice.Play();
	}

	private void OnTriggerStay(Collider other)
	{
		if ((other.tag == "Player") & bBlowing)
		{
			if (fire.isActiveAndEnabled)
			{
				fire.blowMultiplier = 1f;
			}
			audioDevice.Stop();
			base.transform.position = initialLocation;
			audioDevice.PlayOneShot(pah);
		}
	}
}
