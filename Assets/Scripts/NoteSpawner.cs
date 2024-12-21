// using UnityEngine;
// using System.Collections.Generic;
// using System.Linq;


// public class NoteSpawner : MonoBehaviour
// {
//     public GameObject notePrefab;
//     public NoteLoader noteLoader;
//     public Sprite noteSprites;
//     private int _spawnIdx = 0;

//     private float startTime;

//     void Start()
//     {
//         startTime = Time.time;
//     }

//     void Update()
//     {
//         float currentTime = Time.time - startTime;

//         if (currentTime > noteLoader.notes.list[_spawnIdx].timeAppear)
//         {
//             SpawnNote(noteLoader.notes.list[_spawnIdx]);
//             if (_spawnIdx == noteLoader.notes.list.Length) return;
//             _spawnIdx++;
//         }

//     }

//     void SpawnNote(Note note)
//     {
//         int noteRow = noteLoader.uniqueNoteIDs.ToList().IndexOf(note.noteID);

//         GameObject newNote = Instantiate(notePrefab, spawnPos, Quaternion.identity);
//         newNote.GetComponentInChildren<SpriteRenderer>().sprite = noteSprites;
//         newNote.GetComponent<NoteBehavior>().Setup(note);
//     }

// }
