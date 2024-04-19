using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Photon.Pun;

public class VideoPlayerController : MonoBehaviourPunCallbacks
{
    public VideoPlayer videoPlayer;
    public Canvas uiCanvas;
    public Button playButton1;
    public Button playButton2;
    public Button pauseButton;
    public Button exitButton;

    public VideoClip videoClip1;
    public VideoClip videoClip2;

    private bool isMouseOverQuad = false;
    private bool isCanvasVisible = false; // Track the visibility of the canvas

    void Start()
    {
        if (!photonView.IsMine)
        {
            // Disable UI canvas for non-local players
            uiCanvas.gameObject.SetActive(false);
        }
        else
        {
            // Set up button click events only for the local player
            playButton1.onClick.AddListener(() => photonView.RPC("PlayVideo", RpcTarget.All, videoClip1.name));
            playButton2.onClick.AddListener(() => photonView.RPC("PlayVideo", RpcTarget.All, videoClip2.name));
            pauseButton.onClick.AddListener(() => photonView.RPC("TogglePause", RpcTarget.All));
            exitButton.onClick.AddListener(() => photonView.RPC("StopVideo", RpcTarget.All));
        }
    }

    [PunRPC]
    public void PlayVideo(string clipName)
    {
        // Load and play the specified video clip
        VideoClip clipToPlay = null;
        if (clipName == videoClip1.name)
        {
            clipToPlay = videoClip1;
        }
        else if (clipName == videoClip2.name)
        {
            clipToPlay = videoClip2;
        }

        if (clipToPlay != null)
        {
            videoPlayer.clip = clipToPlay;
            videoPlayer.Play();
        }
    }

    [PunRPC]
    public void TogglePause()
    {
        if (videoPlayer.isPlaying)
        {
            // Pause the video if it's playing
            videoPlayer.Pause();
        }
        else
        {
            // Resume playing the video if it's paused
            videoPlayer.Play();
        }
    }

    [PunRPC]
    public void StopVideo()
    {
        // Stop video playback and clear the video clip
        videoPlayer.Stop();
        videoPlayer.clip = null;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // Check if the mouse is over the Quad
            if (isMouseOverQuad && Input.GetButtonDown("js2")) // Check for 'x' key press
            {
                // Toggle the canvas visibility
                isCanvasVisible = !isCanvasVisible;
                uiCanvas.gameObject.SetActive(isCanvasVisible);
            }
        }
    }

    public void OnMouseEnterQuad()
    {
        isMouseOverQuad = true;
    }

    public void OnMouseExitQuad()
    {
        isMouseOverQuad = false;
    }
}
