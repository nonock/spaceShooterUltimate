using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HpBoss : MonoBehaviour
{
    public GameObject explosion;
    private GameController gameController;
    Scene m_Scene;

    public int vie;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        vie--;

        m_Scene = SceneManager.GetActiveScene();
        if(m_Scene.name !="niv5")
        {
            if (vie == 0)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(other.gameObject);
                Destroy(gameObject);
            }

            Destroy(other.gameObject);
        }
        else
        {
            if (vie == 0)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(other.gameObject);
                Destroy(gameObject);
                gameController.Victory();
            }

            Destroy(other.gameObject);
        }
    }
}