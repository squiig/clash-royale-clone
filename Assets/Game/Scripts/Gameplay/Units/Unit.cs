using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CRC
{
    [CreateAssetMenu(fileName = "New Unit Definition")]
    public class UnitDefinition : ScriptableObject
    {
        [SerializeField]
        private string m_UnitName;
        public string Name { get { return m_UnitName; } }

        [SerializeField]
        private float m_MovementSpeed;
        public float MovementSpeed { get { return m_MovementSpeed; } }

        [SerializeField]
        private float m_Damage;
        public float Damage { get { return m_Damage; } }

        [SerializeField]
        private float m_DamageInterval;
        public float DamageInterval { get { return m_DamageInterval; } }
    }

    public class Unit : Damageable
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

        [SerializeField]
        private NavMeshAgent m_Agent;

        private bool m_Enabled;

        private UnitState m_State;

        void Awake()
        {
            m_State = UnitState.Idle;
        }

        public void Initialize(KingTower owner)
        {
            m_Owner = owner;

            m_Renderer.material.color = m_Owner.Definition.Color;

            m_State = UnitState.Idle;
            m_CurrentHealth = m_MaxHealth;

            m_Enabled = true;
        }

        void Update()
        {
            if (!m_Enabled)
                return;

            StartCoroutine(Walk());
        }

        private IEnumerator Walk()
        {
            yield return new WaitForSeconds(0.5f);

            Damageable nearest = null;
            float range = Mathf.Infinity;

            Damageable[] damageables = FindObjectsOfType<Damageable>();
            for (int i = 0; i < damageables.Length; i++)
            {
                Damageable d = damageables[i];

                if (d.Owner != m_Owner)
                {
                    float dist = Vector3.Distance(this.transform.position, d.transform.position);

                    if (dist < range)
                    {
                        nearest = d;
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
