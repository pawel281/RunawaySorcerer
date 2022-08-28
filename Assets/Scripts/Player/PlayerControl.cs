using System;
using UnityEngine;
using Zenject;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Movement _playerMovement;
    [SerializeField] private PlayerCombat _playerCombat;
    private Camera _camera;
    private ViewController _viewController;

    [Inject]
    private void Constructor(Camera camera, ViewController viewController)
    {
        _camera = camera;
        _viewController = viewController;
    }

    private void OnValidate()
    {
        if (_playerMovement == null)
            throw new InvalidOperationException();
        if (_playerCombat == null)
            throw new InvalidOperationException();
    }

    private void Update()
    {
        var moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        _playerMovement.ChangeDirection(moveDirection);
        var handDirection = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5)) - transform.position;
        handDirection.y = 0;
        _playerMovement.Turn(handDirection);

        if (Input.GetMouseButtonDown(1))
        {
            _viewController.ShowViewUp(_viewController.GetView<CreateSpellUISelector>());
        }

        if (Input.GetMouseButtonUp(1))
        {
            _viewController.GetView<CreateSpellUISelector>().Hide();
        }

        if (Input.GetMouseButtonDown(0))
        {
            _playerCombat.Cast();
        }
    }

    private void FixedUpdate()
    {
        _playerMovement.Move();
    }
}