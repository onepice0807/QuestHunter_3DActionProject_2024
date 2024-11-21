using UnityEngine;

public class MoveTrigger : MonoBehaviour
{
    public Transform _regenPoint; // Regen1 ��ġ�� ������ ����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        // Player�� Ʈ���Ÿ� ������ _regenPoint ��ġ�� �̵�
        if (collider.CompareTag("Player"))
        {
            collider.transform.position = _regenPoint.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
