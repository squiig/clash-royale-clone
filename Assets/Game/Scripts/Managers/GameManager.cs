using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : DDOLSingleton<GameManager>
{
    /// <summary>
    /// This function is called first thing in the entire script.
    /// </summary>
    public override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// This function is called after the Awake function.
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// This function is called every frame.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
