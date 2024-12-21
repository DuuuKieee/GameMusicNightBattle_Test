using UnityEngine;
using System.Collections.Generic;

public class NoteLoader : MonoBehaviour
{
    public static NoteLoader Instance { get; private set; } // Singleton property
    public NoteList notes = new NoteList();
    public int[] uniqueNoteIDs;
    public Lane[] lanes;
    public EnemyLane[] enemyLanes;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional: Keep this instance alive across scenes
    }

    void Start()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("notes");
        notes = JsonUtility.FromJson<NoteList>(jsonData.text);
        HashSet<int> uniqueIDs = new HashSet<int>();
        foreach (Note note in notes.list)
        {
            uniqueIDs.Add(note.noteID); 
        }
        uniqueNoteIDs = new int[uniqueIDs.Count];
        uniqueIDs.CopyTo(uniqueNoteIDs);
        System.Array.Sort(uniqueNoteIDs);
        foreach (Lane lane in lanes)
        {
            lane.SetTimeStamps(notes.list);
        }
        foreach (EnemyLane lane in enemyLanes)
        {
            lane.SetTimeStamps(notes.list);
        }
    }
}
