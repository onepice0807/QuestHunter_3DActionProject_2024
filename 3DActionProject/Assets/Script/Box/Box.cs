using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour
{
    private int _hitCount = 0; // �ڽ��� �� �� �浹�Ǿ����� ī��Ʈ �ϴ� ��
    public GameObject _coinPrefab; // ���� Prefab �����ϴ� ��
    public GameObject _sword; // �� ������Ʈ�� ���� �����ϴ� ��
    public Transform _playerTransform; // �÷��̾��� ��ġ�� �����ϴ� ��
    public int _coinCount = 5; // ������ ������ ����
    public float _coinRiseHeight = 3.0f; // ������ �ڽ����� ���� �� ���� ����
    public float _timeBeforeMoveToPlayer = 1.0f; // ������ �÷��̾�� �̵��ϱ� ���� ��ٸ��� �ð�
    public float _moveSpeed = 1.0f; // ������ �÷��̾�� �̵��ϴ� �ӵ�

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"{_hitCount}"); // ��Ʈ ī��Ʈ �ʱⰪ ���
        // ���� �ð� �Ŀ� ������ �����̵��� ����
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == _sword)
        {
            _hitCount++;
            Debug.Log($"Hit Count: {_hitCount}"); // �浹 �ø��� ��Ʈ ī��Ʈ ���

            if (_hitCount >= 3)
            {
                // �ڽ� ����
                Destroy(gameObject);

                // ���� ����
                for (int i = 0; i < _coinCount; i++)
                {
                    Vector3 coinPosition = transform.position + Vector3.up * _coinRiseHeight + Vector3.right * (i - _coinCount / 2);
                    GameObject coinInstance = Instantiate(_coinPrefab, coinPosition, Quaternion.identity);

                    
                    // ���ο� CoinMovement ��ũ��Ʈ �߰�
                    CoinMovement coinMovement = coinInstance.AddComponent<CoinMovement>();
                    coinMovement._playerTransform = _playerTransform;
                    coinMovement._moveSpeed = _moveSpeed;
                    coinMovement._delayBeforeMoving = _timeBeforeMoveToPlayer;
                   
                }
            }
        }
    }
}
