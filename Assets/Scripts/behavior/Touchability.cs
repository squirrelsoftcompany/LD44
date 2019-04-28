/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
namespace behavior {
public class Touchability :
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

    public bool Touchable
    {
        get { return m_timer <= 0; }
    }

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

    [SerializeField] private float m_untouchableTime = 1;

    private float m_timer = 0;
    
    #endregion
    #region Methods
    /***************************************************/
    /***  METHODS               ************************/
    /***************************************************/

    /********  UNITY MESSAGES   ************************/

    // Update is called once per frame
    private void Update()
    {
        if (m_timer > 0) {
            m_timer -= Time.deltaTime;

            if (m_timer <= 0) {
                transform.parent.GetComponent<Animator>().SetTrigger("TouchableAgain");
            }
        }
    }

    /********  OUR MESSAGES     ************************/

    /********  PUBLIC           ************************/

    public void Hitted()
    {
        m_timer = m_untouchableTime;
        transform.parent.GetComponent<Animator>().SetTrigger("Hitted");
    }

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    #endregion
}
}
