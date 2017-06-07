using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    /// <summary>
    /// This function is called after the Awake function.
    /// </summary>
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
