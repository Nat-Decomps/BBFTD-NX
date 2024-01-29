using UnityEngine;

public class TreeScript : MonoBehaviour
{
	public TreesScript trees;

	public float separateDistance;

	private void Start()
	{
		base.transform.position = new Vector3(Random.Range(trees.minX, trees.rangeX), 0f, Random.Range(trees.minZ, trees.rangeZ));
		base.gameObject.layer = 9;
		while (Physics.CheckSphere(base.transform.position, separateDistance, 1024, QueryTriggerInteraction.Collide))
		{
			base.transform.position = new Vector3(Random.Range(trees.minX, trees.rangeX), 0f, Random.Range(trees.minZ, trees.rangeZ));
		}
		base.gameObject.layer = 10;
	}

	private void Update()
	{
	}
}
