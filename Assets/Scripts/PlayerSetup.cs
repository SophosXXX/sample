using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    public GameObject camera;
    public GameObject character; // Reference to the character GameObject

    [PunRPC]
    public void isLocalPlayer()
    {
        camera.SetActive(true);

        // Change character color to a random color
        Renderer renderer = character.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Random.ColorHSV();
        }
        else
        {
            Debug.LogError("Character GameObject does not have a Renderer component.");
        }
    }
}
