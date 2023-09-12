using System;
using UnityEngine;

public struct StructExample {
    public int index;
    public string value;
}

public class ClassExample {
    public int index;
    public string value;
}

public class ValueTypeVsReferenceTypeStructs : MonoBehaviour {

    private void Start() {
        CompareClasses();
        CompareStructs();
    }

    private void CompareClasses() {
        ClassExample class1 = new ClassExample();
        class1.index = 0;
        class1.value = "unknown";

        // since classes are reference types, when we add class1 to class2, we are pointing the class2 reference to the same object in memory, same instance
        // in this case we don't have two instances. So if we modify data in class1 it will be modified in class2 and viceversa
        ClassExample class2 = class1;

        class2.index = 2;
        class2.value = "modified";

        Debug.Log($"class1.index = {class1.index} : class1.value = {class1.value}");
        Debug.Log($"class2.index = {class2.index} : class2.value = {class2.value}");
    }

    private void CompareStructs() {
        StructExample struct1 = new StructExample();
        struct1.index = 0;
        struct1.value = "unknown";

        // since struct are value types, the object is copied since structs contain the actual data not references to objects
        // modifing the data in one object it will not modify it in the other one, since they are two different instances of the struct
        StructExample struct2 = struct1;
        struct2.index = 2;
        struct2.value = "modified";

        Debug.Log($"struct1.index = {struct1.index} : struct1.value = {struct1.value}");
        Debug.Log($"struct2.index = {struct2.index} : struct2.value = {struct2.value}");
    }
}
