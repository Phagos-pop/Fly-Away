using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityLevelController : MonoBehaviour
{
    private PinkBirdsSpawn pinkBirdsSpawn;
    private GreenBirdsSpawn greenBirdsSpawn;

    public float greenBirdIncreaseSpeed;


    void Start()
    {
        pinkBirdsSpawn = GetComponent<PinkBirdsSpawn>();
        greenBirdsSpawn = GetComponent<GreenBirdsSpawn>();
        StartCoroutine(IncreaseIndicators());
    }

    IEnumerator IncreaseIndicators()
    {
        while (true)
        {
            Debug.Log("increase");
            pinkBirdsSpawn.Bird.PowerOfWingsFlap += 2;
            yield return new WaitForSeconds(2f);
        }
    }
}
