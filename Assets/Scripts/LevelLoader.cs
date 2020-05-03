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
    public void LoadLevel(int id)
    {
        StartCoroutine(Load(id));
    }
    public void LoadNextLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ReloadLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator Load(int id)
    {
        animator.SetTrigger("Go");
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene(id);
    }

}
