using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject canvasPanel;
    public GameObject levelPanel;
    public static GameObject staticStartPanel;
    public static GameObject staticLevelSelect;
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(canvasPanel);
        DontDestroyOnLoad(startPanel);
        DontDestroyOnLoad(levelPanel);
        staticStartPanel = startPanel;
        staticLevelSelect = levelPanel;

        startPanel.SetActive(true);
        staticStartPanel = startPanel;
        levelPanel.SetActive(false);
    }


    public void PlayGame() {
        if (startPanel != null) {
            startPanel.SetActive(false);
        }
        levelPanel.SetActive(true);
    }

    public IEnumerator PlayGameCo() {
        if (startPanel != null) {
            startPanel.SetActive(false);
        }
        levelPanel.SetActive(true);
        yield return new WaitForSeconds(2f);

    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
