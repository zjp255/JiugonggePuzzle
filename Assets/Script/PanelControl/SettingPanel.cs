using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    public GameObject sliderOfBc, sliderOfSound;
    private void Awake()
    {
        sliderOfBc.GetComponent<Slider>().value = PlayerPrefs.GetFloat(Const.Music);
        sliderOfSound.GetComponent<Slider>().value = PlayerPrefs.GetFloat(Const.Sound);
    }
}
