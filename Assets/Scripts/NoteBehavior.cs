using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class NoteBehavior : MonoBehaviour
{
    private Note noteData;
    private float fallSpeed = 4f;
    public bool canBePressed;
    public Button button;

    public void Setup(Note note)
    {
        noteData = note;
    }

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
            
        }
        
    }
}
