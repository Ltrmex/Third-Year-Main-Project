using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Kamila Michel
//Source code adapted from: https://www.youtube.com/watch?v=0WPBm9nGHdE

namespace Ability { 

    public class PlayerHandler : MonoBehaviour {

        //Declare Player variable
        public PlayerStatus Player;

        //This field gets serialized even though it is private
        //because it has the SerializeField attribute applied.
        //Unity will serialize all script components, reload the new assemblies, and recreate script components from the serialized verions. 
        [SerializeField]
        //Declares variables for canvas
        private Canvas m_Canvas;
        //Declare to use for user to see the canvas when press tab
        private bool m_seeCanvas;

	    // Update is called once per frame
	    void Update () {
                // if press the E key
                //When press tab canvas can be seen
                if (Input.GetKeyDown("tab")) {
                    if (m_Canvas)
                    {
                        m_seeCanvas = !m_seeCanvas;
                        //Display or not the canvas (following the state of bool)
                        m_Canvas.gameObject.SetActive(m_seeCanvas);
                    }
                }//End of if
	    }//End of Update method
    }//End of class PlayerHandler
}//End of namespace
