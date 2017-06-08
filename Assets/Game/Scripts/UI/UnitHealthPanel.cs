using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRC
{
    public class UnitHealthPanel : MonoBehaviour
    {
        private Unit m_Owner;

        private float m_HPBarYOffPx;

        public void Initialize(Unit owner, float hpBarYOffPx)
        {
            m_Owner = owner;
            m_HPBarYOffPx = hpBarYOffPx;
        }

        void Update()
        {
            if (m_Owner != null)
                FollowOwner();
        }

        private void FollowOwner()
        {
            Vector3 pos = this.transform.position;
            pos = Camera.main.WorldToScreenPoint(m_Owner.transform.position) + Vector3.up * m_HPBarYOffPx;
            pos.z = .0f;
            this.transform.position = pos;
        }
    }
}
