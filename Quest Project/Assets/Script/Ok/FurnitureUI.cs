using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FurnitureUI : MonoBehaviour
{

    [SerializeField]
    private GameObject choooseScroll;

    [SerializeField]
    private GameObject content;

    [SerializeField]
    private GameObject copyObject;

    [SerializeField]
    private List<GameObject> flower = new List<GameObject>();

    [SerializeField]
    private List<GameObject> lamp = new List<GameObject>();

    private List<GameObject> scrollObject = new List<GameObject>();

    void Start()
    {

    }


    public void choice(string category)
    {
        ClearList();

        if (category == "flower")
        {
            foreach (GameObject obj in flower) 
            {
                GameObject copy = Instantiate(copyObject);
                copy.name = obj.name;
                scrollObject.Add(obj);
            }
        }
        else if (category == "lamp")
        {
            foreach (GameObject obj in lamp)
            {
                GameObject copy = Instantiate(copyObject);
                copy.name = obj.name;
                scrollObject.Add(obj);
            }
        }
    }


    private void ClearList()
    {
        foreach (GameObject obj in scrollObject)
            Destroy(obj);
        scrollObject.Clear();
    }

}
