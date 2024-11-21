using UnityEngine;

public class LandTrigger : MonoBehaviour
{
    public PlayerCamera _cameraController;  // ī�޶� ���� ��ũ��Ʈ
    public PlayerController _playerController; // player ���� ��ũ��Ʈ
    public MonsterController _monsterController; // �� ���� ��ũ��Ʈ

    [SerializeField] private bool _isLand2 = false;  // ���� ������ Land2���� ����


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Land2�� ���� ī�޶� ��带 Ⱦ��ũ�� ��� ��ȯ
            if (_isLand2)
            {
                _cameraController._isSideScrolling = true;
                _playerController._isSideScrolling = true;
                _monsterController._isSideScrolling = true;

            }
            // Land�� ���ƿ��� �ٽ� ���ͺ�� ��ȯ
            else
            {
                _cameraController._isSideScrolling = false;
                _playerController._isSideScrolling = false;
                _monsterController._isSideScrolling = false;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
