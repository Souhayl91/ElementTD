using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caretaker : MonoBehaviour {

	private Memento[] _mementos = new Memento[10];

    public void AddMemento(Memento m)
    {
        for (int i = _mementos.Length - 1; i > 0; i--)
        {
            _mementos[i] = _mementos[i - 1];
        }
        _mementos[0] = m;
    }

    public Memento GetMemento(int saveNumber) {
        return _mementos[saveNumber];
    }

    public Memento GetLastMemento()
    {
        return _mementos[0];
    }
}
