using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityLevelController : MonoBehaviour
{
    private PinkBirdsSpawn pinkBirdsSpawn;
    private GreenBirdsSpawn greenBirdsSpawn;


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
            pinkBirdsSpawn.Bird.PowerOfWingsFlap += 0.1f;
            pinkBirdsSpawn.Bird.Speed += 0.1f;
            pinkBirdsSpawn.Bird.TimeBeforeWingsFlap -= 0.001f;
            greenBirdsSpawn.Bird.Speed += 0.1f;
            greenBirdsSpawn.Bird.directionY += 0.01f;
            yield return new WaitForSeconds(5f);
        }
    }
}
