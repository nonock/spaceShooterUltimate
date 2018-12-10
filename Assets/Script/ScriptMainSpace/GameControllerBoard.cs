using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameControllerBoard : MonoBehaviour
{

    public Text selectText;

    void Start()
    {
        selectText.text = "Choose a Planet";
        print("Test");
    }

    void Update()
    {
    }
}