/***************************************************/
/***  INCLUDE               ************************/
/***************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***************************************************/
/***  THE CLASS             ************************/
/***************************************************/
public class ApertureView :
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

    public GameObject m_hero;
    public float m_radius;

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
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 l_middle = m_hero.transform.position;
        Vector3 l_top = new Vector3(m_hero.transform.position.x + m_radius, m_hero.transform.position.y, m_hero.transform.position.z);
        Vector3 l_bottom = new Vector3(m_hero.transform.position.x - m_radius, m_hero.transform.position.y, m_hero.transform.position.z);
        Vector3 l_left = new Vector3(m_hero.transform.position.x, m_hero.transform.position.y, m_hero.transform.position.z + m_radius);
        Vector3 l_right = new Vector3(m_hero.transform.position.x, m_hero.transform.position.y, m_hero.transform.position.z - m_radius);

        OccultingObjects(this.transform.position, l_middle);
        OccultingObjects(this.transform.position, l_top);
        OccultingObjects(this.transform.position, l_bottom);
        OccultingObjects(this.transform.position, l_left);
        OccultingObjects(this.transform.position, l_right);
    }


    /********  PUBLIC           ************************/

    /********  PROTECTED        ************************/

    /********  PRIVATE          ************************/

    void OccultingObjects(Vector3 p_source, Vector3 p_destination, int p_layerMask = Physics.DefaultRaycastLayers)
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(p_source, p_destination - p_source);

        Debug.DrawLine(p_source, p_destination,Color.red);
        foreach (RaycastHit l_hit in hits)
        {
            DynamiqueOpacity[] l_scriptArray = l_hit.transform.gameObject.GetComponentsInChildren<DynamiqueOpacity>();
            foreach (DynamiqueOpacity l_script in l_scriptArray)
            {
                l_script.FadeOutOpacity(0.5f, 0.5f);
            }
        }
    }

    #endregion
}
