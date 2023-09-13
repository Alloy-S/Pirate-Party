using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider;
  public void setVolume() {

    AudioListener.volume = volumeSlider.value;
  }

  public void saveVolume() {
    float volumeValue = volumeSlider.value;
    PlayerPrefs.SetFloat("volume", volumeValue);
    loadVolume();
  }

  public void loadVolume() {
    float volumeValue = PlayerPrefs.GetFloat("volume");
    // Debug.Log(volumeValue);
    volumeSlider.value = volumeValue;
    AudioListener.volume = volumeValue;
  }
}
