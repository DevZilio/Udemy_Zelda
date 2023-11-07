using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers: MonoBehaviour
{

    public GameObject camWall;

    private GameManager _gameManager;


    private void Start() {
        _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
    }
    
    private void OnTriggerEnter(Collider other)
{
    switch (other.gameObject.tag)
    {
        case "CamWall":
            camWall.SetActive(true);
            break; 

        case "Collectable":
        Destroy(other.gameObject);
        _gameManager.SetGems(1);
        break;

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
