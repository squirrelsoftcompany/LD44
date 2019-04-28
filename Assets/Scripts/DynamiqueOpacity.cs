/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class DynamiqueOpacity :
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
    
    private float m_initialOpacity;
    private bool m_modifiedOpacity = false;
    private bool m_modifiedRequested = false;
    private float m_fadeTime = 0.0f;
    private float m_fadeTimer = 0.0f;
    private float m_opacityGoal;


    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        if (transform.GetComponent<Renderer>())
        {
            m_initialOpacity = transform.GetComponent<Renderer>().material.color.a;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(m_modifiedOpacity)
        {
            Color col = transform.GetComponent<Renderer>().material.color;
            if (col != null)
            {
                float l_progressTime;
                if (m_fadeTimer > 0 )
                {
                    l_progressTime =  (m_fadeTime - m_fadeTimer) / m_fadeTime;
                }
                else
                {
                    l_progressTime = 1.0f;
                }
                col.a = Mathf.Lerp(m_initialOpacity, m_opacityGoal, l_progressTime);
                transform.GetComponent<Renderer>().material.color = col;
            }
        }
        
        m_fadeTimer -= Time.deltaTime;
        if (m_fadeTimer < 0)
        {
            if(!m_modifiedRequested)
            {
                m_modifiedOpacity = false;
                //Restoring opacity directly (no fading yet...)
                Color l_col = transform.GetComponent<Renderer>().material.color;
                l_col.a = m_initialOpacity;
                transform.GetComponent<Renderer>().material.color = l_col;
            }
        }
        m_modifiedRequested = false;
    }


    /********  PUBLIC           ************************/

    public void FadeOutOpacity(float p_fadeTime, float p_opacity)
    {
        m_modifiedRequested = true;
        if (!m_modifiedOpacity)
        {
            m_fadeTimer = p_fadeTime;
            m_fadeTime = p_fadeTime;
            m_opacityGoal = p_opacity;
            m_modifiedOpacity = true;
        }
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/


    #endregion
}
