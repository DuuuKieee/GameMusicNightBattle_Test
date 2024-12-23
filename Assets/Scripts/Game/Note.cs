[System.Serializable]
public class Note
{
    public float timeAppear;
    public int noteID;
    public float duration;
}

[System.Serializable]
public class NoteList
{
    public Note[] list;
}
