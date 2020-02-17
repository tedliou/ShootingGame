using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    [Header("Skin")]
    public Material[] materials;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Collider[] attackCollider;
    public GameObject bloodObject;

    [Header("Parameter")]
    public int hp;
    public float attackSpeed;
    public float moveSpeed;
    public Status status;
    public enum Status
    {
        Idle, Walk, Attack, Hit, Death
    }
    public GameObject hpBar;



    NavMeshAgent navMeshAgent;
    Collider _collider;
    Animator animator;
    GameObject playerObject;
    float cooldown = 0;
    float hitCooldown = 0;
    float widthRate;
    int fillHP;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        _collider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();

        // Random Skin
        int rndIndex = Random.Range(0, 4);
        skinnedMeshRenderer.material = materials[rndIndex];

        // Other
        navMeshAgent.speed = moveSpeed;
        fillHP = hp;
    }

    private void Update()
    {
        if (GameManager.Instance.gameOver || status == Status.Death || status == Status.Hit)
        {
            navMeshAgent.velocity = Vector3.zero;
            return;
        }

        animator.SetFloat(Parameter.Animator.Action, 1);
        switch (status)
        {
            case Status.Idle:
                //navMeshAgent.velocity = Vector3.zero;
                animator.SetFloat(Parameter.Animator.Action, 0);
                break;
            case Status.Attack:
                animator.SetFloat(Parameter.Animator.Action, 0);
                animator.Play("Attack");
                break;
        }

        if (Vector3.Distance(playerObject.transform.position, transform.position) <= navMeshAgent.stoppingDistance)
        {
            if (cooldown >= attackSpeed)
            {
                status = Status.Attack;
                cooldown = 0;
            }
            else
            {
                status = Status.Idle;
            }
            cooldown += Time.deltaTime;
        }
        else
        {
            status = Status.Walk;
        }
        navMeshAgent.SetDestination(playerObject.transform.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Bullet")
        {
            // Blood Effect
            GameObject newblood = Instantiate(bloodObject, transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0));
            float rndSize = Random.Range(0.1f, 1f);
            newblood.transform.localScale = new Vector3(rndSize, rndSize, rndSize);
            Destroy(newblood, 3);

            // Reduce HP
            hp -= 5;
            hpBar.GetComponent<Image>().fillAmount = ((float)hp / (float)fillHP);

            // Death Observer
            if (hp <= 0)
            {
                navMeshAgent.enabled = false;
                _collider.enabled = false;
                attackCollider[0].enabled = false;
                attackCollider[1].enabled = false;
                GameManager.Instance.ReduceMonsterCount(this.gameObject);
                SetDeath();
                return;
            }

            SetHit();
        }
    }

    private void SetDeath()
    {
        status = Status.Death;
        animator.SetTrigger(Parameter.Animator.Death);

        // Sound Effect
        GameManager.Instance.monsterDeath.Play();
    }

    private void SetHit()
    {
        status = Status.Hit;
        animator.Play("Get_Hit");
    }

    public void HitEnd()
    {
        status = Status.Idle;
    }
}
