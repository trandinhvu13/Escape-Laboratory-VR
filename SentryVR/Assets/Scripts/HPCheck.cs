using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPCheck : MonoBehaviour
{
    public AudioSource loseSound;
    public Player player;
    public GameObject panelLose;
    public int playerHealth;
    public SceneManagement sceneManagement;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = player.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = player.currentHealth;
        if (playerHealth < 1)
        {
            StartCoroutine("LoseGame");
        }
    }
    IEnumerator LoseGame()
    {
        Time.timeScale = 0;
        panelLose.SetActive(true);
        loseSound.Play();
        yield return new WaitForSecondsRealtime(5f);
        panelLose.SetActive(false);
        sceneManagement.ReloadScene();
    }
}
