using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Monster Spawn Parameter")]
    public GameObject[] monsterPrefabs;

    public float spawnRate;
    [HideInInspector] public List<GameObject> monsterList = new List<GameObject>();
    public GameObject monsterSpawnParent;
    public TextMeshProUGUI highestScoreText;
    public TextMeshProUGUI scoreText;
    public GameObject death;

    [Header("Sound Effect")]
    public AudioSource playerFire;
    public AudioSource playerHit;
    public AudioSource playerDeath;
    public AudioSource monsterDeath;
    /*
        [Header("Player Parameter")]
        public GameObject playerObject;
        public float playerMoveSpeed;
        public float playerRotateSpeed;
        public float playerGravity;
        public GameObject playerWeapon;
    */

    // Monter spawn control
    GameObject[] monsterSpawnPos;
    float monsterSpawnCooldown = 0;
    int highestScore;
    int score = 0;
    public bool gameOver = false;

    // GameManager
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        monsterSpawnPos = GameObject.FindGameObjectsWithTag(Parameter.Tag.MonsterSpawn);
        highestScore = PlayerPrefs.GetInt("HighestScore");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }

        GenerateMonster();
        scoreText.text = "score: " + score.ToString();
        highestScoreText.text = "Highest Score: " + highestScore.ToString();

        if (score > highestScore)
        {
            highestScore = score;
            PlayerPrefs.SetInt("HighestScore", score);
        }


    }

    private void GenerateMonster()
    {
        if (monsterSpawnCooldown < spawnRate) { monsterSpawnCooldown += Time.deltaTime; return; }
        if (monsterList.Count >= 10) { return; }

        monsterSpawnCooldown = 0;
        GameObject randomMonster = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
        GameObject randomSpawnPosition = monsterSpawnPos[Random.Range(0, monsterSpawnPos.Length)];
        GameObject newMonster = Instantiate(randomMonster);
        newMonster.transform.parent = monsterSpawnParent.transform;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomSpawnPosition.transform.position, out hit, 10f, NavMesh.AllAreas))
        {
            newMonster.transform.position = hit.position;
        }

        monsterList.Add(newMonster);
    }

    public void ReduceMonsterCount(GameObject obj)
    {
        monsterList.Remove(obj);
        score++;
        Destroy(obj, 3);
    }
    public Text deathMsgText;
    public void SetGameOver()
    {
        // TODO
        gameOver = true;
        string[] deathMessage = { "死", "哈", "蠢", "呵" };
        death.GetComponent<Animation>().Play();
        deathMsgText.text = deathMessage[Random.Range(0, deathMessage.Length)];
        GetComponent<AudioSource>().Play();
    }
}
