using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCheck : MonoBehaviour
{
    
    public GameObject panelWin;
    public SceneManagement sceneManagement;
    
    public int keysLeft = 4;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        keysLeft = GameObject.FindGameObjectsWithTag("Key").Length;
        Debug.Log(keysLeft);
        if (keysLeft == 0)
        {
            StartCoroutine("WinGame");
        }
    }

    IEnumerator WinGame()
    {
        Time.timeScale = 0;
        panelWin.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        panelWin.SetActive(false);
        sceneManagement.ReloadScene();
    }
   


}
