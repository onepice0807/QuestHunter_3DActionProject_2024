using UnityEngine;

public class Coin : MonoBehaviour
{
    public float _rotationSpeed = 500.0f; // ������ ȸ�� �ӵ�

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // �÷��̾�� �浹 �� ������ ������� �ϰ� ���� �� ����

    // Update is called once per frame
    void Update()
    {
        // ������ ���� �ӵ��� ȸ����Ų��
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
    }
}
