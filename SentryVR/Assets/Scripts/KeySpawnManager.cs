using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawnManager : MonoBehaviour
{
    [Header("Track")]
    public GameObject[] keys;

    [Header("Key Prefab")]
    public GameObject key1;
    public GameObject key2;
    public GameObject key3;
    public GameObject key4;


    [Header("Key 1")]
    public Transform pos11;
    public Transform pos12;
    public Transform pos13;
    public Transform pos14;
    public Transform key1Pos;
    private int ID1;

    [Header("Key 2")]
    public Transform pos21;
    public Transform pos22;
    public Transform pos23;
    public Transform pos24;
    public Transform key2Pos;
    private int ID2;

    [Header("Key 3")]
    public Transform pos31;
    public Transform pos32;
    public Transform pos33;
    public Transform pos34;
    public Transform key3Pos;
    private int ID3;

    [Header("Key 4")]
    public Transform pos41;
    public Transform pos42;
    public Transform pos43;
    public Transform pos44;
    public Transform key4Pos;
    private int ID4;




    // Start is called before the first frame update
    void Start()
    {
        SpawnKey1();
        SpawnKey2();
        SpawnKey3();
        SpawnKey4();
    }

    // Update is called once per frame
    void Update()
    {
        keys = GameObject.FindGameObjectsWithTag("Key");
        if (keys.Length == 0)
        {
            Debug.Log("Win");
        }
        else return;
    }

    void SpawnKey1()
    {
        ID1 = Random.Range(0, 3);
        switch (ID1)
        {
            case 0:
                key1Pos = pos11;
                break;
            case 1:
                key1Pos = pos12;
                break;
            case 2:
                key1Pos = pos13;
                break;
            case 3:
                key1Pos = pos14;
                break;
        }

        Instantiate(key1, key1Pos.transform.position, Quaternion.identity);
    }

    void SpawnKey2()
    {
        ID2 = Random.Range(0, 3);
        switch (ID2)
        {
            case 0:
                key2Pos = pos21;
                break;
            case 1:
                key2Pos = pos22;
                break;
            case 2:
                key2Pos = pos23;
                break;
            case 3:
                key2Pos = pos24;
                break;
        }

        Instantiate(key2, key2Pos.transform.position, Quaternion.identity);
    }

    void SpawnKey3()
    {
        ID3 = Random.Range(0, 3);
        switch (ID3)
        {
            case 0:
                key3Pos = pos31;
                break;
            case 1:
                key3Pos = pos32;
                break;
            case 2:
                key3Pos = pos33;
                break;
            case 3:
                key3Pos = pos34;
                break;
        }

        Instantiate(key3, key3Pos.transform.position, Quaternion.identity);
    }

    void SpawnKey4()
    {
        ID4 = Random.Range(0, 3);
        switch (ID4)
        {
            case 0:
                key4Pos = pos41;
                break;
            case 1:
                key4Pos = pos42;
                break;
            case 2:
                key4Pos = pos43;
                break;
            case 3:
                key4Pos = pos44;
                break;
        }

        Instantiate(key4, key4Pos.transform.position, Quaternion.identity);
    }
}
