using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private GameObject poop;
    [SerializeField]
    private Text scoreTxt;
    [SerializeField]
    private Text bestScore;
    [SerializeField]
    private GameObject panel;

    private int score = 0;      //score
    private float poopDelay = 0.25f; //delays the poop to come out in terms of seconds

    private bool stopTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {

    }

    public void GameStart()
    {
        score = 0;
        scoreTxt.text = "Score: " + score;

        stopTrigger = false;
        StartCoroutine(CreatepoopRoutine());
        panel.SetActive(false);
    }

    public void GameOver()
    {
        stopTrigger = true;
        StopCoroutine(CreatepoopRoutine());

        Debug.Log(score);
        Debug.Log(bestScore.text);
        Debug.Log(PlayerPrefs.GetInt("BestScore", 0));

        if(score > PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", score);
        }

        bestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();

        panel.SetActive(true);
    }

    public void Score()
    {
        score++;
        scoreTxt.text = "Score: " + score;
    }

    IEnumerator CreatepoopRoutine()
    {
        while (!stopTrigger)
        {
            CreatePoop();
            yield return new WaitForSeconds(poopDelay);
        }

    }

    // Update is called once per frame
    private void CreatePoop()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3( UnityEngine.Random.Range(0.0f,1.0f), 1.1f, 0) );
        pos.z = 0.0f;
        Instantiate(poop, pos, Quaternion.identity); //Quaternion.identity => an object does not rotate
    }

    public bool getStopTrigger()
    {
        return stopTrigger;
    }

    public void setStopTrigger(bool trigger)
    {
        stopTrigger = trigger;
    }
}
