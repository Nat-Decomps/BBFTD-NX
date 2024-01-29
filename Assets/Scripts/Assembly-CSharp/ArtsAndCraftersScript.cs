using UnityEngine;
using UnityEngine.AI;

public class ArtsAndCraftersScript : MonoBehaviour
{
	public Transform player;

	public CTRL_CampingScript cs;

	public NavMeshAgent baldi;

	public Vector3 spawn;

	public Vector3 playerTeleport;

	public Vector3 baldiTeleport;

	public AudioSource audioDevice;

	public NavMeshAgent agent;

	public AudioClip start;

	public AudioClip loop;

	private void Start()
	{
		agent.Warp(spawn);
		audioDevice.clip = start;
		audioDevice.Play();
	}

	private void Update()
	{
		agent.SetDestination(player.position);
		if (!audioDevice.isPlaying)
		{
			audioDevice.clip = loop;
			audioDevice.Play();
			audioDevice.loop = true;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			player.position = playerTeleport;
			if (baldi.isActiveAndEnabled)
			{
				baldi.Warp(baldiTeleport);
			}
			cs.AddSticks(cs.sticks * -1);
			Object.Destroy(base.gameObject);
		}
	}
}
