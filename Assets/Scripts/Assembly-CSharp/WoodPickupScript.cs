using UnityEngine;

public class WoodPickupScript : MonoBehaviour
{
	public WoodSpawnerScript woodSpawner;

	public float separateDistance;

	private void Start()
	{
		base.transform.position = new Vector3(Random.Range(0, woodSpawner.rangeX), 4f, Random.Range(0, woodSpawner.rangeZ));
		base.gameObject.layer = 9;
		while (Physics.CheckSphere(base.transform.position, separateDistance, 1024, QueryTriggerInteraction.Collide))
		{
			base.transform.position = new Vector3(Random.Range(0, woodSpawner.rangeX), 4f, Random.Range(0, woodSpawner.rangeZ));
		}
		base.gameObject.layer = 10;
	}

	private void Update()
	{
	}
}
