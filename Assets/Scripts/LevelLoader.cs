using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{

    public GameObject LoadingPanel;
    public Image ProgressBar;
    public void LoadLevel(int index)
    {
        StartCoroutine(Load(index));
    }

    IEnumerator Load(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        LoadingPanel.SetActive(true);
        while (!operation.isDone)
        {
            ProgressBar.fillAmount = operation.progress;
            yield return null;
        }
        LoadingPanel.SetActive(false);
    }
}
