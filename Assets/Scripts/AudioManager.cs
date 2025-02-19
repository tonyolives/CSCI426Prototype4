using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource WinSFX;
    public AudioSource DieSFX;
    public AudioSource GainSFX;
    public AudioSource Lv1_SFX;
    public AudioSource Lv2_SFX;
    public AudioSource Lv3_SFX;
    public AudioSource Lv4_SFX;
    public AudioSource DmgSFX;
    public AudioSource DetonateSFX;
    public AudioSource ShootSFX;

    
    public void Win()
    {
        WinSFX.Play(0);
    }
    public void Die()
    {
        DieSFX.Play(0);
    }
    public void Gain()
    {
        GainSFX.Play(0);
    }
    public void Lv1()
    {
        Lv1_SFX.Play(0);
    }
    public void Lv2()
    {
        Lv2_SFX.Play(0);
    }
    public void Lv3()
    {
        Lv3_SFX.Play(0);
    }
    public void Lv4()
    {
        Lv4_SFX.Play(0);
    }
    public void Dmg()
    {
        DmgSFX.Play(0);
    }
    public void Detonate()
    {
        DetonateSFX.Play(0);
    }
    public void Shoot()
    {
        ShootSFX.Play(0);
    }
    
}
