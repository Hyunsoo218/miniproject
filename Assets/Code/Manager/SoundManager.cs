using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour  
{
    [SerializeField] List<AudioClip> _sounds = new List<AudioClip>();
    [SerializeField] AudioSource playAudio;
    [SerializeField] AudioSource playAudioBack;
    [SerializeField] List<AudioClip> _BGMs = new List<AudioClip>();
    [SerializeField] AudioSource _cBGMPlayer;

    public void PlaySound(SoundObject so)
    {
        if (so == SoundObject.None) return;
        switch (so)
        {
            case SoundObject.sOpheliaHit:
            case SoundObject.sClirisWepon:
            case SoundObject.sClirisArrack:
            case SoundObject.sKnock_3:
            case SoundObject.sKnock_7:
            case SoundObject.sStrongKnock_4:
            case SoundObject.sMove:
            case SoundObject.sRun:
            case SoundObject.sMopo:
            case SoundObject.sOpenDoor:
            case SoundObject.bgmIntro_3_2:
            case SoundObject.sUseMagic:
                playAudio.PlayOneShot(_sounds[(int)so]);
                break;
            case SoundObject.sRoar:
            case SoundObject.sStellaAttack:
            case SoundObject.sExplosion:
            case SoundObject.bgmIntro_1:
            case SoundObject.bgmIntro_1_2:
            case SoundObject.bgmIntro_2:
            case SoundObject.bgmIntro_2_2:
            case SoundObject.bgmIntro_3:
                playAudioBack.PlayOneShot(_sounds[(int)so]);
                break;
        }
    }
    public void Stop()
    {
        playAudio.Stop();
        playAudioBack.Stop();
        _cBGMPlayer.Stop();
    }
    public void PlayBGM(BGM eBGM)
    {
        //_cBGMPlayer.PlayOneShot(_BGMs[(int)eBGM ]);
    }
}
public enum SoundObject
{
    None, sKnock_3, sKnock_7, sStrongKnock_4,
    bgmIntro_1, bgmIntro_1_2,
    sMove, sRun, sMopo, sOpenDoor, sExplosion,
    bgmIntro_2, bgmIntro_2_2, bgmIntro_3_2, bgmIntro_3,
    sOpheliaHit, sRoar, sClirisWepon, sClirisArrack,
    sUseMagic, sStellaAttack
}
public enum BGM
{
    Login_Title
}