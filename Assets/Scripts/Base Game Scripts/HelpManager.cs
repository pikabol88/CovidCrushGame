﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpManager : MonoBehaviour {
    public GameObject bubbleHelp;
    public GameObject lockHelp;
    public GameObject iceHelp;
    public GameObject stoneHelp;
    public GameObject[] roolHelp;
    public GameObject rulesHelp;

    public GameObject leftArrow;
    public GameObject rightArrow;

    public int firstBubbleLevel;
    public int firstIceLevel;
    public int firstLockLevel;
    public int firstStoneLevel;

    public GameObject currentRulePanel;
    public int page;


    public GameObject fadePanel;
    private bool isFirstLvl = false;
    private bool isBlockHint = false;
    // Start is called before the first frame update
    void Start() {
        leftArrow.SetActive(false);
    }


    public void PageRight() {
        if (page < roolHelp.Length - 1) {
            currentRulePanel.SetActive(false);
            page++;
            currentRulePanel = roolHelp[page];
            currentRulePanel.SetActive(true);
            if (page == roolHelp.Length - 1) {
                rightArrow.SetActive(false);
            } else {
                rightArrow.SetActive(true);
                leftArrow.SetActive(true);
            }
        }
    }

    public void PageLeft() {
        if (page > 0) {
            currentRulePanel.SetActive(false);
            page--;
            currentRulePanel = roolHelp[page];
            currentRulePanel.SetActive(true);
            if (page == 0) {
                leftArrow.SetActive(false);
            } else {
                leftArrow.SetActive(true);
                rightArrow.SetActive(true);
            }
        }
    }

    public void Home() {
        rulesHelp.SetActive(false);
        if (isBlockHint || isFirstLvl) {
            fadePanel.SetActive(true);
            isFirstLvl = false;
        }
    }


    public void ShowHelpPanel(int level) {
        Debug.Log(level);
        if (level == 0) {
            Debug.Log("SHOW");
            ShowRules();
            fadePanel.SetActive(false);
            isFirstLvl = true;
        }
        if (level == firstIceLevel) {
            ShowIceHelpPanel();
        } else if (level == firstLockLevel) {
            ShowLockHelpPanel();
        } else if (level == firstStoneLevel) {
            ShowStoneHelpPanel();
        } else if (level == firstBubbleLevel) {
            ShowBubbleHelpPanel();
        } else {
            DeactivateAll();
        }
    }

    public void ShowRules() {
        rulesHelp.SetActive(true);
        roolHelp[0].SetActive(true);
        for (int i = 1; i < roolHelp.Length; i++) {
            roolHelp[i].SetActive(false);
        }
        rightArrow.SetActive(true);
        leftArrow.SetActive(false);
    }

    private void ShowBubbleHelpPanel() {
        isBlockHint = true;
        fadePanel.SetActive(false);
        bubbleHelp.SetActive(true);
    }

    private void ShowIceHelpPanel() {
        isBlockHint = true;
        fadePanel.SetActive(false);
        iceHelp.SetActive(true);
    }

    private void ShowLockHelpPanel() {
        isBlockHint = true;
        fadePanel.SetActive(false);
        lockHelp.SetActive(true);
    }

    private void ShowStoneHelpPanel() {
        isBlockHint = true;
        fadePanel.SetActive(false);
        stoneHelp.SetActive(true);
    }

    private void DeactivateAll() {
        iceHelp.SetActive(false);
        stoneHelp.SetActive(false);
        lockHelp.SetActive(false);
        bubbleHelp.SetActive(false);
        isBlockHint = false;

    }
}

