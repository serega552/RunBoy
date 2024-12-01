using System;
using InputSystem;
using Tasks;
using Tasks.SO;
using UnityEngine;

namespace Player
{
    public class PlayerMoverModel
    {
        private readonly float _maxSpeed = 3f;
        private readonly float _maxTurnSpeed = 1.5f;
        private readonly float _xPosition = 1.7f;
        private readonly float _yJumpForce = 2f;
        private readonly float _maxMoveVariableSpeed = 10;
        private readonly float _editMoveSpeed = 0.01f;
        private readonly Rigidbody _rigidbody;

        private float _turn = 0;
        private float _turnSpeed;
        private float _lastMoveSpeed;
        private float _moveVariableSpeed;
        private float _speedBonus;
        private float _speedTime;
        private float _jumpPower = 3.5f;
        private float _moveCoefficient;
        private bool _isMove = false;
        private bool _isSpeedBoost;
        private PlayerInputHandler _inputHandler;

        public PlayerMoverModel(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public event Action Jumped;

        public event Action<float> SpeedChanging;

        public event Action<float> BoostTimeChanging;

        public float MoveSpeed { get; private set; }

        public void SetDataMove(float coefficient)
        {
            _moveCoefficient = coefficient;
        }

        public void Move()
        {
            if (_isMove)
            {
                Vector3 currentPosition = _rigidbody.position;

                _turn = DefineTurn();

                float newXPosition = currentPosition.x + _turnSpeed * _turn * Time.deltaTime;
                newXPosition = Mathf.Clamp(newXPosition, -_xPosition, _xPosition);

                _rigidbody.position = new Vector3(newXPosition, currentPosition.y, currentPosition.z);

                _rigidbody.position += _rigidbody.transform.forward * MoveSpeed * Time.deltaTime;
            }
        }

        public void StartMove()
        {
            MoveSpeed = _maxSpeed;
            _moveVariableSpeed = _maxSpeed;
            _isMove = true;
            _turnSpeed = _maxTurnSpeed;
            SpeedChanging?.Invoke(MoveSpeed);

            _isSpeedBoost = false;
            BoostTimeChanging?.Invoke(0);
        }

        public void ResetMove()
        {
            _rigidbody.velocity = Vector3.zero;
            _isMove = false;
            MoveSpeed = 0;
            _moveVariableSpeed = 0;
            _turnSpeed = 0;
        }

        public void EndMove()
        {
            _isMove = false;
            _rigidbody.velocity = Vector3.zero;
        }

        public void Update()
        {
            ChangeSpeed();
            Move();
        }

        public void ChangeSpeedCrash(float moveSpeed)
        {
            _moveVariableSpeed = moveSpeed;
            _isSpeedBoost = false;
        }

        public void SetInput(PlayerInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        public void TurnOnSpeedBoost(float bonus, float time)
        {
            if (_isSpeedBoost == false)
            {
                _speedBonus = bonus;
                _speedTime = time;
                _isSpeedBoost = true;

                _lastMoveSpeed = _moveVariableSpeed;
                _moveVariableSpeed += _speedBonus;
                _moveVariableSpeed = _moveVariableSpeed > _maxMoveVariableSpeed ? _maxMoveVariableSpeed : _moveVariableSpeed;
            }
            else
            {
                _speedTime += time;
            }
        }

        public void Jump()
        {
            Jumped?.Invoke();
            _rigidbody.AddForce(0f, _jumpPower, 0f, ForceMode.Impulse);
            TaskCounter.IncereaseProgress(1, Convert.ToString(TaskType.Jump));
        }

        public void Somersault()
        {
            _rigidbody.AddForce(0, _jumpPower, _yJumpForce, ForceMode.Impulse);
        }

        private void ChangeSpeed()
        {
            if (MoveSpeed != _moveVariableSpeed && _isSpeedBoost == false)
                ChangingSpeed();

            if (MoveSpeed != _moveVariableSpeed && _isSpeedBoost)
            {
                _speedTime -= Time.deltaTime;
                BoostTimeChanging?.Invoke(_speedTime);

                if (_speedTime > 0)
                {
                    ChangingSpeed();
                }
                else
                {
                    _moveVariableSpeed = _lastMoveSpeed;
                    _isSpeedBoost = false;
                }
            }
        }

        private float DefineTurn()
        {
            if (_moveCoefficient == 0 && _inputHandler.TurnInput.x != 0)
                _turn = _inputHandler.TurnInput.x;
            else if (_moveCoefficient != 0 && _inputHandler.TurnInput.x == 0)
                _turn = _moveCoefficient;
            else
                _turn = 0;

            return _turn;
        }

        private void ChangingSpeed()
        {
            float turnMultiplier = 2f;

            MoveSpeed = MoveSpeed < _moveVariableSpeed ? MoveSpeed + _editMoveSpeed : MoveSpeed > _moveVariableSpeed ? MoveSpeed - _editMoveSpeed : _moveVariableSpeed;
            _turnSpeed = MoveSpeed / turnMultiplier;
            SpeedChanging?.Invoke(MoveSpeed);
        }
    }
}