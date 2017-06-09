using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRC
{
    public class SpawningHandler : Singleton<SpawningHandler>
    {
        [SerializeField]
        private KingTower m_KingTowerOne, m_KingTowerTwo;
        public KingTower KingTowerOne { get { return m_KingTowerOne; } }
        public KingTower KingTowerTwo { get { return m_KingTowerTwo; } }

        [SerializeField]
        private Unit m_UnitPrefab;

        [SerializeField]
        private LayerMask m_SpawnableLayer;

        [SerializeField]
        private Transform m_Container;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo;
                bool hitSuccess = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, m_SpawnableLayer);

                if (hitSuccess)
                {
                    Unit unit = Instantiate
                    (
                        m_UnitPrefab.gameObject,
                        hitInfo.point + Vector3.up * m_UnitPrefab.transform.lossyScale.y,
                        Quaternion.identity
                    )
                    .GetComponent<Unit>();

                    if (unit == null)
                        return;

                    unit.transform.SetParent(m_Container, true);
                    unit.Initialize(m_KingTowerOne);
                }
            }
        }
    }
}
