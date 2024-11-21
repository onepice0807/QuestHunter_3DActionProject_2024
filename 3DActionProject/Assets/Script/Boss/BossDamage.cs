using UnityEngine;

public class BossDamage : MonoBehaviour
{
    public Transform _target; // ���� ������ ��� (�÷��̾�)
    public int _attackDamage = 10; // ���ݷ�


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Hit"))
        {
            BossController boss = _target.GetComponent<BossController>();

            // �÷��̾ ���� ���� �� ���� ������ �浹�ϸ� �������� ����
            if (boss != null)
            {
                boss.TakeDamage(_attackDamage);
                Debug.Log("�÷��̾ ������ ���ݿ� �¾ҽ��ϴ�!");

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
