using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DestroyByContactNiv1 : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    private GameControllerNiv1 gameController;

    Scene m_Scene;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameControllerNiv1>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }


    }

    void OnTriggerEnter(Collider other)
    {

        m_Scene = SceneManager.GetActiveScene();

        if(m_Scene.name == "niv1")
        {
            if (other.tag == "Boundary" || other.tag == "Enemy")
            {
                return;
            }

            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }

            if (other.tag == "Player")
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.GameOver();
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else
        {
            if (other.tag == "Boundary" || other.tag == "Enemy")
            {
                return;
            }

            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }

            if (other.tag == "Player")
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.GameOver();
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}