using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;
using DG.Tweening;
using Object = UnityEngine.Object;

public class EnemySkullHandler:IInitializable
{
    private EnemyModel _model;
    private TickableManager _tickableManager;
    private skullSetState _skullSetState;
    private dieCheck _dieCheck;
    private Settings _settings;
    private Tween alltween;
    private Tween alltween1;
    private EnemyObservable.Settings _enemyObservable;

    private Vector3 _defaultSkullTransform;

    EnemySkullHandler(EnemyModel enemyModel,TickableManager tickableManager,Settings settings,EnemyObservable.Settings observable)
    {
        _model = enemyModel;
        _tickableManager = tickableManager;
        _settings = settings;
        _enemyObservable = observable;
        _defaultSkullTransform = settings.SkullGO.transform.localPosition;

    }

    public void Initialize()
    {
        _tickableManager.TickStream.Select(x => _model.IsTargeted)
            .Where(x => x.Equals(true)& _skullSetState==skullSetState.NotSetted)
            .Subscribe(x =>
            {
                var skullTransform=_settings.SkullGO.transform;
                _skullSetState = skullSetState.Setted;
                _settings.SkullGO.SetActive(true);
              alltween=(skullTransform
                    .DOLocalRotate(new Vector3(0, 360, 0), 3, 
                        RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1));
              alltween1= (skullTransform
                  .DOMoveY(skullTransform.transform.position.y+2,1).SetLoops(-1,LoopType.Yoyo))
                  .OnKill(delegate
                  {
                      _settings.SkullGO.transform.localPosition = _defaultSkullTransform;
                      _settings.SkullGO.transform.rotation = new Quaternion(0, 0, 0, 0);
                  });
            });
        _tickableManager.TickStream.Select(x => _model.IsTargeted).Where(x => x.Equals(false)&_skullSetState==skullSetState.Setted)
            .Subscribe(x =>
                {
                    alltween.Kill();
                    alltween1.Kill();
                    _skullSetState = skullSetState.NotSetted;
                    _settings.SkullGO.SetActive(false);
                }
            );
        _tickableManager.TickStream
            .Select(x=>_model.IsDead)
            .Where(x=> x.Equals(true)& _dieCheck==dieCheck.alive)
            .Subscribe(x=>
            {
                _dieCheck = dieCheck.dead;
                var go = Object.Instantiate(_settings.skullPrefab,
                    Camera.main.WorldToScreenPoint(_settings.SkullGO.transform.position),
                    _settings.skullImagePanel.transform.rotation, _settings.skullImagePanel.transform);
                go.transform.DOMove(_settings.skullImagePanel.position,0.5f)
                    .SetEase(Ease.InCirc);
                _settings.SkullGO.SetActive(false);
            });

        _tickableManager.TickStream
            .Select(x => _model.RigidBody.gameObject.tag)
            .Where(x => x == "Dead")
            .Subscribe(x =>_settings.SkullGO.SetActive(false));
    }
    
    private enum skullSetState
    {
        NotSetted,
        Setted,
    }

    private enum dieCheck
    {
        alive,
        dead,
    }

    [Serializable]
    public class Settings
    {
        public GameObject SkullGO;
        public GameObject skullPrefab;
        public RectTransform skullImagePanel;
    }
}
