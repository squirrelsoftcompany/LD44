/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class FenwickIA :
    MonoBehaviour
{
    #region Sub-classes/enum
    /***************************************************/
    /***  SUB-CLASSES/ENUM      ************************/
    /***************************************************/

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
    #region Property
    /***************************************************/
    /***  PROPERTY              ************************/
    /***************************************************/

    #endregion
    #region Constants
    /***************************************************/
    /***  CONSTANTS             ************************/
    /***************************************************/

    #endregion
    #region Attributes
    /***************************************************/
    /***  ATTRIBUTES            ************************/
    /***************************************************/
    public float m_forceSpeed = 5;
    public float m_forceSpeedRage = 10;
    public GameObject m_player;
    public GameObject m_spot;
    public float m_rangeDetection = 40;
    public float m_maxPathLenghtHunt = 10;
    public float m_rageTime = 10;

    private Rigidbody m_rb;
    private float m_currentForceSpeed;
    private float m_rageTimeLeft;
    private LayerMask layerMask;
    private bool m_huntPlayer = false;

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        m_currentForceSpeed = m_forceSpeed;
        m_rageTimeLeft = m_rageTime;
        agent.destination = m_spot.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        NavMeshPath l_path = new NavMeshPath();
        agent.stoppingDistance = m_maxPathLenghtHunt;
        //Compute if fenwick should follow the player
        if (!agent.pathPending && agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            if (m_huntPlayer)
            {
                if (agent.remainingDistance == Mathf.Infinity || agent.remainingDistance < m_maxPathLenghtHunt)
                {
                    agent.destination = m_player.transform.position;
                }
                else
                {
                    m_huntPlayer = false;
                }
            }
            else
            {
                agent.destination = m_spot.transform.position;
            }
        }

        //Detecte if fenwick have direct vision on player to trigger rage mode
        int l_layerMask = ~ (1 << LayerMask.NameToLayer("Enemy")) ;
        bool l_activeRageMode = false;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, (m_player.transform.position - transform.position), out hit, m_rangeDetection, l_layerMask) )
        {
            if (hit.transform.tag == "Player")
            {
                l_activeRageMode = true;
            }
        }

        if(l_activeRageMode)
        {
            m_currentForceSpeed = m_forceSpeedRage;
            m_rageTimeLeft = m_rageTime;
            m_huntPlayer = true;
        }
        else
        {
            m_rageTimeLeft -= Time.deltaTime;
            if (m_rageTimeLeft < 0)
            {
                m_currentForceSpeed = m_forceSpeed;
                m_huntPlayer = false;
            }
        }
    }

    void FixedUpdate()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        Vector3 l_currentVelocity = agent.velocity;
        Vector3 l_orientation = Vector3.Normalize(l_currentVelocity);

        //Contrain velocity direction to follow only one axis at same time
        Vector3 l_correctOrientation = l_orientation;
        if (Mathf.Abs(l_orientation.x) >= Mathf.Abs(l_orientation.z))
        {
            l_correctOrientation = new Vector3(l_orientation.x, l_orientation.y, 0);
        }
        if (Mathf.Abs(l_orientation.z) > Mathf.Abs(l_orientation.x))
        {
            l_correctOrientation = new Vector3(0, l_orientation.y, l_orientation.z);
        }

        //Manual management of velocity (override Unity physique !)
        Vector3 l_targetDirection = agent.steeringTarget - this.transform.position;
        if (Vector3.Dot(this.transform.forward, l_targetDirection) < 0)
        {
            if(agent.velocity.magnitude > 0.1)
            {
                agent.velocity = agent.velocity - (Vector3.Normalize(agent.velocity) * Mathf.Clamp(agent.velocity.magnitude, 0, 1f));
            }
            else
            {
                agent.velocity = Vector3.Normalize(l_targetDirection) * m_currentForceSpeed;
            }
        }
        else
        {
            agent.velocity = l_correctOrientation * m_currentForceSpeed;
        }
    }
    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
