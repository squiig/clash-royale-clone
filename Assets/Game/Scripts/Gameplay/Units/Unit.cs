using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace CRC
{
    public class Unit : Offensive
    {
        public enum UnitState
        {
            Idle = 0,
            Moving,
            Attacking
        }

        [SerializeField]
        private UnitDefinition m_Definition;
        public UnitDefinition Definition { get { return m_Definition; } }

        [SerializeField]
        private Renderer m_Renderer;

        [Header("Navigation")]

        [SerializeField]
        private NavMeshAgent m_Agent;

        [Header("Healthbar")]

        [SerializeField]
        private GameObject m_HealthPanelPrefab;

        [SerializeField]
        private float m_HPBarYOffPx = 50.0f;

        private Image m_HPBarForeground;

        private bool m_Enabled;

        private UnitState m_State;

        private float m_Damage;
        public override float Damage { get { return m_Damage; } }

        private float m_AttackDelay;
        public override float AttackDelay { get { return m_AttackDelay; } }

        private float m_InitialAttackDelay;
        public override float InitialAttackDelay { get { return m_InitialAttackDelay; } }

        void Awake()
        {
            m_State = UnitState.Idle;
        }

        public void Initialize(KingTower owner)
        {
            m_Owner = owner;

            // Assign unit definition variables
            m_Damage = m_Definition.Damage;
            m_AttackDelay = m_Definition.AttackDelay;
            m_InitialAttackDelay = m_Definition.InitialAttackDelay;
            m_Agent.speed = m_Definition.MovementSpeed;

            // Assign player colour
            m_Renderer.material.color = m_Owner.Definition.Color;

            // Reset health
            m_CurrentHealth = m_MaxHealth;

            // Instantiate health panel
            GameObject healthPanel = Instantiate
            (
                m_HealthPanelPrefab,
                Camera.main.WorldToScreenPoint(this.transform.position) + Vector3.up * m_HPBarYOffPx,
                Quaternion.identity,
                GameObject.Find("HUD").transform
            );

            healthPanel.GetComponent<UnitHealthPanel>().Initialize(this, m_HPBarYOffPx);

            m_HPBarForeground = healthPanel.transform.GetChild(1).GetComponent<Image>();
            m_HPBarForeground.color = m_Owner.Definition.Color;

            HealthChangeEvent += OnHealthChange;

            m_Enabled = true;
        }

        void Update()
        {
            if (!m_Enabled)
                return;

            StartCoroutine(Walk());
        }

        protected override void OnDestroy()
        {
            HealthChangeEvent -= OnHealthChange;
        }

        private void OnHealthChange()
        {
            m_HPBarForeground.fillAmount = m_CurrentHealth / m_MaxHealth;
        }

        private IEnumerator Walk()
        {
            yield return new WaitForSeconds(0.5f);

            Damageable nearest = null;
            float range = Mathf.Infinity;

            Damageable[] damageables = FindObjectsOfType<Damageable>();
            for (int i = 0; i < damageables.Length; i++)
            {
                Damageable target = damageables[i];

                if (target.Owner != m_Owner)
                {
                    float dist = Vector3.Distance(this.transform.position, target.transform.position);

                    if (dist < range)
                    {
                        nearest = target;
                        range = dist;
                    }
                }
            }

            if (nearest != null)
            {
                m_Agent.SetDestination(nearest.transform.position);
                m_State = UnitState.Moving;
            }
        }
    }
}
