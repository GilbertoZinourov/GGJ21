using UnityEngine;

public class MonsterHand : MonoBehaviour
{
    [SerializeField] private Transform start, end;
    private Vector3 _startVec, _endVec;

    private void Awake()
    {
        _startVec = start.localPosition;
        _endVec = end.localPosition;
    }

    public void DoMovement(float val)
    {
        Vector3 newPos = Vector3.Lerp(_startVec, _endVec, val);
        transform.localPosition = newPos;
    }
}
