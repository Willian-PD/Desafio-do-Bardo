using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _attackPhysics;
    [SerializeField] private GameObject _FixedPosition;
    [SerializeField] private float _attackTime = 0.0f;
    [SerializeField] private float _slideTime = 1f;

    private Animator _animator;
    private UIManager _UIManager;
    private float[] _verticalPosition = new float[] { -0.37f, 0.7f, 1.7f, 2.7f, 3.7f };
    private float _initialLife = 0f;
    private int _positionCount = 0;
    private bool _sliding = false;
    private BoxCollider2D _regularCall;

    public float life = 100f;
    public int boost = 3;

    private AudioSource _audio;

    /// <summary>
    /// When we instantiate de gameObject
    /// </summary>
    private void Awake()
    {
        _attackPhysics.SetActive(false);
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        _initialLife = life;
        _animator = GetComponent<Animator>();
        _audio = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _regularCall = GetComponent<BoxCollider2D>();

        if (_UIManager != null)
        {
            _UIManager.UpdateLives(life);
            _UIManager.UpdateBoost(boost);
            _UIManager.InitScore();
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (life >= 0)
        {
            Movement();
            CanAttack();
            if (boost > 0) UseBoost();
            if (transform.position.x < -15.50f) OutOfScreen();
        }
    }

    private void Movement()
    {
        float verticalInput = 0;
        _animator.SetInteger("Run", 1);
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            verticalInput = 1;
            RunMovement(verticalInput);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            verticalInput = -1;
            RunMovement(verticalInput);
        }
        Slide();
    }

    private void RunMovement(float vertical)
    {
        VerticalMovement(vertical);
    }

    /// <summary>
    /// Here i just set the player position on the y axis
    /// </summary>
    /// <param name="vertical"></param>
    private void VerticalMovement(float vertical)
    {
        if (vertical > 0 && _positionCount <= 4)
        {
            _positionCount++;
        }
        else if (vertical < 0 && _positionCount >= 0)
        {
            _positionCount--;
        }
        switch (_positionCount)
        {
            case 0:
                transform.position = new Vector3(transform.position.x, _verticalPosition[_positionCount], 0);
                break;
            case 1:
                transform.position = new Vector3(transform.position.x, _verticalPosition[_positionCount], 0);
                break;
            case 2:
                transform.position = new Vector3(transform.position.x, _verticalPosition[_positionCount], 0);
                break;
            case 3:
                transform.position = new Vector3(transform.position.x, _verticalPosition[_positionCount], 0);
                break;
            case 4:
                transform.position = new Vector3(transform.position.x, _verticalPosition[_positionCount], 0);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Player attack to destroy each notes
    /// </summary>
    private void CanAttack()
    {
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(AttackCount());
        }
    }

    /// <summary>
    /// Is a method to count time of player attacks
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackCount()
    {
        _animator.SetTrigger("Attack");
        _attackPhysics.SetActive(true);
        yield return new WaitForSeconds(_attackTime);
        _attackPhysics.SetActive(false);
    }

    /// <summary>
    /// Method to make the character slinding to dodge the note
    /// </summary>
    private void Slide()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _sliding = true;
            _animator.SetBool("Slide", _sliding);
            _regularCall.enabled = false;
            StartCoroutine(SlidingCount(_sliding));
        }
    }

    /// <summary>
    /// Couting the time for player slide animation
    /// </summary>
    /// <param name="slideState"></param>
    /// <returns></returns>
    private IEnumerator SlidingCount(bool slideState)
    {
        yield return new WaitForSeconds(_slideTime);

        _sliding = false;
        _animator.SetBool("Slide", _sliding);

        _regularCall.enabled = true;
    }

    /// <summary>
    /// Damage control
    /// </summary>
    /// <param name="objTag"></param>
    public void Damage(string objTag)
    {
        // if the player touch a note
        if (objTag == "Note")
        {
            life--;
            _UIManager.UpdateLives(life);
            if (life <= 0)
            {
                if (life == 0) _animator.SetTrigger("Hurt");
                _animator.SetBool("Dead", true);
                _audio.Stop();
            }
            else if (life > 0)
            {
                _animator.SetTrigger("Hurt");
            }
        }
    }

    public void OutOfScreen()
    {
        life = 0f;
        _UIManager.UpdateLives(life);
        if (life <= 0)
        {
            if (life == 0) _animator.SetTrigger("Hurt");
            _animator.SetBool("Dead", true);
            _audio.Stop();
        }
        else if (life > 0)
        {
            _animator.SetTrigger("Hurt");
        }
    }

    /// <summary>
    /// Allows the use of boost
    /// </summary>
    private void UseBoost()
    {
        // Increase the character life in 25%
        if (Input.GetKeyDown(KeyCode.Z))
        {
            boost--;
            life += _initialLife * 0.25f;
            _UIManager.UpdateLives(life);
            _UIManager.UpdateBoost(boost);
        }
        // Return character to initial position, the same position of the camera handler
        else if (Input.GetKeyDown(KeyCode.X))
        {
            boost--;
            _UIManager.UpdateBoost(boost);
            this.transform.position = _FixedPosition.transform.position;
            this.transform.position = new Vector3(_FixedPosition.transform.position.x, _verticalPosition[_positionCount], 0);
        }
    }
}
