using System.Collections;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    public Transform _playerTransform; // �÷��̾��� ��ġ
    public float _moveSpeed = 0.0f; // ������ �̵��ϴ� �ӵ�
    public float _delayBeforeMoving = 0.0f; // �̵��ϱ� �� ��� �ð�

    private bool _shouldMove = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ���� �ð� �Ŀ� ������ �����̵��� ����
        StartCoroutine(StartMoving());
    }

    IEnumerator StartMoving()
    {
        yield return new WaitForSeconds(_delayBeforeMoving);
        _shouldMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldMove && _playerTransform != null)
        {
            // �÷��̾ ���� �̵�
            Vector3 direction = (_playerTransform.position - transform.position).normalized;
            transform.position += direction * _moveSpeed * Time.deltaTime;

            // ���� �Ÿ� �̳��� ������ ���� ���� �� ȹ�� ó��
            if (Vector3.Distance(transform.position, _playerTransform.position) < 0.5f)
            {
                // �÷��̾�� ���� �߰�
                GameManager._Instance.AddCoin();

                // ���� ������Ʈ ����
                Destroy(gameObject);
            }
        }
    }
}