using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    public string _itemName; // ������ �̸�
    public Sprite _icon; // ������ �̹���
    public string _description; // �� ����
    public int _quantity;  // ����
 
}

public class ItemPickup : MonoBehaviour
{
    public Item _item; // ȹ���� ������

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾�� �浹���� ��
        {
            // �κ��丮�� ������ �߰�
            Inventory._instance.AddItem(_item);

            // ������ ������Ʈ ����
            Destroy(gameObject);
        }
    }
}
