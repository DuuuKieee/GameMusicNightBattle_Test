using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Lane : MonoBehaviour
{
    public GameObject notePrefab;
    public NoteLoader noteLoader;
    public Button ipnut;
    public Sprite noteSprites;
    public string playerAnimatorParameter;
    public Animator playerAnimator;
    List<NoteBehavior> notes = new List<NoteBehavior>();
    public List<double> timeStamps = new List<double>();
    public int noteRetrictions;
    private float startTime;
    public double marginOfError = 0.1;

    int spawnIndex = 0;
    int inputIndex = 0;

    void Start()
    {
        startTime = Time.time;

        // Gắn hàm xử lý cho Button UI (nếu cần qua mã)
        ipnut.onClick.AddListener(OnButtonPressed);
    }

    public void SetTimeStamps(Note[] noteList)
    {
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
        float currentTime = Time.time - startTime; // Điều chỉnh thời gian hiện tại

        if (spawnIndex < timeStamps.Count)
        {
            if (currentTime > timeStamps[spawnIndex])
            {
                SpawnNote(noteLoader.notes.list[spawnIndex]);
                spawnIndex++;
            }
        }
        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex] + 2.2f;
            if (currentTime > timeStamp + marginOfError)
            {
                Miss();
                Debug.Log($"Missed note {inputIndex}");
                inputIndex++;
            }
        }
    }
    void SpawnNote(Note note)
    {
        GameObject newNote = Instantiate(notePrefab, this.transform.position, Quaternion.identity);
        newNote.GetComponentInChildren<SpriteRenderer>().sprite = noteSprites;
        newNote.GetComponent<NoteBehavior>().button = ipnut;
        newNote.GetComponent<NoteBehavior>().Setup(note);
        notes.Add(newNote.GetComponent<NoteBehavior>());
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
                Destroy(notes[inputIndex].gameObject);
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
