using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Button btnStart;
    [SerializeField]
    RectTransform[] clouds;
    [SerializeField, Range(0, 20)]
    float[] moveSpeed;
    RectTransform canvas;

    void Awake()
    {
        canvas = GetComponent<RectTransform>();
        btnStart.onClick.AddListener(LoadScene);
    }

    void Update()
    {
        if (Input.GetButtonDown("Start"))
            LoadScene();

        int i=0;
        foreach(RectTransform cloud in clouds)
        {
            Vector3 position = cloud.transform.position;
            position.x += moveSpeed[i++] * Time.deltaTime;
            cloud.transform.position = position;
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Level01");
    }
}
