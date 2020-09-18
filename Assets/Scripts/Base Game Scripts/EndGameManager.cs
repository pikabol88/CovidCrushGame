using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameType {
    MOVES,
    TIME
}
[System.Serializable]
public class EndGameReguirmenrs {
    public GameType gameType;
    public int counterValue;
}

public class EndGameManager : MonoBehaviour
{
    public GameObject movesLabel;
    public GameObject timeLabel;
    public GameObject youWinPanel;
    public GameObject tryAgainPanel;
    public Text counter;
    public EndGameReguirmenrs requirements;
    public int currentCounterValue;
    private Board board;
    private float timerSeconds;
    public bool isWin;


    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        SetGameType();
        SetupGame();
    }

    void SetGameType() {
        if (board.world != null) {
            if(board.world.levels != null) {
                if (board.level < board.world.levels.Length) {
                    if (board.world.levels[board.level] != null) {
                        requirements = board.world.levels[board.level].endGameReguirmenrs;
                    }
                }
            }
        }
    }

    void SetupGame() {
        currentCounterValue = requirements.counterValue;
        if(requirements.gameType == GameType.MOVES) {
            movesLabel.SetActive(true);
            timeLabel.SetActive(false);
        } else {
            timerSeconds = 1;
            movesLabel.SetActive(false);
            timeLabel.SetActive(true);
        }
        counter.text = "" + currentCounterValue;
    }

    public void DecreaseCounterValue() {
        if (board.currentState != GameState.PAUSE) {
            if (SlimeDeadlockCheck()) {
                if (!isWin) {
                    StartCoroutine(LoseGameCo());
                }
            } else
            if (currentCounterValue >= 1) {
                currentCounterValue--;
                counter.text = "" + currentCounterValue;
                if (currentCounterValue <= 0) {
                    if (!isWin) {
                        StartCoroutine(LoseGameCo());
                    }
                }
            }
        }
    }
    private bool SlimeDeadlockCheck() {
        if(board.slimeTiles != null) {
            if( SlimeCounter() >= board.width * board.height - 3) {
                Debug.Log("slime = " + board.slimeTiles.Length);
                Debug.Log("size" + (board.width * board.height - 3));
                return true;
            }
        }
        return false;
    }

    private int SlimeCounter() {
        int count = 0;
        if (board.slimeTiles != null) {
            for(int i = 0; i < board.width; i++) {
                for(int j = 0; j < board.height; j++) {
                    if (board.slimeTiles[i,j] != null) {
                        count++;
                    }
                }
            }            
        }
        return count;
    } 

    public void WinGame() {
        Debug.Log("isWinPanel");
        isWin = true;
        youWinPanel.SetActive(true);
        board.currentState = GameState.WIN;
        currentCounterValue = 0;
        counter.text = "" + currentCounterValue;
        FadePanelController fade = FindObjectOfType<FadePanelController>();
        fade.GameOver();
    }

    public IEnumerator LoseGameCo() {
        yield return new WaitForSeconds(2f);
        if (isWin != true) {
            Debug.Log(isWin);
            Debug.Log("LoseGame");
            tryAgainPanel.SetActive(true);
            board.currentState = GameState.LOSE;
            currentCounterValue = 0;
            counter.text = "" + currentCounterValue;
            FadePanelController fade = FindObjectOfType<FadePanelController>();
            fade.GameOver();
        } else if (isWin) {
            Debug.Log("isWin - setActive");
            tryAgainPanel.SetActive(false);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if(requirements.gameType == GameType.TIME && currentCounterValue > 0) {
            timerSeconds -= Time.deltaTime;
            if (timerSeconds <= 0) {
                DecreaseCounterValue();
                timerSeconds = 1;
            }
        }
    }
}
