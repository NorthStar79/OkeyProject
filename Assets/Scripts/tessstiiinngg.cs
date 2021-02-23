using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class tessstiiinngg : MonoBehaviour
{
    public List<Stone> deck = new List<Stone>(); // we dont really need to create a deck for this demo but for any functionality extentiton for future will need some sort of deck implementation 

    public List<Stone> hand = new List<Stone>();

    List<List<Stone>> sequence = new List<List<Stone>>();

    public ArrangeCue arranger;
    void Start()
    {
        CreateDeck();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateDeck()
    {
        for (int i = 0; i < 104; i++)
        {
            deck.Add(new Stone(i));
        }
        CreateHand();
    }

    public void CreateHand()
    {
        for (int i = 0; i < 14; i++)
        {
            int j = Random.Range(0, deck.Count);
            hand.Add(deck[j]);
            deck.RemoveAt(j);
        }
        //SeriDiz();


/*
        hand[0].index = 11;
        hand[1].index = 26;
        hand[2].index = 33;
        hand[3].index = 46;
        hand[4].index = 55;
        hand[5].index = 0;
        hand[6].index = 66;
        hand[7].index = 70;
        hand[8].index = 13;
        hand[9].index = 59;
        hand[10].index = 10;
        hand[11].index = 20;
        hand[12].index = 83;
        hand[13].index = 40;
*/


    }
    [ContextMenu("Sıra Diz")]
    void SeriDiz()
    {
        SortHand();

        for (int i = 0; i < hand.Count - 1; i++)
        {
            for (int j = i+1; j < hand.Count; j++)
            {
                if (Mathf.CeilToInt(hand[i].index / 13) == Mathf.CeilToInt(hand[j].index / 13))
                {
                    if (Mathf.Abs(hand[i].index % 52 - hand[j].index % 52) == 1)
                    {
                        Stone tmp = hand[i + 1];
                        hand[i + 1] = hand[j];
                        hand[j] = tmp;
                    }
                    else
                    {

                        /*Stone tmp = new Stone(-1);
                        hand.Insert(i + 1, tmp);
                        i++;*/
                    }
                }
                else
                {
                    /* Stone tmp = new Stone(-1);
                     hand.Insert(i + 1, tmp);
                     i++;*/
                }
            }

        }

        /* foreach (var item in hand)
         {
             Debug.Log(item.index);
         }*/

        //arranger.Arrange(hand);
    }

    [ContextMenu("Çift Diz")]
    void ÇiftDiz()
    {
        SortHand();

        for (int i = 0; i < hand.Count - 1; i++)
        {
            for (int j = i + 1; j < hand.Count; j++)
            {
                if (Mathf.CeilToInt(hand[i].index / 13) != Mathf.CeilToInt(hand[j].index / 13))
                {
                    if (((hand[i].index % 52) % 13 - (hand[j].index % 52) % 13) == 0)
                    {
                        Stone tmp = hand[i + 1];
                        hand[i + 1] = hand[j];
                        hand[j] = tmp;
                    }
                    else
                    {
                        /*Stone tmp = new Stone(-1);
                        hand.Insert(i + 1, tmp);
                        i++;*/
                    }
                }
                else
                {
                    /*Stone tmp = new Stone(-1);
                    hand.Insert(i + 1, tmp);
                    i++;*/
                }
            }

        }

        /*foreach (var item in hand)
        {
            Debug.Log(item.index);
        }
        */
        //arranger.Arrange(hand);
    }

    void AkıllıDiz()
    {

    }

    void SortHand()
    {
        for (int i = 1; i < hand.Count; i++)
        {
            for (int j = 0; j < (hand.Count - i); j++)
            {
                if (hand[j].index > hand[j + 1].index)
                {
                    Stone temp = hand[j];
                    hand[j] = hand[j + 1];
                    hand[j + 1] = temp;
                }
            }
        }
    }
}
