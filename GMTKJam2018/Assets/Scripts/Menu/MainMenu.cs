using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public void OnPlayButtonClick() {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void OnExitButtonClick() {
        Application.Quit();
    }
}
