using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public Transform player;

	public GameControllerScript gc;

	public Transform baldi;

	private float lookBehind;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetButton("Look Behind"))
		{
			lookBehind = 180f;
		}
		else
		{
			lookBehind = 0f;
		}
	}

	private void LateUpdate()
	{
		if (!gc.gameOver)
		{
			base.transform.position = player.position;
			base.transform.rotation = player.rotation * Quaternion.Euler(0f, lookBehind, 0f);
		}
		else
		{
			base.transform.position = baldi.transform.position + baldi.transform.forward * 2f + new Vector3(0f, 5f, 0f);
			base.transform.LookAt(new Vector3(baldi.position.x, baldi.position.y + 5f, baldi.position.z));
		}
	}
}
