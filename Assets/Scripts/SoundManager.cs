using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    AudioSource backgorund;
    [SerializeField]
    AudioSource gameOver;
    [SerializeField]
    AudioSource throwFood;
    [SerializeField]
    AudioSource fail;
    [SerializeField]
    AudioSource order;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        backgorund.Play();
    }

    public void GameOver()
    {
        backgorund.Stop();
        gameOver.Play();
    }

    public void ThrowFood()
    {
        throwFood.Play();
    }

    public void Fail()
    {
        fail.Play();
    }

    public void NewOrder()
    {
        order.Play();
    }
}
