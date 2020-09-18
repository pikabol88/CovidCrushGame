using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanel : MonoBehaviour
{
    public GameObject loadingPanel;
    public GameObject[] VirusesLine;

    public void ActivatePanel() {
        loadingPanel.SetActive(true);
        for(int i = 0; i < VirusesLine.Length; i++) {
            VirusesLine[i].SetActive(false);
        }
        StartCoroutine(ActivateCo());
    }

    private IEnumerator ActivateCo() {
        for(int i = 0; i < VirusesLine.Length; i++) {            
            VirusesLine[i].SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void DeactivatePanel() {        
        for (int i = 0; i < VirusesLine.Length; i++) {
            VirusesLine[i].SetActive(false);
        }
        loadingPanel.SetActive(false);
    }
}
