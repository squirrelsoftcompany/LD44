using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public void goToEndGame() {
        SceneManager.LoadScene("EndGameScene");
    }

    public void replay() {
        SceneManager.LoadScene("Final_scene");
    }
}