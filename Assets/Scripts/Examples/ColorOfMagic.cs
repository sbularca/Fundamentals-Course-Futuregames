using UnityEngine;
using Color = System.Drawing.Color;

// you cannot instantiate/create an object from this class. You need inherit first
public abstract class ColorOfMagic {

    private Color defaultColor = Color.Aquamarine;

    /// <summary>
    /// This is a prototype method
    /// </summary>
    /// <returns></returns>
    public abstract Color GetMyMagicColor();

    /// <summary>
    /// This is an overridable method
    /// </summary>
    /// <returns></returns>
    public virtual Color GetDefaultColor() {
        return defaultColor;
    }
}

/// <summary>
/// Add your explanation about the class here
/// </summary>
public class PreferBlack : ColorOfMagic {

    private Color myColorOfMagic = Color.Black;

    /// <summary>
    ///
    /// </summary>
    public Color MyColorOfMagic {
        get {
            return myColorOfMagic;
        }
        set {
            myColorOfMagic = value;
        }
    }

    /// <summary>
    /// This class is doing something that is allowed
    /// </summary>
    /// <param name="pramater1">This paramter is meant for something great</param>
    /// <param name="parameter2">This paramter is not important at all and has a default value</param>
    public void DoSomethingThatItIsAllowedFromOutsideTheClass(int pramater1, string parameter2) {

    }

    public override Color GetMyMagicColor() {
        return myColorOfMagic;
    }

    public override Color GetDefaultColor() {
        base.GetDefaultColor();
        var color = Color.Aqua;
        return color;
    }
}

public class PreferRed : ColorOfMagic {

    private Color myColorOfMagic = Color.Red;

    public override Color GetMyMagicColor() {
        return myColorOfMagic;
    }

    public override Color GetDefaultColor() {
        var color = myColorOfMagic;
        return color;
    }
}
