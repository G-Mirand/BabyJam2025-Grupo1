using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    MUSICATRANQUILA,
    PREVIEWMUSICA,
    MUSICAROCK,
    MORDIDA,
    ARRANHAO,
    REGAR,
    MADEIRAQUEBRANDO,
    VIDROQUEBRANDO,
    TECIDORESGANDO,
    PONTUEI,
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundsSO SO;
    public static SoundManager instance = null;
    private AudioSource audioSource;
    private AudioSource musicaAtual;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static void PlaySound(SoundType sound, AudioSource source = null, float volume = 1)
    {
        SoundList soundList = instance.SO.sounds[(int)sound];
        AudioClip[] clips = soundList.sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

        if (source)
        {
            source.outputAudioMixerGroup = soundList.mixer;
            source.clip = randomClip;
            source.volume = volume * soundList.volume;
            source.Play();
        }
        else
        {
            instance.audioSource.outputAudioMixerGroup = soundList.mixer;
            instance.audioSource.PlayOneShot(randomClip, volume * soundList.volume);
        }
    }

    public static void PlayMusic(SoundType sound)
    {
        AudioClip clip = instance.GetRandomClip(sound);
        if (clip == null) return;

        if (instance.musicaAtual == null)
        {
            instance.musicaAtual = instance.gameObject.AddComponent<AudioSource>();
            instance.musicaAtual.loop = true;
        }

        SoundList soundList = instance.SO.sounds[(int)sound];

        instance.musicaAtual.clip = clip;
        instance.musicaAtual.outputAudioMixerGroup = soundList.mixer;
        instance.musicaAtual.volume = soundList.volume;
        instance.musicaAtual.Play();
    }

    public static void StopMusic()
    {
        if (instance.musicaAtual != null)
        {
            instance.musicaAtual.Stop();
        }
    }

    private AudioClip GetRandomClip(SoundType sound)
    {
        SoundList soundList = SO.sounds[(int)sound];
        if (soundList.sounds.Length == 0) return null;
        return soundList.sounds[UnityEngine.Random.Range(0, soundList.sounds.Length)];
    }
}

[Serializable]
public struct SoundList
{
    [HideInInspector] public string name;
    [Range(0, 1)] public float volume;
    public AudioMixerGroup mixer;
    public AudioClip[] sounds;
}

#if UNITY_EDITOR
[CustomEditor(typeof(SoundsSO))]
public class SoundsSOEditor : Editor
{
    private void OnEnable()
    {
        ref SoundList[] soundList = ref ((SoundsSO)target).sounds;

        if (soundList == null)
            return;

        string[] names = Enum.GetNames(typeof(SoundType));
        bool differentSize = names.Length != soundList.Length;

        Dictionary<string, SoundList> sounds = new();

        if (differentSize)
        {
            for (int i = 0; i < soundList.Length; ++i)
            {
                sounds.Add(soundList[i].name, soundList[i]);
            }
        }

        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            string currentName = names[i];
            soundList[i].name = currentName;
            if (soundList[i].volume == 0) soundList[i].volume = 1;

            if (differentSize)
            {
                if (sounds.ContainsKey(currentName))
                {
                    SoundList current = sounds[currentName];
                    UpdateElement(ref soundList[i], current.volume, current.sounds, current.mixer);
                }
                else
                    UpdateElement(ref soundList[i], 1, new AudioClip[0], null);

                static void UpdateElement(ref SoundList element, float volume, AudioClip[] sounds, AudioMixerGroup mixer)
                {
                    element.volume = volume;
                    element.sounds = sounds;
                    element.mixer = mixer;
                }
            }
        }
    }
}
#endif
