using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRC
{
    public class Tower : Damageable
    {
        [SerializeField]
        private Text m_HPText;

        [SerializeField]
        private Collider m_DefenseArea;

        protected virtual void Awake()
        {
            m_DefenseArea = GetComponent<Collider>();

            m_CurrentHealth = m_MaxHealth;
            m_HPText.text = m_CurrentHealth.ToString();
        }

        protected virtual void Start()
        {
            HealthChangeEvent += OnHealthChange;
        }

        protected virtual void OnDestroy()
        {
            HealthChangeEvent -= OnHealthChange;
        }

        protected virtual void OnHealthChange()
        {
            m_HPText.text = m_CurrentHealth.ToString();
        }
    }
}
