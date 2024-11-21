using UnityEngine;
using UnityEngine.UI;

public class HealthBarBossRoom : MonoBehaviour
{
    public Slider _healthSlider; // �÷��̾� ü�¹� �����̴�
    public float _maxHealth = 100f; // �÷��̾� �ִ� ü��
    private float _currentHealth; // �÷��̾� ���� ü��
    public Text _healthText; // �÷��̾� ü�°����� �ؽ�Ʈ

    public Slider _bossHealthSlider; // ���� ü�¹� �����̴�
    public float _bossMaxHealth = 100f; // ���� �ִ� ü��
    private float _bossCurrentHealth; // ���� ���� ü��
    public Text _bossHealthText; // ���� ü�°����� �ؽ�Ʈ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // �÷��̾� ü�»���
        _currentHealth = _maxHealth; // �ʱ� ü���� �ִ� ü������ ����
        _healthSlider.value = _currentHealth;
        _healthText.text = _currentHealth.ToString();
        UpdateHealthBar();

        // ���� ü�»���
        _bossCurrentHealth = _bossMaxHealth; // �ʱ� ü���� �ִ� ü������ ����
        _bossHealthSlider.value = _bossCurrentHealth;
        _bossHealthText.text = _bossCurrentHealth.ToString();
        UpdateBossHealthBar();
    }

    // �������� �Ծ��� �� ü�� ����
    public void TakeDamage(float amount)
    {
        _currentHealth = amount;
        if (_currentHealth < 0)
        {
            _currentHealth = 0; // ü���� 0 ���Ϸ� �������� �ʵ���
        }

        UpdateHealthBar();
    }

    public void BossTakeDamage(float amount)
    {
        _bossCurrentHealth = amount;
        if (_bossCurrentHealth < 0)
        {
            _bossCurrentHealth = 0; // ü���� 0 ���Ϸ� �������� �ʵ���
        }

        UpdateBossHealthBar();
    }

    // ü���� ȸ���� �� ���
    public void Heal(float amount)
    {
        _currentHealth += amount;
        if (_currentHealth > _maxHealth) _currentHealth = _maxHealth; // ü���� �ִ� ü�� �̻����� �ö��� �ʵ���
        UpdateHealthBar();
    }

    // ü�¹� ������Ʈ
    private void UpdateHealthBar()
    {
        _healthSlider.value = _currentHealth; // �����̴� ���� ���� ü�� ������ �°� ����
        _healthText.text = _currentHealth.ToString();
    }

    private void UpdateBossHealthBar()
    {
        _bossHealthSlider.value = _bossCurrentHealth; // �����̴� ���� ���� ü�� ������ �°� ����
        _bossHealthText.text = _bossCurrentHealth.ToString();
    }
}
