using UnityEngine;
using Color = System.Drawing.Color;

// you cannot instantiate/create an object from this class. You need inherit first
public abstract class ColorOfMagic {

    private Color defaultColor = Color.Aquamarine;

    // this method needs to exist in each of the classes that inherit from ColorOfMagic. Is enforced by the modifier abstract
    public abstract Color GetMyMagicColor();

    public virtual Color GetDefaultColor() {
        return defaultColor;
    }
}

public class PreferBlack : ColorOfMagic {

    private Color myColorOfMagic = Color.Black;

    public override Color GetMyMagicColor() {
        return myColorOfMagic;
    }

    public override Color GetDefaultColor() {
        var color = base.GetDefaultColor();
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
