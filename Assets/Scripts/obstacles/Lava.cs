using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace obstacles
{
    public class Lava : MonoBehaviour
    {

        [SerializeField]
        private float hurtAmount = 5f;
        private float m_cooldown = 0.5f;

        public GameObject robo;
        private float m_cooldownTimer = 0.5f;

        private void Update()
        {
            if(m_cooldownTimer > 0 )
            {
                m_cooldownTimer -= Time.deltaTime;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (m_cooldown <= 0)
            {
                if (other.transform.gameObject.CompareTag("Player"))
                {
                    behavior.Life l_roboScript = robo.GetComponent<behavior.Life>();
                    l_roboScript.lose(hurtAmount);
                    m_cooldownTimer = m_cooldown;
                }
            }
        }
    }
}