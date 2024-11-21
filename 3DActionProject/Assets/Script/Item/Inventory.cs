using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory _instance;
    public List<Item> _items = new List<Item>(); // ������ ����Ʈ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        // �̱��� ���� ����
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // ������ �߰� �޼���
    public void AddItem(Item item)
    {
        _items.Add(item);
        Debug.Log($"{item._itemName}��(��) �κ��丮�� �߰��߽��ϴ�.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
