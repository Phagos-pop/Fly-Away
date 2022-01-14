using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkBirdsSpawn : MonoBehaviour , ISpawn
{
    float yPosition = -4f;
    int xPositionMax = 6;
    int xPositionMin = -6;
    int lastPositionX;
    int newPositonX;

    public float TimeToSpawn;
    public float DecreaseTime;
    public float MinTimeToSpawn;
    public PinkBird Bird;
    public float ChangesOfBirdSpeed;
    public float ChangesOfBirdPowerOfWingsFlap;
    public float ChangesOfTimeBeforeWingsFlap;

    private void Start()
    {
        Bird.Speed = ChangesOfBirdSpeed + PinkBird.initialSpeed;
        Bird.TimeBeforeWingsFlap = ChangesOfTimeBeforeWingsFlap + PinkBird.initialTimeBeforeWingsFlap;
        Bird.PowerOfWingsFlap = ChangesOfBirdPowerOfWingsFlap + PinkBird.initialPowerOfWingsFlap;
        Messenger.AddListener(GameEvent.STOP_SPAWN, StopSpawn);
        StartCoroutine(SpawnBirds());       
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.STOP_SPAWN, StopSpawn);
    }

    IEnumerator SpawnBirds()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeToSpawn);

            do
            {
                newPositonX = Random.Range(xPositionMin, xPositionMax);
            } while (newPositonX == lastPositionX);
            lastPositionX = newPositonX;

            Instantiate(Bird, new Vector2(newPositonX, yPosition), Quaternion.identity);
            if (TimeToSpawn > MinTimeToSpawn)
            {
                TimeToSpawn -= DecreaseTime;
            }
        }        
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
    }
}
