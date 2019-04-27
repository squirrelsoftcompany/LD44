/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class PlayerMovement :
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

	public float m_forceSpeed = 20;
    public float m_torqueSpeed = 15;

    private float backwardLimit = 175.0f;
    private float forwardLimit = 5.0f;

    private Rigidbody m_rb;

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
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = Quaternion.AngleAxis(45, new Vector3(0, 1, 0)) * new Vector3 (moveHorizontal, 0.0f, moveVertical);

        float angle = Vector3.SignedAngle(transform.forward, movement, transform.up);

        if (angle > backwardLimit || angle < -backwardLimit) // backward
        {
            m_rb.AddForce(-transform.forward * movement.magnitude * m_forceSpeed * 0.8f);
        }
        else if (angle < forwardLimit && angle > -forwardLimit) // forward
        {
            m_rb.AddForce(transform.forward * movement.magnitude * m_forceSpeed);
        }
        else // rotate
        {
            m_rb.AddTorque(transform.up * Mathf.Sign(angle) * m_torqueSpeed);
        }
    }

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
