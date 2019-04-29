using System.Collections;
using System.Collections.Generic;
using EventSystem;
using TMPro;
using UnityEngine;

public class ChooseWinLose : MonoBehaviour {
    [SerializeField] private GameEvent endGame;

    [SerializeField] private GameObject losePanel, winPanel;

    [SerializeField] private TMP_Text valueText;

    // Start is called before the first frame update
    private void Start() {
        if (endGame.sentBool) {
            valueText.text = endGame.sentFloat.ToString();
            winPanel.SetActive(true);
            losePanel.SetActive(false);
        } else {
            winPanel.SetActive(false);
            losePanel.SetActive(transform);
        }
    }
}