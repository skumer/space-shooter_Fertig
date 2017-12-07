using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
    public double fireSlowdown;
    private double delay;
	private double save;
	public float powerUpDuration;
	public float powerUpScore;
	public static GameController GameController;
	 
	private double nextFire;

	void Start ()
	{
		save = fireSlowdown;
	}
	
	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate + delay;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
            delay += fireSlowdown;
        }
        if (delay > fireSlowdown && !(Input.GetButton("Fire1")))
        {
            delay -=fireSlowdown;
        }
		if ((GameController.score % powerUpScore)==0) 
		{
			PowerUp();
		}
    }

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;
		
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}

	void PowerUp()
	{
		while (Time.time < powerUpDuration) 
		{
			delay = 0;
			fireSlowdown = 0;
		}
		fireSlowdown = save;
	}
}
