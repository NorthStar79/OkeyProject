using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StoneText : MonoBehaviour
{

        public StoneObj stoneObj;
        public TextMeshProUGUI textMesh;

    public void Init(int overrideIndex = -1)
    {
        Color color;
        ArrangeCue arrangeCue = FindObjectOfType<ArrangeCue>();
        int displayText = overrideIndex>=0?overrideIndex:stoneObj.stone.index;
        switch (Mathf.CeilToInt(displayText/13))
        {
            case 0: color = arrangeCue.red; break;
            //case 1: color = arrangeCue.red; break;
            case 1: color = arrangeCue.blue; break;
            case 2: color = arrangeCue.black; break;
            case 3: color = arrangeCue.yellow; break;

            case 4: color = arrangeCue.red; break;
            case 5: color = arrangeCue.blue; break;
            case 6: color = arrangeCue.black; break;
            case 7: color = arrangeCue.yellow; break;

            default: color = Color.magenta; Debug.LogError("Stone Color Could not be determined " + (Mathf.CeilToInt(stoneObj.stone.index/13)),this); break;
        }
        textMesh.color = color;
        textMesh.SetText(((displayText%13)+1).ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
