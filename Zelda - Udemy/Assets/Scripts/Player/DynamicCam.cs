using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCam : MonoBehaviour
{

    public GameObject camWall;
    
    private void OnTriggerEnter(Collider other)
{
    switch (other.gameObject.tag)
    {
        case "CamWall":
            camWall.SetActive(true);
            break; 
        // case "CamHalf1":
        //     camHalf1.SetActive(true);
        //     break; 
        // case "CamHalf2":
        //     camHalf2.SetActive(true);
        //     break; 
    }
}

private void OnTriggerExit(Collider other)
{
    switch (other.gameObject.tag)
    {
        case "CamWall":
            camWall.SetActive(false);
            break; // Adicione o break aqui
        // case "CamHalf1":
        //     camHalf1.SetActive(false);
        //     break; 
        // case "CamHalf2":
        //     camHalf2.SetActive(false);
        //     break;       
    }
}
}
