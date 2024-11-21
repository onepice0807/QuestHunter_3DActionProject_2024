using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _PlayerTarget = null; // �÷��̾ Ÿ������ �������� ���� ����

    private float _distance = 6f;  // �÷��̾���� �Ÿ� (Z�� ����)
    private float _height = 3f;    // ī�޶��� ���� (Y�� ����)
    private float _angle = 35f;    // ī�޶��� �����ٺ��� ����

    public bool _isSideScrolling = false;  // ī�޶� Ⱦ��ũ�� ������� ���θ� ��Ÿ���� ����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void LateUpdate()
    {
        if (_PlayerTarget == null) return; // �÷��̾� Ÿ���� null�̶�� ��ȯ

        if (_isSideScrolling)
        {
            // Ⱦ��ũ�� �� ����
            // ī�޶��� ȸ�� ������ ���� (X���� �������� 0��ŭ �Ʒ��� �ٶ󺸵���)
            Quaternion rotation = Quaternion.Euler(0f, 0f, 0f); // Ⱦ��ũ�� ����

            // �÷��̾��� ��ġ�� �������� ī�޶� ��ġ ���
            Vector3 position = _PlayerTarget.position
                             - rotation * Vector3.left * _distance // �Ÿ���ŭ �ڷ�
                             + Vector3.up * 1; // ���̸�ŭ ����

            // ī�޶��� ��ġ�� ȸ�� ����
            transform.position = position;
            transform.LookAt(_PlayerTarget.position + Vector3.up * _height / 2); // ĳ���� �߽��� �ٶ󺸵��� ����
        }
        else
        {
            // ���ͺ� ����

            // ī�޶��� ȸ�� ������ ���� (X���� �������� _angle��ŭ �Ʒ��� �ٶ󺸵���)
            Quaternion rotation = Quaternion.Euler(_angle, 0f, 0f); // 35�� ������ �����ٺ��� ����

            // �÷��̾��� ��ġ�� �������� ī�޶� ��ġ ���
            Vector3 position = _PlayerTarget.position
                             - rotation * Vector3.forward * _distance // �Ÿ���ŭ �ڷ�
                             + Vector3.up * _height; // ���̸�ŭ ����

            // ī�޶��� ��ġ�� ȸ�� ����
            transform.position = position;
            transform.LookAt(_PlayerTarget.position + Vector3.up * _height / 2); // ĳ���� �߽��� �ٶ󺸵��� ����
        }

        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
