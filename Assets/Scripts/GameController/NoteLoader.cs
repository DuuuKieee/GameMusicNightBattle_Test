using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NoteLoader : MonoBehaviour
{
    public static NoteLoader Instance { get; private set; }
    public NoteList notes = new NoteList();
    public int[] uniqueNoteIDs;
    public GameObject notePrefab;
    public PlayerLane[] lanes;
    public OpponentLane[] opponentLanes;
    public float lastNoteTime = 0;

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
        ObjectPool.Instance.InitializePool("Note", notePrefab, 10);
    }

    public void Initialize()
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
        foreach (PlayerLane lane in lanes)
        {
            lane.SetTimeStamps(notes.list);
        }
        foreach (OpponentLane lane in opponentLanes)
        {
            lane.SetTimeStamps(notes.list);
        }
        lastNoteTime = notes.list.Last().timeAppear + notes.list.Last().duration + GameConfig.DELAY_MUSIC + 1;
    }
    public void SetEndGame()
    {
        foreach (PlayerLane lane in lanes)
        {

            lane.timeStamps.Clear();
            lane.ResetLane();
        }
        foreach (OpponentLane lane in opponentLanes)
        {
            lane.timeStamps.Clear();
            lane.ResetLane();
            
        }
    }
}
