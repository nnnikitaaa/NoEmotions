using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance { get; set; }
    static Animator animator;
    private void Awake()
    {
        #region Getting
        instance = this;
        animator = GetComponent<Animator>();

        #endregion
    }
    public void LoadLevel(int id, float timeToWait)
    {
        StartCoroutine(Load(id, timeToWait));
    }
    public void LoadNextLevel(float timeToWait)
    {
        int currentId = SceneManager.GetActiveScene().buildIndex;
        if (currentId >= SceneManager.sceneCountInBuildSettings - 1)
        {
            LoadLevel(1, timeToWait);

        }
        else
        {
            LoadLevel(SceneManager.GetActiveScene().buildIndex + 1, timeToWait);
        }

    }
    public void ReloadLevel(float timeToWait)
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex, timeToWait);
    }
    IEnumerator Load(int id, float timeToWait)
    {
        if (timeToWait < 1)
        {
            timeToWait = 1;
        }
        yield return new WaitForSeconds(timeToWait - 1);
        animator.SetTrigger("Go");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(id);
    }

}
