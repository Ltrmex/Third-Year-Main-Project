using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMusicVolume : MonoBehaviour {

    public Slider Volume;
    public AudioSource myMusic;
    private static ChangeMusicVolume instance = null;

    public static ChangeMusicVolume Instance
    {
        get { return instance; }
    }

    // Update is called once per frame
    void Update () {
        myMusic.volume = Volume.value;
		
	}

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }   //  Awake()
}   //  SoundController
