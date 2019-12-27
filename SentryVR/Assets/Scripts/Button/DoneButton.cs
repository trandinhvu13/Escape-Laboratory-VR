using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneButton : MonoBehaviour
{

    public GameObject panelHelp;
    public GameObject panelMain;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            panelMain.SetActive(true);
            panelHelp.SetActive(false);
        }
        else
        {
            return;
        }
    }
}
