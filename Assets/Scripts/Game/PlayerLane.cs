using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PlayerLane : MonoBehaviour
{
    public Animator fxAnim;
    public NoteLoader noteLoader;
    public Button ipnut;
    public Sprite noteSprites;
    public string playerAnimatorParameter;
    public Animator playerAnimator;
    List<NoteBehavior> notes = new List<NoteBehavior>();
    public List<double> timeStamps = new List<double>();
    public int noteRetrictions;
    private float _startTime;
    private double _marginOfError = GameConfig.MARGIN_OF_ERROR;

    private int _spawnIndex = 0;
    private int _inputIndex = 0;

    void Start()
    {
        _startTime = Time.time;

        ipnut.onClick.AddListener(OnButtonPressed);
    }

    public void SetTimeStamps(Note[] noteList)
    {
        _spawnIndex = 0;
        _inputIndex = 0;
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

    void Update()
    {
        float currentTime = Time.time - _startTime;

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
            if (currentTime > timeStamp + _marginOfError)
            {
                Miss();
                Debug.Log($"Missed note {_inputIndex}");
                _inputIndex++;
            }
        }
    }
    public void ResetLane()
    {
        _spawnIndex = 0;
        _inputIndex = 0;
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
        ObjectPool.Instance.ReturnObject("Note" ,note);
    }

   public void OnButtonPressed()
    {
        float currentTime = Time.time - _startTime;

        if (_inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[_inputIndex] + 2.3f;
            double timeDifference = Math.Abs(currentTime - timeStamp);

            if (timeDifference <= _marginOfError)
            {
                DespawnNote(notes[_inputIndex].gameObject);
                playerAnimator.Play(playerAnimatorParameter);
                Debug.Log($"Hit note {_inputIndex} with margin {timeDifference}s");

                if (timeDifference <= _marginOfError / 2)
                {
                    Hit(true);
                }
                else
                {
                    Hit(false);
                }
                fxAnim.Play("Fx_LinePopup");
                _inputIndex++;
            }
            else
            {
                Debug.Log($"Inaccurate hit note {_inputIndex} with margin {timeDifference}s");
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
