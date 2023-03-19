using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource bc, sound;
    public AudioClip soundClip1,audioClip2;
    static AudioManager instance;


    private void Awake()
    {
        instance = this;
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Audio");
        
        DontDestroyOnLoad(gameObject);
        bc = transform.Find("bc").GetComponent<AudioSource>();
        sound = transform.Find("sound").GetComponent<AudioSource>();

 

        bc.volume = PlayerPrefs.GetFloat(Const.Music);
        sound.volume = PlayerPrefs.GetFloat(Const.Sound);

        
        if (gameObjects.Length > 1)
        {
            GameObject.Destroy(gameObjects[0].gameObject);
            PlaySound();
        }

        bc.clip = audioClip2;
        bc.loop = true;
        bc.Play();
    }
    public void PlaySound()
    {
       
        instance.sound.clip = instance.soundClip1;
        instance.sound.Play();
    }

    public void BcVolumeChange(float f)
    {
        bc.volume = f;
        PlayerPrefs.SetFloat(Const.Music, f);
    }

    public void soindVolumeChange(float f)
    {
        sound.volume = f;
        PlayerPrefs.SetFloat(Const.Sound, f);
    }
}
