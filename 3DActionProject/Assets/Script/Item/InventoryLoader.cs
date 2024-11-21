using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryLoader : MonoBehaviour
{
    // ������ ������ ���� Ŭ������ �����ϴºκ�. [System.Serializable]�� Unity Inspector�� �����ϱ� ���� ���
    [System.Serializable]
    public class Item
    {
        public int _id; // �κ��丮 ID��ȣ
        public string _name; // �̸�
        public string _type; // Ÿ��
        public int _count; // ����
        public int _amount; // �ο��� (ex: ü��ȸ����)
        public int _durability; // ���� ������
        public string _description; // �κ��丮 ������ ����
        public string _iconPath; // ������ �̹��� ���� ���
    }

    // ������ ����Ʈ. ��� �κ��丮 �������� �����ϴ� ����Ʈ
    public List<Item> _inventoryItems = new List<Item>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadItem();
    }

    // �������� �ҷ����� ����
    void LoadItem()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("inventoryItems");
        // CSV ������ �ؽ�Ʈ�� �б� ���� StringReader ���
        StringReader stringReader = new StringReader(csvFile.text);

        bool firstLine = true; // ù ��° ������ ����̹Ƿ� �����ϴ� ����
        while (true)
        {
            string line = stringReader.ReadLine(); // ������ �� ���� ����

            if (line == null) break; // �о�� ������ ������ ��ȯ

            if (firstLine) // ù ��° �� �� ��� �κ��� ����
            {
                firstLine = false;
                continue; // ���� �ٷ� �Ѿ��
            }

            // �� ������ �����Ͱ��� ,(��ǥ)�� �����ؼ� �迭�� ����
            string[] values = line.Split(':');

            // ������ ��ü ���� �� �迭 �����͸� ����Ͽ� �ʱ�ȭ
            Item newItem = new Item
            {
                _id = int.Parse(values[0]),
                _name = values[1],
                _type = values[2],
                _count = int.Parse(values[3]),
                _amount = int.Parse(values[4]),
                _durability = int.Parse(values[5]),
                _description = values[6],
                _iconPath = values[7] // CSV���� ������ ��θ� �о�� �Ҵ�
            };
            // �� �������� �κ��丮 ����Ʈ�� �߰�
            _inventoryItems.Add(newItem);
        }

    }

    // �κ��丮�� ���ο� �������� �߰��ϴ� �Լ�
    public void AddItem(int id, string name, string type, int count, int amount, int durability, string description, string iconPath)
    {
        Item newItem = new Item
        {
            _id = id,
            _name = name,
            _type = type,
            _count = count,
            _amount = amount,
            _durability = durability,
            _description = description,
            _iconPath = iconPath
        };

        _inventoryItems.Add(newItem); // �κ��丮 ����Ʈ�� ������ �߰�
        //UpdateCSV(); // CSV ���� ������Ʈ
    }

    // �κ��丮���� �������� �����ϴ� �Լ�
    public void RemoveItem(int id)
    {
        Item itemToRemove = _inventoryItems.Find(item => item._id == id);
        if (itemToRemove != null)
        {
            _inventoryItems.Remove(itemToRemove); // ������ ����Ʈ���� ����
            UpdateCSV(); // CSV ���� ������Ʈ
        }
    }

    // CSV ������ ������Ʈ�ϴ� �Լ�
    void UpdateCSV()
    {
        string filePath = Path.Combine(Application.dataPath, "Resources/inventoryItems.csv");

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("_id,_name,_type,_count,_amount,_durability,_description,_iconPath");

            foreach (var item in _inventoryItems)
            {
                writer.WriteLine($"{item._id},{item._name},{item._type},{item._count},{item._amount},{item._durability},{item._description},{item._iconPath}");
            }
        }

        Debug.Log("Inventory CSV Updated!"); // ����� �޽���
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
