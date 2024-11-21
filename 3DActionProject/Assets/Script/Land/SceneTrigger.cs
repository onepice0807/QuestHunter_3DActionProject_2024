using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        // Player�� Ʈ���Ÿ� ������ _regenPoint ��ġ�� �̵�
        if (collider.CompareTag("Player"))
        {
            // �̱����� ���� ScenesManager�� OnClickGameClear ȣ��
            if (ScenesManager._Instance != null)
            {
                ScenesManager._Instance.OnClickGameClear();
            }
            else
            {
                Debug.LogError("ScenesManager �ν��Ͻ��� ã�� �� �����ϴ�.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
