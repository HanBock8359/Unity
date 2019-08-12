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
    [SerializeField]
    private CharacterUI charHp;
    [SerializeField]
    private Player player;

    private int score;   //score
    private float reduceDelay; //indicates the amount of score required to reduce the delay of poop generation
    private float poopDelay; //delays the poop to come out in terms of seconds

    private bool stopTrigger;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        reduceDelay = 0.005f;
        poopDelay = 0.25f;
        stopTrigger = false;
    }

    void Update()
    {

    }

    public void GameStart()
    {
        poopDelay = 0.25f;
        charHp.fillAllHp();

        score = 0;
        scoreTxt.text = "Score: " + score;

        stopTrigger = false;
        StartCoroutine(CreatepoopRoutine());
        panel.SetActive(false);
    }

    public void GameOver()
    {
        DestroyPoop();

        stopTrigger = true;
        StopCoroutine(CreatepoopRoutine());

        //Debug.Log(score);
        //Debug.Log(bestScore.text);
        //Debug.Log(PlayerPrefs.GetInt("BestScore", 0));

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

        if(score % 10 == 0 && poopDelay >= 0.05)
        {
            poopDelay -= reduceDelay;
        }
    }

    IEnumerator CreatepoopRoutine()
    {
        while (!stopTrigger)
        {
            CreatePoop();
            yield return new WaitForSeconds(poopDelay);
        }

        DestroyPoop();
    }

    // Update is called once per frame
    private void CreatePoop()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3( UnityEngine.Random.Range(0.0f,1.0f), 1.1f, 0) );
        pos.z = 0.0f;
        Instantiate(poop, pos, Quaternion.identity); //Quaternion.identity => an object does not rotate
    }

    private void DestroyPoop()
    {
        //once the game is over,
        //destroys the all poop existing in the screen
        GameObject[] poopClones = GameObject.FindGameObjectsWithTag("poopClone"); //returns an array of GameObjects with tag "poop"

        foreach (GameObject obj in poopClones)
        {
            obj.GetComponent<Poop>().destroyImmediately();
            //Destroy(obj);
        }
    }

    public bool getStopTrigger()
    {
        return stopTrigger;
    }

    public void setStopTrigger(bool trigger)
    {
        stopTrigger = trigger;
    }

    public CharacterUI getCharacterUI()
    {
        return charHp;
    }

    public void getHit(float val)
    {
        charHp.decreaseHp(val);
    }

    public bool isHpZero()
    {
        return charHp.isHpZero();
    }

    public void gotHit()
    {
        player.gotHit();
    }
}
