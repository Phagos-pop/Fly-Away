using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControllerForTimer : MonoBehaviour
{
    private int _score;
    public float startTimer;
    private bool winFlag;
    private bool defeatFlag;
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
    [SerializeField]
    private GameObject winCanvas;




    void Start()
    {
        pauseCanvas.SetActive(false);
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
        Messenger.AddListener(GameEvent.BIRD_FLEW_AWAY, TakeLifeAway);
        winFlag = false;
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
        if (startTimer > 0 && !defeatFlag)
        {
            timeLabel.text = ($"Time {Mathf.Round(startTimer)}");
            startTimer -= Time.deltaTime;
        }
        
        if (startTimer < 0 && !winFlag)
        {
            winCanvas.SetActive(true);
            Messenger.Broadcast(GameEvent.STOP_SPAWN);
            Messenger.Broadcast(GameEvent.WIN);
            winFlag = true;
        }
        if (!thirdHeard && !defeatFlag)
        {
            GameOver();
        }
    }
    public void LoadNewLevel()
    {
        Messenger.Broadcast(GameEvent.WOOD_BUTTON_PUSH);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadThisScene()
    {
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
        Messenger.Broadcast(GameEvent.WOOD_BUTTON_PUSH);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
