using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float backgroundSize;

    private Transform player;
    private Transform tempBackground;
    List<Transform> background = new List<Transform>();
 
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        background.Add(null);
        background.Add(null);
        background.Add(null);
        GameObject[] backgroundTemp = GameObject.FindGameObjectsWithTag("BackgroundTag");
        foreach (GameObject Obj in backgroundTemp)
        {
            if(Obj.transform.name == "Background 1")
            {
                background[0] = Obj.transform;
                Debug.Log("1");
            }
            if (Obj.transform.name == "Background 2")
            {
                background[1] = Obj.transform;
                Debug.Log("2");
            }
            if (Obj.transform.name == "Background 3")
            {
                background[2] = Obj.transform;
                Debug.Log("3");
            }
        }
    }

    private void Update()
    {
       
       if (background[2].transform.position.x < (player.transform.position.x + 180))
       {
            tempBackground = background[2];
            background[2] = background[0];
            background[0] = background[1];
            background[1] = tempBackground;

            background[2].position = new Vector2(background[1].transform.position.x + 180, 0);
       }
       if (background[0].transform.position.x > (player.transform.position.x - 180))
       {
            tempBackground = background[0];
            background[0] = background[2];
            background[2] = background[1];
            background[1] = tempBackground;

            background[0].position = new Vector2(background[1].transform.position.x - 180, 0);
       }

    }

    private void ScrollLeft()
    {

    }

    private void ScrollRight()
    {
      
    }
}
