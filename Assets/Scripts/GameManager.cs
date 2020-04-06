﻿using UnityEngine;
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

    //Constant for the default next wave starting score bonus/remaining wave time
    private const int Countdown = 15;
    //Remaining time left in wave
    public int WaveTime { get; set; }
    private decimal RemainingTime { get; set; } = 15m;
    private decimal RemainingUnits { get; set; } = 3m;
    //Next Wave Text object to reference
    private Text NextButtonTxt { get; set; }
    public GameObject WaveProgress;
    //References to Starting and End zones
    public GameObject TopStartZone;
    public GameObject BottomStartZone;
    public GameObject LeftStartZone;
    public GameObject RightStartZone;
    public GameObject TopEndZone;
    public GameObject BottomEndZone;
    public GameObject LeftEndZone;
    public GameObject RightEndZone;

    //Queue of current Waves
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
        InvokeRepeating("UpdateTime", 0f, 1f);
        InvokeRepeating("MoveProgressBar", 0f, 0.05f);
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
            RemainingTime = 15m;
            RemainingUnits = 3m;
            StartWave();
        }
        //Update Next Wave button text
        NextButtonTxt.text = "Next Wave +" + WaveTime;
    }
    void MoveProgressBar()
    {
        RemainingTime -= 0.05m;
        RemainingUnits -= 0.01m;
        WaveProgress.transform.position += new Vector3(-0.01f, 0, 0);
    }
    void ShiftProgressBar()
    {
        WaveProgress.transform.position -= new Vector3((float)RemainingUnits, 0, 0);
        RemainingUnits = 3m;
    }
    public void StartWaveNow()
    {
        if (WaveData.Count > 0)
        {
            var currentWave = WaveData.Dequeue();
            if (currentWave.WaveNumber > 1)
            {
                ShiftProgressBar();
            }
            StartCoroutine(SpawnWave(currentWave));
        }
    }
    private void StartWave()
    {
        if(WaveData.Count > 0)
        {
            var currentWave = WaveData.Dequeue();
            StartCoroutine(SpawnWave(currentWave));
        }
        
    }

    private IEnumerator SpawnWave(Wave wave)
    {
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
        yield return null;//new WaitForSeconds(2.5f);
    }

    public void SpawnEnemy(string enemyType, string position, InheritedBehaviour parentBehaviour = null)
    {
        GameObject enemyObject = Pool.GetObject(enemyType);
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
        //Initialize minion behaviour from parent (Group minion behaviour inherited by Groupling)
        if(parentBehaviour != null)
        {
            enemyObject.transform.position = parentBehaviour.SpawnPoint;
            enemyObject.GetComponent<EnemyAI>().SetDestination(parentBehaviour.Destination);
            enemyObject.GetComponent<EnemyAI>().SetAttackOrientation(parentBehaviour.Orientation);
            enemyObject.GetComponent<EnemyAI>().SetEndZone(parentBehaviour.EndZone);
        }
        //Initialize minion behaviour when spawned at spawnpoint
        else
        {
            enemyObject.transform.position = spawnPos.transform.position;
            enemyObject.GetComponent<EnemyAI>().SetDestination(endPos.transform.position);
            enemyObject.GetComponent<EnemyAI>().SetAttackOrientation(orientation);
            enemyObject.GetComponent<EnemyAI>().SetEndZone(endZone);
        }
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
