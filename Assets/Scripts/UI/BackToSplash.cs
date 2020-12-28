using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToSplash : MonoBehaviour
{
    public string sceneToLoad;
    private GameData gameData;
    private Board board;
    private GameStartManager gameStart;

    public void WinOK() {
        StartCoroutine(WinCo());
        
    }

    public void LoseOK() {
        StartCoroutine(LoseCo());
        
    }

    public void FinalOK() {
        StartCoroutine(FinalCo());

    }

    // Start is called before the first frame update
    void Start()
    {
        gameStart = FindObjectOfType<GameStartManager>();
        board = FindObjectOfType<Board>();
        gameData = FindObjectOfType<GameData>();
        Debug.Log("гружу сцену");
        Debug.Log(board.level + 1);
      //  Debug.Log(gameData.saveData.isActive[board.level + 1]);
      
        if (!gameData.saveData.isActive[board.level + 1]) {
            Debug.Log("неактивен");
            gameData.saveData.stars[board.level] = 0;
            gameData.saveData.hightScores[board.level] = 0;
            gameData.Save();
        }
        
    }

    IEnumerator WinCo() {
        yield return new WaitForSeconds(1.4f);
        if (gameData != null) {
            gameData.saveData.isActive[board.level + 1] = true;
            gameData.Save();
        }
        SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator FinalCo() {
        yield return new WaitForSeconds(1.4f);
        if (gameData != null) {
            gameData.Save();
        }
        SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator LoseCo() {
        yield return new WaitForSeconds(1.4f);
        gameData.saveData.stars[board.level] = 0;
        gameData.saveData.hightScores[board.level] = 0;
        SceneManager.LoadScene(sceneToLoad);
        gameStart.PlayGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
