using System;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerView : MonoBehaviour
{
    [SerializeField] private Toggle _aimToogle;
    [SerializeField] private Button _jumpButton;
    [SerializeField] private Button _sprintButton;
    [SerializeField] private Button _shootButton;
    [SerializeField] private UICanvasControllerInput _canvasController;

    public Button ShootButton => _shootButton;

    private void Update()
    {
#if !UNITY_EDITOR
        _jumpButton.gameObject.SetActive(!_aimToogle.isOn);
        _sprintButton.gameObject.SetActive(!_aimToogle.isOn);
        _canvasController.VirtualAimInput(_aimToogle.isOn);
        _shootButton.gameObject.SetActive(_aimToogle.isOn);
#endif
       
    }
}
