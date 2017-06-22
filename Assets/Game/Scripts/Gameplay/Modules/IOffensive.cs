using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRC
{
    public interface IOffensive
    {
        KingTower Owner { get; }

        float Damage { get; }
        float AttackDelay { get; }
        float InitialAttackDelay { get; }

        ColliderEventGroup DefenseArea { get; }
    }

    public abstract class Offensive : Damageable, IOffensive
    {
        public abstract float Damage { get; }
        public abstract float AttackDelay { get; }
        public abstract float InitialAttackDelay { get; }

        [SerializeField]
        protected ColliderEventGroup m_DefenseArea;
        public ColliderEventGroup DefenseArea { get { return m_DefenseArea; } }

        protected bool m_IsAttacking;
        public bool IsAttacking { get { return m_IsAttacking; } }

        protected virtual void Start()
        {
            m_DefenseArea.TriggerEnterEvent += OnTriggerEnter;
        }

        protected virtual void OnDestroy()
        {
            m_DefenseArea.TriggerEnterEvent -= OnTriggerEnter;
        }

        protected virtual void Attack(Damageable target)
        {
            m_IsAttacking = true;

            StartCoroutine(AttackRoutine(target));
        }

        protected virtual IEnumerator AttackRoutine(Damageable target)
        {
            yield return new WaitForSeconds(InitialAttackDelay);

            for (;;)
            {
                target.Hurt(Damage);

                Debug.Log(this.gameObject.name + " attacked " + target.name + " by " + Damage + "!");

                yield return new WaitForSeconds(AttackDelay);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (m_IsAttacking)
                return;

            Damageable target = other.GetComponent<Damageable>();

            if (target == null)
                return;

            if (target.Owner == m_Owner)
                return;

            Attack(target);
        }

        private void OnTriggerExit(Collider other)
        {
            m_IsAttacking = false;

            StopAllCoroutines();
        }
    }
}
