using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour {


    public static GamePlayUI Instance { get; private set; }

    public GameObject normalCursorRef;
    public GameObject interactableCursorRef;
    public Text cursorPromptTextRef;
    public GameObject interactPanel;
    public float skyTextFadeTime = 2;
    public TCUtils.TCFadingUI fadeUIRef;

    public string startSceneName;

    public enum MenuStatus
    {
        WaitingForClick,
        Gameplay
    }
    [SerializeField]
    protected MenuStatus status;


    public enum CursorType
    {
        Normal,
        Interactive
    }

    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
        SetCursor(CursorType.Normal);
        if(TempGlobalVars.shouldInMenu && status == MenuStatus.WaitingForClick)
        {
            StartMenu();
            status = MenuStatus.WaitingForClick;
        }
        else
        {
            status = MenuStatus.Gameplay;
            fadeUIRef.FadeOut(0.1f, null);
        }
        //FadoutSkyText();
    }
	
	// Update is called once per frame
	void Update () {
		if(status == MenuStatus.WaitingForClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartPlayGame();
                status = MenuStatus.Gameplay;
            }
        }
	}

    public void FadinSkyText(string text)
    {
        fadeUIRef.FadeIn(skyTextFadeTime,null);
        fadeUIRef.GetComponentInChildren<Text>().text = text;
    }
    public void FadoutSkyText()
    {
        fadeUIRef.FadeOut(skyTextFadeTime, null);
    }

    public void SetCursor(CursorType type, string message = null) {
        if(type == CursorType.Interactive)
        {
            normalCursorRef.SetActive(false);
            interactableCursorRef.SetActive(true);
        }else if(type == CursorType.Normal)
        {
            normalCursorRef.SetActive(true);
            interactableCursorRef.SetActive(false);
        }

        if(message != null)
        {
            cursorPromptTextRef.text = message;
        }
    }

    public void SetMiddleCursorEnable(bool enable)
    {
        interactPanel.SetActive(enable);
    }

    public void StartPlayGame()
    {
        TCUtils.TCSceneTransitionHelper.Instance.StartLoadingScene(startSceneName);
        TempGlobalVars.shouldInMenu = false;
    }

    public void StartMenu()
    {
        FadoutSkyText();
        PlayerInput.Instance.enabled = false;
        FadinSkyText("Click to Start");
    }
}
