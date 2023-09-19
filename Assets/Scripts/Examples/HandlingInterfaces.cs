using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

internal interface IDamageable {
    public void Damage();
}

public class Human : IDamageable {

    private bool isDead;

    public void Damage() {
        isDead = true;
    }
}

public class Prop : IDamageable {
    private int health = 100;

    public void Damage() {
        health--;
    }
}

interface ICancelable {

    public void CancelEffect(ParticleSystem particleSystem);
}

public class EffectModifier : MonoBehaviour, ICancelable {
    public void CancelEffect(ParticleSystem particleSystem) {
        particleSystem.Stop();
    }
}

public class HandlingInterfaces : MonoBehaviour {

    private int health;

    private IDamageable [] damageableObjects;
    private GameObject explosionEffectPrefab;

    private void Start() {
        damageableObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDamageable>().ToArray();
    }

    private void OnExplosion() {
        var go = Instantiate(explosionEffectPrefab);
        var modfier = go.AddComponent<EffectModifier>();
        var effect = go.GetComponent<ParticleSystem>();
        effect.Play();
        modfier.CancelEffect(effect);
        foreach(var damageMe in damageableObjects) {
            damageMe.Damage();
        }
    }
}
