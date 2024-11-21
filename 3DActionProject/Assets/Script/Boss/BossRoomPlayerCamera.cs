using UnityEngine;

public class BossRoomPlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _PlayerTarget = null; // �÷��̾ Ÿ������ �������� ���� ����

    private float _distance = 3.5f;  // TPS ���������� �Ÿ� (�÷��̾�� ī�޶� ���� �Ÿ�)
    private float _height = 1.5f;  // ī�޶��� ���� (�÷��̾��� ��� ���� ����)
    private float _rotationSpeed = 5f; // ī�޶� ȸ�� �ӵ�

    private float _currentX = 0f;  // ���콺 X�� �� �����
    private float _currentY = 0f;  // ���콺 Y�� �� �����
    private float _minYAngle = -20f; // ī�޶��� �ּ� ���� ���� (���� ����)
    private float _maxYAngle = 20f;  // ī�޶��� �ִ� ���� ���� (���� ����)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ���콺 Ŀ���� ȭ�鿡 �������Ѽ� �þ� ������ ���� �մϴ�.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (_PlayerTarget == null) return; // �÷��̾� Ÿ���� null�̶�� ��ȯ

        // ���콺 �Է��� �޾� ī�޶� ȸ���� ����
        _currentX += Input.GetAxis("Mouse X") * _rotationSpeed;
        _currentY -= Input.GetAxis("Mouse Y") * _rotationSpeed;

        // Y�� ȸ�� ����
        _currentY = Mathf.Clamp(_currentY, _minYAngle, _maxYAngle);

        // ī�޶��� ȸ�� ������ ���� (TPS ����)
        Quaternion rotation = Quaternion.Euler(_currentY, _currentX, 0f);

        // �÷��̾��� ��ġ�� �������� ī�޶� ��ġ ���
        Vector3 position = _PlayerTarget.position
                         - rotation * Vector3.forward * _distance // �÷��̾� �������� ���� �Ÿ���ŭ
                         + Vector3.up * _height; // ���̸�ŭ ����

        // ī�޶� ��ġ �� ȸ�� ����
        transform.position = position;
        transform.LookAt(_PlayerTarget.position + Vector3.up * _height / 2); // ĳ���� �߽��� �ٶ󺸵��� ����
    }

    // Update is called once per frame
    void Update()
    {
        // ī�޶� ȸ���� ���콺 ���������� ����
        _currentX += Input.GetAxis("Mouse X") * _rotationSpeed;
        _currentY -= Input.GetAxis("Mouse Y") * _rotationSpeed;
    }
}
