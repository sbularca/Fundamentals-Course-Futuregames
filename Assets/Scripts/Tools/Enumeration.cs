using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public abstract class Enumeration : IComparable {
    public string Name { get; private set; }
    public int Id { get; private set; }

    protected Enumeration(int id, string name) {
        (Id, Name) = (id, name);
    }

    protected bool Equals(Enumeration other) {
        return Name == other.Name && Id == other.Id;
    }

    public override string ToString() {
        return Name;
    }

    public static IEnumerable<T> GetAll<T>() where T : Enumeration {
        return typeof(T).GetFields(BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly)
            .Select(f => f.GetValue(null))
            .Cast<T>();
    }

    public override bool Equals(object obj) {
        Enumeration otherValue = obj as Enumeration;
        if(otherValue == null) {
            return false;
        }

        bool typeMatches = GetType().Equals(obj.GetType());
        bool valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode() {
        return (Id + Name).GetHashCode();
    }

    public int CompareTo(object other) {
        return Id.CompareTo(((Enumeration)other).Id);
    }
}
