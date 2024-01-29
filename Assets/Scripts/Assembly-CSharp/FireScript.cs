using System.Collections;
using UnityEngine;

public class FireScript : MonoBehaviour
{
	public float initialFireLevel;

	public float fireLevel;

	public float fireRate;

	public float fireOverflow;

	public float stickPower;

	public float multiplier;

	public float powerMultiplier;

	public float blowMultiplier;

	public bool fireOut;

	public AudioSource ambience;

	public AudioSource fireSound;

	public Light fireLight;

	public float lightIntensityBase;

	public float lightAngleBase;

	public GameObject fireSprite;

	public CTRL_CampingScript campingScript;

	private void Start()
	{
		Debug.Log("Fire Script Start");
		StartCoroutine("Die");
		StartCoroutine("GiveScore");
		fireLevel = initialFireLevel;
	}

	private void Update()
	{
	}

	private IEnumerator Die()
	{
		float i = fireLevel;
		while (fireLevel > 0f)
		{
			fireLevel -= fireRate * blowMultiplier * Time.deltaTime;
			ambience.pitch = fireLevel / 2f + 0.5f;
			fireLight.intensity = fireLevel * lightIntensityBase + 5f;
			fireLight.spotAngle = fireLevel * lightAngleBase;
			yield return new WaitForEndOfFrame();
		}
		FireOut();
	}

	private IEnumerator GiveScore()
	{
		float t = 1f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		campingScript.AddScore(Mathf.RoundToInt(fireLevel * 100f));
		if (!fireOut)
		{
			StartCoroutine("GiveScore");
		}
	}

	private void FireOut()
	{
		fireSound.Stop();
		fireSprite.SetActive(false);
		fireOut = true;
		campingScript.SpawnBaldi();
	}

	public void BuildFire(int sticks)
	{
		Debug.Log("Building fire");
		fireLevel += (float)sticks * stickPower;
		if (fireLevel > initialFireLevel)
		{
			campingScript.AddScore(Mathf.RoundToInt(Mathf.Pow((fireLevel - initialFireLevel) * multiplier, powerMultiplier)));
			campingScript.BigScore(Mathf.RoundToInt(Mathf.Pow((fireLevel - initialFireLevel) * multiplier, powerMultiplier)));
			fireLevel = initialFireLevel;
		}
	}
}
