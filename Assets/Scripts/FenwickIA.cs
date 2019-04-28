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
    public float m_rangeDetection = 40;
    public float m_rageTime = 10;

    private Rigidbody m_rb;
    private float m_currentForceSpeed;
    public float m_rageTimeLeft;

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
    }

    // Update is called once per frame
    private void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = m_player.transform.position;
        
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Debug.DrawLine(transform.position, m_player.transform.position,Color.red);
        if (Physics.Raycast(transform.position, m_player.transform.position, out hit, m_rangeDetection) )
        {
            if (hit.transform.tag == "Player")
            {
                Debug.Log("1");
                m_currentForceSpeed = m_forceSpeedRage;
            }
            else
            {
                Debug.Log("2");
                m_rageTimeLeft -= Time.deltaTime;
                if (m_rageTimeLeft < 0)
                {
                    m_currentForceSpeed = m_forceSpeed;
                    m_rageTimeLeft = m_rageTime;
                }
            }
        }
        else
        {
            Debug.Log("3");
            m_rageTimeLeft -= Time.deltaTime;
            if (m_rageTimeLeft < 0)
            {
                m_currentForceSpeed = m_forceSpeed;
                m_rageTimeLeft = m_rageTime;
            }
        }
    }

    void FixedUpdate()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        Vector3 l_currentVelocity = agent.velocity;
        Vector3 l_orientation = Vector3.Normalize(l_currentVelocity);
        
        Vector3 l_correctOrientation = l_orientation;
        if (Mathf.Abs(l_orientation.x) >= Mathf.Abs(l_orientation.z))
        {
            l_correctOrientation = new Vector3(l_orientation.x, l_orientation.y, 0);
        }
        if (Mathf.Abs(l_orientation.z) > Mathf.Abs(l_orientation.x))
        {
            l_correctOrientation = new Vector3(0, l_orientation.y, l_orientation.z);
        }

        Vector3 l_targetDirection = agent.steeringTarget - this.transform.position;
        if (Vector3.Dot(this.transform.forward, l_targetDirection) < 0)
        {
            if(agent.velocity.magnitude > 0.1)
            {
                agent.velocity = agent.velocity - (Vector3.Normalize(agent.velocity) * Mathf.Clamp(agent.velocity.magnitude, 0, 1f));
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
