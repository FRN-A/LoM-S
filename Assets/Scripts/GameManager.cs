using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    GameObject cam1;
    [SerializeField]
    GameObject cam2;

    [SerializeField]
    GameObject Food;
    [SerializeField]
    Transform foodSpawnPoint;

    [SerializeField]
    Table[] tables;

    [SerializeField]
    public Player player;

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

    private void Start()
    {
        StartCoroutine(Orders());
    }


    public void ChangeCam()
    {
        cam1.SetActive(!cam1.activeSelf);
        cam2.SetActive(!cam2.activeSelf);
    }
    public IEnumerator MoveToPoint(GameObject gameObject, Vector3 target, float speed)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        while (Vector3.Distance(gameObject.transform.position, target) > 0.01f)
        {
            float step = speed * Time.deltaTime;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, step);
            yield return null;
        }
        StartCoroutine(FadeOut(gameObject));
    }

    public IEnumerator FadeOut(GameObject gameObject)
    {
        /*for (double i = 1; i > 0; i -= 0.1)
        {
            Color tmp = gameObject.GetComponent<Renderer>().material.color;
            gameObject.GetComponent<Renderer>().material.shader = Shader.Find("HDRP / Lit");
            gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor" ,new Color(tmp.r, tmp.g, tmp.b, (float)i));
            yield return new WaitForSeconds(0.05f);
        }*/
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public void CreateFood()
    {
        Instantiate(Food, foodSpawnPoint.position, Quaternion.identity);
    }

    IEnumerator Orders()
    {
        while (true)
        {
            foreach(Table table in tables)
            {
                if (table.Available)
                {
                    if (Random.value < 0.5)
                    {
                        table.NewOrder();
                    }
                }
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
