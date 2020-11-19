using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackControl : MonoBehaviour
{

    [SerializeField]
    GameObject checkpoints;

    [SerializeField]
    TextMeshProUGUI p1LapsText, p2LapsText;

    [SerializeField]
    GameObject winnerScreen;

    private int p1LapCounter, p2LapCounter;

    private static int checkpoint_amount, round_amount;
    private static Dictionary<int, int> checkpointTracker;

    const int SEC_BEFORE_START = 3;

    void Start()
    {
        checkpoint_amount = checkpoints.transform.childCount;
        round_amount = PlayerPrefs.GetInt("rounds");
        checkpointTracker = new Dictionary<int, int>();
        p1LapsText.text = "1 / " + round_amount;
        p2LapsText.text = "1 / " + round_amount;
        p1LapCounter = 1;
        p2LapCounter = 1;
    }

    public void notifyCheckpointCrossed(int carIdx, string checkpointName)
    {
        if (checkpointTracker.ContainsKey(carIdx))
        {
            if (checkpointTracker.TryGetValue(carIdx, out int value))
            {
                if (value >= 0)
                {
                    if (checkpointName.Equals("Checkpoint (" + (value + 1) + ")"))
                    {
                        checkpointTracker[carIdx]++;
                    }
                } else
                {
                    if (checkpointName == "Checkpoint")
                    {
                        // first checkpoint
                        checkpointTracker[carIdx]++;
                    }
                }
            }
        } else
        {
            if (checkpointName == "Checkpoint")
            {
                // first cross
                checkpointTracker.Add(carIdx, 0);
            }
        }
        
    }

    public void notifyFinishCrossed(int carIdx)
    {
        if (checkpointTracker.TryGetValue(carIdx, out int value))
        {
            if (value == checkpoint_amount - 1)
            {
                checkpointTracker[carIdx] = -1;
                if(carIdx == 1)
                {
                    p1LapCounter++;
                    
                    if (p1LapCounter == round_amount + 1)
                    {
                        // p1 has won
                        winnerScreen.SetActive(true);
                        string winText = PlayerPrefs.GetString("p1Name") + " has won!";
                        winnerScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = winText;
                        Waiter.Wait(7, () =>
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                        });
                    } else
                    {
                        // increment the lapCounter
                        p1LapsText.text = p1LapCounter + " / " + round_amount;
                    }
                } else if (carIdx == 2)
                {
                    p2LapCounter++;
                    
                    if(p2LapCounter == round_amount + 1)
                    {
                        // p2 has won
                        winnerScreen.SetActive(true);
                        string winText = PlayerPrefs.GetString("p2Name") + " has won!";
                        winnerScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = winText;
                        Waiter.Wait(7, () =>
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                        }); 
                    } else
                    {
                        p2LapsText.text = p2LapCounter + " / " + round_amount;
                    }
                }
            }
        }
    }

}
