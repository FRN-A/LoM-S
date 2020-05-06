using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    GameObject cam1;
    [SerializeField]
    GameObject cam2;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeCam()
    {
        if (cam1.activeSelf)
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
        }
        else
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
        }
    }
}
