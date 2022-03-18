using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake(){
        Vector3 spawnSpot = GameObject.Find("Warp").transform.position;
        Debug.Log(GameObject.Find("Warp").transform.position);

        GameObject.Find("Player").transform.position = new Vector3(spawnSpot.x, spawnSpot.y, -100);
    }
}
