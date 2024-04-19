using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AvatarLookAt : MonoBehaviourPun
{
    private Transform mainCameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            mainCameraTransform = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            transform.rotation = Quaternion.Euler(0f, mainCameraTransform.eulerAngles.y, 0f);
        }
    }
}
