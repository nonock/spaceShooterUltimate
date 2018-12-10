using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
	 
	private float nextFire;

    Scene m_Scene;


    void Update ()
	{
        m_Scene = SceneManager.GetActiveScene();
        if(m_Scene.name != "niv1")
        {
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                GetComponent<AudioSource>().Play();
            }
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
    }
}
