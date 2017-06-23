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

        private List<Damageable> m_TargetsInRange = new List<Damageable>();

        protected override void Start()
        {
            base.Start();

            m_DefenseArea.TriggerEnterEvent += OnTriggerEnter;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            m_DefenseArea.TriggerEnterEvent -= OnTriggerEnter;
        }

        protected virtual void Attack(Damageable target)
        {
            if (target == null)
                return;

            if (target.IsDead)
                return;

            m_IsAttacking = true;

            StopAllCoroutines();

            StartCoroutine(AttackRoutine(target));
        }

        protected virtual IEnumerator AttackRoutine(Damageable target)
        {
            yield return new WaitForSeconds(InitialAttackDelay);

            for (;;)
            {
                if (m_IsDead)
                    yield return null;

                target.Hurt(Damage);

                if (target.IsDead)
                {
                    m_TargetsInRange.Remove(target);

                    if (m_TargetsInRange.Count > 0)
                        Attack(GetClosestTarget(m_TargetsInRange));
                }

                yield return new WaitForSeconds(AttackDelay);
            }
        }

        private Damageable GetClosestTarget(List<Damageable> targets)
        {
            Damageable result = null;

            float distOld = Mathf.Infinity;

            foreach (Damageable target in targets)
            {
                if (target.IsDead)
                    continue;

                float distNew = Vector3.Distance(this.transform.position, target.transform.position);

                if (distNew < distOld)
                {
                    distOld = distNew;
                    result = target;
                }
            }

            return result;
        }

        private void OnTriggerEnter(Collider other)
        {
            Damageable target = other.GetComponent<Damageable>();

            if (target == null)
                return;

            if (target.Owner == m_Owner)
                return;

            if (!m_TargetsInRange.Contains(target))
                m_TargetsInRange.Add(target);

            if (m_IsAttacking)
                return;

            Attack(target);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!m_IsAttacking)
                return;

            Damageable target = other.GetComponent<Damageable>();

            if (target == null)
                return;

            if (target.Owner == m_Owner)
                return;

            m_IsAttacking = false;

            StopAllCoroutines();
        }
    }
}
