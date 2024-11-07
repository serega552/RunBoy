using System.Collections;
using UnityEngine;

public class ProtectCollectibleItem : OtherItem
{
    private bool _isActivated = false;
    private float _duration = 10;
    private WaitForSeconds _protectTime = new WaitForSeconds(1f);
    private float _time;

    private void OnDisable()
    {
        ProtectDisable();
    }

    public override void Boost()
    {
        if (PlayerMoverView != null)
        {
            _time = _duration;
            _isActivated = true;
            Delay = _duration + 1f;
            PlayerMoverView.Protect(_isActivated);
            StartCoroutine(ProtectOnTime());
        }
    }

    private IEnumerator ProtectOnTime()
    {
        while (_isActivated)
        {
            _time--;

            if (_time <= 0)
            {
                _time = _duration;
                ProtectDisable();
            }

            yield return _protectTime;
        }
    }

    private void ProtectDisable()
    {
        if(PlayerMoverView != null && _isActivated)
        {
            _isActivated = false;
            PlayerMoverView.Protect(_isActivated);
        }
    }
}
