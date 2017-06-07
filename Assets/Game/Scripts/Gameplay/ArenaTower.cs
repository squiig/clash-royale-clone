using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRC
{
    public class ArenaTower : Tower
    {
        [SerializeField]
        private Renderer m_Renderer;

        void Awake()
        {
            m_Renderer.material.color = m_Owner.Definition.Color;
        }
    }
}
