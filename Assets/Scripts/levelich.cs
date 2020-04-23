using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelich : MonoBehaviour
{
    [SerializeField] int destroyedEnemyShips; //dlya debaga
    sceneSwap sceneloader;
    private void Start()
    {
        sceneloader = FindObjectOfType<sceneSwap>();
    }
    public void CountDestroyedEnemyShips()
    {
        destroyedEnemyShips++;
    }
    public void AllShipsDestroyed()
    {
        destroyedEnemyShips--;
        if (destroyedEnemyShips <= 0)
        {
            sceneloader.LoadNextScene();
        }
    }
}

