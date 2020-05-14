using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    int points;
    public int Points { 
        get => points; 
        set => points = value; 
    }

    private bool isQuitting = false;
    void OnApplicationQuit()
    {
        isQuitting = true;
    }
    private void OnDestroy()
    {
        if (!isQuitting) {
            GameManager.instance.CreateFood();
        }
    }
}
