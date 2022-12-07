using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training : MonoBehaviour
{
    [SerializeField] private List<ItemCircle> TrainingCircles = new List<ItemCircle>();
    private int random = 5;

    public void randomizeCircle()
    {
        int lastrandom = random;
        random = Random.Range(0, 4);
        while (random == lastrandom)
        {
            random = Random.Range(0, 4);
        }

        int i = 0;
        foreach(ItemCircle obj in TrainingCircles)
        {
            if (i == random)
            {
                obj.SetTheOne(true);
            } else
            {
                obj.SetTheOne(false);
            }
            i++;
        }
    }
}
