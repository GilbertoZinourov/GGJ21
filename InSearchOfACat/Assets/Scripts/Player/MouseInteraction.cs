using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseInteraction : MonoBehaviour
{
    private Camera _cam;
    private Ray _ray;
    private RaycastHit _hit;

    public bool _hasKey;
    public bool _hasRope;
    public bool _hasWindowKey;
    private bool _inCoroutine;

    public AbstractInteractable _currentInteractable;
    public GameObject _currentInteractableObj;
    [SerializeField] private LayerMask interactableMask;
    public float actionRange = 5f;
    private CandleBehaviour _candleBehaviour;

    public Text missingItemText;

    public event Action<AbstractInteractable, Vector3, bool> Hover;
    public event Action HoverOff;

    private void Awake()
    {
        _cam = Camera.main;
        _candleBehaviour = GetComponent<CandleBehaviour>();
    }

    private void Update()
    {
        CheckMouse();
    }

    private void CheckMouse()
    {
        _ray = _cam.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(_ray.origin, _ray.direction * 10, Color.blue);
        bool isInRange;
        if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, interactableMask))
        {
            //Debug.Log("Hovering");
            if (_currentInteractableObj != _hit.collider.gameObject)
            {
                _currentInteractableObj = _hit.collider.gameObject;
                _currentInteractable = _currentInteractableObj.GetComponent<AbstractInteractable>();
            }

            

            if (_currentInteractableObj.CompareTag("Crate") && _candleBehaviour.HasCandle())
            {
                isInRange = false;
            }
            else
            {
                Vector3 vec = new Vector3(_hit.point.x, _hit.point.y, 0);
                isInRange = CheckDistanceToInteractable(_currentInteractableObj.transform.position,
                    transform.position);
            }
            
            var temp1 = _currentInteractableObj.GetComponent<NextRoom>();
            if (temp1 && temp1.needKey)
            {
                if (!_hasKey)
                {
                    isInRange = false;
                    if(!_inCoroutine)
                        StartCoroutine(MissingItemText(temp1.interactionText));
                }
            }
            
            var temp2 = _currentInteractableObj.GetComponent<WindowNext>();
            if (temp2 && temp2.needKey)
            {
                if (!_hasWindowKey)
                {
                    isInRange = false;if(!_inCoroutine)
                        StartCoroutine(MissingItemText(temp2.interactionText));
                }
            }
            
            var temp4 = _currentInteractableObj.GetComponent<RopeNext>();
            if (temp4 && temp4.needKey)
            {
                if (!_hasRope)
                {
                    isInRange = false;
                    if(!_inCoroutine)
                        StartCoroutine(MissingItemText(temp4.interactionText));
                }
            }

            OnHover(_currentInteractable, Input.mousePosition, isInRange);


            if (Input.GetMouseButtonDown(0) && isInRange)
            {
                var temp = _currentInteractableObj.GetComponent<KeyDrawer>();
                if (temp)
                {
                    bool tempKey = temp.Clicked();
                    if (!_hasKey)
                    {
                        _hasKey = tempKey;
                    }
                }
                
                var temp3 = _currentInteractableObj.GetComponent<WindowKeyDrawer>();
                if (temp3)
                {
                    bool tempKey = temp3.Clicked();
                    if (!_hasWindowKey)
                    {
                        _hasWindowKey = tempKey;
                    }
                }

                if (temp1 && temp1.needKey && _hasKey)
                {
                    temp1.needKey = false;
                    _hasKey = false;
                }
                
                if (temp2 && temp2.needKey && _hasWindowKey)
                {
                    temp2.needKey = false;
                    _hasWindowKey = false;
                }
                
                if (temp4 && temp4.needKey && _hasRope)
                {
                    temp4.needKey = false;
                    _hasRope = false;
                }

                if (_currentInteractable.CompareTag("Rope"))
                {
                    _hasRope = true;
                }
                
                if (_currentInteractable.CompareTag("HideSpot"))
                {
                    GetComponent<PlayerMovement>().Hide();
                }
                
                if (_currentInteractableObj.CompareTag("Candle"))
                {
                    _candleBehaviour.PickUpOrDropCandle();
                }

                if (_currentInteractableObj.CompareTag("CandleDrawer") && !_candleBehaviour._droppedCandle)
                {
                    _candleBehaviour.CandleReset();
                    if (!_candleBehaviour._hasCandle)
                    {
                        _candleBehaviour.PickUpOrDropCandle();
                    }
                }

                _currentInteractable.Click();
            }
        }
        else
        {
            OnHoverOff();
        }

        if (Input.GetMouseButtonDown(1) && _candleBehaviour.HasCandle())
        {
            _candleBehaviour.PickUpOrDropCandle();
        }
    }

    private IEnumerator MissingItemText(string interactionText)
    {
        _inCoroutine = true;
        missingItemText.text = interactionText;
        missingItemText.enabled = true;
        yield return new WaitForSeconds(5);
        missingItemText.enabled = false;
        _inCoroutine = false;
    }

    private bool CheckDistanceToInteractable(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b) < actionRange;
    }

    protected virtual void OnHover(AbstractInteractable obj, Vector3 pos, bool isInRange)
    {
        Hover?.Invoke(obj, pos, isInRange);
    }

    protected virtual void OnHoverOff()
    {
        HoverOff?.Invoke();
    }
}