using UnityEngine;

public class CandleBehaviour : MonoBehaviour
{
    [SerializeField] private Animator spriteAnim;
    [SerializeField] private GameObject candle;
    [SerializeField] private GameObject candleMask;
    [SerializeField] private FearMonsterHandsScript monsters;
    [SerializeField] private float safeDistance = 2, fearSpeed = .01f, safeSpeed = .1f;
    public bool _hasCandle = true;
    public Transform _droppedCandle;
    public float _fearLevel = 0, _maxFear = 1, _minFear = 0;
    private bool _gameOver = false;

    [SerializeField] private float consumptionSpeed = .01f;
    public float _currentDuration = 1, _maxDuration = 1, _minDuration = 0;

    private void Update()
    {
        FearLevel();
        CandleDuration();
    }

    private void CandleDuration()
    {
        _currentDuration -= consumptionSpeed * Time.deltaTime;
        if (_currentDuration <= _minDuration)
        {
            _currentDuration = _minDuration;
            if (_hasCandle)
            {
                PickUpOrDropCandle();
                _droppedCandle = null;
                //Debug.Log("DROP CANDLE");
            }
        }
    }

    public void CandleReset()
    {
        _currentDuration = _maxDuration;
    }

    private void FearLevel()
    {
        if (!_hasCandle)
        {
            if (_droppedCandle && Vector3.Distance(transform.position, _droppedCandle.position) > safeDistance &&
                _fearLevel < _maxFear)
            {
                Fear();
            }
            else
            {
                if (!_droppedCandle)
                {
                    Fear();
                }
                else
                {
                    Safe();
                }
            }
        }
        else
        {
            Safe();
        }

        monsters.Fear(_fearLevel);
    }

    private void Safe()
    {
        if (!_gameOver && _fearLevel > _minFear)
        {
            _fearLevel -= safeSpeed * Time.deltaTime;
            if (_fearLevel <= _minFear)
            {
                _fearLevel = _minFear;
            }
        }
    }

    private void Fear()
    {
        _fearLevel += fearSpeed * Time.deltaTime;
        if (_fearLevel >= _maxFear)
        {
            _fearLevel = _maxFear;
            _gameOver = true;
            Debug.Log("Game Over");
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    public void PickUpOrDropCandle()
    {
        if (_hasCandle)
        {
            GameObject obj = Instantiate(candle, transform.position, Quaternion.identity);
            _droppedCandle = obj.transform;
            var tempVar = obj.GetComponentInChildren<Candle>();
            tempVar.duration = _currentDuration;
            tempVar.speed = consumptionSpeed;
            candleMask.SetActive(false);
        }
        else
        {
            candleMask.SetActive(true);
            _droppedCandle = null;
        }

        _hasCandle = !_hasCandle;
        spriteAnim.SetBool("WithCandle", _hasCandle);
    }

    public bool HasCandle()
    {
        return _hasCandle;
    }
}