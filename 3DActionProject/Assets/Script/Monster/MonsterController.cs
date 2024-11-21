using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public Transform _target; // ���� ������ ��� (�÷��̾�)
    private Animator _animator;
    public float _attackRange = 2.5f; // ���� ����
    public int _attackDamage = 10; // ���ݷ�
    public float _chaseRange = 10.0f; // ���� ����
    public int _health = 3; // ���� ü�� (3�� ������ ����)

    private bool _isAttacking = false; // ���� ����
    private NavMeshAgent _navAgent; // ���� ������ ���� NavMeshAgent

    // ��������Ʈ �� �̺�Ʈ ���� (���� ��� �� �˸�)
    public delegate void MonsterDeathHandler(GameObject monster);
    public event MonsterDeathHandler OnMonsterDeath; // ���� ��� �̺�Ʈ

    public bool _isSideScrolling = false;  // ī�޶� Ⱦ��ũ�� ������� ���θ� ��Ÿ���� ����


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _navAgent = GetComponent<NavMeshAgent>();

        // Ⱦ��ũ�� ����� �� y�� �� x�� ȸ���� ��ױ�
        if (_isSideScrolling)
        {
            _navAgent.updateRotation = false; // NavMeshAgent�� ȸ�� ��Ȱ��ȭ
        }
    }

    public void setTarget(Transform target)
    {
        _target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, _target.position);

            // ���� ���� ���� ���� �� ����
            if (distanceToTarget <= _attackRange && !_isAttacking)
            {
                StartCoroutine(Attack());
            }
            // ���� ���� �ȿ� ���� �� ����
            else if (distanceToTarget <= _chaseRange && distanceToTarget > _attackRange)
            {
                _navAgent.isStopped = false;
                Vector3 targetPosition = _target.position;

                // Ⱦ��ũ�� ��忡���� x�� �̵��� �����Ͽ� z�ุ �̵�
                if (_isSideScrolling)
                {
                    targetPosition.x = transform.position.x;
                }
                _navAgent.SetDestination(_target.position); // �÷��̾ ����
                _animator.SetBool("Move", true); // �̵� �ִϸ��̼� ����
            }
            else
            {
                _navAgent.isStopped = true;
                _animator.SetBool("Move", false); // �̵� �ִϸ��̼� ����
            }
        }
    }

    IEnumerator Attack()
    {
        _isAttacking = true;

        // �÷��̾ �ٶ󺸵��� ȸ��
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10.0f);

        // ���� �ִϸ��̼� ����
        if (_animator != null)
        {
            _animator.SetTrigger("Attack");
        }

        // �ִϸ��̼� �ð��� ���� ������ ��� (����: 1��)
        yield return new WaitForSeconds(1.0f);

        // �浹 ���� �� ������ ó��
        PlayerInteraction playerInteraction = _target.GetComponent<PlayerInteraction>();
        if (playerInteraction != null)
        {
            playerInteraction.TakeDamage(_attackDamage); // �������� ����
        }

        // ���� �� ���� �ð� ���
        yield return new WaitForSeconds(0.5f);

        _isAttacking = false;
    }

    // ���� �������� ���� �� ȣ��Ǵ� �Լ�
    public void TakeDamage(int damage)
    {
        _health -= damage;
        Debug.Log($"���� {damage} �������� �޾ҽ��ϴ�. ���� ü��: {_health}");

        if (_health <= 0)
        {
            Die();
        }
        else
        {
            // �ǰ� �ִϸ��̼� ����
            _animator.SetTrigger("Hit");
        }
    }

    // ���� �״� �޼���
    private void Die()
    {
        // ���� ��� �� OnMonsterDeath �̺�Ʈ ȣ��
        if (OnMonsterDeath != null)
        {
            OnMonsterDeath(gameObject); // �����ڵ鿡�� ������ ������ �˸�
        }

        // �� ���� ���� �ణ�� ��� �ð��� �߰��Ͽ� ��� �ִϸ��̼� ����
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        // ��� �ִϸ��̼� ����
        _animator.SetTrigger("Die");
        yield return new WaitForSeconds(1.2f); // ��� �ִϸ��̼� ��� �ð�
        Destroy(gameObject); // �� ������Ʈ ����
        GameManager._Instance.AddMonster();
        Debug.Log("���� ����߽��ϴ�.");
    }
}
