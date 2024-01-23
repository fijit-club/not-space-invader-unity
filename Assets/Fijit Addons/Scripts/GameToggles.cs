using NotSpaceInvaders;
using UnityEngine;

public class GameToggles : MonoBehaviour
{
    public static bool VibrationOn = true;
    
    [SerializeField] private AudioSource[] audioSources;
    
    private void Start()
    {
        UpdateSettings();
    }

    private void UpdateSettings()
    {
        if (Bridge.GetInstance().thisPlayerInfo.volumeBg)
        {
            foreach (var audioSource in audioSources)
                audioSource.volume = 1f;
        }
        else
        {
            foreach (var audioSource in audioSources)
                audioSource.volume = 0f;
        }

        VibrationOn = Bridge.GetInstance().thisPlayerInfo.vibrationOn;
    }

    public void TurnOffVibration()
    {
        Bridge.GetInstance().thisPlayerInfo.vibrationOn = false;
        UpdateSettings();
    }

    public void TurnOnVibration()
    {
        Bridge.GetInstance().thisPlayerInfo.vibrationOn = true;
        UpdateSettings();
    }
    
    public void TurnOnVolume()
    {
        Bridge.GetInstance().thisPlayerInfo.volumeBg = true;
        UpdateSettings();
    }
    
    public void TurnOffVolume()
    {
        Bridge.GetInstance().thisPlayerInfo.volumeBg = false;
        UpdateSettings();
    }
}
