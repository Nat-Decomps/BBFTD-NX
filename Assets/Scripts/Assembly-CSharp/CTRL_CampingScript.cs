using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CTRL_CampingScript : MonoBehaviour
{
	public int sticks;

	public int score;

	public int time;

	public float speedScale;

	public GameObject baldi;

	public GameObject reward1;

	public GameObject reward2;

	public GameObject reward3;

	public GameObject reward4;

	public Text stickText;

	public Text scoreText;

	public Text bigScoreText;

	public Text timeText;

	public Text finalText;

	public Transform player;

	public PlayerScript ps;

	private float normalWalkSpeed;

	private float normalRunSpeed;

	public FireScript fire;

	public AudioSource audioDevice;

	public AudioClip bigScoreSound;

	public AudioClip winMusic;

	private void Start()
	{
		Debug.Log("Camping Controller Start");
		StartCoroutine("Timer");
		UpdateStickCount();
		normalWalkSpeed = ps.walkSpeed;
		normalRunSpeed = ps.runSpeed;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("Click");
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo))
			{
				if ((hitInfo.transform.tag == "Firewood") & (Vector3.Distance(player.position, hitInfo.transform.position) < 10f))
				{
					Debug.Log("My object is clicked by mouse");
					Object.Destroy(hitInfo.transform.gameObject);
					AddSticks(1);
				}
				else if ((hitInfo.transform.name == "Fire") & (Vector3.Distance(player.position, hitInfo.transform.position) < 10f))
				{
					fire.BuildFire(sticks);
					AddSticks(sticks * -1);
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			AddSticks(sticks * -1);
		}
	}

	private IEnumerator Timer()
	{
		float t = 1f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		time--;
		if (time < 0)
		{
			Time.timeScale = 0f;
			AddScore(Mathf.RoundToInt(fire.fireLevel * 10000f));
			finalText.gameObject.SetActive(true);
			finalText.text = "Wow-time, you won!\nFire Bonus:\n" + Mathf.RoundToInt(fire.fireLevel * 10000f) + "\n\nFinal Score:\n" + score + "\n\nWith that score, you get these great items! Congrat!";
			if (score >= 50000)
			{
				reward1.SetActive(true);
				reward2.SetActive(true);
				reward3.SetActive(true);
				reward4.SetActive(true);
			}
			else if (score >= 25000)
			{
				reward1.SetActive(true);
				reward2.SetActive(true);
				reward3.SetActive(true);
			}
			else
			{
				reward1.SetActive(true);
			}
			StartCoroutine("WinTime");
			audioDevice.PlayOneShot(winMusic);
		}
		else
		{
			timeText.text = "Time: " + time;
			StartCoroutine("Timer");
		}
	}

	public void AddSticks(int amount)
	{
		sticks += amount;
		UpdateStickCount();
		if (sticks > 3)
		{
			ps.walkSpeed = normalWalkSpeed - (float)(8 * (sticks - 3)) / ((float)(sticks - 3) + 2f / speedScale);
			ps.slowSpeed = ps.walkSpeed;
			ps.runSpeed = normalRunSpeed - (float)(8 * (sticks - 3)) / ((float)(sticks - 3) + 2f / speedScale);
		}
		else
		{
			ps.walkSpeed = normalWalkSpeed;
			ps.slowSpeed = normalWalkSpeed;
			ps.runSpeed = normalRunSpeed;
		}
	}

	private void UpdateStickCount()
	{
		stickText.text = sticks + " Sticks";
	}

	public void AddScore(int points)
	{
		score += points;
		scoreText.text = score + " Points";
	}

	public void BigScore(int value)
	{
		bigScoreText.text = "EXTRA SCORE \n" + value;
		bigScoreText.gameObject.SetActive(true);
		audioDevice.PlayOneShot(bigScoreSound);
		StartCoroutine("BigScoreTime");
	}

	private IEnumerator BigScoreTime()
	{
		float t = 1f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		bigScoreText.gameObject.SetActive(false);
	}

	private IEnumerator WinTime()
	{
		float t = 10f;
		while (t > 0f)
		{
			t -= Time.unscaledDeltaTime;
			yield return new WaitForEndOfFrame();
		}
		SceneManager.LoadScene("End");
	}

	public void SpawnBaldi()
	{
		baldi.SetActive(true);
	}
}
