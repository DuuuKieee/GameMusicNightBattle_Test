using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Lane : MonoBehaviour
{
    public GameObject notePrefab;
    public Animator fxAnim;
    public NoteLoader noteLoader;
    public Button ipnut;
    public Sprite noteSprites;
    public string playerAnimatorParameter;
    public Animator playerAnimator;
    List<NoteBehavior> notes = new List<NoteBehavior>();
    public List<double> timeStamps = new List<double>();
    public int noteRetrictions;
    private float startTime;
    private double marginOfError = GameConfig.MARGIN_OF_ERROR;

    private int spawnIndex = 0;
    private int inputIndex = 0;

    void Start()
    {
        startTime = Time.time;

        // Gắn hàm xử lý cho Button UI (nếu cần qua mã)
        ipnut.onClick.AddListener(OnButtonPressed);
    }

    public void SetTimeStamps(Note[] noteList)
    {
        spawnIndex = 0;
        inputIndex = 0;
        startTime = Time.time;
        timeStamps.Clear();
        notes.Clear();
        foreach (var note in noteList)
        {
            if (NoteLoader.Instance.uniqueNoteIDs.ToList().IndexOf(note.noteID) == noteRetrictions)
            {
                timeStamps.Add(note.timeAppear);
            }
        }
    }

    void Update()
    {
        float currentTime = Time.time - startTime;

        if (spawnIndex < timeStamps.Count)
        {
            if (currentTime > timeStamps[spawnIndex])
            {
                SpawnNote();
                spawnIndex++;
            }
        }
        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex] + 2.3f;
            if (currentTime > timeStamp + marginOfError)
            {
                Miss();
                Debug.Log($"Missed note {inputIndex}");
                inputIndex++;
            }
        }
    }

    public void SpawnNote()
    {
        GameObject note = ObjectPool.Instance.GetObject();
        note.transform.position = this.transform.position;
        note.GetComponentInChildren<SpriteRenderer>().sprite = noteSprites;
        notes.Add(note.GetComponent<NoteBehavior>());

    }

    public void DespawnNote(GameObject note)
    {
        ObjectPool.Instance.ReturnObject(note);
    }

   public void OnButtonPressed()
    {
        float currentTime = Time.time - startTime;

        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex] + 2.2f;
            double timeDifference = Math.Abs(currentTime - timeStamp);

            if (timeDifference <= marginOfError)
            {
                DespawnNote(notes[inputIndex].gameObject);
                playerAnimator.Play(playerAnimatorParameter);
                Debug.Log($"Hit note {inputIndex} with margin {timeDifference}s");

                if (timeDifference <= marginOfError / 2)
                {
                    Hit(true);
                }
                else
                {
                    Hit(false);
                }
                fxAnim.Play("Fx_LinePopup");
                inputIndex++;
            }
            else
            {
                Debug.Log($"Inaccurate hit note {inputIndex} with margin {timeDifference}s");
            }
        }
    }


    private void Hit(bool _isPerfect)
    {
        ScoreManager.Instance.Hit(_isPerfect);
    }

    private void Miss()
    {
        ScoreManager.Instance.Miss();
    }
}
