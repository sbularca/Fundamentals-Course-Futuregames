using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable {
    public string ID { get; set;}
    public void Save();
    public void Load();

    public void Register();
}
