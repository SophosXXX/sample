using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BasketballScoreSystem : MonoBehaviour
{
    private GameObject basketball;
    private GameObject new_basketball;
    private Transform basketball_spawn_point;

    public Transform Basketball_StartPosition;

    public float force = 1000f;

    public int basketballScore = 0;

    public TextMeshProUGUI BasketballScore;

    // Start is called before the first frame update
    void Start()
    {
        basketball = GameObject.Find("basketball");
        basketball_spawn_point = GameObject.Find("Basketball_Spawn_Loc").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBasketball()
    {
        basketball = GameObject.Find("basketball");

        new_basketball = (GameObject)Instantiate(basketball);
        new_basketball.transform.position = basketball_spawn_point.transform.position;

        Rigidbody rb = new_basketball.GetComponent<Rigidbody>();

        rb.constraints &= ~RigidbodyConstraints.FreezePosition;

        rb.AddForce(new_basketball.transform.right * force);
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (collision.gameObject.name.Contains("basketball"))
            {
                // Debug.Log("Basketball in Hoop!");

                Vector3 distanceToHoop = transform.position - Basketball_StartPosition.position;

                basketballScore += Mathf.RoundToInt(100 - 100*distanceToHoop.magnitude);

                BasketballScore.text = "Score : " + basketballScore.ToString();

                Basketball_StartPosition.transform.position = transform.position;
            }
        }
        
    }

    public void ResetGame()
    {
        BasketballScore.text = "Score : 0";
        basketballScore = 0;
        Basketball_StartPosition.transform.position = transform.position;

        GameObject[] basketballs = GameObject.FindGameObjectsWithTag("Outline Objects");

        foreach(GameObject basketball_ in basketballs)
        {
            if(basketball_.name.Contains("basketball") && basketball_.name.Contains("(Clone)"))
            {
                Destroy(basketball_);
            }
        }
    }
}
