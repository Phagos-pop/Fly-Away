using System.Collections;
using UnityEngine;

public class PinkBird : MonoBehaviour , IEnemy
{
    public const float initialTimeBeforeWingsFlap = 0.5f;
    public const float initialPowerOfWingsFlap = 240f;
    public const float initialSpeed = 0.5f;

    public float TimeBeforeWingsFlap;
    public float TimeToDeath;
    public float PowerOfWingsFlap;
    public float IncreasePower;
    public float Speed;
    public GameObject Feather;
    public GameObject BirdClouds;
    public float direction;
    public Sprite sprite1;
    public Sprite sprite2;

    private Rigidbody2D rb;
    private SpriteRenderer spriteenderer;
    private new AudioSource audio;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.WIN, OnWin);
        spriteenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        StartCoroutine(WingsFlap());
        direction = Random.Range(-1, 1);
        if (direction < 0)
        {
            spriteenderer.flipX = true;
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
        transform.Translate(new Vector2(direction, 0f) * Speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (direction > 0)
        {
            direction = -1;
            spriteenderer.flipX = true;
        }
        else if (direction < 0)
        {
            direction = 1;
            spriteenderer.flipX = false;
        }
        else
        {
            if (collision.collider.GetComponent<PinkBird>())
            {
                if (collision.collider.GetComponent<PinkBird>().direction > 0)
                {
                    direction = -1;
                    spriteenderer.flipX = true;
                }
                else
                {
                    direction = 1;
                    spriteenderer.flipX = false;
                }
            }
        }
    }

    private void Repeat()
    {
        StartCoroutine(WingsFlap());
    }

    IEnumerator WingsFlap()
    {
        yield return new WaitForSeconds(TimeBeforeWingsFlap);
        if (rb.isKinematic)
        {
            rb.isKinematic = false;
            
        }
        rb.AddForce(new Vector2(0f,PowerOfWingsFlap));
        audio.Play();
        StartCoroutine(ChangeSprite());
        PowerOfWingsFlap += IncreasePower;
        Repeat();
    }

    IEnumerator ChangeSprite()
    {
        spriteenderer.sprite = sprite2;
        yield return new WaitForSeconds(0.2f);
        spriteenderer.sprite = sprite1;
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
