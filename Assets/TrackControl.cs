using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackControl : MonoBehaviour
{

    [SerializeField]
    GameObject cars;

    [SerializeField]
    GameObject finishLine;

    [SerializeField]
    GameObject checkpoints;

    private static int checkpoint_amount;
    private static Dictionary<int, int> checkpointTracker;

    const int SEC_BEFORE_START = 3;

    void Start()
    {
        checkpoint_amount = checkpoints.transform.childCount;
        checkpointTracker = new Dictionary<int, int>();
    }

    
    void Update()
    {
        
    }

    public static void notifyCheckpointCrossed(int carIdx, string checkpointName)
    {
        if (!checkpointTracker.ContainsKey(carIdx))
        {
            if (checkpointName == "Checkpoint")
            {
                // first cross
                checkpointTracker.Add(carIdx, 0);
            }
        } else
        {
            if (checkpointTracker.TryGetValue(carIdx, out int value))
            {
                if (checkpointName == "Checkpoint (" + (value + 1) + ")")
                {
                    checkpointTracker[carIdx]++;
                }
            }

        }
        
    }

    public static void notifyFinishCrossed(int carIdx)
    {
        if (checkpointTracker.TryGetValue(carIdx, out int value))
        {
            if (value == checkpoint_amount - 1)
            {
                checkpointTracker[carIdx] = 0;
            }
        }
    }

}
