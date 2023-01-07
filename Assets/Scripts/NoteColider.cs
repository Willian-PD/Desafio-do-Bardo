using System.Collections;
using UnityEngine;

public class NoteColider : MonoBehaviour
{
    [SerializeField] private Player _player;
    private UIManager _UIManager;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Warrior").GetComponent<Player>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(DestroyNote(other));
    }

    IEnumerator DestroyNote(Collider2D other)
    {
        if (other.tag == "Attack")
        {
            _UIManager.UpdateScore();
            Destroy(this.gameObject);
        }
        yield return new WaitForSecondsRealtime(0.2f);
        if (other.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage("Note");
            }
            _UIManager.DecreaseScore();
            Destroy(this.gameObject);
        }
    }
}
