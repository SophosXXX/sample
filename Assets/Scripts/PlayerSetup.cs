using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    public GameObject camera;
    public GameObject character; // Reference to the character GameObject

    Color color;

    void update(){
        photonView.RPC("Color", RpcTarget.All);
    }
    public void isLocalPlayer(Color color1)
    {
        color = color1;
        camera.SetActive(true);
        photonView.RPC("Color", RpcTarget.All);
        
    }

    [PunRPC]
    public void Color()
    {
        // Change character color to a random color
        Renderer renderer = character.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
        else
        {
            Debug.LogError("Character GameObject does not have a Renderer component.");
        }
    }
}
