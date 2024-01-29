using UnityEngine;

public class TreesScript : MonoBehaviour
{
	public int rangeX;

	public int rangeZ;

	public int minX;

	public int minZ;

	public int treeCount;

	public GameObject tree;

	private void Start()
	{
		for (int i = 0; i < treeCount; i++)
		{
			GameObject gameObject = Object.Instantiate(tree);
			gameObject.GetComponent<TreeScript>().trees = base.gameObject.GetComponent<TreesScript>();
		}
	}
}
