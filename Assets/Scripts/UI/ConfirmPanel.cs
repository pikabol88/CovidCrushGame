using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmPanel : MonoBehaviour
{
    [Header("Level Information")]
    public string levelToLoad;
    public int level;
    private GameData gameData; 
    private int hightScore;

    [Header("UI stuff")]
    public Image[] stars;
    public Text hightScoreText;
    public Text starText;
    private int starsActive;

    public GameObject confirmPanelBackground;
    public Animator panelAnim;

    public LoadingPanel loadingPanel;


    // Start is called before the first frame update
    void OnEnable()
    {
        gameData = FindObjectOfType<GameData>();
        LoadData();
        ActivateStars();
        SetText();
    }

    void LoadData() {
        if (gameData != null) {            
            starsActive = gameData.saveData.stars[level - 1];
            hightScore = gameData.saveData.hightScores[level - 1];
        } 
    }

    void SetText() {
        hightScoreText.text = "" + hightScore;
        starText.text = "" + starsActive + "/3"; 
    }

    void ActivateStars() {
        for (int i = 0; i < starsActive; i++) {
            stars[i].enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Cancel() {
        panelAnim.SetBool("Out", true);
        StartCoroutine(CancelCo());
    }

    public void Play() {
        panelAnim.SetBool("Out", true);
        loadingPanel.ActivatePanel();
        StartCoroutine(PlayCo());
    }

    IEnumerator PlayCo() {
        yield return new WaitForSeconds(1.5f);
        confirmPanelBackground.SetActive(false);
        PlayerPrefs.SetInt("Current Level", level - 1);
        Debug.Log("Confirm Panel" + level);
        SceneManager.LoadScene(levelToLoad);
    }

    IEnumerator CancelCo() {
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
        confirmPanelBackground.SetActive(false);
    }
}

