using Cinemachine;
using UnityEngine;


/*
 * Moves camera further away from player as player is moving
 * Once player stands still. camera zoom back in.
 *
 * JH
 */
public class MovingZoom : MonoBehaviour
{
    [SerializeField] private float defaultZoom = 10f;
    [SerializeField] private float maxZoom = 14f;
    [SerializeField] private float zoomSpeed = 0.5f;
    private CinemachineFramingTransposer _transposer;
    private PlayerController _controller;

    private float targetZoom;
    private float currentzoom;

    private void Awake()
    {
        currentzoom = defaultZoom;
        _controller = FindObjectOfType<PlayerController>();
        CinemachineComponentBase componentBase = FindObjectOfType<CinemachineVirtualCamera>()
            .GetCinemachineComponent(CinemachineCore.Stage.Body);
        _transposer = (CinemachineFramingTransposer) componentBase;
    }

    private void LateUpdate()
    {
        if (_controller == null)
            return;
        targetZoom = _controller.GetVelocity().x != 0.0f ? maxZoom : defaultZoom;
        currentzoom = Mathf.Lerp(currentzoom, targetZoom, zoomSpeed * Time.deltaTime);
        _transposer.m_CameraDistance = currentzoom;
    }
}