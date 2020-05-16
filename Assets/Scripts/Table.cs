using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    private bool available;
    Order order;
    [SerializeField]
    int orderTime;

    public bool Available
    {
        get => available;
        set => available = value;
    }
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    Text txt_food;
    [SerializeField]
    public Image timer;

    [SerializeField]
    GameObject alert;

    IEnumerator timeToLive;

    private void Awake()
    {
        available = true;
        order = null;
    }

    public void NewOrder()
    {
        int food = (int)UnityEngine.Random.Range(1f, 6f);
        int time = food * orderTime;
        order = new Order(food, time, this);
        available = false;
        timeToLive = order.TimeToLive();
        StartCoroutine(timeToLive);
        canvas.SetActive(true);
        txt_food.text = $"{food}";
        timer.fillAmount = 0.7f;
        SoundManager.instance.NewOrder();
        alert.SetActive(true);
    }

    public void ServeFood(int points)
    {
        if (!available)
        {
            if (order.CurrentTime > 0.01f) {
                GameManager.instance.UpdateScore(points);
                if (order.Food > 1)
                {
                    order.Food--;
                    txt_food.text = $"{order.Food}";
                }
                else
                {
                    EndOrder();
                }
            }
        }
        else
        {
            GameManager.instance.UpdateLifes(-1);
            SoundManager.instance.Fail();
        }
    }

    void EndOrder()
    {
        if(order != null)
        {
            GameManager.instance.UpdateScore(30);
            StopCoroutine(timeToLive);
            order = null;
            timeToLive = null;
            available = true;
            canvas.SetActive(false);
            alert.SetActive(false);
        }
    }

    public void EndOrderTime()
    {
        if (order != null)
        {
            order = null;
            timeToLive = null;
            available = true;
            GameManager.instance.UpdateLifes(-1);
            canvas.SetActive(false);
            SoundManager.instance.Fail();
            alert.SetActive(false);
        }
    }
}
