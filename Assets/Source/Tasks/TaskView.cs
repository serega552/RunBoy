using System;
using Tasks.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Windows;

namespace Tasks
{
    public class TaskView : MonoBehaviour
    {
        private readonly ParticleSystem _takeRewardParticle;

        [SerializeField] private Button _startExecution;
        [SerializeField] private Button _takeReward;
        [SerializeField] private Image _rewardIcon;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _amountRewardText;

        private float _amountProgress;
        private Task _task;
        private Slider _amountCompleted;
        private TaskWindow _window;

        public event Action<TaskView> OnComplete;

        public int Id { get; private set; }

        private void OnEnable()
        {
            _window = GetComponentInParent<TaskWindow>();
            _startExecution.onClick.AddListener(_window.Close);
            _takeReward.onClick.AddListener(TakeReward);
            TaskCounter.Exicuted += ExecuteTask;
        }

        private void OnDisable()
        {
            _startExecution.onClick.RemoveListener(_window.Close);
            _takeReward.onClick.RemoveListener(TakeReward);
            TaskCounter.Exicuted -= ExecuteTask;
        }

        public void Init()
        {
            _amountCompleted = GetComponentInChildren<Slider>();
            _amountCompleted.minValue = 0;
            _amountCompleted.maxValue = _task.AmountMaxCollect;
            _startExecution.gameObject.SetActive(true);

            Invoke(nameof(UpdateUI), 0.1f);
        }

        public void InitId(int id)
        {
            Id = id;
        }

        public void InitProgress(float amount)
        {
            _amountProgress = amount;
            UpdateUI();

            if (_amountCompleted.value >= _task.AmountMaxCollect)
                CompleteTask();
        }

        public void AddTask(Task task)
        {
            _task = task;
            _task.TurnOnTask();
        }

        private void CompleteTask()
        {
            _startExecution.gameObject.SetActive(false);
            _takeReward.gameObject.SetActive(true);
            _task.TurnOffTask();
        }

        private void ExecuteTask(float amount, string name)
        {
            if (_task.TaskType == name)
            {
                if (_task.Type != TaskType.RecordDistance)
                {
                    _amountProgress += amount;
                }
                else
                {
                    _amountProgress = amount;
                }

                UpdateUI();

                if (_amountCompleted.value >= _task.AmountMaxCollect)
                {
                    CompleteTask();
                }
            }
        }


        private void UpdateUI()
        {
            _amountRewardText.text = $"{_task.AmountReward}";
            _rewardIcon.sprite = _task.RewardIcon;
            _descriptionText.text = $"{Lean.Localization.LeanLocalization.GetTranslationText(_task.Description)}: {_task.AmountMaxCollect}";
            _amountCompleted.value = _amountProgress;
            _amountCompleted.value = _amountProgress;
        }

        private void TakeReward()
        {
            _task.RewardPlayer();
            _takeReward.interactable = false;
            _takeRewardParticle?.Play();
            Invoke(nameof(Destroy), 1f);
        }

        public void Destroy()
        {
            OnComplete?.Invoke(this);
            Destroy(gameObject);
        }
    }
}