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

        cam1.SetActive(!cam1.activeSelf);
        cam2.SetActive(!cam2.activeSelf);

    }
}
