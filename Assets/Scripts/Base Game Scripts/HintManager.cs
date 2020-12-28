using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    private Board board;
    public float hintDelay;
    public float hintDelaySeconds;
    public GameObject hintParticle;
    public GameObject currentHint;

    // Start is called before the first frame update
    void Start() {
        board = FindObjectOfType<Board>();
        hintDelaySeconds = hintDelay;
    }

    // Update is called once per frame
    public void Update() {
        hintDelaySeconds -= Time.deltaTime;
        if (hintDelaySeconds <= 0 && currentHint == null) {
            MarkHint();
            hintDelaySeconds = hintDelay;
        }
    }

    ////Найти все возможные ходы 
    //List<GameObject> FindAllMatches() {
    //    List<GameObject> possibleMoves = new List<GameObject>();
    //    for (int i = 0; i < board.width; i++) {
    //        for (int j = 0; j < board.height; j++) {
    //            if (board.allDots[i, j] != null) {
    //                if (i < board.width - 1) {
    //                    if (board.SwitchAndCheck(i, j, Vector2Int.right)) {
    //                        //ВОТ ТУТ ИЗМЕНИЛА
    //                        if (!board.concreteTiles[i, j] && !board.lockTiles[i, j] && !board.slimeTiles[i, j]){
    //                            possibleMoves.Add(board.allDots[i, j]);
    //                        }
    //                    }
    //                }
    //                if (j < board.height - 1) {
    //                    if (board.SwitchAndCheck(i, j, Vector2Int.up)) {
    //                        //ВОТ ТУТ ИЗМЕНИЛА
    //                        if (!board.concreteTiles[i, j] && !board.lockTiles[i, j] && !board.slimeTiles[i, j]) {
    //                            possibleMoves.Add(board.allDots[i, j]);
    //                        }
    //                     //   possibleMoves.Add(board.allDots[i, j]);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    return possibleMoves;
    //}

    //ВТОРОЙ ВАРИАНТ
    //Найти все возможные ходы 
    List<GameObject> FindAllMatches() {
        List<GameObject> possibleMoves = new List<GameObject>();
        for (int i = 0; i < board.width; i++) {
            for (int j = 0; j < board.height; j++) {
                if (board.allDots[i, j] != null) {
                    if (i < board.width - 1) {
                        if (board.SwitchAndCheck(i, j, Vector2Int.right)) {
                            //ВОТ ТУТ ИЗМЕНИЛА
                            if (!board.concreteTiles[i +1, j] && !board.lockTiles[i+1, j] && !board.slimeTiles[i+1, j]) {
                                possibleMoves.Add(board.allDots[i + 1, j]);

                            }
                        }
                    }
                    if (j < board.height - 1) {
                        if (board.SwitchAndCheck(i, j, Vector2Int.up)) {
                            //ВОТ ТУТ ИЗМЕНИЛА
                            if (!board.concreteTiles[i, j+1] && !board.lockTiles[i, j+1] && !board.slimeTiles[i, j+1]) {
                                possibleMoves.Add(board.allDots[i, j + 1]);
                            }
                            //   possibleMoves.Add(board.allDots[i, j]);
                        }
                    }

                    if (i > 0) {
                        if (board.SwitchAndCheck(i, j, Vector2Int.left)) {
                            //ВОТ ТУТ ИЗМЕНИЛА
                            if (!board.concreteTiles[i-1, j] && !board.lockTiles[i-1, j] && !board.slimeTiles[i-1, j]) {
                                possibleMoves.Add(board.allDots[i - 1, j]);

                            }
                        }
                    }
                    if (j > 0) {
                        if (board.SwitchAndCheck(i, j, Vector2Int.down)) {
                            //ВОТ ТУТ ИЗМЕНИЛА
                            if (!board.concreteTiles[i, j-1] && !board.lockTiles[i, j-1] && !board.slimeTiles[i, j-1]) {
                                possibleMoves.Add(board.allDots[i, j - 1]);
                            }
                            //   possibleMoves.Add(board.allDots[i, j]);
                        }
                    }
                }
            }
        }
        return possibleMoves;
    }

    //выбрать один рандомно
    private GameObject PickOneMatch() {
        List<GameObject> possibleMoves = new List<GameObject>();
        possibleMoves = FindAllMatches();
        if (possibleMoves.Count > 0) {
            int pieceToUse = Random.Range(0, possibleMoves.Count);
            return possibleMoves[pieceToUse];
        } else {
            board.currentState = GameState.PAUSE;
            StartCoroutine(board.ShuffleBoard());
        }
        return null; 
    }

    //Создать подсказку
    private void MarkHint() {
        GameObject move = PickOneMatch();
        if (move != null) {
            currentHint = Instantiate(hintParticle, move.transform.position, Quaternion.identity);
        }
    }
    //Удаление подсказки
    public void DestroyHint() {
        if (currentHint != null) {
            Destroy(currentHint);
            currentHint = null;
            hintDelaySeconds = hintDelay;
        }
    }
}
