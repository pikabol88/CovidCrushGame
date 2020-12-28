using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePanelController : MonoBehaviour
{
    public Animator panelAnim;
    public Animator winPanelAnim;
    public Animator tryPanelAnim;
    public Animator gameInfoAnim;
    public Animator iceHelpAnim;
    public Animator bubbleHelpAnim;
    public Animator stoneHelpAnim;
    public Animator lockHelpAnim;
    public Animator finalHelpAnim;

    public void OK() {
        if (panelAnim != null && gameInfoAnim != null) {
            panelAnim.SetBool("Out", true);
            gameInfoAnim.SetBool("Out", true);
            StartCoroutine(GameStartCo());
        }
    }

    public void FinalOk() {
        if (finalHelpAnim != null ) {
            finalHelpAnim.SetBool("Out", true);
            StartCoroutine(GameStartCo());
        }
    }

    public void IceOk() {
        if (iceHelpAnim != null ) {
           // panelAnim.SetBool("Out", true);
            iceHelpAnim.SetBool("Out", true);
            StartCoroutine(GameStartCo());
        }
    }

    public void BubbleOk() {
        if (bubbleHelpAnim != null) {
          //  panelAnim.SetBool("Out", true);
            bubbleHelpAnim.SetBool("Out", true);
            StartCoroutine(GameStartCo());
        }
    }

    public void LockOk() {
        if (lockHelpAnim != null) {
         //  panelAnim.SetBool("Out", true);
            lockHelpAnim.SetBool("Out", true);
            StartCoroutine(GameStartCo());
        }
    }

    public void StoneOk() {
        if (stoneHelpAnim != null) {
          //  panelAnim.SetBool("Out", true);
            stoneHelpAnim.SetBool("Out", true);
            StartCoroutine(GameStartCo());
        }
    }

    public void WinOK() {
        if (winPanelAnim != null ) {
            winPanelAnim.SetBool("Out", true);
            StartCoroutine(TryWinPanelWaitingCo());
          //  winPanelAnim.SetBool("Out", false);
        }
    }

    public void TryOK() {
        if (tryPanelAnim != null ) {
            tryPanelAnim.SetBool("Out", true);
            StartCoroutine(TryWinPanelWaitingCo());
          //  tryPanelAnim.SetBool("Out",false);
        }
    }

    public void GameOver() {
        panelAnim.SetBool("Out", false);
        panelAnim.SetBool("Game Over", true);
    }

    IEnumerator GameStartCo() {
        yield return new WaitForSeconds(1f);
        Board board = FindObjectOfType<Board>();
        board.currentState = GameState.MOVE;
    }

    IEnumerator TryWinPanelWaitingCo() {
        yield return new WaitForSeconds(5f);
    }
}
