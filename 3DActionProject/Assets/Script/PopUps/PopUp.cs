using System;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public Action _callFunc;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnClickOkButton()
    {
        _callFunc();
    }

    public void OnClickClose()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1; // ���� �簳
        Cursor.visible = false; // ���콺 Ŀ�� ����
        Cursor.lockState = CursorLockMode.Locked; // ���콺 ���
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
