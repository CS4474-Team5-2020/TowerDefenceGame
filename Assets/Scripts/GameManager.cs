using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public ObjectPool Pool { get; set; }
    //Keeps track of the current time in the wave
    //TODO: Update number next to "Next Wave" button with time remaining to indicate how much gold they will get for pressing it

    //Constant for the default next wave score bonus/remaining wave time
    private const int Countdown = 50;
    //Remaining time left in wave
    public int WaveTime { get; set; }
    //Next Wave Text object to reference
    private Text NextButtonTxt { get; set; }
    public GameObject TopStartZone;
    public GameObject BottomStartZone;
    public GameObject LeftStartZone;
    public GameObject RightStartZone;
    public GameObject TopEndZone;
    public GameObject BottomEndZone;
    public GameObject LeftEndZone;
    public GameObject RightEndZone;

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }
    // Use this for initialization
    void Start()
    {
        //Set up Next Wave button text
        WaveTime = Countdown;
        NextButtonTxt = GameObject.Find("NextWaveBtn").GetComponentInChildren<Button>().GetComponentInChildren<Text>();
        NextButtonTxt.text = "Next Wave +" + WaveTime;
        //Update remaining time every second
        InvokeRepeating("UpdateTime", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateTime()
    {
        WaveTime--;
        //Reset wave time and spawn next wave at the end of the wave time
        if( WaveTime < 0)
        {
            WaveTime = Countdown;
            StartWave();
        }
        //Update Next Wave button text
        NextButtonTxt.text = "Next Wave +" + WaveTime;
    }
    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }
    private IEnumerator SpawnWave()
    {

        //TODO: Have txt file with the composition of each wave (enemy type(s) and number of enemies)
        //TODO: Get current wave number and spawn corresponding enemies for wave
        //TODO: Spawn enemies in certain sections of map

        //Reset Next Wave Text
        WaveTime = Countdown;
        NextButtonTxt.text = "Next Wave +" + WaveTime;
        //TODO: Increase gold based on how much time left in current wave
        SpawnEnemy("","top");
        SpawnEnemy("", "bottom");
        SpawnEnemy("", "right");
        SpawnEnemy("", "left");

        yield return new WaitForSeconds(2.5f);
    }

    private void SpawnEnemy(string enemyType, string position)
    {
        string type = "Enemy";
        GameObject enemyObject = Pool.GetObject(type);
        GameObject spawnPos;
        GameObject endPos;
        switch (position)
        {
            case "top":
                spawnPos = TopStartZone;
                endPos = TopEndZone;
                break;
            case "bottom":
                spawnPos = BottomStartZone;
                endPos = BottomEndZone;
                break;
            case "right":
                spawnPos = RightStartZone;
                endPos = RightEndZone;
                break;
            case "left":
                spawnPos = LeftStartZone;
                endPos = LeftEndZone;
                break;
            default:
                spawnPos = null;
                endPos = null;
                break;
        }

        enemyObject.transform.position = spawnPos.transform.position;
        enemyObject.GetComponent<EnemyAI>().SetDestination(endPos.transform.position);
    }
}
