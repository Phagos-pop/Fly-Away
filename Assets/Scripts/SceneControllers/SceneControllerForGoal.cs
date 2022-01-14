using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControllerForGoal : MonoBehaviour
{
    private int _score;
    private bool winFlag;
    public int goalToWin;
    public GameObject firstHeard;
    public GameObject secondHeard;
    public GameObject thirdHeard;
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
        _score = goalToWin;
        winFlag = false;
        for (int i = 0; i < gameCanvas.transform.childCount; i++)
        {
            if (gameCanvas.transform.GetChild(i).GetComponent<Text>())
            {
                scoreLabel = gameCanvas.transform.GetChild(i).GetComponent<Text>();
                scoreLabel.text = "Goal: " + _score.ToString();
            }
        }
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
        Messenger.RemoveListener(GameEvent.BIRD_FLEW_AWAY, TakeLifeAway);
    }

    private void OnEnemyHit()
    {
        _score -= 1;
        scoreLabel.text = "Goal: " + _score.ToString();
    }

    private void Update()
    {
        if (_score == 0 && !winFlag)
        {
            winCanvas.SetActive(true);
            Messenger.Broadcast(GameEvent.STOP_SPAWN);
            Messenger.Broadcast(GameEvent.WIN);
            winFlag = true;
        }
        if (!thirdHeard)
        {
            GameOver();
        }
    }
    public void LoadNewLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadThisScene()
    {
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
        defeatCanvas.SetActive(true);
    }
    public void PauseOn()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseOff()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
