using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalIntro : MonoBehaviour
{
    public GameObject AnimalsInfo;

    public void Start()
    {
        AnimalsInfo.SetActive(false);
    }

    public void OnMouseOver()
    {
        AnimalsInfo.SetActive(true);
    }

    public void OnMouseExit()
    {
        AnimalsInfo.SetActive(false);
    }


}
