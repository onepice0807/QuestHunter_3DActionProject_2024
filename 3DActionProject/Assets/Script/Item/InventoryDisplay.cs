using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public InventoryLoader _inventoryLoader; // InventoryLoader ��ũ��Ʈ �����ؼ� �κ��丮 �����͸� ��������

    // ������ ������ ǥ���� UI ������ (icon �� ����)
    public GameObject _itemUIPrefab;

    // ������ ������ ǥ���� UI ������ (�̸�, �󼼳����) - ������ �гο�
    public GameObject _ItemUIRightPrefab;

    // ������ UI�� ��ġ�� �θ� ������Ʈ, �Ϲ������� ��ũ�Ѻ� ���� �г��� ���(icon �� ����)
    public Transform _InventoryLeftPanel;

    // ������ ������ ǥ���� ������ �г� (�� ����)
    public Transform _InventoryRightPanel;

    // ���� ������ �гο� ǥ�õǰ� �ִ� ������ ���� UI
    private GameObject _currentRightItemUI;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        DisplayInventory(); // �κ��丮 UI�� ǥ���ϴ� �Լ� ȣ��
        ClearRightPanel(); // ������ �� ������ �г��� ���
    }

    // �κ��丮�� �ִ� �������� UI�� ǥ���ϴ� �Լ�
    void DisplayInventory()
    {
        foreach (var item in _inventoryLoader._inventoryItems)
        {
            // �κ��丮 �гο� ������ UI �������� �ν��Ͻ�ȭ (���� �г�)
            GameObject newItemUI = Instantiate(_itemUIPrefab, _InventoryLeftPanel);

            // ���� �гο� �ؽ�Ʈ �� �� ������ �Ҵ�
            Text countText = newItemUI.transform.Find("CountText")?.GetComponent<Text>();
            if (countText != null)
                countText.text = "����: " + item._count;

            Transform iconTransform = newItemUI.transform.Find("ItemIcon");
            if (iconTransform != null)
            {
                Image itemIcon = iconTransform.GetComponent<Image>();
                Sprite iconSprite = Resources.Load<Sprite>(item._iconPath);
                if (iconSprite != null)
                    itemIcon.sprite = iconSprite;
            }

            // ������ Ŭ�� �̺�Ʈ �߰�
            Button itemButton = newItemUI.GetComponent<Button>();
            if (itemButton != null)
            {
                InventoryLoader.Item currentItem = item; // ���� ������ �����Ͽ� Ŭ���� ���� �ذ�
                itemButton.onClick.AddListener(() => DisplayItemDetails(currentItem)); // Ŭ�� �̺�Ʈ �߰�
                Debug.Log("��ư Ŭ�� �̺�Ʈ�� �߰�: " + item._name); // ����� �α׷� Ȯ��
            }

        }
    }

    // ������ �гο� ������ �� ������ ǥ���ϴ� �Լ�
    public void DisplayItemDetails(InventoryLoader.Item item)
    {
        // ������ �г��� ���� ������ ���� (�׻� �ֽ� ������ ������ ǥ��)
        ClearRightPanel();

        // ���ο� �������� �ν��Ͻ�ȭ�Ͽ� ������ �гο� ǥ��
        _currentRightItemUI = Instantiate(_ItemUIRightPrefab, _InventoryRightPanel);

        // ������ �гο� �ؽ�Ʈ �� �Ҵ�
        Text nameText = _currentRightItemUI.transform.Find("NameText").GetComponent<Text>();
        Text typeText = _currentRightItemUI.transform.Find("TypeText").GetComponent<Text>();
        Text amountText = _currentRightItemUI.transform.Find("AmountText").GetComponent<Text>();
        Text durabilityText = _currentRightItemUI.transform.Find("DurabilityText").GetComponent<Text>();
        Text descriptionText = _currentRightItemUI.transform.Find("DescriptionText").GetComponent<Text>();

        if (nameText != null) nameText.text = "�̸�: " + item._name;
        if (typeText != null) typeText.text = "Ÿ��: " + item._type;
        if (amountText != null) amountText.text = "�ο� �Ӽ�: " + item._amount;
        if (durabilityText != null) durabilityText.text = "������: " + item._durability;
        if (descriptionText != null) descriptionText.text = "����: " + item._description;
    }

    // ������ �г��� ������ �����ϴ� �Լ�
    public void ClearRightPanel()
    {
        if (_currentRightItemUI != null)
        {
            Destroy(_currentRightItemUI); // ������ ǥ�õ� ������ ���� UI�� ����
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
