using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _vertical = 0.0f;
    private float _horizon = 0.0f;
    private float _moveSpeed = 7.0f;
    private Vector3 _direct;
    private CharacterController _playerController;
    private Animator _animator;
    private Rigidbody _rigidbody; // �÷��̾� ������ ���� Rigidbody

    public int playerDamage = 1; // �÷��̾ ������ ������ �ο�

    public GameObject _inventoryUI; // �κ��丮 UI â (Ȱ��/��Ȱ��)
    public GameObject _AttackEffectPrefab; // �÷��̾��� ����ȿ���� ���� ������
    private bool _isInventoryOpen = false; // �κ��丮 â�� ���ȴ��� ����

    public bool _isSideScrolling = false;  // ī�޶� Ⱦ��ũ�� ������� ���θ� ��Ÿ���� ����

    private bool _isOnLadder = false;  // �÷��̾ ��ٸ��� �ִ��� ��Ÿ���� ����

    private int _leftClickCount = 0; // ���� ���콺 ��ư Ŭ�� Ƚ��
    public bool _isAttacking = false; // ���� Ȱ��ȭ ����
    public bool _isSkill = false; // ��ų���� Ȱ��ȭ ����


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    void Move()
    {
        if (_isSideScrolling)
        {
            // Ⱦ��ũ�� ��忡���� �̵�
            float moveX = Input.GetAxisRaw("Horizontal"); // A/D Ű�� �¿� �̵�
            float moveY = 0.0f;

            if (_isOnLadder)
            {
                moveY = Input.GetAxisRaw("Vertical"); // W/S Ű�� ���� ���Ʒ� �̵� Ȱ��ȭ (��ٸ�������)
                _direct = new Vector3(0, moveY, moveX).normalized; // Y�� �̵��� ���
                this.transform.rotation = Quaternion.Euler(0, -90, 0); // ĳ���Ͱ� ��ٸ��� �ٶ󺸵��� ȸ�� ���� (��ٸ� ���⿡ �°� ���� �ʿ�)
                this.transform.position += _direct * _moveSpeed * Time.deltaTime;
                _animator.SetBool("Move", true);
            }
            else
            {
                _direct = new Vector3(0, moveY, moveX).normalized;
                this.transform.position += _direct * _moveSpeed * Time.deltaTime;

                if (_direct != Vector3.zero)
                {
                    this.transform.rotation = Quaternion.LookRotation(_direct);
                    _animator.SetBool("Move", true);
                }
            }

        }
        else
        {
            _vertical = Input.GetAxis("Vertical");
            _horizon = Input.GetAxis("Horizontal");

            _direct = new Vector3(_horizon, 0.0f, _vertical).normalized;
            this.transform.position += _direct * _moveSpeed * Time.deltaTime;

            if (_direct != Vector3.zero)
            {
                this.transform.rotation = Quaternion.LookRotation(_direct);
                _animator.SetBool("Move", true);
            }
        }

            
    }

    void Stop()
    {
        if (_direct != Vector3.zero)
        {
            this.transform.rotation = Quaternion.LookRotation(_direct);
        }
        _animator.SetBool("Move", false);
    }

    void Clash()
    {
        _animator.SetBool("Clash", true);
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag.Contains("Coin")) // �����̶� �浹 �� ������ ������� �ϰ� ui ���� �� ����
        {
            // ����ȹ��� ���� �߰�
            SoundManager.Instance.Play_CoinSound();
            Destroy(collision.gameObject);
            GameManager._Instance.AddCoin(); // ���� �� ����
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            MonsterController enemy = collider.GetComponent<MonsterController>();
            if (enemy != null && _animator.GetBool("Attack")) // �÷��̾ ���� ������ ����
            {

                enemy.TakeDamage(playerDamage); // ������ ������ ����

                // �浹 ������ ������ ȿ�� ����
                if (_AttackEffectPrefab != null)
                {
                    // �浹 ���� ���
                    Vector3 collisionPoint = collider.ClosestPoint(transform.position);
                    // ���ݽ� ���� �߰�
                    SoundManager.Instance.Play_PlayerDoungonAttackSound();
                    // ������ ȿ�� �������� �浹 ������ ����
                    Instantiate(_AttackEffectPrefab, collisionPoint, Quaternion.identity);
                }
                
            }
        }

    }

    void Attack()
    {
        _animator.SetTrigger("Attack");
        // ���� �ִϸ��̼� ����� ���ÿ� ���� Ÿ���ϴ� ����
    }

    public void SkillOn()
    {
        _isSkill = true;
        _animator.SetBool("Attack2", true);
    }
    
    // ��ų�ִϸ��̼��� ������ �̺�Ʈ�� ȣ��
    public void SkillOff()
    {
        _isSkill = false;
        _animator.SetBool("Attack2", false);
    }

    void Jump()
    {
        _animator.SetBool("Jump", true);

    }

    void Defend()
    {
        _animator.SetBool("Defend", true);
    }

    private void ToggleInventory()
    {
        _isInventoryOpen = !_isInventoryOpen;

        if (_isInventoryOpen)
        {
            _inventoryUI.SetActive(true);
            Time.timeScale = 0; // ���� �Ͻ�����
            Cursor.visible = true; // ���콺 Ŀ�� ǥ��
            Cursor.lockState = CursorLockMode.None; // ���콺 ��� ����
        }
        else
        {
            _inventoryUI.SetActive(false);
            Time.timeScale = 1; // ���� �簳
            Cursor.visible = false; // ���콺 Ŀ�� ����
            Cursor.lockState = CursorLockMode.Locked; // ���콺 ���
        }
    }

    private void KeyEventProcess()
    {
        // Ű���� �Է� ó�� (�׻� ����)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            _animator.SetBool("Jump", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        // OŰ�� �������� �ɼ�PopUp ����
        if (Input.GetKeyDown(KeyCode.O))
        {
            if(GameManager._Instance != null)
            {
                GameManager._Instance.ShowOptionPopUp(); // GameManager���� ShowOptionPopUpȣ��
            }
        }

        // ���� �������� ��������  
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager._Instance != null)
            {
                GameManager._Instance.GameExitPopUp();
            }
            else
            {
                Debug.LogError("�ν��Ͻ��� ã�� �� �����ϴ�.");
            }
        }

        // �κ��丮 â ���� �ݱ�
        if (Input.GetKeyDown(KeyCode.I))
        {
            _isInventoryOpen = !_isInventoryOpen;  // �κ��丮 â ���
            _inventoryUI.SetActive(_isInventoryOpen);
        }


        if (Input.GetKeyDown(KeyCode.F1))
        {
            SkillOn();
        }

    }

    void Update()
    {
        // �κ��丮 UI ��� ó��
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
            return; // �κ��丮 ��� �� �ٸ� �Է��� ó������ ����
        }

        KeyEventProcess();

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
        {
            Move();
        }
        else
        {
            Stop();
        }

    }
}
