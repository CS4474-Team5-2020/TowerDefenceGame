using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public ObjectPool Pool { get; set; }
    //Keeps track of the current time in the wave
    //TODO: Update number next to "Next Wave" button with time remaining to indicate how much gold they will get for pressing it
    public int Time { get; set; }
    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        //TODO: Increase gold based on how much time left in current wave
        
        string type = "Enemy";
        Pool.GetObject(type);
        yield return new WaitForSeconds(2.5f);
    }
}
