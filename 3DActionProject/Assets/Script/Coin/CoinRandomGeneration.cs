using UnityEngine;

public class CoinRandomGeneration : MonoBehaviour
{
    public GameObject coinPrefab; // ���� ������
    public int coinCount = 30; // ������ ������ ��
    public float spawnRadius = 50f; // ������ ������ �ݰ�
    public float spawnInterval = 2f; // ���� ���� �ּ� �Ÿ�
    public LayerMask navMeshLayer; // NavMesh�� �ִ� ���̾�

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CoinGeneration();
    }

    public void CoinGeneration()
    {

    }


    Vector3 GetRandomPosition()  // ���ε��� ������ ��ġ�� �ֵ��� �ϴ� method
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
        randomDirection += transform.position; // �� ������Ʈ�� �߽����� ���� ��ġ ���
        randomDirection.y = 0; // y���� 0���� �����Ͽ� NavMesh ���� ��ġ�� ����
        return randomDirection;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
