using UnityEngine;
using System.Collections;

public class ItemBox : MonoBehaviour
{
    private int _hitCount = 0; // �ڽ��� �� �� �浹�Ǿ����� ī��Ʈ �ϴ� ��
    public GameObject _itemPrefab; // ���� ������ ����
    public GameObject _sword; // �� ������Ʈ�� ���� �����ϴ� ��
    public Transform _playerTransform; // �÷��̾��� ��ġ�� �����ϴ� ��
    public int _itemCount = 5; // ������ ������ ���� (����)
    public float _itemRiseHeight = 3.0f; // �������� �ڽ����� ���� �� ���� ����
    public float _timeBeforeMoveToPlayer = 1.0f; // �������� �÷��̾�� �̵��ϱ� ���� ��ٸ��� �ð�
    public float _moveSpeed = 1.0f; // �������� �÷��̾�� �̵��ϴ� �ӵ�

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == _sword)
        {
            _hitCount++;
            Debug.Log($"Hit Count: {_hitCount}");

            if (_hitCount >= 3)
            {
                // �ڽ� �ı�
                Destroy(gameObject);



                // ������ ����
                Vector3 itemPosition = transform.position + Vector3.up * _itemRiseHeight;
                GameObject itemInstance = Instantiate(_itemPrefab, transform.position, Quaternion.identity);

                // ItemPickup ������Ʈ Ȯ��
                ItemPickup itemPickup = itemInstance.GetComponent<ItemPickup>();
                if (itemPickup != null)
                {
                    
                }

                // �������� �÷��̾�� �̵���Ű�� ����
                ItemMovement itemMovement = itemInstance.AddComponent<ItemMovement>();
                itemMovement._playerTransform = _playerTransform;
                itemMovement._moveSpeed = _moveSpeed;
                itemMovement._delayBeforeMoving = _timeBeforeMoveToPlayer;
            }
        }
    }
}