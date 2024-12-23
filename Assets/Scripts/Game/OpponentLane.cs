using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class OpponentLane : MonoBehaviour
{
    public NoteLoader noteLoader;
    public Sprite noteSprites;
    public string playerAnimatorParameter;
    public Animator playerAnimator;
    List<NoteBehavior> notes = new List<NoteBehavior>();
    public List<double> timeStamps = new List<double>();
    public int noteRetrictions;
    public double marginOfError = 0.1;
    public bool isInteractable;

    private float _startTime;
    private int _spawnIndex = 0;
    private int _inputIndex = 0;

    void Start()
    {
        _startTime = Time.time;
    }

    public void SetTimeStamps(Note[] noteList)
    {
        _startTime = Time.time;
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

    public void ResetLane()
    {
        _spawnIndex = 0;
        _inputIndex = 0;
    }

    void Update()
    {
        float currentTime = Time.time - _startTime; // Điều chỉnh thời gian hiện tại

        if (_spawnIndex < timeStamps.Count)
        {
            if (currentTime > timeStamps[_spawnIndex])
            {
                SpawnNote();
                _spawnIndex++;
            }
        }
        if (_inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[_inputIndex] + 2.3f;
            double timeDifference = Math.Abs(currentTime - timeStamp);

            if (timeDifference <= marginOfError)
            {
                DespawnNote(notes[_inputIndex].gameObject);
                playerAnimator.Play(playerAnimatorParameter);
                _inputIndex++;
            }
        }
    }
    public void SpawnNote()
    {
        GameObject note = ObjectPool.Instance.GetObject("Note");
        note.transform.position = this.transform.position;
        note.GetComponentInChildren<SpriteRenderer>().sprite = noteSprites;
        notes.Add(note.GetComponent<NoteBehavior>());

    }

    public void DespawnNote(GameObject note)
    {
        ObjectPool.Instance.ReturnObject("Note",note);
    }
}
