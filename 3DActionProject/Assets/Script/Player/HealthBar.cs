using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider _healthSlider; // ü�¹� �����̴�
    public float _maxHealth = 100f; // �ִ� ü��
    private float _currentHealth; // ���� ü��
    public Text _healthText; // ü�°����� �ؽ�Ʈ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = _maxHealth; // �ʱ� ü���� �ִ� ü������ ����
        _healthSlider.value = _currentHealth;
        _healthText.text = _currentHealth.ToString();
        UpdateHealthBar();
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

}