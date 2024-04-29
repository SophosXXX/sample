using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartGameLevels : MonoBehaviour
{
    public float speed = 0.01f; // Speed of the movement
    public float distance = 3f; // Distance to move

    private Vector3 startPos_large; // Starting position
    private Vector3 startPos_small;

    private Vector3 Original_startPos_large; // Starting position
    private Vector3 Original_startPos_small;

    private float startTime; // Time when movement started

    public GameObject smallBoard;
    public GameObject largeBoard;

    public int levelNumber = 0;
 
    void Start()
    {
        Original_startPos_large = largeBoard.transform.position;
        Original_startPos_small = smallBoard.transform.position;
    }

    void Update()
    {
        switch(levelNumber)
        {
            case 1: Level1();
                    break;
            case 2: Level2();
                    break;
            case 3: Level3();
                    break;
            default:
                    break;
        }
    }

    public void StartLevel(int level)
    {
        levelNumber = level;
        largeBoard.transform.position = Original_startPos_large;
        smallBoard.transform.position = Original_startPos_small;

        if(level == 0)
        {
            speed = 0f;
            distance = 0f;
        }
        else if(level == 1)
        {
            startPos_small = smallBoard.transform.position;

            startTime = Time.time;

            speed = 0.5f;
            distance = 3f;
        }
        else if(level == 2)
        {
            startPos_large = largeBoard.transform.position;
            startPos_small = smallBoard.transform.position;

            startTime = Time.time;

            speed = 0.5f;
            distance = 2f;
        }
        else if(level == 3)
        {
            startPos_large = largeBoard.transform.position;
            startPos_small = smallBoard.transform.position;

            startTime = Time.time;

            speed = 0.5f;
            distance = 2.5f;
        }
    }

    private void Level1()
    {
        // Calculate the distance to move
        float distCovered = (Time.time - startTime) * speed;

        // Use Mathf.Sin to create smooth oscillating motion
        float fraction = Mathf.Sin(distCovered / distance * Mathf.PI * 2);

        // Calculate the target position
        Vector3 targetPos_small = startPos_small + Vector3.forward * fraction * distance;

        // Move the object
        smallBoard.transform.position = targetPos_small;
    }

    private void Level2()
    {
        // Calculate the distance to move
        float distCovered = (Time.time - startTime) * speed;

        // Use Mathf.Sin to create smooth oscillating motion
        float fraction = Mathf.Sin(distCovered / distance * Mathf.PI * 2);

        // Calculate the target position
        Vector3 targetPos_small = startPos_small - Vector3.up * fraction * distance;

        Vector3 targetPos_large = startPos_large + Vector3.forward * fraction * distance;

        // Move the object
        smallBoard.transform.position = targetPos_small;

        largeBoard.transform.position = targetPos_large;

    }

    private void Level3()
    {
        // Calculate the distance to move
        float distCovered_large = (Time.time - startTime) * speed;

        float distCovered_small = (Time.time - startTime) * speed * 2;

        // Use Mathf.Sin to create smooth oscillating motion
        float fraction_large = Mathf.Sin(distCovered_large / distance * Mathf.PI * 2);
        float fraction_small = Mathf.Sin(distCovered_small / distance * Mathf.PI * 2);

        // Calculate the target position
        Vector3 targetPos_small = startPos_small + Vector3.forward * fraction_small * distance;

        Vector3 targetPos_large = startPos_large + Vector3.forward * fraction_large * distance;

        // Move the object
        smallBoard.transform.position = targetPos_small;

        largeBoard.transform.position = targetPos_large;
    }
}
