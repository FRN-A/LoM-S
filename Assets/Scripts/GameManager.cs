using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    Transform playerResetPosition;
    [SerializeField]
    int lifesAmount;
    int lifes;
    [SerializeField]
    Text txt_lifes;
    int score;
    [SerializeField]
    Text txt_score;

    [SerializeField]
    Image blkImage;
    [SerializeField]
    Image gameOverImage;
    [SerializeField]
    Text txtGameOver;
    public bool gameOver;
    [SerializeField]
    GameObject tutorial;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        lifes = lifesAmount;
        txt_lifes.text = $"{lifes}";
        score = 0;
        txt_score.text = $"{score}";
        StartCoroutine(Orders());
        StartCoroutine(FoodStart());
        gameOver = false;
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Reset();
                SceneManager.LoadScene("Level01");
            }
        }
        if (tutorial.activeSelf)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Time.timeScale = 1;
                tutorial.SetActive(false);
            }
        }
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

    IEnumerator FadeIn(MaskableGraphic element)
    {
        for (float i = 0; i <= 1; i += 0.1f)
        {
            Color tmp = element.color;
            tmp.a = i;
            element.color = tmp;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    public void CreateFood()
    {
        Instantiate(Food, foodSpawnPoint.position, Quaternion.identity);
    }

    IEnumerator FoodStart()
    {
        for (int i = 0; i < 5; i++)
        {
            CreateFood();
            yield return new WaitForSeconds(4f);
        }
    }

    IEnumerator Orders()
    {
        while (true)
        {
            for (int i = 0; i < tables.Length; ++i)
            {
                if (tables[i].Available)
                {
                    if (Random.value < 0.5)
                    {
                        tables[i].NewOrder();
                    }
                    yield return new WaitForSeconds(2f);
                }
            }
            yield return new WaitForSeconds(4f);
        }
    }

    public void UpdateLifes(int life)
    {
        lifes += life;
        txt_lifes.text = $"{lifes}";
        if (lifes <= 0)
        {
            GameOver();
        }
    }

    public void UpdateScore(int score)
    {
        this.score += score;
        txt_score.text = $"{this.score}";
    }

    IEnumerator WaitForGameOver()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        gameOver = true;
    
    }

    void GameOver()
    {
        StartCoroutine(WaitForGameOver());
        blkImage.gameObject.SetActive(true);
        StartCoroutine(FadeIn(blkImage));
        StartCoroutine(FadeIn(gameOverImage));
        StartCoroutine(FadeIn(txtGameOver));
        Time.timeScale = 0f;
        SoundManager.instance.GameOver();
    }

    private void Reset()
    { 
        Time.timeScale = 1f;
    }
}
