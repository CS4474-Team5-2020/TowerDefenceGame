using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    public TowerBtn ClickedBtn { get; private set; }
    public ObjectPool Pool { get; set; }
    //Keeps track of the current time in the wave
    //TODO: Update number next to "Next Wave" button with time remaining to indicate how much gold they will get for pressing it

    //Constant for the default next wave score bonus/remaining wave time
    private const int Countdown = 2;
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
    private Queue<Wave> WaveData = new Queue<Wave>();

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
        LoadWaveData();
        StartWave();
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
        if(WaveData.Count > 0)
        {
            var currentWave = WaveData.Dequeue();
            StartCoroutine(SpawnWave(currentWave));
        }
        
    }
    private IEnumerator SpawnWave(Wave wave)
    {

        //TODO: Have txt file with the composition of each wave (enemy type(s) and number of enemies)
        //TODO: Get current wave number and spawn corresponding enemies for wave
        //TODO: Spawn enemies in certain sections of map

        //Reset Next Wave Text
        WaveTime = Countdown;
        NextButtonTxt.text = "Next Wave +" + WaveTime;
        //TODO: Increase gold based on how much time left in current wave
        string minionType = wave.MinionType;
        //Spawn required number of enemies at each spawn point
        while(wave.TotalMinionCount > 0)
        {
            if(wave.TopSpawnCount > 0)
            {
                SpawnEnemy(minionType, "top");
                wave.TopSpawnCount--;
                wave.TotalMinionCount--;
            }
            if (wave.BottomSpawnCount > 0)
            {
                SpawnEnemy(minionType, "bottom");
                wave.BottomSpawnCount--;
                wave.TotalMinionCount--;
            }
            if (wave.LeftSpawnCount > 0)
            {
                SpawnEnemy(minionType, "left");
                wave.LeftSpawnCount--;
                wave.TotalMinionCount--;
            }
            if (wave.RightSpawnCount > 0)
            {
                SpawnEnemy(minionType, "right");
                wave.RightSpawnCount--;
                wave.TotalMinionCount--;
            }
        }
        yield return new WaitForSeconds(2.5f);
    }

    private void SpawnEnemy(string enemyType, string position)
    {
        GameObject enemyObject = Pool.GetObject(enemyType);
        string type = "Enemy";
        string orientation, endZone;
        GameObject spawnPos;
        GameObject endPos;
        switch (position)
        {
            case "top":
                spawnPos = TopStartZone;
                endPos = TopEndZone;
                orientation = "vertical";
                endZone = "bottom";
                break;
            case "bottom":
                spawnPos = BottomStartZone;
                endPos = BottomEndZone;
                orientation = "vertical";
                endZone = "top";
                break;
            case "right":
                spawnPos = RightStartZone;
                endPos = RightEndZone;
                orientation = "horizontal";
                endZone = "left";
                break;
            case "left":
                spawnPos = LeftStartZone;
                endPos = LeftEndZone;
                orientation = "horizontal";
                endZone = "right";
                break;
            default:
                spawnPos = null;
                endPos = null;
                orientation = "";
                endZone = "";
                break;
        }

        enemyObject.transform.position = spawnPos.transform.position;
        enemyObject.GetComponent<EnemyAI>().SetDestination(endPos.transform.position);
        enemyObject.GetComponent<EnemyAI>().SetAttackOrientation(orientation);
        enemyObject.GetComponent<EnemyAI>().SetEndZone(endZone);
    }
    public void PickTower(TowerBtn towerbtn)
    {
        this.ClickedBtn = towerbtn;
    }
    public void BuyTower()
    {
        this.ClickedBtn = null;
    }
    //CSV Format: Wave #,Minion Type, # of minions, top spawn count, bottom spawn count, left spawn count, right spawn count
    //Loads Wave Data into Queue
    private void LoadWaveData()
    {
        string[] lines = File.ReadAllLines(@"Assets\WaveComposition.csv");
        foreach(var line in lines)
        {
            string[] lineContents = line.Split(',');
            var newWave = new Wave()
            {
                WaveNumber = Int32.Parse(lineContents[0]),
                MinionType = lineContents[1],
                TotalMinionCount = Int32.Parse(lineContents[2]),
                TopSpawnCount = Int32.Parse(lineContents[3]),
                BottomSpawnCount = Int32.Parse(lineContents[4]),
                LeftSpawnCount = Int32.Parse(lineContents[5]),
                RightSpawnCount = Int32.Parse(lineContents[6])
            };
            WaveData.Enqueue(newWave);
        }
        
    }
}
 class Wave
{
    public int WaveNumber { get; set; }
    public string MinionType { get; set; }
    public int TotalMinionCount { get; set; }
    public int TopSpawnCount { get; set; }
    public int BottomSpawnCount { get; set; }
    public int LeftSpawnCount { get; set; }
    public int RightSpawnCount { get; set; }


}
