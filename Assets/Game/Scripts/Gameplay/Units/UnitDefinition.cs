using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        [Header("Offense")]

        [SerializeField]
        private float m_Damage;
        public float Damage { get { return m_Damage; } }

        [SerializeField]
        private float m_AttackDelay;
        public float AttackDelay { get { return m_AttackDelay; } }

        [SerializeField]
        private float m_InitialAttackDelay;
        public float InitialAttackDelay { get { return m_InitialAttackDelay; } }
    }
}
