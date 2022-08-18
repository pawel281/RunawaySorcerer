using System;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    [SerializeField] private View _currentView;
    [SerializeField] private View _currentViewUp;
    [SerializeField] private View[] _views;
    private View _previousView;

    public T GetView<T>() where T : View
    {
        for (int i = 0; i < _views.Length; i++)
        {
            if (_views[i] is T)
            {
                return (T) _views[i];
            }
        }

        return null;
    }

    public void ShowView<T>(bool remember = false) where T : View
    {
        for (int i = 0; i < _views.Length; i++)
        {
            if (_views[i] is T)
            {
                if (_currentView)
                {
                    if (remember)
                    {
                        _previousView = _currentView;
                    }

                    _currentView.Hide();
                }

                _views[i].Show();
                _currentView = _views[i];
            }
        }
    }

    public void ShowViewUp(View _view)
    {
        if (_currentViewUp)
        {
            _currentViewUp.Hide();
        }
        _view.Show();
        _currentViewUp = _view;
    }
    public void ShowPreviousView(View _view)
    {
     _previousView?.Show();
    }

    private void Start()
    {
        foreach (var i in _views)
        {
            i.Initialize();
        }
        _currentView?.Show();
    }
}