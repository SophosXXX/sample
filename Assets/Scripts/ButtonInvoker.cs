using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Import the EventSystem namespace
using UnityEngine.SceneManagement;

public class ButtonInvoker : MonoBehaviour
{
    void Update()
    {
        // Check if the 'B' key is pressed
        if (Input.GetButtonDown("js2"))
        {
            SceneManager.LoadScene("Project");
        }
    }
}
