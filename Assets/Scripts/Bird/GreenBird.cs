using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GreenBird : MonoBehaviour, IEnemy
{
    public const float initialSpeed = 1.5f;
    public const float initialdirectionY = 0.3f;
    public float directionY;
    public float TimeToDeath;
    public float Speed;
    public GameObject Feather;
    public GameObject BirdClouds;
    public float directionX;
    public Sprite[] spriteSheet;

    private SpriteRenderer spriterenderer;
    private new AudioSource audio;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.WIN, OnWin);
        spriterenderer = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();

        StartCoroutine(ChangeSprite());
        do
        {
            directionX = Random.Range(-1, 1);
        } while (directionX == 0);
        if (directionX < 0)
        {
            spriterenderer.flipX = true;
        }
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.WIN, OnWin);
    }

    private void Update()
    {
        if (this.transform.position.y > 7)
        {
            Destroy(this.gameObject);
            Messenger.Broadcast(GameEvent.BIRD_FLEW_AWAY);
        }
        transform.Translate(new Vector2(directionX, directionY) * Speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (directionX > 0)
        {
            directionX = -1;
            spriterenderer.flipX = true;
        }
        else if (directionX < 0)
        {
            directionX = 1;
            spriterenderer.flipX = false;
        }
    }

    IEnumerator ChangeSprite()
    {
        do
        {
            for (int i = 0; i < spriteSheet.Length; i++)
            {
                spriterenderer.sprite = spriteSheet[i];
                yield return new WaitForSeconds(0.04f);
            }
        } while (true);
    }

    public void OnHit()
    {
        StopAllCoroutines();
        GameObject FeatherObj = Instantiate(Feather, transform.position, transform.rotation);
        GameObject BirdCloudsObj = Instantiate(BirdClouds, transform.position, transform.rotation);
        Destroy(FeatherObj, TimeToDeath);
        Destroy(BirdCloudsObj, TimeToDeath);
        Destroy(this.gameObject);
        Messenger.Broadcast(GameEvent.ENEMY_HIT);
    }


    public void OnWin()
    {
        Destroy(this.gameObject);
    }
}
