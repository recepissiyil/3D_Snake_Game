using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is attached Snake prefab
public class PlayerController : MonoBehaviour
{
    #region Variables
    public static PlayerController instance;

    [HideInInspector]
    public PlayerDirection direction;

    public float stepLength = .2f;

    public float movementFrequency;

    private float counter;
    private bool move;

    [SerializeField]
    private GameObject tailPrefab;

    private List<Vector3> deltaPosition;

    private List<Rigidbody> nodes;

    private Rigidbody mainBody;
    private Rigidbody headBody;
    private Transform playerTransform;

    private bool createNodeAtTail;
    #endregion

    #region Definitions before the scene opens
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        playerTransform = transform;
        mainBody = GetComponent<Rigidbody>();

        InitSnakeNodes();
        InitPlayer();

        deltaPosition = new List<Vector3>()
        {
            new Vector3(-stepLength,0f),//left
            new Vector3(0f,stepLength),//up
            new Vector3(stepLength,0f),//right
            new Vector3(0f,-stepLength),//down
        };
    } 
    #endregion
    private void Update()
    {
        CheckMovementFrequency();
    }
    private void FixedUpdate()
    {
        if (move)
        {
            move = false;
            Move();
        }
    }
    #region Movement and Direction
    private void InitSnakeNodes()
    {
        nodes = new List<Rigidbody>();
        nodes.Add(playerTransform.GetChild(0).GetComponent<Rigidbody>());
        nodes.Add(playerTransform.GetChild(1).GetComponent<Rigidbody>());
        nodes.Add(playerTransform.GetChild(2).GetComponent<Rigidbody>());
        headBody = nodes[0];
    }

    private void SetDirectionRandom()
    {
        int dirRandom = Random.Range(0, (int)PlayerDirection.COUNT);
        direction = (PlayerDirection)dirRandom;
    }
    private void InitPlayer()
    {
        SetDirectionRandom();
        switch (direction)
        {
            case PlayerDirection.LEFT:
                nodes[1].position = nodes[0].position + new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position + new Vector3(Metrics.NODE * 2f, 0f, 0f);
                break;
            case PlayerDirection.UP:
                nodes[1].position = nodes[0].position - new Vector3(0f, Metrics.NODE, 0f);
                nodes[2].position = nodes[0].position - new Vector3(0f, Metrics.NODE * 2f, 0f);
                break;
            case PlayerDirection.RIGHT:
                nodes[1].position = nodes[0].position - new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position - new Vector3(Metrics.NODE * 2f, 0f, 0f);
                break;
            case PlayerDirection.DOWN:
                nodes[1].position = nodes[0].position + new Vector3(0f, Metrics.NODE, 0f);
                nodes[2].position = nodes[0].position + new Vector3(0f, Metrics.NODE * 2f, 0f);
                break;
            default:
                break;
        }
    }
    private void Move()
    {
        Vector3 dPosition = deltaPosition[(int)direction];

        Vector3 parentPos = headBody.position;
        Vector3 prevPosition;

        mainBody.position = mainBody.position + dPosition;
        headBody.position = headBody.position + dPosition;
        for (int i = 1; i < nodes.Count; i++)
        {
            prevPosition = nodes[i].position;

            nodes[i].position = parentPos;
            parentPos = prevPosition;
        }
        if (createNodeAtTail)
        {
            createNodeAtTail = false;
            GameObject newNode = Instantiate(tailPrefab, nodes[nodes.Count - 1].position, Quaternion.identity);
            newNode.transform.SetParent(transform, true);
            nodes.Add(newNode.GetComponent<Rigidbody>());
        }
    }
    private void CheckMovementFrequency()
    {
        counter += Time.deltaTime;
        if (counter >= movementFrequency)
        {
            counter = 0f;
            move = true;
        }
    }
    public void SetInputDirection(PlayerDirection dir)
    {
        if (dir == PlayerDirection.UP && direction == PlayerDirection.DOWN ||
            dir == PlayerDirection.DOWN && direction == PlayerDirection.UP ||
            dir == PlayerDirection.RIGHT && direction == PlayerDirection.LEFT ||
            dir == PlayerDirection.LEFT && direction == PlayerDirection.RIGHT)
        {
            return;
        }
        direction = dir;
        ForceMove();
    }
    private void ForceMove()
    {
        counter = 0;
        move = false;
        Move();
    }
    #endregion

    #region Touching other objects
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.FRUIT)
        {
            other.gameObject.SetActive(false);
            createNodeAtTail = true;
            GamePlayController.instance.InstantiateEffects(0, other.transform.position);
            GamePlayController.instance.IncreaseScore();
            AudioManager.instance.PlaySound(0);
        }

        if (other.tag == Tags.WALL || other.tag == Tags.BOMB || other.tag == Tags.TAIL)
        {
            GamePlayController.instance.InstantiateEffects(1, other.transform.position);
            StartCoroutine(DelayCallingPanel());
        }
    }
    IEnumerator DelayCallingPanel()
    {
        yield return new WaitForSeconds(.5f);
        AudioManager.instance.PlaySound(1);
        GamePlayController.instance.ActivateFailedPanel();
        Time.timeScale = 0;
    } 
    #endregion
}
