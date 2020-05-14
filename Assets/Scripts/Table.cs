﻿using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        available = true;
        order = null;
    }

    [SerializeField]
    GameObject canvas;
    [SerializeField]
    Text txt_food;
    [SerializeField]
    public Image timer;

    public void NewOrder()
    {
        int food = (int)UnityEngine.Random.Range(1f, 6f);
        int time = food * orderTime;
        order = new Order(food, time, this);
        available = false;
        StartCoroutine(order.TimeToLive());
        canvas.SetActive(true);
        txt_food.text = $"{food}";
        timer.fillAmount = 0.7f;
        Debug.Log($"Nueva orden: quesos:{food}, tiempo:{time}");
    }

    public void ServeFood(int points)
    {
        if (!available)
        {
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
        else
        {
            GameManager.instance.UpdateLifes(-1);
        }
    }

    void EndOrder()
    {
        GameManager.instance.UpdateScore(30);
        order = null;
        available = true;
        canvas.SetActive(false);
        Debug.Log("Orden servida");
    }

    public void EndOrderTime()
    {
        order = null;
        available = true;
        GameManager.instance.UpdateLifes(-1);
        canvas.SetActive(false);
    }
}
