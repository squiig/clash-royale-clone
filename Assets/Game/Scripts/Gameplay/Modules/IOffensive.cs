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
        float AttackDelayInitial { get; }

        Collider DefenseArea { get; }
    }

    public abstract class Offensive : Damageable, IOffensive
    {
        [SerializeField]
        private float m_Damage;
        public float Damage { get { return m_Damage; } }

        [SerializeField]
        private float m_AttackDelay;
        public float AttackDelay { get { return m_AttackDelay; } }

        [SerializeField]
        private float m_AttackDelayInitial;
        public float AttackDelayInitial { get { return m_AttackDelayInitial; } }

        [SerializeField]
        private Collider m_DefenseArea;
        public Collider DefenseArea { get { return m_DefenseArea; } }

        protected virtual void Attack(Damageable target)
        {
            StartCoroutine(AttackRoutine(target));
        }

        protected virtual IEnumerator AttackRoutine(Damageable target)
        {
            yield return new WaitForSeconds(m_AttackDelayInitial);

            for (;;)
            {
                target.Hurt(m_Damage);

                yield return new WaitForSeconds(m_AttackDelay);
            }
        }
    }
}
