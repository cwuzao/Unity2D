using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour {

    private static MainUIController _instance;
    public static MainUIController Instance
    {
        get
        {
            return _instance;
        }
    }

    public int score = 0;
    public int length = 0;
    public Text msgText;
    public Text scoreText;
    public Text lengthText;
    public Image bgImage;
    private Color tempColor;
    public Image pauseImage;
    public Sprite[] pauseSprites;
    public bool isPause = false;
    public bool hasBorder = true;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("border", 1) == 0)
        {
            hasBorder = false;
            foreach (Transform t in bgImage.gameObject.transform)
            {
                t.gameObject.GetComponent<Image>().enabled = false;
            }
        }
        else
        {
            hasBorder = true;
        }
    }

    void Update()
    {
        switch (score / 200)
        {
            case 0:
                break;
            case 1:
                ColorUtility.TryParseHtmlString("#CCEEFFFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "阶段2";
                bgImage.color = new Color();
                break;
            case 2:
                ColorUtility.TryParseHtmlString("#CCEEFFFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "阶段3";
                bgImage.color = new Color();
                break;
            case 3:
                ColorUtility.TryParseHtmlString("#EBFFDBFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "阶段4";
                bgImage.color = new Color();
                break;
            case 4:
                ColorUtility.TryParseHtmlString("#FFF3CCFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "阶段5";
                bgImage.color = new Color();
                break;
            default:
                ColorUtility.TryParseHtmlString("#CCDACCFF", out tempColor);
                bgImage.color = tempColor;
                msgText.text = "无尽阶段";
                bgImage.color = new Color();
                break;
        }
    }

    public void UpdateUI(int s = 5, int l = 1)
    {
        score += s;
        length += l;
        scoreText.text = "得分:\n" + score;
        lengthText.text = "长度:\n" + length;
    }

    public void Pause()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0;
            pauseImage.sprite = pauseSprites[1];
        }
        else
        {
            Time.timeScale = 1;
            pauseImage.sprite = pauseSprites[0];
        }
    }

    public void Home()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
