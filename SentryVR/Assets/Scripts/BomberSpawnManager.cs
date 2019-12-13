using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberSpawnManager : MonoBehaviour
{
    public GameObject bomberPrefab;
    public Transform bomberSpawnPlace;
    public GameObject[] bomber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bomber = GameObject.FindGameObjectsWithTag("Bomber");
        if(bomber.Length < 4)
        {
            Instantiate(bomberPrefab, bomberSpawnPlace.transform.position, Quaternion.identity);
        }
    }
}
