using Audio;
using System;
using Tasks;
using Tasks.SO;
using TMPro;
using UnityEngine;
using YG;

namespace BoostSystem
{
    public abstract class Boost : MonoBehaviour
    {
        private readonly int _maxCountUpgrade = 5;
        private readonly int _timeIncreaseNumber = 5;

        [SerializeField] private float _bonus;
        [SerializeField] private TMP_Text _timeText;

        private float _time = 5;

        public event Action OnUpdateCount;

        public float Bonus => _bonus;
        public float Time => _time;
        public int CountUpgrade { get; protected set; }
        public int Count { get; protected set; }

        private void Awake()
        {
            if (YandexGame.SDKEnabled)
                Load();
        }

        private void OnEnable()
        {
            YandexGame.GetDataEvent += Load;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= Load;
        }

        public abstract void Save();

        public abstract void Load();

        public bool TryUse()
        {
            bool _canUse = Count > 0;

            if (_canUse)
            {
                Decrease();
                SoundSwitcher.Instance.Play("UseBoost");
                TaskCounter.IncereaseProgress(1, TaskType.UseBoost.ToString());
            }

            return _canUse;
        }

        public void Increase()
        {
            Count++;
            UpdateText();
        }

        public void Upgrade()
        {
            if (CountUpgrade < _maxCountUpgrade)
            {
                CountUpgrade++;
                _time += _timeIncreaseNumber;
                SoundSwitcher.Instance.Play("UpgradeBoost");

                Save();
            }
        }

        public void UpdateText()
        {
            OnUpdateCount?.Invoke();
            Save();
        }

        public void SetTimeText(float amount)
        {
            if (amount > 0)
                _timeText.text = Math.Round(amount, 1).ToString();
            else
                _timeText.text = "-";
        }

        public void LoadTimer()
        {
            _time = CountUpgrade * _timeIncreaseNumber;
        }

        private void Decrease()
        {
            Count--;
            UpdateText();
        }
    }
}