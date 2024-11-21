using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    private Animator _animator;
    private bool _isDamaged = false; // �÷��̾ �������� �޾Ҵ��� Ȯ���ϴ� ����
    private int _damageCount = 0; // ������ ī��Ʈ�� ���� ����
    public int _health = 100; // �÷��̾� ü��
    [SerializeField] public HealthBar _healthBar; // HealthBar ���� �߰�
    [SerializeField] private GameObject _gameOverUI; // �÷��̾ �׾����� ���ӿ��� ǥ���ϱ� ���� ����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // �������� �޴� �޼���
    public void TakeDamage(int damage)
    {
        if (!_isDamaged) // �̹� �������� ���� ���°� �ƴϸ�
        {
            _isDamaged = true; // �÷��̾ �������� ����
            _health -= damage; // �������� ü�¿��� ����

            Debug.Log($"�÷��̾ {damage} �������� �޾ҽ��ϴ�. ���� ü��: {_health}");

            // ü�¹� ������Ʈ
            if (_healthBar != null)
            {
                _healthBar.TakeDamage(_health); // ü�¹��� TakeDamage�� ������ ������ ����
            }

            if (_health <= 0)
            {
                Die();
            }

            // ���� �ð� �� ������ ���� ����
            Invoke("ResetDamage", 3.0f); // 3�� �� ������ ���¸� ����
        }
    }

    // ������ ���¸� �����ϴ� �޼���
    private void ResetDamage()
    {
        _isDamaged = false;
    }

    // �÷��̾� ��� ó�� �޼���
    // �÷��̾� ��� ó�� �޼���
    private void Die()
    {
        Debug.Log("�÷��̾ ����߽��ϴ�.");

        // ��� �ִϸ��̼� ����
        if (_animator != null)
        {
            _animator.SetTrigger("Die");
        }

        // �ִϸ��̼��� ���� �� ���� ���� ȣ��
        StartCoroutine(WaitForDeathAnimation());
    }

    // ��� �ִϸ��̼� �Ϸ� �� ���� ���� ȣ��
    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(2.0f); // �ִϸ��̼� ���̿� �°� ����

        // �̱����� ���� ScenesManager�� OnClickGameOver ȣ��
        if (ScenesManager._Instance != null)
        {
            ScenesManager._Instance.OnClickGameOver();
        }
        else
        {
            Debug.LogError("ScenesManager �ν��Ͻ��� ã�� �� �����ϴ�.");
        }
    }

    // ���Ϳ��� �浹�� �����ϴ� �޼���
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("EnemySword"))
        {
            Debug.Log("���Ϳ��� ���ݴ��߽��ϴ�.");
            _damageCount++;
            // ������ �ִϸ��̼��� �ִٸ� ����
            if (_animator != null && _damageCount == 3) // 3���¾����� 1�� ��� ���
            {
                _animator.SetTrigger("Damaged");
                _damageCount = 0; // ������ ī��Ʈ �ʱ�ȭ
            }
            TakeDamage(10); // �浹 �� �⺻������ 5�� ������ ó��
        }

        

    }

}
