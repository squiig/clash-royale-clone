using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CRC
{
    public class Tower : Offensive
    {
        [SerializeField]
        protected TextMeshProUGUI m_HPText;

        [SerializeField]
        protected Renderer m_Renderer;

        [SerializeField]
        protected float m_Damage;
        public override float Damage { get { return m_Damage; } }

        [SerializeField]
        protected float m_AttackDelay;
        public override float AttackDelay { get { return m_AttackDelay; } }

        [SerializeField]
        protected float m_InitialAttackDelay;
        public override float InitialAttackDelay { get { return m_InitialAttackDelay; } }

        protected virtual void Awake()
        {
            m_CurrentHealth = m_MaxHealth;
            m_HPText.text = m_CurrentHealth.ToString();
        }

        protected override void Start()
        {
            base.Start();

            HealthChangeEvent += OnHealthChange;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            HealthChangeEvent -= OnHealthChange;
        }

        protected virtual void OnHealthChange()
        {
            m_HPText.text = m_CurrentHealth.ToString();
        }

        protected override void OnDeath()
        {
            Destroy(m_HPText.transform.parent.gameObject);

            base.OnDeath();
        }
    }
}
