using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterTrigger : MonoBehaviour
{
    [SerializeField] private Transform[] _RegenTrigger;
    [SerializeField] private MonsterController _TriggerMonster;
    [SerializeField] private GameObject _MoveTrigger; // MoveTrigger�� Ȱ��ȭ�ϱ� ���� ����
    private int _maxMonsterCount = 5; // �ִ� ��ȯ�� ���� ��
    private int _currentMonsterCount = 0; // ���� ��ȯ�� ���� ��
    private List<GameObject> _triggerMonstersList = new List<GameObject>(); // ��ȯ�� ���͸� �����ϴ� ����Ʈ
    [SerializeField] private List<Transform> _defaultMonstersList = new List<Transform>(); // ������ DefaltMonster ���� ����Ʈ
    [SerializeField] private Text _clearDungeonText; // �� ������ Ŭ���� �Ҷ��ٸ� ǥ�����ִ� Text
    [SerializeField] private GameObject _clearDungeonUI; // ������ Ŭ����Text�� Ȱ��ȭ�ϱ� ���� ����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // MoveTrigger�� ó���� ��Ȱ��ȭ
        if (_MoveTrigger != null)
        {
            _MoveTrigger.SetActive(false);
            _clearDungeonUI.SetActive(false);
        }

        // �⺻ ���͵��� �����ϸ�, �� ���Ͱ� �׾��� �� ����Ʈ���� �����ϴ� ���� ����
        foreach (var monster in _defaultMonstersList)
        {
            var monsterGameObject = monster.gameObject;
            var monsterController = monsterGameObject.GetComponent<MonsterController>();

            if (monsterController != null)
            {
                monsterController.OnMonsterDeath += DefaultMonsterDied;
            }
        }
    }
    private void OnTriggerEnter(Collider collider)
    { 
        // �±װ� 'Player' ���¿��� ��ȯ�� ���Ͱ� 6���� �̸��� ���� ����
        if (collider.CompareTag("Player") && _currentMonsterCount < _maxMonsterCount)
        {
            StartCoroutine(CreateMonster(collider));
        }
    }

    IEnumerator CreateMonster(Collider collider)
    {

        foreach (var trigger in _RegenTrigger)
        {
            if (_currentMonsterCount >= _maxMonsterCount)
                yield break; // �ִ� ��ȯ ���� �����ϸ� �ߴ�

            var enemy = Instantiate(_TriggerMonster.gameObject, trigger.position, trigger.rotation);
            enemy.GetComponent<MonsterController>().setTarget(collider.gameObject.transform);
            yield return new WaitForSecondsRealtime(0.5f);

            // ��ȯ�� ���͸� ����Ʈ�� �߰��ϰ� ī���� ����
            _triggerMonstersList.Add(enemy);
            _currentMonsterCount++;

            // ���Ͱ� �׾��� �� ����Ʈ���� �����ϴ� ���� ����
            enemy.GetComponent<MonsterController>().OnMonsterDeath += MonsterDied;

            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    // ��ȯ�� ���Ͱ� �׾��� �� ȣ��Ǵ� �Լ�
    private void MonsterDied(GameObject monster)
    {
        _triggerMonstersList.Remove(monster); // ��ȯ�� ���� ����Ʈ���� ����
        CheckIfAllMonstersAreDead(); // ��� ���Ͱ� ����ߴ��� Ȯ��
    }

    // �⺻ ���Ͱ� �׾��� �� ȣ��Ǵ� �Լ�
    private void DefaultMonsterDied(GameObject monster)
    {
        _defaultMonstersList.Remove(monster.transform); // �⺻ ���� ����Ʈ���� ����
        CheckIfAllMonstersAreDead(); // ��� ���Ͱ� ����ߴ��� Ȯ��
    }

    // ��� ����(��ȯ�� ���Ϳ� �⺻ ����)�� ����ߴ��� Ȯ��
    private void CheckIfAllMonstersAreDead()
    {
        if (_triggerMonstersList.Count == 0 && _defaultMonstersList.Count == 0)
        {
            // ��� ���Ͱ� ������� 3�� �� MoveTrigger Ȱ��ȭ
            if (_MoveTrigger != null)
            {
                _clearDungeonUI.SetActive(true);
                Invoke("ActivateMoveTrigger", 3f); // 3�� �Ŀ� ActivateMoveTrigger ȣ��
            }
        }
    }

    // MoveTrigger Ȱ��ȭ�Ҷ� Ŭ���� Text�� ��Ȱ��ȭ
    private void ActivateMoveTrigger()
    {
        _clearDungeonUI.SetActive(false);
        _MoveTrigger.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {

    }
}


