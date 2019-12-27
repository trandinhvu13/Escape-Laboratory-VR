using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject Door;
    public void StartGame()
    {
        Door.transform.position += transform.forward * Time.deltaTime;
        Debug.Log("U pressed me");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
