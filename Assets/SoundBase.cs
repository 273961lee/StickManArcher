using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBase : MonoBehaviour {
    public static SoundBase instance;
    public AudioClip[] clips;
    public AudioSource self;
    // Use this for initialization
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start () {
        
	}
    public void PlayHitAU() {
        self.PlayOneShot(clips[0], clips[0].length);
    }

    public void PlayDeadAU() {
        self.PlayOneShot(clips[1],clips[1].length);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
