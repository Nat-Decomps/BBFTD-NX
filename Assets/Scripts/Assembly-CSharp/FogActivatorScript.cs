using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FogActivatorScript : MonoBehaviour
{
	public float fogDivisor;

	public float outTime;

	private bool bFog;

	public GameObject crafters;

	public NavMeshAgent baldiAgent;

	public CTRL_CampingScript cs;

	private void Start()
	{
		RenderSettings.skybox.SetFloat("_Exposure", 0.38f);
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "outBounds")
		{
			StopCoroutine("FogOff");
			StartCoroutine("FogOn");
			StartCoroutine("OutTimer");
		}
		else if (other.tag == "inBounds" || other.tag == "Crafters")
		{
			StopCoroutine("FogOn");
			StopCoroutine("OutTimer");
			StartCoroutine("FogOff");
		}
	}

	private IEnumerator FogOn()
	{
		float i = RenderSettings.skybox.GetFloat("_Exposure");
		float j = i;
		RenderSettings.fog = true;
		while (i > 0f)
		{
			i -= Time.deltaTime;
			RenderSettings.skybox.SetFloat("_Exposure", i);
			RenderSettings.fogDensity = (j - i) / fogDivisor;
			yield return new WaitForEndOfFrame();
		}
		bFog = true;
	}

	private IEnumerator FogOff()
	{
		float i = RenderSettings.skybox.GetFloat("_Exposure");
		float j = RenderSettings.fogDensity;
		while (i < 0.38f)
		{
			i += Time.deltaTime;
			RenderSettings.skybox.SetFloat("_Exposure", i);
			RenderSettings.fogDensity = (j - i) / fogDivisor;
			yield return new WaitForEndOfFrame();
		}
		RenderSettings.skybox.SetFloat("_Exposure", 0.38f);
		RenderSettings.fog = false;
		bFog = false;
	}

	private IEnumerator OutTimer()
	{
		float i = outTime;
		while (i > 0f)
		{
			i -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		GameObject current = Object.Instantiate(crafters);
		current.GetComponent<ArtsAndCraftersScript>().player = base.transform;
		current.GetComponent<ArtsAndCraftersScript>().baldi = baldiAgent;
		current.GetComponent<ArtsAndCraftersScript>().cs = cs;
	}
}
