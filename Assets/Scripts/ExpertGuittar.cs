using UnityEngine;

public class ExpertGuittar : MonoBehaviour
{
    private float _acceleration = 2f;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        //transform.Translate(Vector3.down * _acceleration * Time.deltaTime);
        if (_acceleration == 2)
        {
            /*Do Nothing*/
        }
        if (Input.GetKeyDown(KeyCode.P)) Time.timeScale = Time.timeScale == 1.0f ? 0.0f : 1.0f;
    }
}
