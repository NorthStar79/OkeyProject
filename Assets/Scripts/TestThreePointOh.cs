using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestThreePointOh : MonoBehaviour
{
    public List<Stone> deck = new List<Stone>(); // we dont really need to create a deck for this demo but for any functionality extentiton for future will need some sort of deck implementation 

    public List<Stone> hand = new List<Stone>();
    public ArrangeCue arranger;

    public int OkeyIndex;
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
        OkeyIndex = Random.Range(0,104);
    }

     void CreateHand()
    {
        for (int i = 0; i < 14; i++)
        {
            int j = Random.Range(0, deck.Count);
            hand.Add(deck[j]);
            deck.RemoveAt(j);
        }
        //SeriDiz();


/*
        hand[0].index = 0;
        hand[1].index = 1;
        hand[2].index = 2;
        hand[3].index = 91;
        hand[4].index = 55;
        hand[5].index = 56;
        hand[6].index = 5;
        hand[7].index = 70;
        hand[8].index = 13;
        hand[9].index = 59;
        hand[10].index = 10;
        hand[11].index = 31;
        hand[12].index = 83;
        hand[13].index = 52;
*/
        /*
                hand[0].index = 56;
                hand[1].index = 69;
                hand[2].index = 80;
                hand[3].index = 88;
                hand[4].index = 89;
                hand[5].index = 90;
                hand[6].index = 5;
                hand[7].index = 16;
                hand[8].index = 17;
                hand[9].index = 21;
                hand[10].index = 32;
                hand[11].index = 34;
                hand[12].index = 45;
                hand[13].index = 53;
        */
 /*       
                hand[0].index = 44;
                hand[1].index = 95;
                hand[2].index = 17;
                hand[3].index = 4;
                hand[4].index = 72;
                hand[5].index = 7;
                hand[6].index = 8;
                hand[7].index = 86;
                hand[8].index = 35;
                hand[9].index = 36;
                hand[10].index = 1;
                hand[11].index = 9;
                hand[12].index = 101;
                hand[13].index = 50;
 */       
    }

    List<List<Stone>> SeriDiz(List<Stone> myHand)
    {

        myHand = SortHand(myHand);
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
                /*foreach (var it in sequence[i])
                {
                    sequence[sequence.Count - 1].Add(it);
                }*/
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

    List<List<Stone>> EsDiz(List<Stone> myHand)
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
                /*foreach (var it in sequence[i])
                {
                    sequence[sequence.Count - 1].Add(it);
                }*/
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

    void AkilliDiz(List<Stone> myHand)
    {
        List<Stone> handCache = new List<Stone>(myHand);

        int SeriArtik, CiftArtik;
        List<List<Stone>> temp, temp1;

        temp = SeriDiz(myHand);
        SeriArtik = temp[temp.Count - 1].Count;
        temp.Clear();
        myHand.Clear();
        myHand = new List<Stone>(handCache);
        temp1 = EsDiz(myHand);
        CiftArtik = temp1[temp1.Count - 1].Count;
        temp1.Clear();
        myHand.Clear();
        myHand = new List<Stone>(handCache);

        if (SeriArtik < CiftArtik)
        {
            temp = SeriDiz(myHand);
            myHand = new List<Stone>(temp[temp.Count - 1]);
            temp1 = EsDiz(myHand);

            temp.RemoveAt(temp.Count - 1);

            arranger.Arrange(temp, temp1);
        }
        else
        {
            temp1 = EsDiz(myHand);
            myHand = new List<Stone>(temp1[temp1.Count - 1]);
            temp = SeriDiz(myHand);

            temp1.RemoveAt(temp1.Count - 1);

            arranger.Arrange(temp1, temp);
        }
    }

    List<List<Stone>> AkilliDiz1(List<Stone> myHand)
    {
        List<Stone> handCache = new List<Stone>(myHand);

        List<List<Stone>> temp = new List<List<Stone>>(), temp1= new List<List<Stone>>();

        int i, j;

        temp = SeriDiz(myHand);
        myHand = new List<Stone>(temp[temp.Count - 1]);
        temp1 = EsDiz(myHand);

        i = temp1[temp1.Count - 1].Count;

        temp.Clear();
        temp1.Clear();
        myHand = new List<Stone>(handCache);


        temp1 = EsDiz(myHand);
        myHand = new List<Stone>(temp1[temp1.Count - 1]);
        temp = SeriDiz(myHand);

        j = temp[temp.Count - 1].Count;

        temp.Clear();
        temp1.Clear();
        myHand = new List<Stone>(handCache);

        if (i < j)
        {
            Debug.Log("ÖnceSeri");
            temp = SeriDiz(myHand);
            myHand = new List<Stone>(temp[temp.Count - 1]);
            temp1 = EsDiz(myHand);

            temp.RemoveAt(temp.Count - 1);

            for (int a = 0; a < temp1.Count; a++)
            {
                temp.Add(new List<Stone>());
                for (int b = 0; b < temp1[a].Count; b++)
                {
                    temp[temp.Count-1].Add(temp1[a][b]);
                }
            }

            return temp;
        }
        else
        {
            Debug.Log("ÖnceEş");
            temp1 = EsDiz(myHand);
            myHand = new List<Stone>(temp1[temp1.Count - 1]);
            temp = SeriDiz(myHand);

            temp1.RemoveAt(temp1.Count - 1);

            for (int a = 0; a < temp.Count; a++)
            {
                temp1.Add(new List<Stone>());
                for (int b = 0; b < temp[a].Count; b++)
                {
                    temp1[temp1.Count-1].Add(temp[a][b]);
                }
            }

            return temp1;
        }

    }

    List<Stone> SortHand(List<Stone> myHand)
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

    [ContextMenu("Seri diz")]

    public void Seri()
    {
        List<Stone> temp = hand.Select(stone => stone.Clone()).ToList();
        arranger.Arrange(SeriDiz(temp));
    }
    [ContextMenu("Es Diz")]

    public void Es()
    {
        List<Stone> temp = hand.Select(stone => stone.Clone()).ToList();

        arranger.Arrange(EsDiz(temp));
    }
    [ContextMenu("Akilli Diz")]

    public void Akilli()
    {
        List<Stone> temp = hand.Select(stone => stone.Clone()).ToList();
        //AkilliDiz(temp);
        arranger.Arrange(AkilliDiz1(temp));
    }
    [ContextMenu("Reset Hand")]
    public void ResetHand()
    {
        Start();
    }

    [ContextMenu("OkeySeri")]
    void OkeySeri()
    {
        List<Stone> myHand = hand.Select(stone => stone.Clone()).ToList();

        bool haveOkey = false;

        for (int i = 0; i < myHand.Count; i++)
        {
            if (myHand[i].index == OkeyIndex)
            {
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
                List<List<Stone>> temp = SeriDiz(myHand);
                artiklar.Add(temp[temp.Count - 1].Count);
            }

            int IndexOfMin = artiklar.IndexOf(artiklar.Min());

            myHand = myHandCache.Select(stone => stone.Clone()).ToList();
            myHand.Add(new Stone(IndexOfMin));

            arranger.Arrange(SeriDiz(myHand));
        }
        else
        {
            List<Stone> temp = hand.Select(stone => stone.Clone()).ToList();
            arranger.Arrange(SeriDiz(temp));
        }
    }
[ContextMenu("OkeyEs")]
    void OkeyEs()
    {
        List<Stone> myHand = hand.Select(stone => stone.Clone()).ToList();

        bool haveOkey = false;

        for (int i = 0; i < myHand.Count; i++)
        {
            if (myHand[i].index == OkeyIndex)
            {
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
                List<List<Stone>> temp = EsDiz(myHand);
                artiklar.Add(temp[temp.Count - 1].Count);
            }

            int IndexOfMin = artiklar.IndexOf(artiklar.Min());

            myHand = myHandCache.Select(stone => stone.Clone()).ToList();
            myHand.Add(new Stone(IndexOfMin));

            arranger.Arrange(EsDiz(myHand));
        }
        else
        {
            List<Stone> temp = hand.Select(stone => stone.Clone()).ToList();
            arranger.Arrange(EsDiz(temp));
        }
    }

[ContextMenu("OkeyAkilli")]
    void OkeyAkilli()
    {
        List<Stone> myHand = hand.Select(stone => stone.Clone()).ToList();

        bool haveOkey = false;

        for (int i = 0; i < myHand.Count; i++)
        {
            if (myHand[i].index == OkeyIndex)
            {
                haveOkey = true;
                myHand.RemoveAt(i);
                break;
            }
        }

        if (haveOkey)
        {
            Debug.Log("ı HAVE GOT THE POWER");
            List<int> artiklar = new List<int>();
            List<Stone> myHandCache = myHand.Select(stone => stone.Clone()).ToList();
            for (int i = 0; i < 104; i++)
            {
                myHand = myHandCache.Select(stone => stone.Clone()).ToList();
                myHand.Add(new Stone(i));
                List<List<Stone>> temp = AkilliDiz1(myHand);
                artiklar.Add(temp[temp.Count - 1].Count);
            }

            int IndexOfMin = artiklar.IndexOf(artiklar.Min());

            myHand = myHandCache.Select(stone => stone.Clone()).ToList();
            myHand.Add(new Stone(IndexOfMin));

            arranger.Arrange(AkilliDiz1(myHand));
        }
        else
        {
            List<Stone> temp = hand.Select(stone => stone.Clone()).ToList();
            arranger.Arrange(AkilliDiz1(temp));
        }
    }
}

