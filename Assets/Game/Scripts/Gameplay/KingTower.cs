using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRC
{
    [CreateAssetMenu(fileName = "New Player Type")]
    public class PlayerDefinition : ScriptableObject
    {
        [SerializeField]
        private string m_Tag;

        [SerializeField]
        private Color m_Color;
        public Color Color { get { return m_Color; } }
    }

    public class KingTower : Tower
    {
        [SerializeField]
        private PlayerDefinition m_Definition;
        public PlayerDefinition Definition { get { return m_Definition; } }

        [SerializeField]
        private Renderer m_Renderer;

        protected override void Awake()
        {
            base.Awake();

            m_Renderer.material.color = m_Definition.Color;
        }
    }
}
