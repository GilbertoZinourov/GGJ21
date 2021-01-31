using UnityEngine;

public class Candle : AbstractInteractable
{
    public float duration;
    public float speed;

    private void Update()
    {
        duration -= speed * Time.deltaTime;
        if (duration <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    public override void Click()
    {
        Destroy(transform.parent.gameObject);
    }
}
