using UnityEngine;

public class Polymorphism : BaseClass {

    public Polymorphism() {
        DoSomething();
    }

    protected override void DoSomething() {
        base.DoSomething();
        Debug.Log("Derived class");
    }
}

public class BaseClass {
    protected BaseClass() {

    }
    protected virtual void DoSomething() {
        Debug.Log("Base class");
    }
}
