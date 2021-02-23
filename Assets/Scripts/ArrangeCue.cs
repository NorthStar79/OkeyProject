using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArrangeCue : MonoBehaviour
{

    [SerializeField] GameObject cueObject, stonePrefab;
    [SerializeField] float leftPadding, topPadding, stoneWidth, stoneHeight;
    [SerializeField] int columnCount;
    int rowCount = 0;

    public Color32 red, blue, black, yellow;
    // Start is called before the first frame update

    public void Arrange(List<List<Stone>> hand)
    {
        StoneObj[] o = FindObjectsOfType<StoneObj>();
        {
            for (int a = 0; a < o.Length; a++)
            {
                Destroy(o[a].gameObject);
            }
        }
        rowCount = 0;
        int k = 0;
        for (int i = 0; i < hand.Count; i++)
        {
            for (int j = 0; j < hand[i].Count; j++)
            {
                GameObject obj = GameObject.Instantiate(stonePrefab, cueObject.transform);
                RectTransform rt = obj.GetComponent<RectTransform>();
                StoneObj st = obj.GetComponent<StoneObj>();
                st.stone = new Stone(hand[i][j].index);
                st.transform.name = st.stone.index.ToString();
                st.Init();

                if (k >= columnCount) rowCount = -1;
                rt.anchoredPosition = new Vector2(leftPadding + Mathf.FloorToInt(k % columnCount) * stoneWidth, topPadding + (rowCount * stoneHeight));

                k++;
            }
            k++;
        }

    }

    public void Arrange(List<Stone> hand)
    {
        StoneObj[] o = FindObjectsOfType<StoneObj>();
        {
            for (int a = 0; a < o.Length; a++)
            {
                Destroy(o[a].gameObject);
            }
        }
        rowCount = 0;
        for (int i = 0; i < hand.Count; i++)
        {
            if (hand[i].index >= 0)
            {
                GameObject obj = GameObject.Instantiate(stonePrefab, cueObject.transform);
                RectTransform rt = obj.GetComponent<RectTransform>();
                StoneObj st = obj.GetComponent<StoneObj>();
                st.stone = new Stone(hand[i].index);

                st.Init();


                //st.stone = (Stone)hand[i].Clone();
                if (i >= columnCount) rowCount = -1;
                rt.anchoredPosition = new Vector2(leftPadding + Mathf.FloorToInt(i % columnCount) * stoneWidth, topPadding + (rowCount * stoneHeight));
            }
        }
    }

    public void Arrange(List<List<Stone>> hand,List<List<Stone>> hand1)
    {
        List<List<Stone>> temp,temp1;
        temp = hand;
        temp1 = hand1;

        foreach (var item in temp)
        {
            foreach (var it in item)
            {
                Debug.Log(it.index);
            }
        }

        foreach (var item in temp1)
        {
            foreach (var it in item)
            {
                Debug.Log(it.index);
            }
        }

        StoneObj[] o = FindObjectsOfType<StoneObj>();
        {
            for (int a = 0; a < o.Length; a++)
            {
                Destroy(o[a].gameObject);
            }
        }
        rowCount = 0;   
        int k = 0;
        for (int i = 0; i < hand.Count; i++)
        {
            for (int j = 0; j < hand[i].Count; j++)
            {
                GameObject obj = GameObject.Instantiate(stonePrefab, cueObject.transform);
                RectTransform rt = obj.GetComponent<RectTransform>();
                StoneObj st = obj.GetComponent<StoneObj>();
                st.stone = new Stone(hand[i][j].index);
                st.transform.name = st.stone.index.ToString();
                st.Init();

                if (k >= columnCount) rowCount = -1;
                rt.anchoredPosition = new Vector2(leftPadding + Mathf.FloorToInt(k % columnCount) * stoneWidth, topPadding + (rowCount * stoneHeight));

                k++;
            }
            k++;
        }

        for (int i = 0; i < hand1.Count; i++)
        {
            for (int j = 0; j < hand1[i].Count; j++)
            {
                GameObject obj = GameObject.Instantiate(stonePrefab, cueObject.transform);
                RectTransform rt = obj.GetComponent<RectTransform>();
                StoneObj st = obj.GetComponent<StoneObj>();
                st.stone = new Stone(hand1[i][j].index);
                st.transform.name = st.stone.index.ToString();
                st.Init();

                if (k >= columnCount) rowCount = -1;
                rt.anchoredPosition = new Vector2(leftPadding + Mathf.FloorToInt(k % columnCount) * stoneWidth, topPadding + (rowCount * stoneHeight));

                k++;
            }
            k++;
        }
    }
}
