using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Kamila Michel
//Source code adapted from: https://www.youtube.com/watch?v=0WPBm9nGHdE

namespace Ability { 

    public class PlayerHandler : MonoBehaviour {

        public PlayerStatus Player;
        [SerializeField]
        private Canvas m_Canvas;
        private bool m_seeCanvas;

	// Update is called once per frame
	void Update () {
            // if press the E key
            if (Input.GetKeyDown("tab")) {
                if (m_Canvas)
                {
                    m_seeCanvas = !m_seeCanvas;
                    //Display or not the canvas (following the state of bool)
                    m_Canvas.gameObject.SetActive(m_seeCanvas);
                }
            }
	}
}
}
