using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public int slots = 20;
    public bool enabled;
    public List<Item> items = new List<Item>();
}
