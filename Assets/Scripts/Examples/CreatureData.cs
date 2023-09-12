using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

// base class
[Serializable]
public class CreatureData {
    // public members
    public string name; // don't do this
    public int level;
    public float health;

    public CreatureData(string name, int level, float health) {
        this.name = name;
        this.level = level;
        this.health = health;
    }
    public CreatureData() { }

    public float GetHealth() {
        return health;
    }
}

//child class
public class HumanData : CreatureData {
    public int iq;
    public HumanData(string name, int level, float health) : base(name, level, health) { }
}


public class Skills {
    public int strength;
    public int stamina;
    public int inteligence;
    public int wisdom;
}

public class Menu {
    public static Action openMenu;
    public int numberOfButtons;
    private bool isInitialized;

    public Menu() {
        Debug.Log("initialized");
    }

    public static Menu ShowMenuButton(int index) {
        Menu subMenu = new();

        subMenu.numberOfButtons = 5;
        return subMenu;
    }

    public void WorkWithSubClasses() {
        Submenu subMenuObject = new();
    }

    public class Submenu {
        public GameObject subMenuPrefab;

        public void InstantiateSubmenu() {
            UnityEngine.Object.Instantiate(subMenuPrefab);
        }
    }
}
