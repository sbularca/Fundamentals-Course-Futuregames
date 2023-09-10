using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CollectionsAndStatements : MonoBehaviour {

    public string [] namesArray; // fast member access, slow to add and remove, fixed size
    public PlayerData [] playersDataArray;

    public List<string> namesList = new List<string>(); // fast to add and remove, slower member access, memory overhead

    public Dictionary<int, PlayerData> players = new Dictionary<int, PlayerData>(); // fast to add and remove, fast member access, memory overhead

    // Used occasionally but not often do to their memory overhead
    public Queue<string> namesQueue = new Queue<string>(); // first in first out (FIFO)
    public Stack<string> namesStack = new Stack<string>(); // last in first out (LIFO)


    public HashSet<string> namesSet = new HashSet<string>(); // very fast to add and remove, no duplicates, no member access

    public LinkedList<string> namesLinkedList = new LinkedList<string>(); // rarely used, fast to add and remove, slower member access, memory overhead

    private void Start() {
        var co = new CollectionsAndStatements();
    }
}
