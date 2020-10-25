using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [Header("Active Stuff")]
    public bool isActive;
    public Sprite activeSprite;
    public Sprite lockedSprite;
    private Image buttonImage;
    private Button myButton;

    public GameObject levelPrefab;

    [Header("Level UI")]
    public Image[] stars;
    public Text levelText;
    public int level;
    public GameObject confirmPanel;
    public GameObject confirmPanelBackground;
    private int starsActive;

    private GameData gameData;

    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        buttonImage = GetComponent<Image>();
        myButton = GetComponent<Button>();
        LoadData();
        ActivateStars();
        DecideSprite();
        ShowLevel();
    }

    void LoadData() {
        if (gameData != null) {
            if(gameData.saveData == null) {
            }
            if (gameData.saveData.isActive[level - 1]) {
                isActive = true;
            } else {
                isActive = false;
            }
            starsActive = gameData.saveData.stars[level - 1];
        } else {
            Debug.Log("null");
        }
    }

    void ActivateStars() {
       // Debug.Log("Active" + starsActive);
        for(int i = 0; i < starsActive; i++) {
            if(level <= gameData.saveData.isActive.Length) {
                if (gameData.saveData.isActive[level]) {
                    stars[i].enabled = true;
                } else {
                    stars[i].enabled = false;
                }
            } else {
                stars[i].enabled = true;
            }
           
        }
    }
    void DecideSprite() {
        if (isActive) {
            buttonImage.sprite = activeSprite;
            myButton.enabled = true;
            levelText.enabled = true;
            
        } else {
            buttonImage.sprite = lockedSprite;
            myButton.enabled = false;
            levelText.enabled = false;
        }
    }

    void ShowLevel() {
        //levelText.text = "" + level;
    }

    public void ConfirmPanel(int level) {
        confirmPanelBackground.SetActive(true);
        confirmPanel.GetComponent<ConfirmPanel>().level = level;
        confirmPanel.SetActive(true);
    }
}
