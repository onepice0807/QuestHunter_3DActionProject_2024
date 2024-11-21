using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public Transform _playerTransform;
    public float _moveSpeed = 1.0f;
    public float _delayBeforeMoving = 1.0f;

    private bool _shouldMove = false;
    private ItemPickup _itemPickup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _itemPickup = GetComponent<ItemPickup>();  // ItemPickup ������Ʈ ��������
        if (_itemPickup == null)
        {
            Debug.LogError("ItemPickup ������Ʈ�� �����ۿ� ����Ǿ� ���� �ʽ��ϴ�.");
        }
        Invoke("StartMoving", _delayBeforeMoving);
    }

    void StartMoving()
    {
        _shouldMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldMove && _playerTransform != null)
        {
            // �÷��̾� �������� �̵�
            transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, _moveSpeed * Time.deltaTime);

            // �������� �÷��̾ �����ϸ� ȹ�� ó��
            if (Vector3.Distance(transform.position, _playerTransform.position) < 0.5f)
            {
                // �κ��丮�� ������ �߰�
                if (_itemPickup != null && _itemPickup._item != null)
                {
                    // ī�װ��� �ʿ信 ���� ���� ("���" �Ǵ� "����")
                    
                }

                // ������ ������Ʈ ����
                Destroy(gameObject);
            }
        }
    }
}