using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int activeButton = 0;

    private Color originalColor;
    private Color selectedColor;

    public string originalColorHex = "#FFFFFF";
    public string selectedColorHex = "#FF6F00";

    private ColorBlock colorBlock1;
    private ColorBlock colorBlock2;
    private ColorBlock colorBlock3;

    public Button Play_button;
    public Button Settings_button;
    public Button Quit_button;

    // private GameObject RayCast;
    // public GameObject Character;

    // private RayCastPointer ray;
    // private CharacterMovement charMovement;

    void Start()
    {
        // Transform XR = Character.transform.Find("XRCardboardRig");
        // Transform HeightO = XR.Find("HeightOffset");
        // Transform CharCamera = HeightO.Find("Main Camera");

        // RayCast = CharCamera.gameObject;

        // ray = RayCast.GetComponent<RayCastPointer>();
        // charMovement = Character.GetComponent<CharacterMovement>();

        ColorUtility.TryParseHtmlString(selectedColorHex, out selectedColor);
        ColorUtility.TryParseHtmlString(originalColorHex, out originalColor);
    }

    void Update()
    {

        if(Input.GetButtonDown("js7")) //Input.GetButtonDown("js7") OK js0
        {
            activeButton = (activeButton + 1) % 3;
            SetButtonColor(activeButton);
        }

        // Invoke button
        if(Input.GetButtonDown("js5")) //Input.GetButtonDown("js3") Y
        {
            switch(activeButton){
                case 0:
                    Play_button.onClick.Invoke();
                    break;
                case 1:
                    Settings_button.onClick.Invoke();
                    break;
                case 2:
                    Quit_button.onClick.Invoke();
                    break;
                default:
                    break;
            }
        }
    }

    private void SetButtonColor(int activeButton)
    {
        switch(activeButton)
        {
            case 0:{
                // Play Button
                colorBlock1 = Play_button.colors;
                colorBlock1.normalColor = selectedColor;
                Play_button.colors = colorBlock1;
                // Settings Button
                colorBlock2 = Settings_button.colors;
                colorBlock2.normalColor = originalColor;
                Settings_button.colors = colorBlock2;
                // Quit Button
                colorBlock3 = Quit_button.colors;
                colorBlock3.normalColor = originalColor;
                Quit_button.colors = colorBlock3;
                break;
            }
            case 1:{
                // Play Button
                colorBlock1 = Play_button.colors;
                colorBlock1.normalColor = originalColor;
                Play_button.colors = colorBlock1;
                // Settings Button
                colorBlock2 = Settings_button.colors;
                colorBlock2.normalColor = selectedColor;
                Settings_button.colors = colorBlock2;
                // Quit Button
                colorBlock3 = Quit_button.colors;
                colorBlock3.normalColor = originalColor;
                Quit_button.colors = colorBlock3;
                break;
            }
            case 2:{
                // Play Button
                colorBlock1 = Play_button.colors;
                colorBlock1.normalColor = originalColor;
                Play_button.colors = colorBlock1;
                // Settings Button
                colorBlock2 = Settings_button.colors;
                colorBlock2.normalColor = originalColor;
                Settings_button.colors = colorBlock2;
                // Quit Button
                colorBlock3 = Quit_button.colors;
                colorBlock3.normalColor = selectedColor;
                Quit_button.colors = colorBlock3;
                break;
            }
            default:
                break;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
