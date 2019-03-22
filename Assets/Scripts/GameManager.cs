using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    public bool isEnd = false;
    public AudioSource audioSource;
    public Canvas canvas;
    public Text winnerText;
    public Text scoreText;
    public GameObject tank1Prefab;
    public GameObject tank2Prefab;
    public GameObject map;
    public UITweener mainMenuTween;
    public UITweener optionMenuTween;
    public GameObject menu;
    [HideInInspector]public GameObject tank1;
    [HideInInspector]public GameObject tank2;

    private int tank1Score;
    private int tank2Score;
    private Vector3 tank1Pos = new Vector3(-25, 0, 0);
    private Vector3 tank2Pos = new Vector3(25, 0, 25);
    private int winnerFlag = 0;

    // Use this for initialization
    void Start () {
        _instance = this;
        //InitGame();
	}
	

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }

    void InitGame()
    {
        isEnd = false;
        InitTanks();
        InitCamera();
        if (audioSource.isActiveAndEnabled == false)
        {
            audioSource.gameObject.SetActive(true);
        }

    }
    
    public void GameEnd(string playerTag)
    {
        isEnd = true;
        audioSource.gameObject.SetActive(false);
        canvas.gameObject.SetActive(true);
        winnerText.text = "Winner:" + playerTag;
        if (playerTag == "Tank1") { tank1Score++;winnerFlag = 1; }
        else { tank2Score++;winnerFlag = 2; }
        scoreText.text = "Score   " + tank1Score + ":" + tank2Score;
    }
    
    public void ReturnToMainMenu()
    {
        menu.SetActive(true);
        canvas.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetGame()
    {
        InitGame();
        InitCamera();
        canvas.gameObject.SetActive(false);
    }

    private void InitTanks()
    {
        if (winnerFlag == 0)
        {
            tank1=GameObject.Instantiate(tank1Prefab, tank1Pos, Quaternion.identity) as GameObject;
            tank2=GameObject.Instantiate(tank2Prefab, tank2Pos, Quaternion.identity) as GameObject;
        }else if (winnerFlag == 1)
        {
            tank2=GameObject.Instantiate(tank2Prefab, tank2Pos, Quaternion.identity) as GameObject;
            tank1.transform.position = tank1Pos;
        }else if (winnerFlag == 2)
        {
            tank1=GameObject.Instantiate(tank1Prefab, tank1Pos, Quaternion.identity) as GameObject;
            tank2.transform.position = tank2Pos;
        }
    }

    private void InitCamera()
    {
        this.GetComponentInParent<Follow>().ResetCamera();
    }

    public void OnStartGameButtonDown()
    {
        menu.SetActive(false);
        InitGame();
    }

    public void OnOptionsButtonDown()
    {
        mainMenuTween.PlayForward();
        optionMenuTween.PlayForward();
    }

    public void OnOptionsFinishedButtonDown()
    {
        mainMenuTween.PlayReverse();
        optionMenuTween.PlayReverse();
    }
}
