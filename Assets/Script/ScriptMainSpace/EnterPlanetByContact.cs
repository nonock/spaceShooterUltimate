using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPlanetByContact : MonoBehaviour {
    
    private GameControllerBoard gameController;

    // Use this for initialization
    void Start () {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameControllerBoard");

        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameControllerBoard>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "niv1")
        {
            print("niv1");
        }
        else if (other.tag == "niv2")
        {
            print("niv2");
        }
        else if (other.tag == "niv3")
        {
            print("niv3");
        }
        else if (other.tag == "niv4")
        {
            print("niv4");
        }
    }
}
