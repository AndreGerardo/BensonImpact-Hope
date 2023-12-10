using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HopeRecording
{



}

public class HopeManager : MonoBehaviour
{
    public static HopeManager instance;

    public Transform levelSpawnPos;

    [Header("HOPE CONFIGURATION")]
    [SerializeField]
    private PlayerHope hopePrefab;

    public PlayerDeadAnim deathPrefab;

    public float recordingTime;

    private List<MoveList> hopeMoveList = new List<MoveList>();

    private List<PlayerHope> hopeObjList = new List<PlayerHope>();

    private Dictionary<MoveDirection, Vector2> _movementPresetVector = new Dictionary<MoveDirection, Vector2>();

    private bool isReplaying = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        InitDirectionPreset();
    }


    public void SetNewHope(MoveList hopeMove)
    {
        //Debug.Log(hopeMove.moveSet.Count);
        hopeMoveList.Add(hopeMove);
    }

    public void PlayRecordedHope()
    {
        ResetCurrentHope();

        for (int i = 0; i < hopeMoveList.Count; i++)
        {
            PlayerHope obj = Instantiate(hopePrefab);

            obj.transform.position = levelSpawnPos.position;

            Queue<ReplayMove> moveToSend = new Queue<ReplayMove>(hopeMoveList[i].replaySet);
            obj.SetMove(moveToSend);

            hopeObjList.Add(obj);



            //Debug.Log($"Saved Move Count : {hopeMoveList[i].moveSet.Count}");

            //float currentDelay = 0f;
            //for (int j = 0; j < hopeMoveList[i].moveSet.Count; j++)
            //{
            //    RecordMove currentRecord = hopeMoveList[i].moveSet[j];
            //    LeanTween.delayedCall(currentDelay, () =>
            //    {
            //        obj.SetTargetDirection(_movementPresetVector[currentRecord.Key]);
            //        //LeanTween.move(obj.gameObject, currentRecord.Pos, currentRecord.Value);
            //    });

            //    currentDelay += currentRecord.Value;
            //}

        }

    }

    public void ResetCurrentHope()
    {
        for (int i = 0; i < hopeObjList.Count; i++)
        {
            PlayerDeadAnim obj = Instantiate(deathPrefab);

            obj.transform.position = hopeObjList[i].transform.position;
            obj.SetDeathAnim(false);

            Destroy(hopeObjList[i].gameObject);
        }
        hopeObjList.Clear();

        isReplaying = false;
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

}
