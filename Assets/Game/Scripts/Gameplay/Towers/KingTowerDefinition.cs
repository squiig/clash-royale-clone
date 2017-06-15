using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRC
{
    [CreateAssetMenu(fileName = "New King Tower Type")]
    public class KingTowerDefinition : ScriptableObject
    {
        [SerializeField]
        private string m_Tag;

        [SerializeField]
        private Color m_Color;
        public Color Color { get { return m_Color; } }
    }
}
