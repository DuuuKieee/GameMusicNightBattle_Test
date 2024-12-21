using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class EnemyLane : MonoBehaviour
{
    public GameObject notePrefab;
    public NoteLoader noteLoader;
    public Sprite noteSprites;
    public string playerAnimatorParameter;
    public Animator playerAnimator;
    List<NoteBehavior> notes = new List<NoteBehavior>();
    public List<double> timeStamps = new List<double>();
    public int noteRetrictions;
    private float startTime;
    public double marginOfError = 0.1;
    public bool isInteractable;

    int spawnIndex = 0;
    int inputIndex = 0;

    void Start()
    {
        startTime = Time.time;
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
            double timeStamp = timeStamps[inputIndex] + 2.3f;
            double timeDifference = Math.Abs(currentTime - timeStamp);

            if (timeDifference <= marginOfError)
            {
                Destroy(notes[inputIndex].gameObject);
                playerAnimator.Play(playerAnimatorParameter);
                inputIndex++;
            }
        }
    }
    void SpawnNote(Note note)
    {
        GameObject newNote = Instantiate(notePrefab, this.transform.position, Quaternion.identity);
        newNote.GetComponentInChildren<SpriteRenderer>().sprite = noteSprites;
        newNote.GetComponent<NoteBehavior>().Setup(note);
        notes.Add(newNote.GetComponent<NoteBehavior>());
    }
}
