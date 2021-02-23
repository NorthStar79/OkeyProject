using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class testTwoPointOh : MonoBehaviour
{
    public List<Stone> deck = new List<Stone>(); // we dont really need to create a deck for this demo but for any functionality extentiton for future will need some sort of deck implementation 

    public List<Stone> hand = new List<Stone>();

    List<List<Stone>> sequence = new List<List<Stone>>();

    public ArrangeCue arranger;
    void Start()
    {
        CreateDeck();
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



        hand[0].index = 0;
        hand[1].index = 1;
        hand[2].index = 2;
        hand[3].index = 3;
        hand[4].index = 55;
        hand[5].index = 56;
        hand[6].index = 52;
        hand[7].index = 70;
        hand[8].index = 13;
        hand[9].index = 59;
        hand[10].index = 10;
        hand[11].index = 20;
        hand[12].index = 83;
        hand[13].index = 40;

    }

[ContextMenu("Sıra diz")]
    void SeriDiz()
    {
        SortHand();

        int i =0;
        int countCache =0;
        while (hand.Count>0)
        {
            sequence.Add(new List<Stone>());
            sequence[i].Add(hand[0]);
            hand.RemoveAt(0);
            int count =0;
            countCache = count;

            for (int j = 0; j < hand.Count; j++)
            {
                if(Mathf.Abs(sequence[i][count].index%52 - hand[j].index%52)==1)
                //if(sequence[i][count].index+1 == hand[j].index || sequence[i][count].index+53 == hand[j].index )
                {
                    if(Mathf.FloorToInt(sequence[i][0].index/13) == Mathf.FloorToInt(hand[j].index/13))
                    {
                        sequence[i].Add(hand[j]);
                        hand.RemoveAt(j);
                        count++;
                        j=0;
                    }
                }
            }
            if(count==countCache) i++;
        }

        foreach (var item in sequence)
        {
            foreach (var item1 in item)
            {
                Debug.Log(item1.index);
            }
            Debug.Log("Sequence");
        }

        //arranger.Arrange(sequence);
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
