using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayerBehaviors;
using PlayerState;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class SliceController : MonoBehaviour
{
	
	
	public List<GameObject> _Objects = new List<GameObject>();
	public Image _healthImage;
	public ParticleSystem _deathParticle;
	public PlayerFacade _playerFacade;
	public GameObject _FinalGameObject;
	private LayerMask LayerMask;

	Vector3 sliceStartPos, sliceEndPos;
	bool isSlicing = false;
	GameObject startUI, endUI, lineUI;
	private GameObject _hitParticle;
	RectTransform lineRectTransform;
	float lineWidth;
	private Camera _camera;
	private RaycastHit _startHitPoint;
	Vector3 mousePos;
	private SettingsInstalled _settingsEnum;
	enum  SettingsInstalled
	{
		NotInstalled,
		Installed
		
	}
	
	private void OnEnable()
	{
		
		MeshSlicer.totalHit = 0;
		MeshSlicer.totalGameObjects.Clear();
	}
	

	void Awake()
	{
		if (_settingsEnum == SettingsInstalled.NotInstalled)
		{
			_camera = Camera.main;
			MeshSlicer.uvCamera = Instantiate((GameObject) Resources.Load("Prefabs/UV_Camera"));
			GameObject canvas = GameObject.Find("Canvas");
			GameObject pointPrefab = (GameObject) Resources.Load("Prefabs/Slice_Point");
			GameObject linePrefab = (GameObject) Resources.Load("Prefabs/Slice_Line");
			GameObject _hitParticlePrefab = (GameObject) Resources.Load("Prefabs/Hit_Particle");
			startUI = Canvas.Instantiate(pointPrefab);
			endUI = Canvas.Instantiate(pointPrefab);
			lineUI = Canvas.Instantiate(linePrefab);
			_hitParticle = Instantiate(_hitParticlePrefab);
			startUI.transform.SetParent(canvas.transform);
			endUI.transform.SetParent(canvas.transform);
			lineUI.transform.SetParent(canvas.transform);
			startUI.SetActive(false);
			endUI.SetActive(false);
			lineUI.SetActive(false);
			lineRectTransform = lineUI.GetComponent<RectTransform>();
			lineWidth = lineRectTransform.rect.width;
			LayerMask = LayerMask.GetMask("Enemy");

		}
	
		this.UpdateAsObservable().Select(x => Input.touches)
			.Where(x =>
			{

				if (_playerFacade.StateManager.CurrentState==PlayerStateManager.PlayerStates.FinalState)
				{
					if (_settingsEnum == SettingsInstalled.NotInstalled)
					{

						_FinalGameObject?.transform.LookAt(_playerFacade.transform);
						_healthImage.enabled = true;
						_Objects = MeshSlicer.totalGameObjects;
						_settingsEnum = SettingsInstalled.Installed;
							
					}
					
					foreach (var touch in x) 
					{ 
						switch (touch.phase) 
						{
						case TouchPhase.Began:
							isSlicing = true;
							mousePos = new Vector3(touch.position.x, touch.position.y, 1.0f);
							sliceStartPos = _camera.ScreenToWorldPoint(mousePos);
							startUI.SetActive(true);
							startUI.transform.position = touch.position;

							Ray camRay1 = _camera.ScreenPointToRay(touch.position);
							if (Physics.Raycast(camRay1, out var hit1, 501, LayerMask))
							{
								_startHitPoint = hit1;
							}

							break;

						case TouchPhase.Moved:
							endUI.SetActive(true);
							lineUI.SetActive(true);
							endUI.transform.position = touch.position;
							Vector2 linePos = (endUI.transform.position + startUI.transform.position) / 2f;
							float lineHeight = (endUI.transform.position - startUI.transform.position).magnitude;
							Vector3 lineDir = (endUI.transform.position - startUI.transform.position).normalized;
							lineUI.transform.position = linePos;
							lineRectTransform.sizeDelta = new Vector2(lineWidth, lineHeight);
							lineUI.transform.rotation = Quaternion.FromToRotation(Vector3.up, lineDir);
							break;

						case TouchPhase.Ended:
							isSlicing = false;
							mousePos = new Vector3(touch.position.x, touch.position.y,
								1.0f); // "z" value defines distance from camera
							sliceEndPos = _camera.ScreenToWorldPoint(mousePos);
							if (sliceStartPos != sliceEndPos)
							{
								mousePos = new Vector3(touch.position.x, touch.position.y, 10.0f);
								Vector3 point3 = _camera.ScreenToWorldPoint(mousePos);
								MeshSlicer.CustomPlane plane =
									new MeshSlicer.CustomPlane(sliceStartPos, sliceEndPos, point3);
								Slice(plane);
							}

							startUI.SetActive(false);
							endUI.SetActive(false);
							lineUI.SetActive(false);

							//TODO DÜZELTİLİR

							Ray camRay = _camera.ScreenPointToRay(touch.position);
							if (Physics.Raycast(camRay, out var hit, 501, LayerMask))
							{
								var vv = GetCenter(new[] {_startHitPoint.point, hit.point});
								vv.z = hit.point.z;
								_hitParticle.gameObject.transform.position = vv;
								_hitParticle.GetComponent<ParticleSystem>().Play();
							}


							if (MeshSlicer.totalHit > 5)
							{
								this.enabled = false;
								_deathParticle.Play();
								_playerFacade.StateManager.ChangeState(PlayerStateManager.PlayerStates.FinishState); //TODO Burayı düzelt  
							}
							break;
					} 
					} 
				}
				return false;
			}).Subscribe(x=>Debug.Log("sa") );

	}




	Vector3 GetCenter(Vector3[] components)
	{
		if (components != null && components.Length > 0)
		{
			Vector3 min = components[0];
			Vector3 max = min;
			foreach (var comp in components)
			{
				min = Vector3.Min(min, comp);
				max = Vector3.Max(max, comp);
			}

			return min + ((max - min) / 2);
		}

		return Vector3.zero;
	}



	// Detect & slice "sliceable" GameObjects whose bounding box intersects slicing plane
	private void Slice(MeshSlicer.CustomPlane plane)
	{
		SliceableObject[] sliceableTargets = (SliceableObject[]) FindObjectsOfType(typeof(SliceableObject));
		bool isSliced = false;

		MeshSlicer.totalHit += 1; //TODO 
		_healthImage.fillAmount -= 0.25f;

		foreach (SliceableObject sliceableTarget in sliceableTargets)
		{
			GameObject target = sliceableTarget.gameObject;
			if (plane.HitTest(target))
			{
				if (target.GetComponent<SliceableObject>().isConvex)
				{
					MeshSlicer.SliceMesh(target, plane, true);
					isSliced = true;
				}
				else
				{
					MeshSlicer.SliceMesh(target, plane, false);

					isSliced = true;
				}
			}
		}

	}
}

