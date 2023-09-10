using System;

[Serializable]
public class PlayerData {
    // public members
    public string name;
    public int level;
    public float health;

    //constructor
    public PlayerData(string name) {
        this.name = name;
    }
}
