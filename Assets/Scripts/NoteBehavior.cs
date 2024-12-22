using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class NoteBehavior : MonoBehaviour
{
    private float fallSpeed = 4f;

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        if (transform.position.y < -5.0f)
        {
            ObjectPool.Instance.ReturnObject(gameObject);
            
        }
        
    }
}
