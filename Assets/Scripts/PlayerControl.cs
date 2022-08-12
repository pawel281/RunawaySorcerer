using UnityEngine;
using Zenject;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Movement _playerMovement;
    [SerializeField] private Camera _camera;


    private void Constructor(Camera camera)
    {
        _camera = camera;
    }

    private void Update()
    {
        var moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        _playerMovement.ChangeDirection(moveDirection);
        var handDirection = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5)) - transform.position;
        handDirection.y = 0;
        _playerMovement.Turn(handDirection);
    }

    private void FixedUpdate()
    {
        _playerMovement.Move();
    }
}