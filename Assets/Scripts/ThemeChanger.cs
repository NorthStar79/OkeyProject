using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ThemeChanger : MonoBehaviour
{
    int currentTheme = 0;

    public Sprite[] CueThemes;

    public Color32[] StoneThemes;

    public Image CueImage;


    void Start()
    {
        ChangeTheme();
    }

    public void ChangeTheme()
    {
        StoneObj[] stones = FindObjectsOfType<StoneObj>();

        foreach (var item in stones)
        {
            item.GetComponent<Image>().color = StoneThemes[currentTheme%StoneThemes.Length];
        }

        CueImage.sprite = CueThemes[currentTheme%CueThemes.Length];

        currentTheme++;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
