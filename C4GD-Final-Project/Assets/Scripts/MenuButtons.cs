using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    public void switchToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    public void switchToControls() {
        SceneManager.LoadScene("ControlsScreen");
    }
    public void switchToLevel1() {
        SceneManager.LoadScene("Level 1");
    }
}
