using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;

	void Start ()
	{
		InvokeRepeating ("Fire", delay, fireRate);
	}

	void Fire ()
	{
        Vector3 pos = new Vector3(shotSpawn.position.x, shotSpawn.position.y, shotSpawn.position.z);

        pos.y = 0;

        //print(shotSpawn.position);
		Instantiate(shot, pos, shotSpawn.rotation);
		GetComponent<AudioSource>().Play();
	}
}
