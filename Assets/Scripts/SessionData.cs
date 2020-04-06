using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SessionData
{
    public static float difficultyMultiplier { get; set; } = 0.5f;
    public static int minionsKilled;
    public static int recordMinions { get; private set; }

    public static int currentTime;
    public static int recordTime { get; private set; }

    public static void IncrementMinion(GameObject gameObject)
    {
        minionsKilled++;
    }

    public static void SetRecordMinions()
    {
        if (minionsKilled > recordMinions)
            recordMinions = minionsKilled;
    }
    public static void SetCurrentTime(float time)
    {
        currentTime = (int)time;
        if (time > recordTime)
            recordTime = (int)time;
    }

}
