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
    public float m_MaxSpeed = 5;

    private float backwardLimit = 170.0f;
    private float forwardLimit = 5.0f;

    private Rigidbody m_rb;
    private Animator animator;
    private static readonly int IS_MOVING = Animator.StringToHash("isMoving");

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
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = Vector3.Normalize(new Vector3 (moveVertical, 0.0f, -moveHorizontal));
        if (animator) {
            //TODO check if moving correctly!!!
            if (movement.sqrMagnitude > 0) {
                animator	.SetBool	(IS_MOVING, true);
            } else {
                animator	.SetBool	(IS_MOVING	,false);
            }
        }
        
        float angle = Vector3.SignedAngle(transform.forward, movement, transform.up);

        if (angle > backwardLimit || angle < -backwardLimit) // backward
        {
            if (m_rb.velocity.sqrMagnitude < (m_MaxSpeed * m_MaxSpeed))
            {
                m_rb.AddForce(-transform.forward * movement.magnitude * m_forceSpeed * 0.8f);
            }
        }
        else if (angle < forwardLimit && angle > -forwardLimit) // forward
        {
            if (m_rb.velocity.sqrMagnitude < (m_MaxSpeed * m_MaxSpeed))
            {
                m_rb.AddForce(transform.forward * movement.magnitude * m_forceSpeed);
            }
        }
        else // rotate
        {
            m_rb.AddTorque(transform.up * Mathf.Sign(angle) * m_torqueSpeed);
            m_rb.velocity = new Vector3(0, 0, 0);
        }
    }

    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
