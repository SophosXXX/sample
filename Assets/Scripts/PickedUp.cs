using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickedUp : MonoBehaviour
{
    public bool pickedUp = false;
    public Vector3 pivot = new Vector3(0f, 0f, 0f);

    public float force = 1000f;

    public GameObject Ray;
    // public GameObject Hand;

    private Rigidbody rb;

    private DartScoreSystem dartScoreSystem;
    public GameObject Raycast;
    private RayCastPointer rayCastPointer;
    private TextMeshProUGUI DartsLeft;

    private BasketballScoreSystem basketballScoreSystem;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        dartScoreSystem = GameObject.Find("DartScoreSystem").GetComponent<DartScoreSystem>();
        rayCastPointer = Raycast.GetComponent<RayCastPointer>();
        DartsLeft = GameObject.Find("DartsLeft").GetComponent<TextMeshProUGUI>();

        basketballScoreSystem = GameObject.Find("basketball_hoop").GetComponent<BasketballScoreSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pickedUp)
        {
            //transform.position = Ray.GetComponent<RayCastPointer>().rayPivot;

            //GetComponent<BoxCollider>().enabled = false;

            Freeze();

            if(Input.GetButtonDown("js5"))
            {
                if(gameObject.name.Contains("basketball"))
                {
                    basketballScoreSystem.Basketball_StartPosition = gameObject.transform;
                }
                
                transform.SetParent(null);
                rb.useGravity = true;

                rb.AddForce(Ray.transform.up * force);
                pickedUp = false;

                // GetComponent<BoxCollider>().enabled = true;
                if (gameObject.name.Contains("dart"))
                {
                    // Unfreeze position along all axes
                    rb.constraints &= ~RigidbodyConstraints.FreezePosition;

                    // Unfreeze the position along the Y axis
                    rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
                    rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
                }
                else
                {
                    Unfreeze();
                }

                if (gameObject.name.Contains("dart"))
                {
                    // Update Dart score
                    dartScoreSystem.currentDarts = dartScoreSystem.currentDarts - 1;
                    DartsLeft.text = "Darts Left : " + dartScoreSystem.currentDarts.ToString();

                    GetComponent<AvatarLookAt>().enabled = false;
                    gameObject.tag = "ToBeDestroyed";
                    GetComponent<Outline>().enabled = true;
                }

                // reset raycast length
                rayCastPointer.maxRayDistance = rayCastPointer.raycast_length;
            }
        }

    }

    void Unfreeze()
    {
        // Unfreeze position along all axes
        rb.constraints &= ~RigidbodyConstraints.FreezePosition;

        // Unfreeze rotation along all axes
        rb.constraints &= ~RigidbodyConstraints.FreezeRotation;
    }

    void Freeze()
    {
        // Freeze position along all axes
        rb.constraints = RigidbodyConstraints.FreezePosition;

        // Freeze rotation along all axes
        rb.constraints |= RigidbodyConstraints.FreezeRotation;
    }

}
