using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public TextMeshProUGUI Volume;
    private Slider VolumeSlider;
    private Toggle VolumeToggle;
    public Image VolumeToggleImage;
    public AudioMixer audioMixer;
    private float current_Volume;

    public TextMeshProUGUI RaycastLength;
    private TextMeshProUGUI RaycastLength_small;
    private TextMeshProUGUI RaycastLength_med;
    private TextMeshProUGUI RaycastLength_long;

    public TextMeshProUGUI Speed;
    private TextMeshProUGUI Speed_low;
    private TextMeshProUGUI Speed_med;
    private TextMeshProUGUI Speed_high;

    public TextMeshProUGUI VoiceChat;

    private GameObject RayCast;
    public GameObject Character;

    private RayCastPointer ray;
    private CharacterMovement charMovement;

    public Button Back_button;
    private ColorBlock colorBlock;
    private Color originalColor;
    private Color selectedColor;

    public string originalColorHex = "#FFFFFF";
    public string selectedColorHex = "#FF6F00";

    public int activeButton = 0;

    // Start is called before the first frame update
    void Start()
    {
        VolumeSlider = Volume.transform.Find("VolumeSlider").gameObject.GetComponent<Slider>();
        VolumeToggle = Volume.transform.Find("VolumeToggle").gameObject.GetComponent<Toggle>();

        Volume.color = new Color(0f, 0f, 0f, 0.7f);

        Transform XR = Character.transform.Find("XRCardboardRig");
        Transform HeightO = XR.Find("HeightOffset");
        Transform CharCamera = HeightO.Find("Main Camera");

        RayCast = CharCamera.gameObject;

        ray = RayCast.GetComponent<RayCastPointer>();
        charMovement = Character.GetComponent<CharacterMovement>();

        ColorUtility.TryParseHtmlString(selectedColorHex, out selectedColor);
        ColorUtility.TryParseHtmlString(originalColorHex, out originalColor);

        RaycastLength_small = RaycastLength.transform.Find("Raycast_Low").gameObject.GetComponent<TextMeshProUGUI>();
        RaycastLength_med = RaycastLength.transform.Find("Raycast_Med").gameObject.GetComponent<TextMeshProUGUI>();
        RaycastLength_long = RaycastLength.transform.Find("Raycast_High").gameObject.GetComponent<TextMeshProUGUI>();

        Speed_low = Speed.transform.Find("Speed_Low").gameObject.GetComponent<TextMeshProUGUI>();
        Speed_med = Speed.transform.Find("Speed_Med").gameObject.GetComponent<TextMeshProUGUI>();
        Speed_high = Speed.transform.Find("Speed_High").gameObject.GetComponent<TextMeshProUGUI>();

        RaycastLengthChange();
        SpeedChange();
        audioMixer.GetFloat("current_volume", out current_Volume);
        VolumeSlider.value = current_Volume;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("js7")) //Input.GetButtonDown("js7") OK js0
        {
            activeButton = (activeButton + 1) % 5;
            SetButtonColor(activeButton);
        }

        switch(activeButton)
        {
            case 0:
                VolumeChange();
                break;
            case 1:{
                if(Input.GetButtonDown("js5"))
                {
                    RaycastLengthChange();
                }
                break;
            }
            case 2:{
                if(Input.GetButtonDown("js5"))
                {
                    SpeedChange();
                }
                break;
            }
            case 3:{
                if(Input.GetButtonDown("js5"))
                {
                    VoiceChatToggle(); // Not implemented
                }
                break;
            }
            case 4:{
                if(Input.GetButtonDown("js5"))
                {
                    Back_button.onClick.Invoke();
                }
                break;
            }
            default:
                break;
        }


    }

    private void VolumeChange()
    {
        if(Input.GetButtonDown("js5"))
        {
            audioMixer.GetFloat("current_volume", out current_Volume);
            if(current_Volume >= -70f)
            {
                current_Volume -= 10f;
                VolumeToggle.isOn = true;
            } 
            VolumeSlider.value = current_Volume;
        }

        else if(Input.GetButtonDown("js2"))//Input.GetButtonDown("js5")
        {
            audioMixer.GetFloat("current_volume", out current_Volume);
            if(current_Volume <= -10f)
            {
                current_Volume += 10f;
                VolumeToggle.isOn = true;
            } 
            VolumeSlider.value = current_Volume;
        }

        else if(Input.GetButtonDown("js5"))
        {
            if(VolumeToggle.isOn)
            {
                VolumeToggle.isOn = false;
                VolumeToggleImage.enabled = true;
                VolumeSlider.value = -80f;
            }
            else
            {
                VolumeToggle.isOn = true;
                VolumeToggleImage.enabled = false;
            }
        }
    }

    public void VolumeMute(bool mute)
    {
        if(mute)
        {
            VolumeSlider.value = 0f;
            VolumeToggle.isOn = false;
        }
        else
        {
            VolumeSlider.value = current_Volume;
            VolumeToggle.isOn = true;
        }
    }

    public void SetVolume(float volume_)
    {
        audioMixer.SetFloat("current_volume", volume_);
    }

    private void RaycastLengthChange()
    {
        float current_ray_length = ray.raycast_length;

        switch(current_ray_length)
        {
            case 3f:{
                ray.raycast_length = 7f;
                RaycastLength_small.color = new Color(0f, 0f, 0f, 0.7f);
                RaycastLength_med.color = Color.white;
                RaycastLength_long.color = new Color(0f, 0f, 0f, 0.7f);
                break;
            }
            case 7f:{
                ray.raycast_length = 11f;
                RaycastLength_small.color = new Color(0f, 0f, 0f, 0.7f);
                RaycastLength_med.color = new Color(0f, 0f, 0f, 0.7f);
                RaycastLength_long.color = Color.white;
                break;
            }
            case 11f:{
                ray.raycast_length = 3f;
                RaycastLength_small.color = Color.white;
                RaycastLength_med.color = new Color(0f, 0f, 0f, 0.7f);
                RaycastLength_long.color = new Color(0f, 0f, 0f, 0.7f);
                break;
            }
            default:
                break;
        }
    }

    private void SpeedChange()
    {
        float current_speed = charMovement.speed;

        switch(current_speed)
        {
            case 3f:{
                charMovement.speed = 7f;
                Speed_low.color = new Color(0f, 0f, 0f, 0.7f);
                Speed_med.color = Color.white;
                Speed_high.color = new Color(0f, 0f, 0f, 0.7f);
                break;
            }
            case 7f:{
                charMovement.speed = 11f;
                Speed_low.color = new Color(0f, 0f, 0f, 0.7f);
                Speed_med.color = new Color(0f, 0f, 0f, 0.7f);
                Speed_high.color = Color.white;
                break;
            }
            case 11f:{
                charMovement.speed = 3f;
                Speed_low.color = Color.white;
                Speed_med.color = new Color(0f, 0f, 0f, 0.7f);
                Speed_high.color = new Color(0f, 0f, 0f, 0.7f);
                break;
            }
            default:
                break;
        }
    }

    private void VoiceChatToggle()
    {

    }

    private void SetButtonColor(int activeButton)
    {
        switch(activeButton)
        {
            case 0:{
                Volume.color = new Color(0f, 0f, 0f, 0.7f);
                RaycastLength.color = Color.white;
                Speed.color = Color.white;
                VoiceChat.color = Color.white;
                colorBlock = Back_button.colors;
                colorBlock.normalColor = originalColor;
                Back_button.colors = colorBlock;
                break;
            }
            case 1:{
                Volume.color = Color.white;
                RaycastLength.color = new Color(0f, 0f, 0f, 0.7f);
                Speed.color = Color.white;
                VoiceChat.color = Color.white;
                colorBlock = Back_button.colors;
                colorBlock.normalColor = originalColor;
                Back_button.colors = colorBlock;
                break;
            }
            case 2:{
                Volume.color = Color.white;
                RaycastLength.color = Color.white;
                Speed.color = new Color(0f, 0f, 0f, 0.7f);
                VoiceChat.color = Color.white;
                colorBlock = Back_button.colors;
                colorBlock.normalColor = originalColor;
                Back_button.colors = colorBlock;
                break;
            }
            case 3:{
                Volume.color = Color.white;
                RaycastLength.color = Color.white;
                Speed.color = Color.white;
                VoiceChat.color = new Color(0f, 0f, 0f, 0.7f);
                colorBlock = Back_button.colors;
                colorBlock.normalColor = originalColor;
                Back_button.colors = colorBlock;
                break;
            }
            case 4:{
                Volume.color = Color.white;
                RaycastLength.color = Color.white;
                Speed.color = Color.white;
                VoiceChat.color = Color.white;
                colorBlock = Back_button.colors;
                colorBlock.normalColor = selectedColor;
                Back_button.colors = colorBlock;
                break;
            }
            default:
                break;
        }        
    }
}
