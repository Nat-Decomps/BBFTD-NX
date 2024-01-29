using UnityEngine;

public class WoodSpawnerScript : MonoBehaviour
{
	public int rangeX;

	public int rangeZ;

	public int woodCount;

	public GameObject firewood;

	private void Start()
	{
		for (int i = 0; i < woodCount; i++)
		{
			GameObject gameObject = Object.Instantiate(firewood);
			gameObject.GetComponent<WoodPickupScript>().woodSpawner = base.gameObject.GetComponent<WoodSpawnerScript>();
		}
	}
}
