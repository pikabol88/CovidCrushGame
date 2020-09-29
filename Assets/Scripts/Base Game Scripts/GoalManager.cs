using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MEANING??
[System.Serializable]
public class BlankGoal {
    public int numberNeeded;
    public int numberCollected;
    public Sprite goalSprite;
    public string matchValue;
   
}

public class GoalManager : MonoBehaviour
{
    public BlankGoal[] levelGoals;
    public List<GoalPanel> currentGoals = new List<GoalPanel>();
    public GameObject goalPrefab;
    public GameObject goalPrefabGame;
    public GameObject goalIntroParent;
    public GameObject goalGameParent;
    private EndGameManager endGame;
    private Board board;

  //  public GameData gameData;
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        endGame = FindObjectOfType<EndGameManager>();
        GetGoals();
        SetUpIntroGoals();
    }

    void GetGoals() {
        if (board != null) {
            if (board.world != null) {
                if (board.level < board.world.levels.Length) {
                    if (board.world.levels[board.level] != null) {
                        levelGoals = board.world.levels[board.level].levelGoals;
                        if (levelGoals != null) {
                            for (int i = 0; i < levelGoals.Length; i++) {
                                levelGoals[i].numberCollected = 0;
                            }
                        }
                    }
                }
            }
        }
    }

    void SetUpIntroGoals() {
        if (levelGoals != null) {
            for (int i = 0; i < levelGoals.Length; i++) {
                //Create new goal panel at the goal intro panel position
                GameObject goal = Instantiate(goalPrefab, goalIntroParent.transform.position, Quaternion.identity);
                goal.transform.SetParent(goalIntroParent.transform, false);
                //Set image and text of the goal
                GoalPanel panel = goal.GetComponent<GoalPanel>();
                panel.thisSprite = levelGoals[i].goalSprite;
                panel.thisString = "0/" + levelGoals[i].numberNeeded;

                GameObject gameGoal = Instantiate(goalPrefabGame, goalGameParent.transform.position, Quaternion.identity);
                gameGoal.transform.SetParent(goalGameParent.transform, false);
                panel = gameGoal.GetComponent<GoalPanel>();
                currentGoals.Add(panel);
                panel.thisSprite = levelGoals[i].goalSprite;
                panel.thisString = "0/" + levelGoals[i].numberNeeded;
            }
        }
    }

    public void UpdateGoals() {
        int goalsCompleted = 0;
        if (levelGoals != null) {
            for (int i = 0; i < levelGoals.Length; i++) {
                currentGoals[i].thisText.text = "" + levelGoals[i].numberCollected + "/" + levelGoals[i].numberNeeded;
                if (levelGoals[i].numberCollected >= levelGoals[i].numberNeeded) {
                    goalsCompleted++;
                    currentGoals[i].thisText.text = "" + levelGoals[i].numberNeeded + "/" + levelGoals[i].numberNeeded;
                }
            }
            if (goalsCompleted >= levelGoals.Length) {
                if (endGame != null) {
                    Debug.Log("endGame.isWin = true");
                    endGame.isWin = true;
                    endGame.WinGame();
                }
            }
        }
    }


    public void UpdateSlimeGoal() {
        for (int i = 0; i < levelGoals.Length; i++) {
            if (levelGoals[i].matchValue == "Slime") {
                levelGoals[i].numberNeeded+=1;
                UpdateGoals();
            }
        }
    }

    public void CompareGoal(string goalToCompare) {
        if (levelGoals != null) {
            for (int i = 0; i < levelGoals.Length; i++) {
                if (goalToCompare == levelGoals[i].matchValue) {
                    levelGoals[i].numberCollected++;
                }
            }
        }
    }
}
