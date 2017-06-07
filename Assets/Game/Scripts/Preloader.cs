using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{
	void Start()
	{
		SceneManager.LoadScene(1);
	}
}
