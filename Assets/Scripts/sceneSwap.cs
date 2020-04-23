using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneSwap : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 1f;
    int currentSceneIndex;
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGameOverScene()
    {
        //int CDScene = 1;
        //yield return new WaitForSeconds(CDScene);
        //FindObjectOfType<Player>().ResetGame();
        StartCoroutine(WaitAndLoad());
        //SceneManager.LoadScene("GameOverScene");
    }
    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(2);
    }    
}