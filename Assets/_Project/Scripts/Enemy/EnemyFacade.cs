using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFacade : MonoBehaviour
{
    EnemyModel _model;
        
    [Inject]
    public void Construct(EnemyModel enemyModel)
    {
        _model = enemyModel;
    }

    public bool IsTargeted
    {
        get => _model.IsTargeted;
        set => _model.IsTargeted = value;
    }

    public Vector3 Position
    {
        get => _model.Position;
        set =>_model.Position=value;
    }

    public Quaternion Rotation => _model.Rotation;


    public Animator Animator => _model.Animator;


    public Rigidbody Rigidbody => _model.RigidBody;

}
