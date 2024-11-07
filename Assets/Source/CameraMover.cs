using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Vector3 _offSet;
    [SerializeField] private float _speed = 5f;

    private Transform _player;
    private bool _isMove = false;
    private Vector3 _startPosition = new Vector3(-1.213f, -34.5f, 17.892f);
    private Quaternion _startRotation = Quaternion.Euler(22, 140, 0);

    private void Awake()
    {
        _startPosition = transform.position;

        SetStartPosition();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void StartMove()
    {
        _isMove = true;
    }

    public void ResetCameraPosition()
    {
        SetStartPosition();
    }

    public void EndMove()
    {
        _isMove = false;
    }

    public void GetPlayerTransform(Transform transform)
    {
        _player = transform;
    }

    private void SetStartPosition()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
    }

    private void Move()
    {
        if (_isMove)
        {
            Vector3 newCamPosition = new Vector3(_player.position.x + _offSet.x, _player.position.y + _offSet.y, _player.position.z + _offSet.z);
            Quaternion newCamRotation = Quaternion.Euler(20, 0, 0);
            transform.position = Vector3.Lerp(transform.position, newCamPosition, _speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, newCamRotation, _speed * Time.deltaTime);
        }
    }
}
