using System.Collections;
using UnityEngine;

public class BearTrapScript : MonoBehaviour
{
	public AudioSource audioDevice;

	public GameObject barrier;

	public bool activated;

	public Sprite closed;

	public SpriteRenderer sprite;

	public TrapSpawnerScript trapSpawner;

	public float separateDistance;

	private void Start()
	{
		base.transform.position = new Vector3(Random.Range(0, trapSpawner.rangeX), 0f, Random.Range(0, trapSpawner.rangeZ));
		base.gameObject.layer = 9;
		while (Physics.CheckSphere(base.transform.position, separateDistance, 1024, QueryTriggerInteraction.Collide))
		{
			base.transform.position = new Vector3(Random.Range(0, trapSpawner.rangeX), 0f, Random.Range(0, trapSpawner.rangeZ));
		}
		base.gameObject.layer = 10;
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if ((other.name == "Player") & !activated)
		{
			sprite.sprite = closed;
			audioDevice.Play();
			activated = true;
			barrier.SetActive(true);
			StartCoroutine("TrapTimer");
		}
	}

	private IEnumerator TrapTimer()
	{
		float t = 5f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		barrier.SetActive(false);
	}
}
