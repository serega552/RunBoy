using System.Collections;
using UnityEngine;

public class ChuchpanMover : MonoBehaviour
{
    public float speed = 0.2f;
    private float _targetX = 1f;
    private float _minX = -0.9f;
    private float _maxX = 1f;
    private bool _isPushed = false;
    private Vector3 _targetPosition;
    private Chuchpan _chuchpan;

    private void Start()
    {
        _targetPosition = new Vector3(_targetX, transform.position.y, transform.position.z);
        _chuchpan = GetComponent<Chuchpan>();
        _chuchpan.OnHit += Push;
    }

    private void Update()
    {
        if (!_isPushed)
        {
            MoveTowardsTarget();
            if (Mathf.Abs(transform.position.x - _targetPosition.x) < 0.001f)
            {
                ChangeDirection();
            }
        }
        else
        {
            transform.Rotate(0f, 0f, 0f);
        }
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
    }

    private void ChangeDirection()
    {
        _targetX = (_targetX == _minX) ? _maxX : _minX;
        _targetPosition = new Vector3(_targetX, transform.position.y, transform.position.z);

        transform.Rotate(0f, 180f, 0f);
    }

    private void Push(float speed)
    {
        float pushDistance = speed += 3f;
        StartCoroutine(SmoothPush(pushDistance));
    }

    private IEnumerator SmoothPush(float pushDistance)
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, 0, pushDistance);

        float pushDuration = 0.5f;
        float time = 0;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 180, 0);

        while (time < pushDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time / pushDuration);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, time / pushDuration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
        transform.rotation = endRotation;
        _isPushed = true;
    }
}
