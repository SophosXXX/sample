using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AvatarLookAt : MonoBehaviourPun
{
    private Transform mainCameraTransform;
    public GameObject camera;
    public GameObject hand;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            mainCameraTransform = camera.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            // Only the owner of the avatar controls its rotation
            transform.rotation = Quaternion.Euler(0f, mainCameraTransform.eulerAngles.y, 0f);
            
            // Send RPC to synchronize camera rotation across network
            photonView.RPC("SyncCameraRotation", RpcTarget.Others, mainCameraTransform.eulerAngles.y);
        }
    }

    // RPC method to synchronize camera rotation
    [PunRPC]
    void SyncCameraRotation(float cameraYRotation)
    {
        // Update rotation of avatar for all clients except the owner
        transform.rotation = Quaternion.Euler(0f, cameraYRotation, 0f);
        hand.transform.SetParent(transform);
    }
}
