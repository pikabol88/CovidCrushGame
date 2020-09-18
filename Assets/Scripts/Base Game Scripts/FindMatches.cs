using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//объединение листов
using System.Linq;


//Основно класс, отвечающий за обновление поля
public class FindMatches : MonoBehaviour {

    private Board board; //игровое поле
    public List<GameObject> currentMatches = new List<GameObject>(); //текущие совпадения

    
    void Start() {
        //тк имеется единтсвенный объект данного типа
        board = FindObjectOfType<Board>();       
    }

    public void FindAllMatches() {
        StartCoroutine(FindAllMatchesCo());
    }

    //
    private List<GameObject> isAdjacentBomb(Dot dot1, Dot dot2, Dot dot3) {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isAdjacentBomb) {
            currentMatches.Union(GetAdjacentPieces(dot1.column, dot1.row));
        }
        if (dot2.isAdjacentBomb) {
            currentMatches.Union(GetAdjacentPieces(dot2.column, dot2.row));
        }
        if (dot3.isAdjacentBomb) {
            currentMatches.Union(GetAdjacentPieces(dot3.column, dot3.row));
        }
        return currentDots;
    }
    
    private List<GameObject> IsRowBomb(Dot dot1, Dot dot2, Dot dot3) {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isRowBomb) {
            currentMatches.Union(GetRowPieces(dot1.row));
            board.BombRow(dot1.row);
        }
        if (dot2.isRowBomb) {
            currentMatches.Union(GetRowPieces(dot2.row));
            board.BombRow(dot2.row);
        }
        if (dot3.isRowBomb) {
            currentMatches.Union(GetRowPieces(dot3.row));
            board.BombRow(dot3.row);
        }
        return currentDots;
    }

    private List<GameObject> IsColomnBomb(Dot dot1, Dot dot2, Dot dot3) {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isColumnBomb) {
            currentMatches.Union(GetColumnPieces(dot1.column));
            board.BombColumn(dot1.column);
        }
        if (dot2.isColumnBomb) {
            currentMatches.Union(GetColumnPieces(dot2.column));
            board.BombColumn(dot2.column);
        }
        if (dot3.isColumnBomb) {
            currentMatches.Union(GetColumnPieces(dot3.column));
            board.BombColumn(dot3.column);
        }
        return currentDots;
    }

    private void AddToListAndMatch(GameObject dot) {
        if (!currentMatches.Contains(dot)) {
            currentMatches.Add(dot);
        }
        dot.GetComponent<Dot>().isMatched = true;
    }

    private void GetNearbyPieces(GameObject dot1, GameObject dot2, GameObject dot3) {
        AddToListAndMatch(dot1);
        AddToListAndMatch(dot2);
        AddToListAndMatch(dot3);
    }

    private IEnumerator FindAllMatchesCo() {
        //задержка перед поиском новых совпадений
        //  yield return new WaitForSeconds(.2f);
        yield return new WaitForEndOfFrame();
        for(int i = 0; i < board.width; i++) {
            for(int j = 0; j < board.height; j++) {
                GameObject currentDot = board.allDots[i, j];                
                if (currentDot != null) {
                    Dot currentDotDot = currentDot.GetComponent<Dot>();
                    if (i > 0 && i < board.width - 1) {
                        GameObject leftDot = board.allDots[i - 1, j];                       
                        GameObject rightDot = board.allDots[i + 1, j];                       
                        if (leftDot!=null && rightDot != null) {
                            Dot leftDotDot = leftDot.GetComponent<Dot>();
                            Dot rightDotDot = rightDot.GetComponent<Dot>();
                            
                            if (leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag) {
                                currentMatches.Union(IsRowBomb(leftDotDot, currentDotDot, rightDotDot));
                                currentMatches.Union(IsColomnBomb(leftDotDot, currentDotDot, rightDotDot));
                                currentMatches.Union(isAdjacentBomb(leftDotDot, currentDotDot, rightDotDot));
                                GetNearbyPieces(leftDot, currentDot, rightDot);
                            }
                        }
                    }

                    if (j > 0 && j < board.height - 1) {
                        GameObject upDot = board.allDots[i, j + 1];                       
                        GameObject downDot = board.allDots[i, j - 1];                       
                        if (upDot != null && downDot != null) {
                            Dot upDotDot = upDot.GetComponent<Dot>();
                            Dot downDotDot = downDot.GetComponent<Dot>();
                            if (upDot.tag == currentDot.tag && downDot.tag == currentDot.tag) {
                                currentMatches.Union(IsColomnBomb(upDotDot, currentDotDot, downDotDot));
                                currentMatches.Union(IsRowBomb( upDotDot, currentDotDot, downDotDot));
                                currentMatches.Union(isAdjacentBomb(upDotDot, currentDotDot, downDotDot));
                                GetNearbyPieces(upDot, currentDot, downDot);
                            }
                        }
                    }
                }
            }          
        }
    }


    //При взрыве цветной бомбы разбить все определённого цвета
    public void MatchPiecesOfColor(string color) {
        for(int i = 0; i < board.width; i++) {
            for(int j = 0; j < board.height; j++) {
                if (board.allDots[i, j] != null) {
                    if (board.allDots[i, j].tag == color) {
                        board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                    }
                }
            }
        }
    }

    //При взрыве круговой бомбы разбить все вокруг
    List<GameObject> GetAdjacentPieces(int column, int row) {
        List<GameObject> dots = new List<GameObject>();
        for(int i = column - 1; i <= column + 1; i++) {
            for(int j = row - 1; j <= row + 1; j++) {
                //Check if the piece is inside the board
                if (i >= 0 && i < board.width && j >= 0 && j < board.height){
                    if (board.allDots[i, j] != null) {
                        dots.Add(board.allDots[i, j]);
                        board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                    }
                }
            }
        }
        return dots;
    }

    //Добавить все элементы колонки
    List<GameObject> GetColumnPieces(int column) {
        List<GameObject> dots = new List<GameObject>();
        for (int k = 0; k < board.height; k++) {
            if (board.allDots[column, k] != null) {
                Dot dot = board.allDots[column, k].GetComponent<Dot>();
                if (dot.isRowBomb) {
                    dots.Union(GetRowPieces(k)).ToList();
                }
                dots.Add(board.allDots[column, k]);
                dot.isMatched = true;
            }
        }
        return dots;
    }

    //Добавить все элементы строки
    List<GameObject> GetRowPieces(int row) {
        List<GameObject> dots = new List<GameObject>();
        for (int k = 0; k < board.width; k++) {
            if (board.allDots[k, row]!=null) {
                Dot dot = board.allDots[k, row].GetComponent<Dot>();
                if (dot.isColumnBomb) {
                    dots.Union(GetColumnPieces(k)).ToList();
                }
                dots.Add(board.allDots[k, row]);
                dot.isMatched = true;
            }
        
        }
        return dots;
    }

    
    private void CheckBombAndMake(Dot dot) {
            dot.isMatched = false;
            if ((dot.swipeAngle > -45 && dot.swipeAngle <= 45)
                 || (dot.swipeAngle > -135 && dot.swipeAngle >= 135)) {
               dot.MakeRowBomb();
            } else  {
              dot.MakeColumnBomb();
            }
        
    }

    public void CheckBombs() {
        //Did the player move smth?
        if(board.currentDot != null) {
            //Is the piece they moved matched?
            if (board.currentDot.isMatched) {
                CheckBombAndMake(board.currentDot);
            }
            //Is the other piece matched?
            else if (board.currentDot.otherDot != null) {
                Dot otherDot = board.currentDot.otherDot.GetComponent<Dot>();
                if (otherDot.isMatched) {
                    CheckBombAndMake(otherDot);
                }
            }
        }
    }

    public void CheckBombs(MatchType matchType) {
        //Did the player move something?
        if (board.currentDot != null) {
            //Is the piece they moved matched?
            if (board.currentDot.isMatched && board.currentDot.tag == matchType.color) {
                //make it unmatched
                board.currentDot.isMatched = false;
                //Decide what kind of bomb to make
                /*
                int typeOfBomb = Random.Range(0, 100);
                if(typeOfBomb < 50){
                    //Make a row bomb
                    board.currentDot.MakeRowBomb();
                }else if(typeOfBomb >= 50){
                    //Make a column bomb
                    board.currentDot.MakeColumnBomb();
                }
                */
                if ((board.currentDot.swipeAngle > -45 && board.currentDot.swipeAngle <= 45)
                   || (board.currentDot.swipeAngle < -135 || board.currentDot.swipeAngle >= 135)) {
                    board.currentDot.MakeRowBomb();
                } else {
                    board.currentDot.MakeColumnBomb();
                }
            }
            //Is the other piece matched?
            else if (board.currentDot.otherDot != null) {
                Dot otherDot = board.currentDot.otherDot.GetComponent<Dot>();
                //Is the other Dot matched?
                if (otherDot.isMatched && otherDot.tag == matchType.color) {
                    //Make it unmatched
                    otherDot.isMatched = false;
                    /*
                    //Decide what kind of bomb to make
                    int typeOfBomb = Random.Range(0, 100);
                    if (typeOfBomb < 50)
                    {
                        //Make a row bomb
                        otherDot.MakeRowBomb();
                    }
                    else if (typeOfBomb >= 50)
                    {
                        //Make a column bomb
                        otherDot.MakeColumnBomb();
                    }
                    */
                    if ((board.currentDot.swipeAngle > -45 && board.currentDot.swipeAngle <= 45)
                   || (board.currentDot.swipeAngle < -135 || board.currentDot.swipeAngle >= 135)) {
                        otherDot.MakeRowBomb();
                    } else {
                        otherDot.MakeColumnBomb();
                    }
                }
            }

        }
    }
}
