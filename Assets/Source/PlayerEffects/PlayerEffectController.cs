using System.Collections.Generic;
using Player;
using UnityEngine;

namespace PlayerEffects
{
    public class PlayerEffectController : MonoBehaviour
    {
        private readonly int _minCountBoosts = 3;

        [SerializeField] private List<ParticleSystem> _effects;

        private PlayerView _view;
        private PlayerMoverView _viewMover;
        private PlayerEffectType _effectType;
        private Dictionary<PlayerEffectType, ParticleSystem> _effectsDictionary = new Dictionary<PlayerEffectType, ParticleSystem>();

        private void OnDisable()
        {
            _viewMover.Protected -= ProtectBoostEffect;
            _view.MoneyChanging -= CoinBoostEffect;
        }

        public void Init(PlayerMoverView playerMoverView, PlayerView playerView)
        {
            _view = playerView;
            _viewMover = playerMoverView;
            _viewMover.Protected += ProtectBoostEffect;
            _view.MoneyChanging += CoinBoostEffect;

            for (int i = 0; i < _effects.Count; i++)
            {
                if (_effectsDictionary.ContainsKey((PlayerEffectType)i) == false)
                    _effectsDictionary.Add((PlayerEffectType)i, _effects[i]);
            }
        }

        private void PlayEffect()
        {
            if (_effectsDictionary.TryGetValue(_effectType, out var effect))
            {
                effect.Play();
            }
        }

        private void StopEffect()
        {
            if (_effectsDictionary.TryGetValue(_effectType, out var effect))
            {
                effect.Stop();
            }
        }

        private void ProtectBoostEffect(bool isProtect)
        {
            _effectType = PlayerEffectType.ProtectBoost;

            if (isProtect)
            {
                PlayEffect();
            }
            else if (!isProtect)
            {
                StopEffect();
            }
        }

        private void EnergyBoostEffect(float count)
        {
            if (IsBoost(count))
            {
                _effectType = PlayerEffectType.EnergyBoost;
                PlayEffect();
            }
            else
            {
                EnergyDeboostEffect();
            }
        }

        private void EnergyDeboostEffect()
        {
            _effectType = PlayerEffectType.EnergyDeboost;
            PlayEffect();
        }

        private void CoinBoostEffect(float count, bool isBoost)
        {
            if (IsBoost(count) && isBoost)
            {
                _effectType = PlayerEffectType.CoinBoost;
                PlayEffect();
            }
            else if (isBoost)
            {
                CoinDeboostEffect();
            }
        }

        private void CoinDeboostEffect()
        {
            _effectType = PlayerEffectType.CoinDeboost;
            PlayEffect();
        }

        private bool IsBoost(float count)
        {
            return count > _minCountBoosts;
        }
    }
}