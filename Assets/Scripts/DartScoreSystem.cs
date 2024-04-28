using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DartScoreSystem : MonoBehaviour
{
    public int dartScore = 0;

    public TextMeshProUGUI DartScore;
    public TextMeshProUGUI DartsLeft;

    public int currentDarts = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScore()
    {

        dartScore = 0;
        currentDarts = 5;
        GameObject[] board_objects = GameObject.FindGameObjectsWithTag("ToBeDestroyed");

        foreach(GameObject board_object in board_objects)
        {
            if(board_object.name.Contains("dart") && board_object.name.Contains("(Clone)"))
            {
                Destroy(board_object);
            }
        }
        DartScore.text = "Score : 0";
        DartsLeft.text = "Darts Left : 5";
    }

    public void UpdateScore(float distance, float scale)
    {
        distance = 100 * distance;
        dartScore = dartScore + Mathf.RoundToInt((200 - distance)/scale);

        // currentDarts = currentDarts - 1;

        DartScore.text = "Score : " + dartScore.ToString();
        // DartsLeft.text = "Darts Left : " + currentDarts.ToString();
    }
}
