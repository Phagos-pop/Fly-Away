using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControllerInfinity : MonoBehaviour
{
    private int _score;
    private bool defeatFlag;
    private float timer;
    public GameObject firstHeard;
    public GameObject secondHeard;
    public GameObject thirdHeard;
    private Text timeLabel;
    private Text scoreLabel;
    [SerializeField]
    private GameObject gameCanvas;
    [SerializeField]
    private GameObject defeatCanvas;
    [SerializeField]
    GameObject pauseCanvas;




    void Start()
    {
        timer = 0f;
        pauseCanvas.SetActive(false);
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
        Messenger.AddListener(GameEvent.BIRD_FLEW_AWAY, TakeLifeAway);

        defeatFlag = false;
        for (int i = 0; i < gameCanvas.transform.childCount; i++)
        {
            if (gameCanvas.transform.GetChild(i).GetComponent<Text>() && scoreLabel == null)
            {
                scoreLabel = gameCanvas.transform.GetChild(i).GetComponent<Text>();
                continue;
            }
            if (gameCanvas.transform.GetChild(i).GetComponent<Text>() && timeLabel == null)
            {
                timeLabel = gameCanvas.transform.GetChild(i).GetComponent<Text>();
                continue;
            }
        }
        MaxSdk.ShowBanner("06df718d586aee81");
    }
    private void OnEnemyHit()
    {
        _score += 1;
        scoreLabel.text = "Score: " + _score.ToString();
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
        Messenger.RemoveListener(GameEvent.BIRD_FLEW_AWAY, TakeLifeAway);
    }


    private void Update()
    {
        if (!defeatFlag)
        {
            timer += Time.deltaTime;
            timeLabel.text = ($"Time {Mathf.Round(timer)}");
            if (!thirdHeard)
            {
                GameOver();
            }
        }
    }
    public void LoadNewLevel()
    {
        MaxSdk.HideBanner("06df718d586aee81");
        Messenger.Broadcast(GameEvent.WOOD_BUTTON_PUSH);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadThisScene()
    {
        MaxSdk.HideBanner("06df718d586aee81");
        if (MaxSdk.IsRewardedAdReady("c6348a13bd76eeff"))
        {
            MaxSdk.ShowRewardedAd("c6348a13bd76eeff");
        }
        Messenger.Broadcast(GameEvent.WOOD_BUTTON_PUSH);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TakeLifeAway()
    {
        if (firstHeard)
        {
            Destroy(firstHeard.gameObject);
        }
        if (!firstHeard && secondHeard)
        {
            Destroy(secondHeard.gameObject);
        }
        if (!firstHeard && !secondHeard && thirdHeard)
        {
            Destroy(thirdHeard.gameObject);
        }
    }

    public void GameOver()
    {
        Messenger.Broadcast(GameEvent.SHOW_INTERSTITIAL);
        defeatFlag = true;
        defeatCanvas.SetActive(true);
        Messenger.Broadcast(GameEvent.STOP_SPAWN);
        Messenger.Broadcast(GameEvent.DEFEAT);
    }
    public void PauseOn()
    {
        Messenger.Broadcast(GameEvent.WOOD_BUTTON_PUSH);
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseOff()
    {
        Messenger.Broadcast(GameEvent.WOOD_BUTTON_PUSH);
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadMenu()
    {
        MaxSdk.HideBanner("06df718d586aee81");
        Messenger.Broadcast(GameEvent.WOOD_BUTTON_PUSH);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}