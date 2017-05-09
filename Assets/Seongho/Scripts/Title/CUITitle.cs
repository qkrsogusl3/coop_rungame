﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CUITitle : MonoBehaviour
{
    public AudioClip BtnSfx;
    
    AudioSource MySource;

    public GameObject mFind;

    public static CUITitle instance;

    private void Awake()
    {
        if(CUITitle.instance==null)
        {
            CUITitle.instance = this;
        }
    }
    private void Start()
    {
        MySource = this.gameObject.GetComponent<AudioSource>();
        mFind = GameObject.Find("Main Camera");

        DontDestroyOnLoad(mFind);
    }

    public void OnClickBtnMoveLobby()
    {
        MySource.PlayOneShot(BtnSfx);
        Invoke("invokeLoadScene", 0.5f);
        
       // SceneManager.LoadScene("MainTitleScene",LoadSceneMode.Additive);
    }

    void invokeLoadScene()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
