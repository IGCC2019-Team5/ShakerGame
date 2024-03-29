﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBGSize : MonoBehaviour
{
    public static ChangeBGSize instance = null;
    Animator m_Animator;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (m_Animator == null)
        {
            m_Animator = GetComponent<Animator>();
        }
    }
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void ZoomOutBG()
    {
        m_Animator.SetTrigger("ZoomOut");
    }
    public void ZoomInBG()
    {
        m_Animator.SetTrigger("ZoomIn");
    }
    public static void Destory()
    {
        if (instance)
            Destroy(instance.gameObject);
    }
}