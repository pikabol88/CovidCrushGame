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
        Debug.Log(staticStartPanel);
        //startPanel = GameObject.FindGameObjectWithTag("Start Panel");
        //levelPanel = GameObject.FindGameObjectWithTag("Level Select");
        startPanel.SetActive(true);
        staticStartPanel = startPanel;
        levelPanel.SetActive(false);
    }


    public void PlayGame() {
        Debug.Log(startPanel);
        Debug.Log(levelPanel);
        if (startPanel != null) {
            startPanel.SetActive(false);
            Debug.Log("startPanel = false");
        }
        levelPanel.SetActive(true);
    }

    public IEnumerator PlayGameCo() {
        Debug.Log(startPanel);
        Debug.Log(levelPanel);
        if (startPanel != null) {
            startPanel.SetActive(false);
            Debug.Log("startPanel = false");
        }
        levelPanel.SetActive(true);
        yield return new WaitForSeconds(2f);

    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
