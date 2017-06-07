using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
	[SerializeField]
	private GameManager m_GameManager;

	[SerializeField]
	private AudioManager m_SoundManager;

	void Awake()
	{
		if (GameManager.Instance == null) Instantiate(m_GameManager.gameObject);
		if (AudioManager.Instance == null) Instantiate(m_SoundManager.gameObject);

		Destroy(gameObject);
	}
}
