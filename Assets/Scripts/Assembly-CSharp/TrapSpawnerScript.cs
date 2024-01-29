using UnityEngine;

public class TrapSpawnerScript : MonoBehaviour
{
	public int rangeX;

	public int rangeZ;

	public int woodCount;

	public GameObject trap;

	private void Start()
	{
		for (int i = 0; i < woodCount; i++)
		{
			GameObject gameObject = Object.Instantiate(trap);
			gameObject.GetComponent<BearTrapScript>().trapSpawner = base.gameObject.GetComponent<TrapSpawnerScript>();
		}
	}
}
