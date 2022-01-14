using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawn : MonoBehaviour
{
    public float XPosition = -12f;
    int YPositionMax = 3;
    int YPositionMin = -1;
    public float TimeToSpawn = 20f;
    public GameObject[] Clouds;

    int lastInstance;
    int newInstance;
    int lastPositionY;
    int newPositonY;

    private void Start() 
    {
        StartCoroutine(SpawnClouds());
        lastInstance = 0;
        lastPositionY = 0;
    }

    private void Repeat()
    {
        StartCoroutine(SpawnClouds());
    }

    IEnumerator SpawnClouds()
    {
        do
        {
            newInstance = Random.Range(0, Clouds.Length);
        } while (newInstance == lastInstance);
        do
        {
            newPositonY = Random.Range(YPositionMin, YPositionMax);
        } while (newPositonY == lastPositionY);
        lastInstance = newInstance;
        lastPositionY = newPositonY;

        Instantiate(Clouds[newInstance], new Vector3(XPosition, newPositonY , 10f), Quaternion.identity);
        yield return new WaitForSeconds(TimeToSpawn);
        Repeat();
    }
}