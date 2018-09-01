using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public void OnPlayButtonClick() {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void OnExitButtonClick() {
        Application.Quit();
    }
}
