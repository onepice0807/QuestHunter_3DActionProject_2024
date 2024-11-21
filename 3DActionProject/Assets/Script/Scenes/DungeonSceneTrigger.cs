using UnityEngine;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� ���ӽ����̽� �߰�
using UnityEngine.UI; // UI Text�� ����ϱ� ���� ���ӽ����̽� �߰�

public class DungeonSceneTrigger : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad = "DungeonScene"; // �ε��� ���� �̸�
    [SerializeField] private float _delayBeforeLoad = 5.0f; // �� ��ȯ �� ��� �ð�
    [SerializeField] private GameObject _popUp; // �÷��̾��� �ش�� �̵����� �Ǵ��ϴ� �˾�â
    [SerializeField] private Text _popUpText; // ��� �ð� ī��Ʈ�ٿ� UI �ؽ�Ʈ

    private bool _playerInside = false; // �÷��̾ Ʈ���� �ȿ� �ִ��� ���� Ȯ��
    private bool _insideScene = false; // �ش� ���� �̵��ϴ� ���� Ȯ��

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _popUp.SetActive(false);
    }

    // �÷��̾ Ʈ���ſ� ������ �� ȣ��Ǵ� �Լ�
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")) // �±װ� 'Player'�� ��쿡�� ����
        {
            Time.timeScale = 0; // ���� �Ͻ�����
            Cursor.visible = true; // ���콺 Ŀ�� ǥ��
            Cursor.lockState = CursorLockMode.None; // ���콺 ��� ����
            _playerInside = true;
            _popUp.SetActive(true);
            _popUpText.text = "������ �����Ͻðڽ��ϱ�?";
            _popUp.GetComponent<PopUp>()._callFunc = OnClickNextStage;
        }
    }
    public void OnClickNextStage()
    {
        Time.timeScale = 1; // ���� �簳
        Cursor.visible = false; // ���콺 Ŀ�� ����
        Cursor.lockState = CursorLockMode.Locked; // ���콺 ���
        SceneManager.LoadScene(_sceneToLoad);
    }

    // Close��ư�� ��������
    public void OnClickCloseButton()
    {
        _playerInside = false;
        _popUp.SetActive(false);
        Time.timeScale = 1; // ���� �簳
        Cursor.visible = false; // ���콺 Ŀ�� ����
        Cursor.lockState = CursorLockMode.Locked; // ���콺 ���
    }
}
