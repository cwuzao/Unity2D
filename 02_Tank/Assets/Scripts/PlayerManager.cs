using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {
    public int lifeValue = 3;
    public int playerScore = 0;
    public bool isDead;
    public bool isDefeat;

    public GameObject Born;
    public Text playerScoreText;
    public Text PlayerHealthText;
    public GameObject isDefeatUI;

    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
            Invoke("ReturnToTheMainMenu", 3);
            return;
        }
        if (isDead)
            Recover();
        playerScoreText.text = playerScore.ToString();
        PlayerHealthText.text = lifeValue.ToString();
    }

    protected void Recover()
    {
        if (lifeValue < 0)
        {
            //Game Over
            isDefeat = true;
            Invoke("ReturnToTheMainMenu", 3);
        }
        else
        {
            lifeValue--;
            GameObject go = Instantiate(Born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().isCreatePlayer = true;
            isDead = false;
        }
    }

    private void ReturnToTheMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
