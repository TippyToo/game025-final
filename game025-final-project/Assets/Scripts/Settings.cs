using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private Slider volumeSetting;
    // Start is called before the first frame update
    void Start()
    {
        volumeSetting = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("Volume", volumeSetting.value);
    }
}
