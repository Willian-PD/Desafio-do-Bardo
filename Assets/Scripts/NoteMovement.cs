using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    private float _speed;

    /// <summary>
    /// The Note Speed.
    /// </summary>
    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Speed * Vector3.left * Time.deltaTime);
    }
}
