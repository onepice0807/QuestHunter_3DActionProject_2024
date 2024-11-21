using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public GameObject sword; // �� ������Ʈ�� ���� �����ϴ� ��
    public float moveSpeed = 0.0f; // �̵��ϴ� �ӵ�
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == sword)
        {
            Debug.Log("Hit");

            // Vector3 monsterPosition = transform.position + Vector3.up * 3.0f + Vector3.left * 3.0f;

            Vector3 monsterPosition = (transform.position + Vector3.up * 3.0f + Vector3.left * 3.0f - transform.position).normalized;
            transform.position += monsterPosition * moveSpeed * Time.deltaTime;

            gameObject.transform.position = monsterPosition;

            // ���͸� ���� �ð� �Ŀ� ����
            Destroy(gameObject, 2f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
