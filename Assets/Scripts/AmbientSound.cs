/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class AmbientSound :
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

    public GameObject m_fenwick;
    public float m_minDistance;
    public float m_maxDistance;
    public AnimationCurve m_audioCurve;

    private AudioSource m_ambientSound = new AudioSource();
    

    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Use this for initialization
    private void Start()
    {
        m_ambientSound = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (! m_fenwick) return;

        float l_distanceFromFen = (m_fenwick.transform.position - this.transform.position).magnitude;

        if (l_distanceFromFen > m_maxDistance)
        {
            m_ambientSound.volume = 1.0f;
        }
        else if (l_distanceFromFen < m_minDistance)
        {
            m_ambientSound.volume = 0.0f;
        }
        else
        {
            m_ambientSound.volume = m_audioCurve.Evaluate(Mathf.Lerp(m_minDistance, m_maxDistance, l_distanceFromFen) * 0.01f);
        }
    }


    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
