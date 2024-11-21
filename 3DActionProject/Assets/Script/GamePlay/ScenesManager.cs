using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� ���ӽ����̽� �߰�
using UnityEngine.UI; // UI Text�� ����ϱ� ���� ���ӽ����̽� �߰�

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager _Instance; // �̱��� ���� ���

    // ���� ��ư Ŭ�� �� �̵��� �� �̸�
    [SerializeField] private string _sceneToLoad = "MainScene"; // �ε��� ���� �̸�
    [SerializeField] private string _titleSceneToLoad = "TitleScene"; // Ÿ��Ʋ ȭ������ �̵��� ����
    private string _selectSceneToLoad = "SelectionWindowScene"; // ���� ����â���� �̵��� ����
    [SerializeField] private float _delayBeforeLoad = 5.0f; // �� ��ȯ �� ��� �ð�
    [SerializeField] private GameObject _dungeonOut; // ��� �ð��� ������ ���� ������ UI ǥ��
    [SerializeField] private Text _countdownText; // ��� �ð� ī��Ʈ�ٿ� UI �ؽ�Ʈ
    [SerializeField] public GameObject _gameOver; // �÷��̾ �׾����� ���� ������ UI ǥ��
    [SerializeField] private Text _gameOverCountdownText; // ��� �ð� ī��Ʈ�ٿ� UI �ؽ�Ʈ
    [SerializeField] public GameObject _gameClear; // ���Ͱ� �׾����� Ŭ���� UI ǥ��
    [SerializeField] private Text _gameClearCountdownText; // ��� �ð� ī��Ʈ�ٿ� UI �ؽ�Ʈ

    private bool _playerInside = false; // �÷��̾ Ʈ���� �ȿ� �ִ��� ���� Ȯ��

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _dungeonOut.SetActive(false);
        _gameOver.SetActive(false);
        _gameClear.SetActive(false);
    }

    // �̱��� ������ �����ϱ� ���� Awake �޼��忡�� �ν��Ͻ��� ����
    void Awake()
    {
        // _Instance�� null�� ��쿡�� ���� �ν��Ͻ��� �Ҵ�
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(gameObject); // �ߺ��� ScenesManager�� ������ ���� �ν��Ͻ� �ı�
        }
    }

    // �÷��̾ Ʈ���ſ� ������ �� ȣ��Ǵ� �Լ�
    private void OnTriggerEnter(Collider collider)
    {
        // �浹�� ��ü�� �±װ� 'Player'�� ��쿡�� ����
        if (collider.CompareTag("Player"))
        {
            _playerInside = true; // �÷��̾ Ʈ���� �ȿ� �ִ� ������ ǥ��
            StartCoroutine(LoadSceneAfterDelay()); // �� ��ȯ ��� �ڷ�ƾ ����
            _dungeonOut.SetActive(true); // ���� ������ UI Ȱ��ȭ
        }
    }

    // ���� �ð� ��� �� ���� ��ȯ�ϴ� �ڷ�ƾ
    IEnumerator LoadSceneAfterDelay()
    {
        float delay = _delayBeforeLoad; // ��� �ð��� ���� ������ ����
        while (delay > 0) // ��� �ð��� 0���� ū ���� �ݺ�
        {
            _countdownText.text = "���� �ð�: " + delay.ToString("F1") + " ��"; // UI �ؽ�Ʈ�� ���� �ð� ǥ��
            delay -= Time.deltaTime; // �� �����Ӹ��� ���� �ð��� ����
            yield return null; // ���� �����ӱ��� ���
        }

        // ��� �ð��� ���� ��, �÷��̾ Ʈ���� �ȿ� ���� ��� �� ��ȯ
        if (_playerInside)
        {
            SceneManager.LoadScene(_sceneToLoad); // ������ ������ ��ȯ
        }
    }

    // Ÿ��Ʋ ���� �ε��ϴ� �޼���
    public void LoadTitleScene()
    {
        SceneManager.LoadScene(_titleSceneToLoad);
        Cursor.visible = true; // ���콺 Ŀ�� ǥ��
        Cursor.lockState = CursorLockMode.None; // ���콺 ��� ����
    }

    // ���� ���þ��� �ε��ϴ� �޼���
    public void LoadSelectScene()
    {
        SceneManager.LoadScene(_selectSceneToLoad);
        Cursor.visible = true; // ���콺 Ŀ�� ǥ��
        Cursor.lockState = CursorLockMode.None; // ���콺 ��� ����
    }

    // ���� ���������� �̵��� �� ȣ��Ǵ� �Լ�
    public void OnClickNextStage()
    {
        SceneManager.LoadScene(_sceneToLoad); // ������ ������ ��ȯ
        Time.timeScale = 1; // ���� �簳
        Cursor.visible = false; // ���콺 Ŀ�� ����
        Cursor.lockState = CursorLockMode.Locked; // ���콺 ���
    }

    public void OnStartButtionClick()
    {
        SceneManager.LoadScene(_sceneToLoad); // ���۹�ư�� �������� ������ ������ ��ȯ
        Time.timeScale = 1; // ���� �簳
        Cursor.visible = false; // ���콺 Ŀ�� ����
        Cursor.lockState = CursorLockMode.Locked; // ���콺 ���
    }

    // �÷��̾ ��� �� ȣ��Ǵ� �Լ�
    public void OnClickGameOver()
    {
        StartCoroutine(GameOverSequence()); // ���� ���� UI�� ǥ���� �� ���� ��ȯ�ϴ� �ڷ�ƾ ����
    }

    // ���� ���� UI ǥ�� �� ���� ��ȯ�ϴ� �ڷ�ƾ
    private IEnumerator GameOverSequence()
    {
        _gameOver.SetActive(true); // ���� ���� UI Ȱ��ȭ
        Debug.Log("���ӿ���"); // ���� ���� �α� ���

        float delay = 3.0f; // ��� �ð� ����
        while (delay > 0)
        {
            _gameOverCountdownText.text = delay.ToString("F1") + " �� �ڿ� �̵��մϴ�"; // ���� �ð� ǥ��
            delay -= Time.deltaTime; // ��� �ð� ����
            yield return null; // ���� �����ӱ��� ���
        }

        SceneManager.LoadScene(_sceneToLoad); // ������ ������ ��ȯ
    }

    public void OnClickGameClear()
    {
        StartCoroutine(GameClearSequence()); // ���� ���� UI�� ǥ���� �� ���� ��ȯ�ϴ� �ڷ�ƾ ����
    }

    // ���� Ŭ���� UI ǥ�� �� ���� ��ȯ�ϴ� �ڷ�ƾ
    private IEnumerator GameClearSequence()
    {
        _gameClear.SetActive(true); // ���� ���� UI Ȱ��ȭ
        Debug.Log("���� Ŭ����"); // ���� Ŭ���� �α� ���

        float delay = 5.0f; // ��� �ð� ����
        while (delay > 0)
        {
            _gameClearCountdownText.text = delay.ToString("F1") + " �� �ڿ� Ȩ���� �̵��մϴ�"; // ���� �ð� ǥ��
            delay -= Time.deltaTime; // ��� �ð� ����
            yield return null; // ���� �����ӱ��� ���
        }

        SceneManager.LoadScene(_sceneToLoad); // ������ ������ ��ȯ
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
