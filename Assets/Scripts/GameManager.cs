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

    //Constant for the default next wave starting score bonus/remaining wave time. 
    //Note: Prefer to keep at 15 seconds from now on
    private const int Countdown = 15;  

    //Remaining units until the next block of the wave progress bar
    private decimal RemainingUnits { get; set; } = 1.83m;
    //Remaining time left in wave
    public float WaveTime { get; set; }
    //Next Wave Text object to reference
    private Text NextButtonTxt { get; set; }

    private MoneyManager money;

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

    private bool paused;
    //Queue of current Waves
    private Queue<Wave> WaveData = new Queue<Wave>();
    private float timePassed;

    public GameObject victoryScreen;
    public GameObject defeatScreen;

    private void Awake()
    {
        paused = false;
        Pool = GetComponent<ObjectPool>();
        GamePause.onPause += OnPause;
        GamePause.onResume += OnResume;
        timePassed = 0;
        victoryScreen.SetActive(false);
        defeatScreen.SetActive(false);
    }
    // Use this for initialization
    void Start()
    {
        this.money = GameObject.FindObjectOfType<MoneyManager>();
        //Set up Next Wave button text
        WaveTime = Countdown;
        NextButtonTxt = GameObject.Find("NextWaveBtn").GetComponentInChildren<Button>().GetComponentInChildren<Text>();
        NextButtonTxt.text = "Next Wave +" + WaveTime.ToString("F0");
        LoadWaveData();
        StartWave();
        //Update remaining time every second
        InvokeRepeating("UpdateTime", 0f, 1f);
        InvokeRepeating("MoveProgressBar", 0f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
            return;
        timePassed += Time.deltaTime;

        WaveTime -= Time.deltaTime;
        //Reset wave time and spawn next wave at the end of the wave time
        if( WaveTime < 0)
        {
            WaveTime = Countdown;
            RemainingUnits = 1.83m;
            StartWave();
        }
        //Update Next Wave button text
        NextButtonTxt.text = "Next Wave +" + WaveTime.ToString("F0");
    }

    //Incrementally move progress bar
    void MoveProgressBar()
    {
        if (paused)
            return;
        RemainingUnits -= 0.0061m;
        WaveProgress.transform.position += new Vector3(-0.0061f, 0, 0);
    }

    //Shift wave progress bar all the way to next block
    void ShiftProgressBar()
    {
        WaveProgress.transform.position -= new Vector3((float)RemainingUnits, 0, 0);
        RemainingUnits = 1.83m;
    }

    //Start wave onclick event when pressing Next Wave button
    public void StartWaveNow()
    {
        if (paused)
            return;
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

    //Start wave event when the countdown for each wave ends or at start of game
    private void StartWave()
    {
        if(WaveData.Count > 0)
        {
            var currentWave = WaveData.Dequeue();
            StartCoroutine(SpawnWave(currentWave));
        }
        //Check if an enemy is left
        else if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            Victory();
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
        enemyObject.GetComponent<EnemyAI>().onDeath += Pool.ReturnGameObject;
        enemyObject.GetComponent<EnemyAI>().onDeath += SessionData.IncrementMinion;
    }
    public void PickTower(TowerBtn towerbtn)
    {
        if (money.GetMoneyBalance() >= towerbtn.Price)
        {
            this.ClickedBtn = towerbtn;
            Hover.Instance.Activate(towerbtn.Sprite);
            
        }
    }
    public void BuyTower()
    {
        money.DecreaseMoneyBalance(ClickedBtn.Price);
        this.ClickedBtn = null;
    }
    protected void OnResume()
    {
        paused = false;
    }

    protected void OnPause()
    {
        paused = true;
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

    private void Victory ()
    {
        EndGame();
        victoryScreen.SetActive(true);
    }

    public void Defeat()
    {
        EndGame();
        defeatScreen.SetActive(true);
    }
    private void EndGame()
    {
        SessionData.SetRecordMinions();
        SessionData.SetCurrentTime(timePassed);
        paused = true;
        enabled = false;
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
