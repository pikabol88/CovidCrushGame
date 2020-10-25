using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{

    public GameObject startPanel;
    public GameObject levelPanel;

    public GameObject[] panels;
    public GameObject currentPanel;
    public int page;
    private GameData gameData;
    public int currentLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        for(int i = 0; i < panels.Length; i++) {
            panels[i].SetActive(false);
        }
        if (gameData != null) {
            for(int i = 0; i < gameData.saveData.isActive.Length; i++) {
                if (gameData.saveData.isActive[i]) {
                    currentLevel = i;                    
                }
            }
            if (!gameData.saveData.isActive[currentLevel+1]) {
                Debug.Log("stars and score to zero in start() ob lvl select");
                gameData.saveData.stars[currentLevel] = 0;
                gameData.saveData.hightScores[currentLevel] = 0;
                gameData.Save();
            }
        }
        page = (int)Mathf.Floor(currentLevel / 9);
        currentPanel = panels[page];
        panels[page].SetActive(true);
        gameData.Save();
    }
    
    public void PageRight() {
        if (page < panels.Length - 1) {
            currentPanel.SetActive(false);
            page++;
            currentPanel = panels[page];
            currentPanel.SetActive(true);
        }
    }

    public void PageLeft() {
        if (page > 0) {
            currentPanel.SetActive(false);
            page--;
            currentPanel = panels[page];
            currentPanel.SetActive(true);
        }
    }

    public void Home() {
        Debug.Log("HOME");
        levelPanel = GameObject.FindGameObjectWithTag("Level Select");
        startPanel.SetActive(true);
        levelPanel.SetActive(false);
    }


}
