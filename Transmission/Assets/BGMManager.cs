using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BGMManager : MonoBehaviour {

    public List<AudioClip> musicList;
    public AudioMixer mixer;
    public AudioSource audioSource;

    public float smoothChangeTime = 2;

    protected struct ParameterChange{
        public float targetValue;
        public float timeLeft;
        public float speed;
    }

    protected Dictionary<string, ParameterChange> mixerParametersToChange; 


    // Use this for initialization
    void Start()
    {
        mixerParametersToChange = new Dictionary<string, ParameterChange>();
        if (!audioSource.playOnAwake)
        {
            audioSource.clip = musicList[Random.Range(0, musicList.Count)];
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = musicList[Random.Range(0, musicList.Count)];
            audioSource.Play();
        }

        foreach(var k in mixerParametersToChange.Keys)
        {
            float current;
            mixer.GetFloat(k, out current);

            float value = current + mixerParametersToChange[k].speed*Time.deltaTime;
            mixer.SetFloat(k, value);
        }
    }

    public void ChangeLPFCutoffSmooth(float value)
    {
        float current;
        mixer.GetFloat("Space_Music_LPF_Cufoff", out current);

        ParameterChange p = new ParameterChange();
        p.targetValue = value;
        p.timeLeft = smoothChangeTime;
        p.speed = (value - current) / smoothChangeTime;
        mixerParametersToChange["Space_Music_LPF_Cufoff"] = p;
    }
    
}
