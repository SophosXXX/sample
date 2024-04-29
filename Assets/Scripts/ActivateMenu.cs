using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMenu : MonoBehaviour
{
    public GameObject Camera;
    public GameObject Character;
    public GameObject Menu;

    private RayCastPointer ray;
    private CharacterMovement charMovement;

    // Start is called before the first frame update
    void Start()
    {
        ray = Camera.GetComponent<RayCastPointer>();

        charMovement = Character.GetComponent<CharacterMovement>();        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)) //(Input.GetButtonDown("js0") OK js0
        {
            Menu.SetActive(true);
            
            ray.enabled = false;

            charMovement.enabled = false;
        }        
    }
}
