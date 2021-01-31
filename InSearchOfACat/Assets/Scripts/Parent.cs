using UnityEngine;

public class Parent : MonoBehaviour
{
    private bool _locked = true;
    private Vector3 _startingPos;
    [SerializeField] private float speed = 1;
    private Transform _pater;

    private void Awake()
    {
        _startingPos = transform.localPosition;
        _pater = transform.parent;
    }

    private void Update()
    {
        if (!_locked)
        {
            transform.position += Vector3.right * (speed * Time.deltaTime);
            
            if (transform.position.x >= 9)
            {
                ResetParent();
            }
        }
    }

    public void UnlockParent()
    {
        if (_locked)
        {
            ResetParent();
            _locked = false;
            transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_locked) return;
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<PlayerMovement>().isHiding)
            {
                //Debug.Log("gameOver");
                FindObjectOfType<GameManager>().GameOver();
            }
        }

        if (other.CompareTag("Candle"))
        {
            Destroy(other.gameObject);
            ResetParent();
        }
    }

    private void ResetParent()
    {
        _locked = true;
        transform.parent = _pater;
        transform.localPosition = _startingPos;
    }
}
