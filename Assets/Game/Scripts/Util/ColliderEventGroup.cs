using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRC
{
    public delegate void TriggerEventHandler(Collider other);
    public delegate void CollisionEventHandler(Collision coll);
    public class ColliderEventGroup : MonoBehaviour
    {
        private event TriggerEventHandler m_TriggerEnterEvent;
        private event TriggerEventHandler m_TriggerStayEvent;
        private event TriggerEventHandler m_TriggerExitEvent;

        private event CollisionEventHandler m_CollisionEnterEvent;
        private event CollisionEventHandler m_CollisionStayEvent;
        private event CollisionEventHandler m_CollisionExitEvent;

        public TriggerEventHandler TriggerEnterEvent { get { return m_TriggerEnterEvent; } set { m_TriggerEnterEvent = value; } }
        public TriggerEventHandler TriggerStayEvent { get { return m_TriggerStayEvent; } set { m_TriggerStayEvent = value; } }
        public TriggerEventHandler TriggerExitEvent { get { return m_TriggerExitEvent; } set { m_TriggerExitEvent = value; } }

        public CollisionEventHandler CollisionEnterEvent { get { return m_CollisionEnterEvent; } set { m_CollisionEnterEvent = value; } }
        public CollisionEventHandler CollisionStayEvent { get { return m_CollisionStayEvent; } set { m_CollisionStayEvent = value; } }
        public CollisionEventHandler CollisionExitEvent { get { return m_CollisionExitEvent; } set { m_CollisionExitEvent = value; } }

        void OnTriggerEnter(Collider other)
        {
            if (m_TriggerEnterEvent != null)
                m_TriggerEnterEvent(other);
        }

        void OnTriggerStay(Collider other)
        {
            if (m_TriggerStayEvent != null)
                m_TriggerExitEvent(other);
        }

        void OnTriggerExit(Collider other)
        {
            if (m_TriggerExitEvent != null)
                m_TriggerExitEvent(other);
        }

        void OnCollisionEnter(Collision coll)
        {
            if (m_CollisionEnterEvent != null)
                m_CollisionEnterEvent(coll);
        }

        void OnCollisionStay(Collision coll)
        {
            if (m_CollisionStayEvent != null)
                m_CollisionStayEvent(coll);
        }

        void OnCollisionExit(Collision coll)
        {
            if (m_CollisionExitEvent != null)
                m_CollisionExitEvent(coll);
        }
    }
}
