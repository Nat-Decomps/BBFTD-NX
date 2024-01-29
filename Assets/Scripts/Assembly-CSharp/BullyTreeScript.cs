using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BullyTreeScript : MonoBehaviour
{
	public CTRL_CampingScript cs;

	public Transform player;

	public Transform tree;

	private Vector3 lockdown;

	public GameObject realBully;

	public GameObject behindBully;

	public Renderer bullyRenderer;

	public NavMeshAgent agent;

	public bool lookingAt;

	public bool revealed;

	public int rangeX;

	public int rangeZ;

	public int minX;

	public int minZ;

	public float separateDistance;

	public float reactTime;

	public float treeFlyHeight;

	public float treeFlySpeed;

	private float agentSpeed;

	public AudioSource audioDevice;

	public AudioClip giveSticks;

	private void Start()
	{
		agentSpeed = agent.speed;
		base.transform.position = new Vector3(Random.Range(minX, rangeX), 0f, Random.Range(minZ, rangeZ));
		base.gameObject.layer = 9;
		while (Physics.CheckSphere(base.transform.position, separateDistance, 1024, QueryTriggerInteraction.Collide))
		{
			base.transform.position = new Vector3(Random.Range(minX, rangeX), 0f, Random.Range(minZ, rangeZ));
		}
		base.gameObject.layer = 10;
	}

	private void Update()
	{
		if (agent.enabled)
		{
			agent.SetDestination(player.position);
		}
	}

	private void FixedUpdate()
	{
		Vector3 direction = player.position - base.transform.position;
		RaycastHit hitInfo;
		if (Physics.Raycast(base.transform.position + Vector3.up * 2f, direction, out hitInfo, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & (hitInfo.transform.tag == "Player") & bullyRenderer.isVisible & !revealed)
		{
			lookingAt = true;
			StartCoroutine("Sighted");
		}
		else if (!revealed)
		{
			lookingAt = false;
			StartMoving();
		}
	}

	private void StartMoving()
	{
		agent.speed = agentSpeed;
	}

	private void StopMoving()
	{
		agent.speed = 0f;
	}

	private void OnTriggerEnter(Collider other)
	{
		if ((other.tag == "Player") & !revealed & (cs.sticks > 0))
		{
			lockdown = other.transform.position;
			revealed = true;
			StopAllCoroutines();
			StartCoroutine("Reveal");
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if ((other.tag == "Player") & (cs.sticks > 0))
		{
			other.transform.LookAt(new Vector3(base.transform.position.x, other.transform.position.y, base.transform.position.z));
			other.transform.position = lockdown;
		}
	}

	private IEnumerator Reveal()
	{
		StopMoving();
		while (tree.position.y < treeFlyHeight)
		{
			tree.position += Vector3.up * treeFlySpeed * Time.deltaTime;
			behindBully.transform.position -= Vector3.up * treeFlySpeed * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		while (behindBully.transform.localScale.x < 0.6f)
		{
			behindBully.transform.localScale += Vector3.right * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		cs.AddSticks(cs.sticks * -1);
		behindBully.SetActive(false);
		realBully.SetActive(true);
		audioDevice.PlayOneShot(giveSticks);
		agent.enabled = false;
		base.transform.LookAt(new Vector3(player.position.x, base.transform.position.y, player.position.z));
		float i = 0f;
		while (i < 100f)
		{
			base.transform.position += base.transform.forward * 25f * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

	private IEnumerator Sighted()
	{
		float i = reactTime;
		while (i > 0f)
		{
			i -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		StopMoving();
	}
}
