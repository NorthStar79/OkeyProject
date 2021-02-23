using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class GameMaster : MonoBehaviour
{
    [SerializeField] GameObject cueObject, stonePrefab,stoneHolder;
    [SerializeField] float leftPadding, topPadding, stoneWidth, stoneHeight;
    [SerializeField] int columnCount;
    [SerializeField] public Color32 red, blue, black, yellow;
    [SerializeField] StoneObj OkeyInfoObject;

    [SerializeField] Button[] gameButtons;

    List<Stone> deck = new List<Stone>(); // we dont really need to create a deck for this demo but for any functionality extentiton for future will need some sort of deck implementation 

    List<Stone> hand = new List<Stone>();
    int OkeyIndex;

    bool isAnimating=false;
    void Start()
    {
        hand.Clear();
        deck.Clear();
        CreateDeck();
    }

    void CreateDeck()
    {
        for (int i = 0; i < 104; i++)
        {
            deck.Add(new Stone(i));
        }
        SelectOkey();
        CreateHand();
    }

    void SelectOkey()
    {
        OkeyIndex = Random.Range(0, 104);

        OkeyInfoObject.stone.index = OkeyIndex;
        OkeyInfoObject.Init();

    }

    void CreateHand()
    {
        for (int i = 0; i < 14; i++)
        {
            int j = Random.Range(0, deck.Count);
            hand.Add(deck[j]);
            deck.RemoveAt(j);
        }
        StartCoroutine(Arranger(hand));
    }
    List<List<Stone>> Sequent(List<Stone> myHand)
    {

        myHand = BubleSort(myHand);
        List<List<Stone>> sequence = new List<List<Stone>>();
        for (int i = 0; i < myHand.Count; i++)
        {
            int k = 0;
            sequence.Add(new List<Stone>());
            sequence[i].Add(myHand[k]);
            myHand.RemoveAt(0);
            for (int j = 0; j < myHand.Count; j++)
            {
                if ((sequence[i][k].index % 52) + 1 == (myHand[j].index % 52)
                && Mathf.FloorToInt((sequence[i][k].index / 13)) % 4 == Mathf.FloorToInt(myHand[j].index / 13) % 4)
                {
                    sequence[i].Add(myHand[j]);
                    myHand.RemoveAt(j);
                    k++;
                    j = -1;
                }
            }
        }
        sequence.Add(new List<Stone>());

        foreach (var item in myHand)
        {
            sequence[sequence.Count - 1].Add(item);
        }
        myHand.Clear();

        for (int i = 0; i < sequence.Count; i++)
        {
            if (sequence[i].Count < 3)
            {
                
                for (int j = 0; j < sequence[i].Count; j++)
                {
                    sequence[sequence.Count - 1].Add(sequence[i][j]);
                }
                sequence.RemoveAt(i);
                i--;
            }
        }
        return sequence;
    }

    List<List<Stone>> Coequal(List<Stone> myHand)
    {
        //SortHand();
        List<List<Stone>> sequence = new List<List<Stone>>();
        for (int i = 0; i < myHand.Count; i++)
        {
            int k = 0;
            sequence.Add(new List<Stone>());
            sequence[i].Add(myHand[k]);
            myHand.RemoveAt(0);
            for (int j = 0; j < myHand.Count; j++)
            {
                if ((sequence[i][k].index % 13) == (myHand[j].index % 13)
                && Mathf.FloorToInt((sequence[i][k].index / 13)) % 4 != Mathf.FloorToInt(myHand[j].index / 13) % 4)
                {
                    bool b = false;
                    foreach (var item in sequence[i])
                    {
                        if ((item.index / 13) % 4 == (myHand[j].index / 13) % 4)
                        {
                            b = true;
                        }
                    }
                    if (!b)
                    {
                        sequence[i].Add(myHand[j]);
                        myHand.RemoveAt(j);
                        k++;
                        j = -1;
                    }
                }
            }
        }
        sequence.Add(new List<Stone>());

        foreach (var item in myHand)
        {
            sequence[sequence.Count - 1].Add(item);
        }
        myHand.Clear();

        for (int i = 0; i < sequence.Count; i++)
        {
            if (sequence[i].Count < 3)
            {
                
                for (int j = 0; j < sequence[i].Count; j++)
                {
                    sequence[sequence.Count - 1].Add(sequence[i][j]);
                }
                sequence.RemoveAt(i);
                i--;
            }
        }
        return sequence;
    }

    List<List<Stone>> Smart(List<Stone> myHand)
    {
        List<Stone> handCache = new List<Stone>(myHand);

        List<List<Stone>> temp = new List<List<Stone>>(), temp1 = new List<List<Stone>>();

        int i, j;

        temp = Sequent(myHand);
        myHand = new List<Stone>(temp[temp.Count - 1]);
        temp1 = Coequal(myHand);

        i = temp1[temp1.Count - 1].Count;

        temp.Clear();
        temp1.Clear();
        myHand = new List<Stone>(handCache);


        temp1 = Coequal(myHand);
        myHand = new List<Stone>(temp1[temp1.Count - 1]);
        temp = Sequent(myHand);

        j = temp[temp.Count - 1].Count;

        temp.Clear();
        temp1.Clear();
        myHand = new List<Stone>(handCache);

        if (i < j)
        {
            temp = Sequent(myHand);
            myHand = new List<Stone>(temp[temp.Count - 1]);
            temp1 = Coequal(myHand);

            temp.RemoveAt(temp.Count - 1);

            for (int a = 0; a < temp1.Count; a++)
            {
                temp.Add(new List<Stone>());
                for (int b = 0; b < temp1[a].Count; b++)
                {
                    temp[temp.Count - 1].Add(temp1[a][b]);
                }
            }

            return temp;
        }
        else
        {
            temp1 = Coequal(myHand);
            myHand = new List<Stone>(temp1[temp1.Count - 1]);
            temp = Sequent(myHand);

            temp1.RemoveAt(temp1.Count - 1);

            for (int a = 0; a < temp.Count; a++)
            {
                temp1.Add(new List<Stone>());
                for (int b = 0; b < temp[a].Count; b++)
                {
                    temp1[temp1.Count - 1].Add(temp[a][b]);
                }
            }

            return temp1;
        }

    }

    List<Stone> BubleSort(List<Stone> myHand)
    {
        for (int i = 1; i < myHand.Count; i++)
        {
            for (int j = 0; j < (myHand.Count - i); j++)
            {
                if ((myHand[j].index) > (myHand[j + 1].index))
                {
                    Stone temp = myHand[j];
                    myHand[j] = myHand[j + 1];
                    myHand[j + 1] = temp;
                }
            }
        }
        return myHand;
    }

    IEnumerator Arranger(List<List<Stone>> hand, int usedOkeyIndex = -1)
    {
        isAnimating = true;
        foreach (var item in gameButtons)
        {
            item.interactable = false;
        }
        StoneObj[] o = FindObjectsOfType<StoneObj>();
        
            RectTransform ort = OkeyInfoObject.GetComponent<RectTransform>();
            for (int a = 0; a < o.Length; a++)
            {
                if(o[a]!=OkeyInfoObject)
                {
                    RectTransform rt = o[a].GetComponent<RectTransform>(); 
                    while ((rt.anchoredPosition - ort.anchoredPosition).magnitude >.1f)
                    {
                        rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition,ort.anchoredPosition,.5f);
                        yield return new WaitForSeconds(.01f);
                    }
                    Destroy(o[a].gameObject);
                }
            }
        
        int k = 0;
        for (int i = 0; i < hand.Count; i++)
        {
            for (int j = 0; j < hand[i].Count; j++)
            {
                GameObject obj = GameObject.Instantiate(stonePrefab,stoneHolder.transform);
                RectTransform rt = obj.GetComponent<RectTransform>();
                StoneObj st = obj.GetComponent<StoneObj>();
                bool isOkey = hand[i][j].index%52==usedOkeyIndex%52||hand[i][j].index%52==OkeyIndex%52;
                st.stone = new Stone(hand[i][j].index,isOkey);
                st.transform.name = st.stone.index.ToString();
                int overrideIndex = isOkey?OkeyIndex:-1;
                st.Init(overrideIndex);

                rt.anchoredPosition = ort.anchoredPosition;
                while ((rt.anchoredPosition-cueObject.transform.GetChild(k).GetComponent<RectTransform>().anchoredPosition).magnitude >.1f)
                {
                    rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition,cueObject.transform.GetChild(k).GetComponent<RectTransform>().anchoredPosition,.5f);
                    yield return new WaitForSeconds(.01f);
                }

                rt.anchoredPosition = cueObject.transform.GetChild(k).GetComponent<RectTransform>().anchoredPosition;
                cueObject.transform.GetChild(k).GetComponent<ItemSlot>().stoneObj = st;

                k++;
            }
            k++;
        }
        isAnimating = false;
        foreach (var item in gameButtons)
        {
            item.interactable = true;
        }
    }
    public IEnumerator Arranger(List<Stone> hand,int usedOkeyIndex = -1)
    {
        isAnimating = true;
        foreach (var item in gameButtons)
        {
            item.interactable = false;
        }
        yield return new WaitForEndOfFrame();  //just wait for grid layout to calculate its numbers.
        StoneObj[] o = FindObjectsOfType<StoneObj>();
        
            RectTransform ort = OkeyInfoObject.GetComponent<RectTransform>();
            for (int a = 0; a < o.Length; a++)
            {
                if(o[a]!=OkeyInfoObject)
                {
                    RectTransform rt = o[a].GetComponent<RectTransform>(); 
                    while ((rt.anchoredPosition - ort.anchoredPosition).magnitude >.1f)
                    {
                        rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition,ort.anchoredPosition,.5f);
                        yield return new WaitForSeconds(.01f);
                    }
                    Destroy(o[a].gameObject);
                }
            }
        
        for (int i = 0; i < hand.Count; i++)
        {
            if (hand[i].index >= 0)
            {
               GameObject obj = GameObject.Instantiate(stonePrefab,stoneHolder.transform);
                RectTransform rt = obj.GetComponent<RectTransform>();
                StoneObj st = obj.GetComponent<StoneObj>();
                bool isOkey = hand[i].index%52==usedOkeyIndex%52||hand[i].index%52==OkeyIndex%52;;
                st.stone = new Stone(hand[i].index,isOkey);
                st.transform.name = st.stone.index.ToString();
                int overrideIndex = isOkey?OkeyIndex:-1;
                st.Init(overrideIndex);

                rt.anchoredPosition = ort.anchoredPosition;
                 while ((rt.anchoredPosition-cueObject.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition).magnitude >.1f)
                {
                    rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition,cueObject.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition,.5f);
                    yield return new WaitForSeconds(.01f);
                }

                rt.anchoredPosition = cueObject.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition;
                cueObject.transform.GetChild(i).GetComponent<ItemSlot>().stoneObj = st;
            }
        }
        isAnimating = false;
        foreach (var item in gameButtons)
        {
            item.interactable = true;
        }
    }
    [ContextMenu("Sequent Arrenge")]
    public void SequentArrenge()
    {
        if(isAnimating) return;
        List<Stone> myHand = hand.Select(stone => stone.Clone()).ToList();

        bool haveOkey = false;

        for (int i = 0; i < myHand.Count; i++)
        {
            if (myHand[i].index%52 == OkeyIndex%52)
            {
                Debug.Log("I have Okey");
                haveOkey = true;
                myHand.RemoveAt(i);
                break;
            }
        }

        if (haveOkey)
        {
            List<int> artiklar = new List<int>();
            List<Stone> myHandCache = myHand.Select(stone => stone.Clone()).ToList();
            for (int i = 0; i < 104; i++)
            {
                myHand = myHandCache.Select(stone => stone.Clone()).ToList();
                myHand.Add(new Stone(i));
                List<List<Stone>> temp = Sequent(myHand);
                artiklar.Add(temp[temp.Count - 1].Count);
            }

            int IndexOfMin = artiklar.IndexOf(artiklar.Min());
            Debug.Log(IndexOfMin);
            myHand = myHandCache.Select(stone => stone.Clone()).ToList();
            myHand.Add(new Stone(IndexOfMin));

           StartCoroutine(Arranger(Sequent(myHand),IndexOfMin));
        }
        else
        {
            List<Stone> temp = hand.Select(stone => stone.Clone()).ToList();
            StartCoroutine(Arranger(Sequent(temp)));
        }
    }
    [ContextMenu("Coequal Arrange")]
    public void CoequalArrange()
    {
        if(isAnimating) return;
        List<Stone> myHand = hand.Select(stone => stone.Clone()).ToList();

        bool haveOkey = false;

        for (int i = 0; i < myHand.Count; i++)
        {
            if (myHand[i].index%52 == OkeyIndex%52)
            {
                Debug.Log("I have Okey");
                haveOkey = true;
                myHand.RemoveAt(i);
                break;
            }
        }

        if (haveOkey)
        {
            List<int> artiklar = new List<int>();
            List<Stone> myHandCache = myHand.Select(stone => stone.Clone()).ToList();
            for (int i = 0; i < 104; i++)
            {
                myHand = myHandCache.Select(stone => stone.Clone()).ToList();
                myHand.Add(new Stone(i));
                List<List<Stone>> temp = Coequal(myHand);
                artiklar.Add(temp[temp.Count - 1].Count);
            }

            int IndexOfMin = artiklar.IndexOf(artiklar.Min());
            Debug.Log(IndexOfMin);
            myHand = myHandCache.Select(stone => stone.Clone()).ToList();
            myHand.Add(new Stone(IndexOfMin));

            StartCoroutine(Arranger(Coequal(myHand),IndexOfMin));
        }
        else
        {
            List<Stone> temp = hand.Select(stone => stone.Clone()).ToList();
            StartCoroutine(Arranger(Coequal(temp)));
        }
    }

    [ContextMenu("SmartArrange")]
    public void SmartArrange()
    {
        if(isAnimating) return;
        List<Stone> myHand = hand.Select(stone => stone.Clone()).ToList();

        bool haveOkey = false;

        for (int i = 0; i < myHand.Count; i++)
        {
            if (myHand[i].index%52 == OkeyIndex%52)
            {
                Debug.Log("I have Okey");
                haveOkey = true;
                myHand.RemoveAt(i);
                break;
            }
        }

        if (haveOkey)
        {
            List<int> artiklar = new List<int>();
            List<Stone> myHandCache = myHand.Select(stone => stone.Clone()).ToList();
            for (int i = 0; i < 104; i++)
            {
                myHand = myHandCache.Select(stone => stone.Clone()).ToList();
                myHand.Add(new Stone(i));
                List<List<Stone>> temp = Smart(myHand);
                artiklar.Add(temp[temp.Count - 1].Count);
            }

            int IndexOfMin = artiklar.IndexOf(artiklar.Min());
            Debug.Log(IndexOfMin);
            myHand = myHandCache.Select(stone => stone.Clone()).ToList();
            myHand.Add(new Stone(IndexOfMin));

            StartCoroutine(Arranger(Smart(myHand),IndexOfMin));
        }
        else
        {
            List<Stone> temp = hand.Select(stone => stone.Clone()).ToList();
            StartCoroutine(Arranger(Smart(temp)));
        }
    }
    [ContextMenu("Reset Hand")]
    public void ResetHand()
    {
        Start();
    }
}
