using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRC
{
    public class KingTower : Tower
    {
        [SerializeField]
        private KingTowerDefinition m_Definition;
        public KingTowerDefinition Definition { get { return m_Definition; } }

        [SerializeField]
        private Renderer m_Renderer;

        protected override void Awake()
        {
            base.Awake();

            m_Renderer.material.color = m_Definition.Color;
        }
    }
}
