using System;
using UnityEngine;

public enum AnimalKind {
    None = 0,
    Skeletal = 1,
    NonSkeletal = 2,
    Alien = 4
}

public enum Mamal {
    Shark,
    Elephant,
    Fish
}

public class HandleEnums : MonoBehaviour {

    [SerializeField] private AnimalKind animalType;

    private void Start() {
        switch(animalType) {
            case AnimalKind.Skeletal:
                Debug.Log("You have selected a type that has bones");
                int enumIndex = (int)animalType;
                Debug.Log(enumIndex);
                break;
            case AnimalKind.NonSkeletal:
                Debug.Log("You have selected a squishy type");
                break;
        }


    }

#if UNITY_EDITOR
    private void OnValidate() {
    }
#endif
}
