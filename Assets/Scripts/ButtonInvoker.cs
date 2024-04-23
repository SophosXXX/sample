using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Import the EventSystem namespace

public class ButtonInvoker : MonoBehaviour
{
    // Reference to the event system
    private EventSystem eventSystem;

    // Reference to the currently selected button
    private Button selectedButton;

    void Start()
    {
        // Get reference to the EventSystem component
        eventSystem = EventSystem.current;
    }

    void Update()
    {
        // Check if the 'B' key is pressed
        if (Input.GetButtonDown("js5"))
        {
            // Get the currently selected game object
            GameObject selectedObject = eventSystem.currentSelectedGameObject;

            // If a button is selected
            if (selectedObject != null)
            {
                // Get the Button component
                selectedButton = selectedObject.GetComponent<Button>();

                // If the button has a function assigned
                if (selectedButton != null)
                {
                    // Invoke the button's click event
                    selectedButton.onClick.Invoke();
                }
            }
        }
    }
}
