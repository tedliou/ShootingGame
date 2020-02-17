using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [Header("Player Settings")]
    public float gravity;
    public float moveSpeed;
    public float rotateSpeed;

    [Header("Weapon Settings")]
    public float rof;
    public Transform aim;
    public Transform muzzle;
    public GameObject bulletPrefab;
    public ParticleSystem muzzleEffect;
    float rps;
    float fireCooldown = 0;

    InputManager inputManager = new InputManager();
    CharacterController controller;
    Animator animator;
    Vector3 globalVelocity;
    [HideInInspector] public int hp = 200;
    AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        globalVelocity = transform.TransformVector(Vector3.zero);
        inputManager.HideCursor();

        rps = 1 / rof;
    }

    private void Update()
    {
        if (GameManager.Instance.gameOver) { return; }
        Fire();
        Move();
        Rotate();
        GetGravity();
        fireCooldown += Time.deltaTime;
    }

    private void GetGravity()
    {
        globalVelocity.y -= gravity * Time.deltaTime;
    }

    private void Move()
    {
        // Position
        Vector3 wv = transform.TransformVector(inputManager.GetMoveVector());
        globalVelocity = Vector3.Lerp(globalVelocity, wv, 0.5f);
        controller.Move(globalVelocity * Time.deltaTime * moveSpeed);

        animator.SetFloat(Parameter.Animator.Horizontal, Input.GetAxis(Parameter.Input.Horizontal));
        animator.SetFloat(Parameter.Animator.Vertical, Input.GetAxis(Parameter.Input.Vertical));
    }

    private void Rotate()
    {
        Vector2 angle = inputManager.GetRotateVecter();
        Vector3 hr = new Vector3(0, angle.x * rotateSpeed, 0);
        transform.Rotate(hr);
    }

    private void Fire()
    {
        if (inputManager.GetFireInput())
        {
            if (fireCooldown >= rps)
            {

                fireCooldown = 0;

                // Aim
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(aim.position);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~2))
                {
                    GameObject newBullet = Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.identity);
                    newBullet.transform.LookAt(hit.point);
                    Destroy(newBullet, 3);
                }

                // Muzzle Effect
                muzzleEffect.Play();

                // Sound Effect
                audioSource.Play();
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.gameOver) { return; }
        if (other.transform.tag == "Monster")
        {
            hp -= 10;
            UI.Instance.GetHurt(10);
            GameManager.Instance.playerHit.Play();
            if (hp <= 0)
            {
                animator.SetTrigger("Death");
                GameManager.Instance.SetGameOver();
            }
        }
    }
}
