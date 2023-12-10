using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHope : MonoBehaviour
{

    [Header("HOPE CONFIG")]
    [SerializeField]
    private float speed;

    private Transform _transform;
    private Rigidbody2D _rb;
    private Vector2 _targetDirection = Vector2.zero;
    private bool _isFacingRight = true;

    private Queue<ReplayMove> moveList = new Queue<ReplayMove>();

    private bool isReplaying = false;

    private void OnDestroy()
    {
        LeanTween.cancel(gameObject);
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        if (!isReplaying) return;

        bool hasMoreFrames = false;
        hasMoreFrames = PlayNextFrame();

        if (!hasMoreFrames)
        {
            isReplaying = false;
        }
    }


    private void MovePlayer()
    {
        _rb.velocity = speed * _targetDirection;

        FlipPlayer(_targetDirection.x);
    }


    public void SetTargetDirection(Vector2 _dir)
    {
        _targetDirection = _dir;
    }

    public void SetMove(Queue<ReplayMove> mv)
    {
        moveList = mv;

        isReplaying = true;
    }

    public bool PlayNextFrame()
    {
        bool hasMoreFrames = false;
        if (moveList.Count != 0)
        {
            ReplayMove data = moveList.Dequeue();
            SetTargetData(data);
            hasMoreFrames = true;
        }
        return hasMoreFrames;
    }

    public void SetTargetData(ReplayMove replay)
    {
        _transform.position = replay.Pos;
        _transform.localScale = replay.Scale;
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
}


