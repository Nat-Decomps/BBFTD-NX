using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
	public GameControllerScript gc;

	public bool gameOver;

	public bool jumpRope;

	public bool sweeping;

	public bool hugging;

	public bool trapped;

	public float sweepingFailsave;

	private Quaternion playerRotation;

	public Vector3 frozenPosition;

	public float mouseSensitivity;

	public float walkSpeed;

	public float runSpeed;

	public float slowSpeed;

	public float maxStamina;

	public float staminaRate;

	public float guilt;

	public float initGuilt;

	private float moveX;

	private float moveZ;

	private float playerSpeed;

	public float stamina;

	public Rigidbody rb;

	public Slider staminaBar;

	public float db;

	public string guiltType;

	public GameObject jumpRopeScreen;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		stamina = maxStamina;
		playerRotation = base.transform.rotation;
	}

	private void Update()
	{
		MouseMove();
		StaminaCheck();
		GuiltCheck();
		if (rb.velocity.magnitude > 0f)
		{
			gc.LockMouse();
		}
		if (jumpRope & ((base.transform.position - frozenPosition).magnitude >= 1f))
		{
			DeactivateJumpRope();
		}
		if (sweepingFailsave > 0f)
		{
			sweepingFailsave -= Time.deltaTime;
			return;
		}
		sweeping = false;
		hugging = false;
	}

	private void FixedUpdate()
	{
		PlayerMove();
	}

	private void MouseMove()
	{
		playerRotation.eulerAngles += new Vector3(0f, Input.GetAxis("Mouse X") * mouseSensitivity, 0f);
	}

	private void PlayerMove()
	{
		base.transform.rotation = playerRotation;
		Vector3 vector = new Vector3(0f, 0f, 0f);
		Vector3 vector2 = new Vector3(0f, 0f, 0f);
		db = Input.GetAxisRaw("Forward");
		if (stamina > 0f)
		{
			if (Input.GetAxisRaw("Run") > 0f)
			{
				playerSpeed = runSpeed;
				if ((rb.velocity.magnitude > 0.1f) & !hugging & !sweeping)
				{
					ResetGuilt("running", 0.1f);
				}
			}
			else
			{
				playerSpeed = walkSpeed;
			}
		}
		else
		{
			playerSpeed = walkSpeed;
		}
		vector = base.transform.forward * Input.GetAxis("Forward");
		vector2 = base.transform.right * Input.GetAxis("Strafe");
		if (trapped)
		{
			playerSpeed = 0f;
		}
		rb.velocity = (vector + vector2).normalized * playerSpeed;
		trapped = false;
	}

	private void StaminaCheck()
	{
		if (rb.velocity.magnitude > 0.1f)
		{
			if ((Input.GetAxisRaw("Run") > 0f) & (stamina > 0f))
			{
				stamina -= staminaRate * Time.deltaTime;
			}
			if ((stamina < 0f) & (stamina > -5f))
			{
				stamina = -5f;
			}
		}
		else if (stamina < maxStamina)
		{
			stamina += staminaRate * Time.deltaTime;
		}
		staminaBar.value = stamina / maxStamina * 100f;
	}

	private void OnTriggerEnter(Collider other)
	{
		if ((other.transform.name == "Baldi") & !gc.debugMode)
		{
			gameOver = true;
		}
	}

	public void ResetGuilt(string type, float amount)
	{
		if (amount >= guilt)
		{
			guilt = amount;
			guiltType = type;
		}
	}

	private void GuiltCheck()
	{
		if (guilt > 0f)
		{
			guilt -= Time.deltaTime;
		}
	}

	public void ActivateJumpRope()
	{
		jumpRopeScreen.SetActive(true);
		jumpRope = true;
		frozenPosition = base.transform.position;
	}

	public void DeactivateJumpRope()
	{
		jumpRopeScreen.SetActive(false);
		jumpRope = false;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.transform.name == "Baldi")
		{
			gc.gameOver = true;
		}
		else if (other.transform.tag == "Trap")
		{
			trapped = true;
		}
	}
}
