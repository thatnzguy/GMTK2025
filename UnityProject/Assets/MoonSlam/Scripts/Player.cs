using UnityEngine;

public class Player : MonoBehaviour
{
    public void SetSpawn(Transform location)
    {
        transform.position = location.position;
        transform.rotation = location.rotation;
    }
}