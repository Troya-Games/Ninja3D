using System;
using System.Collections.Generic;
using System.Linq;
using PlayerBehaviors;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(LineRenderer))]

public class LineDrawer: ITickable
{
    private Player _player;
    private LineRenderer _lineRenderer;
    private List<Vector3> _points=new List<Vector3>();
    readonly LayerMask layer_mask= 1 << 8;

    private Vector3 lastPoint;
    
    LineDrawer(Player player)
    {
        _player = player;
        _lineRenderer = _player.LineRenderer;
        _points.Add(_player.Position);

    }


    public void Tick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit,255,layer_mask) )
            {
                if (hit.collider.GetComponent<EnemyFacade>().IsTargeted) return;

                hit.collider.GetComponent<EnemyFacade>().IsTargeted = true;
                _points.Add(hit.point);
                _lineRenderer.positionCount = _points.Count;


            }
        }
        DisplayLineDestination();
    }

    private void DisplayLineDestination()
    {

        for (int j = 0; j < _points.Count; j++)
        {
            _lineRenderer.SetPosition(j,_points[j]);
        }
        
    }
    }

