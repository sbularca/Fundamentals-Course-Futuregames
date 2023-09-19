using UnityEngine;

// We create a base class that inherits from enumeration
public class BaseAnimalType : Enumeration {

    public static BaseAnimalType None => new BaseAnimalType(0, "None");
    public static BaseAnimalType Skeletal => new BaseAnimalType(1, "Skeletal");
    public static BaseAnimalType NonSkeletal => new BaseAnimalType(2, "NonSkeletal");

    public BaseAnimalType(int id, string name) : base(id, name) { }
}

// we create a child class that inherits from base enumeration to demonstrate how we can extend the types
public class AnimalType : BaseAnimalType{
    public static AnimalType Alien => new AnimalType(3, "Alien");

    public AnimalType(int id, string name) : base(id, name) { }
}

public class UsingEnumeration : MonoBehaviour {

    // we need to case the base class type as the child class types in order to use them
    // this will not serialize in the unity inspector
    public AnimalType animalType = (AnimalType)BaseAnimalType.NonSkeletal;
    public AnimalType animalType2 = AnimalType.Alien;

    private void Start() {
        // to use it with switch, we can always use the Id property defined in the Enumeration base class
        switch(animalType.Id) {
            case 0:
                //do something
            break;
            case 2:
                //do something
            break;
        }
    }
}
