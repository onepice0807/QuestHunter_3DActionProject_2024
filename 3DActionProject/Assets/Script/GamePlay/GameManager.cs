using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _Instance; // �̱��� ���� ���

    public Text _coinText; // ���� ���� ǥ���� Text
    private int _coinCount = 0; // ���� ������ �����ϴ� ����
    public Text _monsterText; // ���� ���� ǥ���� Text
    private int _monsterCount = 0; // ���� ó������ �����ϴ� ����
    [SerializeField] private WarrningPopUp _WarrningPopUp; // �������Ḧ ���� �˾�
    [SerializeField] GameObject _OptionPopUp; // �ɼ�(BGM)�� ���� �˾�
    [SerializeField] private Sprite[] _BgmOnOffSprite; // Bgm �̹����� ���� Splite
    [SerializeField] private Sprite[] _SoundEffectOnOffSprite; // ȿ������ �̹����� ���� Splite

    [SerializeField] private GameObject _sceneAButtonCheck; // ������ ����â���� ���� ��ư Ŭ�� �� Ȱ��ȭ
    [SerializeField] private GameObject _sceneBButtonCheck; // ������ ����â���� ������ ��ư Ŭ�� �� ��ư �ؽ�Ʈ
    [SerializeField] private Text _sceneAButtonText; // ������ ����â���� ���� ��ư Ŭ�� �� ��ư �ؽ�Ʈ
    [SerializeField] private Text _sceneBButtonText; // ������ ����â���� ������ ��ư Ŭ�� �� ��ư �ؽ�Ʈ
    private string _selectedScene = "";  // ���� ���õ� �� �̸� (�ʱⰪ�� ������ �Ǳ� ���̱� ������ �� ���ڿ�)


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _OptionPopUp.gameObject.SetActive(false);
        SoundManager.Instance.Play_BackgroundMusic();
    }

    void Awake()
    {
        // �̱��� �������� ���� �Ŵ��� �ν��Ͻ� ����
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin()
    {
        _coinCount++; // ���μ� ����
        UpdateUI();

    }

    public void AddMonster()
    {
        _monsterCount++; // ���� ó���� ����
        UpdateUI();

    }

    public void UpdateUI()
    {
        if (_coinText != null)
        {
            _coinText.text = "���� �� : " + _coinCount.ToString();
        }

        if (_monsterText != null)
        {
            _monsterText.text = "���� ó���� : " + _monsterCount.ToString();
        }
    }

    // ���� ��ư�� Ŭ������ �� ȣ��Ǵ� �޼���
    public void SelectLeftBoss()
    {
        _selectedScene = "BossScene"; // ���� ��ư�� �Ҵ�� �� �̸� ����
        _sceneAButtonCheck.SetActive(true); // ���õǾ��ٴ� üũ�̹���â Ȱ��ȭ
        _sceneBButtonCheck.SetActive(false);
        _sceneAButtonText.text = "���õ�";
        _sceneBButtonText.text = "����"; 
        Debug.Log("���� �� ���õ�: " + _selectedScene); // ����� �α׷� ���� Ȯ��
    }

    // ������ ��ư�� Ŭ������ �� ȣ��Ǵ� �޼���
    public void SelectRightBoss()
    {
        _selectedScene = "Boss2Scene"; // ������ ��ư�� �Ҵ�� �� �̸� ����
        _sceneAButtonText.text = "����";
        _sceneBButtonText.text = "���õ�";
        _sceneBButtonCheck.SetActive(true); // ���õǾ��ٴ� üũ�̹���â Ȱ��ȭ
        _sceneAButtonCheck.SetActive(false);
        Debug.Log("������ �� ���õ�: " + _selectedScene); // ����� �α׷� ���� Ȯ��

    }

    public void BossRoomStartButtonClick()
    {
        if (!string.IsNullOrEmpty(_selectedScene))
        {
            ScenesManager._Instance.LoadScene(_selectedScene);
            Debug.Log("���� ��ư Ŭ��, " + _selectedScene + "�� �̵�");
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Debug.LogWarning("���� ���õ��� �ʾҽ��ϴ�!");
        }
    }

    public void BgmOnOff(GameObject btn)
    {
        if (SoundManager.Instance.MusicOnOff)
        {
            SoundManager.Instance.MusicOnOff = false;
            btn.GetComponent<Image>().sprite = _BgmOnOffSprite[0];
        }
        else
        {
            SoundManager.Instance.MusicOnOff = true;
            btn.GetComponent<Image>().sprite = _BgmOnOffSprite[1];
        }
    }

    public void SoundEffectOnOff(GameObject btn)
    {
        if (SoundManager.Instance.EffectOnOff)
        {
            SoundManager.Instance.EffectOnOff = false;
            btn.GetComponent<Image>().sprite = _SoundEffectOnOffSprite[0];
        }
        else
        {
            SoundManager.Instance.EffectOnOff = true;
            btn.GetComponent<Image>().sprite = _SoundEffectOnOffSprite[1];
        }
    }


    public void ShowOptionPopUp()
    {
        _OptionPopUp.gameObject.SetActive(true);
        Time.timeScale = 0; // ���� �Ͻ�����
        Cursor.visible = true; // ���콺 Ŀ�� ǥ��
        Cursor.lockState = CursorLockMode.None; // ���콺 ��� ����
    }

    public void CloseOptionPopUp()
    {
        _OptionPopUp.gameObject.SetActive(false);
        Time.timeScale = 1; // ���� �簳
        Cursor.visible = false; // ���콺 Ŀ�� ����
        Cursor.lockState = CursorLockMode.Locked; // ���콺 ���
    }

    public void CloseTitleOption()
    {
        _OptionPopUp.gameObject.SetActive(false);
    }

    public void GameExitPopUp()
    {
        if (_WarrningPopUp != null)
        {
            _WarrningPopUp.gameObject.SetActive(true);
            _WarrningPopUp.SetDescription("������ ������ �����ðڽ��ϱ�?");
            Time.timeScale = 0; // ���� �Ͻ�����
            Cursor.visible = true; // ���콺 Ŀ�� ǥ��
            Cursor.lockState = CursorLockMode.None; // ���콺 ��� ����
            _WarrningPopUp.SetOkButtonCallback(ExitGame); // OK ��ư�� ExitGame �޼��� ����
            _WarrningPopUp.SetCancelButtonCallback(CloseGameExitPopUp); // ��� ��ư�� �˾� �ݱ� ����
        }
    }

    public void CloseGameExitPopUp()
    {
        _WarrningPopUp.gameObject.SetActive(false);
        Time.timeScale = 1; // ���� �簳
        Cursor.visible = false; // ���콺 Ŀ�� ����
        Cursor.lockState = CursorLockMode.Locked; // ���콺 ���
    }

    public void ShowExitPopUp()
    {
        _WarrningPopUp.gameObject.SetActive(true);
        _WarrningPopUp.SetDescription("������ �����ðڽ��ϱ�?");
        _WarrningPopUp._oKCallFunc = ExitGame;
        Time.timeScale = 0; // ���� �Ͻ�����
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
