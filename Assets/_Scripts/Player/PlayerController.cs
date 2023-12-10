using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public enum MoveDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    DIAGRIGHTUP,
    DIAGLEFTUP,
    DIAGRIGHTDOWN,
    DIAGLEFTDOWN,
    STAY
}

[System.Serializable]
public class RecordMove
{
    public MoveDirection Key;
    public float Value;

    public Vector3 Pos;

    public RecordMove(MoveDirection key, float value, Vector3 pos)
    {
        Key = key;
        Value = value;
        Pos = pos;
    }
}

[System.Serializable]
public class MoveList
{
    public List<RecordMove> moveSet;
    public Queue<ReplayMove> replaySet;

    public MoveList(Queue<ReplayMove> replaySet)
    {
        //this.moveSet = move;
        this.replaySet = replaySet;
    }
}

[System.Serializable]
public class ReplayMove
{
    public Vector3 Pos;
    public Vector3 Scale;

    public ReplayMove(Vector3 pos, Vector3 scale)
    {
        Pos = pos;
        Scale = scale;
    }
}

public class PlayerController : MonoBehaviour
{

    [Header("PLAYER CONFIG")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject torchSprite;

    private bool canMove = true;
    private Transform _transform;
    private Rigidbody2D _rb;
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _targetDirection = Vector2.zero;
    private bool _isFacingRight = true;
    private bool isLevelOver = false;


    [Header("MOVEMENT RECORD CONFIG")]
    private List<RecordMove> movementRecord = new List<RecordMove>();

    private Queue<ReplayMove> moveQueue = new Queue<ReplayMove>();

    private Dictionary<MoveDirection, Vector2> _movementPresetVector = new Dictionary<MoveDirection, Vector2>();

    private bool _startRecord = false;
    private float _timer = 0f;

    private float _currentRecordingTimer = 0f;

    private MoveDirection _lastDirection = MoveDirection.STAY;

    private void Awake()
    {
        EventManager.OnPlayerWin += SetLevelOver;
        EventManager.OnPlayerLose += SetLevelOver;
        EventManager.OnResetPlayer += ResetPlayer;
        EventManager.OnGamePause += SetPlayerIdleState;
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerWin -= SetLevelOver;
        EventManager.OnPlayerLose -= SetLevelOver;
        EventManager.OnResetPlayer -= ResetPlayer;
        EventManager.OnGamePause -= SetPlayerIdleState;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        InitDirectionPreset();

        StartCoroutine(FootStep(0.2f));
    }

    private IEnumerator FootStep(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            if(_rb.velocity.magnitude > Vector2.zero.magnitude)
            EventManager.OnFootStep?.Invoke();   
        }
    }

    private void FixedUpdate()
    {
        if(canMove && !isLevelOver) MovePlayer();
    }

    private void Update()
    {
        if (isLevelOver) return;

        MoveInput();

        if (_targetDirection.magnitude > 0f && !_startRecord && canMove)
        {
            PlayRecordedMove();
            StartRecordMovement();
        }

        //if (_startRecord)
        //{
        //    RecordMove();
        //}

        //if (!canMove && _targetDirection.magnitude <= 0.1f)
        //{
        //    canMove = true;
        //}


    }

    private void LateUpdate()
    {
        if (_startRecord)
        {
            RecordMove();
        }
    }


    private void MoveInput()
    {
        _targetDirection.x = Input.GetAxisRaw("Horizontal");
        _targetDirection.y = Input.GetAxisRaw("Vertical");
        _targetDirection.Normalize();

    }

    private void MovePlayer()
    {
        _rb.velocity = speed * _targetDirection;

        FlipPlayer(_targetDirection.x);

        _anim.SetFloat("Speed", _rb.velocity.magnitude);
    }

    private void StartRecordMovement()
    {
        _startRecord = true;
        movementRecord.Clear();
        Debug.Log("Start Record.");
    }

    private void PlayRecordedMove()
    {
        if (_startRecord) return;

        _transform.position = HopeManager.instance.levelSpawnPos.position;

        HopeManager.instance.PlayRecordedHope();


    }


    private void RecordMove()
    {

        //MoveDirection currentDirection = VectorToDir(_targetDirection);

        //_timer += Time.deltaTime;
        //_currentRecordingTimer += Time.deltaTime;

        //if (_lastDirection != currentDirection)
        //{
        //    movementRecord.Add(new RecordMove(_lastDirection, _timer, _transform.position));

        //    _lastDirection = currentDirection;
        //    _timer = 0f;
        //}

        //if (Input.GetKeyDown(KeyCode.Space)) //(_currentRecordingTimer >= HopeManager.instance.recordingTime)
        //{
        //    ResetRecording(currentDirection);
        //}

        ReplayMove recordMove = new ReplayMove(_transform.position, _transform.localScale);
        moveQueue.Enqueue(recordMove);

        if (Input.GetKeyDown(KeyCode.Space)) //(_currentRecordingTimer >= HopeManager.instance.recordingTime)
        {
            StopRecording(); //(currentDirection);
        }


    }

    private void StopRecording() //(MoveDirection _lastMove)
    {
        //movementRecord.Add(new RecordMove(_lastMove, _timer, _transform.position));

        //_lastDirection = MoveDirection.STAY;
        //_timer = 0f;
        //_currentRecordingTimer = 0f;

        //movementRecord.Add(new RecordMove(MoveDirection.STAY, 0f, _transform.position));

        //List<RecordMove> moveSetToSend = new List<RecordMove>(movementRecord);
        //HopeManager.instance.SetNewHope(new MoveList(moveSetToSend));

        Queue<ReplayMove> moveSetToSend = new Queue<ReplayMove>(moveQueue);
        HopeManager.instance.SetNewHope(new MoveList(moveSetToSend));

        moveQueue.Clear();

        EventManager.OnSoulUsed.Invoke();
        _startRecord = false;
        StartReset();

        Debug.Log("End Record.");

    }

    private void StartReset()
    {
        PlayerDeadAnim obj = Instantiate(HopeManager.instance.deathPrefab);

        obj.transform.position = _transform.position;
        obj.SetDeathAnim(true);

        _spriteRenderer.enabled = false;
        torchSprite.SetActive(false);

        HopeManager.instance.ResetCurrentHope();
        SetPlayerToIdle();
    }

    private void ResetPlayer()
    {
        _spriteRenderer.enabled = true;
        torchSprite.SetActive(true);
        _transform.position = HopeManager.instance.levelSpawnPos.position;
        _anim.SetTrigger("Reset");
        _rb.velocity = Vector2.zero;
        canMove = true;


    }

    private void SetPlayerToIdle()
    {
        canMove = false;
    }

    private void SetLevelOver()
    {
        canMove = false;
        isLevelOver = true;
    }

    private void SetPlayerIdleState(bool isPause)
    {
        canMove = !isPause;
    }

    private void FlipPlayer(float horizontalMove)
    {
        if (_isFacingRight && horizontalMove < 0f || !_isFacingRight && horizontalMove > 0f)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void InitDirectionPreset()
    {
        _movementPresetVector.Add(MoveDirection.UP, new Vector2(0f, 1f));
        _movementPresetVector.Add(MoveDirection.DOWN, new Vector2(0f, -1f));
        _movementPresetVector.Add(MoveDirection.LEFT, new Vector2(-1f, 0f));
        _movementPresetVector.Add(MoveDirection.RIGHT, new Vector2(1f, 0f));

        _movementPresetVector.Add(MoveDirection.DIAGRIGHTUP, new Vector2(1f, 1f).normalized);
        _movementPresetVector.Add(MoveDirection.DIAGLEFTUP, new Vector2(-1f, 1f).normalized);
        _movementPresetVector.Add(MoveDirection.DIAGRIGHTDOWN, new Vector2(1f, -1f).normalized);
        _movementPresetVector.Add(MoveDirection.DIAGLEFTDOWN, new Vector2(-1f, -1f).normalized);

        _movementPresetVector.Add(MoveDirection.STAY, new Vector2(0f, 0f));
    }

    private MoveDirection VectorToDir(Vector2 dir)
    {
        if (dir.magnitude > 0f)
        {
            if (Vector2.Distance(dir, _movementPresetVector[MoveDirection.UP]) <= 0.1f)
            {
                return MoveDirection.UP;
            }
            else if (Vector2.Distance(dir, _movementPresetVector[MoveDirection.DOWN]) <= 0.1f)
            {
                return MoveDirection.DOWN;
            }
            else if (Vector2.Distance(dir, _movementPresetVector[MoveDirection.LEFT]) <= 0.1f)
            {
                return MoveDirection.LEFT;
            }
            else if (Vector2.Distance(dir, _movementPresetVector[MoveDirection.RIGHT]) <= 0.1f)
            {
                return MoveDirection.RIGHT;
            }
            else if (Vector2.Distance(dir, _movementPresetVector[MoveDirection.DIAGRIGHTUP]) <= 0.1f)
            {
                return MoveDirection.DIAGRIGHTUP;
            }
            else if (Vector2.Distance(dir, _movementPresetVector[MoveDirection.DIAGLEFTUP]) <= 0.1f)
            {
                return MoveDirection.DIAGLEFTUP;
            }
            else if (Vector2.Distance(dir, _movementPresetVector[MoveDirection.DIAGRIGHTDOWN]) <= 0.1f)
            {
                return MoveDirection.DIAGRIGHTDOWN;
            }
            else
            {
                return MoveDirection.DIAGLEFTDOWN;
            }
        }
        else
        {
            return MoveDirection.STAY;
        }
    }

  

}
