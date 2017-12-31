using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerController : MonoBehaviour {
    public GameObject menuPanel;
    public GameObject operationPanel;
    public GameObject diffcultyPanel;
    public GameObject characterSelectPanel;
    public Color operationPanelColor;

    public void OnMenuPlay()
    {
        menuPanel.SetActive(false);
        operationPanel.SetActive(true);
    }

    public void OnOperationPlay()
    {
        operationPanel.GetComponent<Image>().color =
            operationPanelColor;
        diffcultyPanel.SetActive(true);
    }

    public void OnDiffcultyClick()
    {
        operationPanel.SetActive(false);
        diffcultyPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
    }
    public void OnCharacterSelect()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        // Application.LoadLevel(1);
    }
}
