using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorModel : MonoBehaviour {

    
    public void OnCollisionStay(Collision col)
    {
        
        if (col.gameObject.transform.parent.gameObject.tag == "Grid")
        {
            Grid collidedGrid = col.gameObject.transform.parent.gameObject.GetComponent<GridObject>().grid;
            if (!transform.parent.gameObject.GetComponent<Indicator>().collided.Contains(collidedGrid))
            {
                transform.parent.gameObject.GetComponent<Indicator>().collided.Add(collidedGrid);
            }
        }
    }

    public void OnCollisionExit(Collision col)
    {
        if (col.gameObject.transform.parent.gameObject.tag == "Grid")
        {
            Grid collidedGrid = col.gameObject.transform.parent.gameObject.GetComponent<GridObject>().grid;
            if (transform.parent.gameObject.GetComponent<Indicator>().collided.Contains(collidedGrid))
            {
                transform.parent.gameObject.GetComponent<Indicator>().collided.Remove(collidedGrid);
            }
        }
    }
    
}
