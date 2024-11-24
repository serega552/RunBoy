using Audio;
using UnityEngine;

namespace Windows
{
    [RequireComponent(typeof(CanvasGroup))]

    public class Window : MonoBehaviour
    {
        [SerializeField] protected SoundSwitcher SoundSwitcher;
        [SerializeField] private CanvasGroup _canvasGroup;

        private ParticleSystem _effectButtonClick;

        private void Start()
        {
            _effectButtonClick = GetComponentInChildren<ParticleSystem>();
        }

        public virtual void Open()
        {
            SoundSwitcher.Play("ClickOpen");
            _effectButtonClick?.Play();
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
            _canvasGroup.alpha = 1f;
        }

        public virtual void Close()
        {
            SoundSwitcher.Play("ClickClose");
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0f;
        }

        public virtual void OpenWithoutSound()
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
            _canvasGroup.alpha = 1f;
        }

        public virtual void CloseWithoutSound()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0f;
        }
    }
}