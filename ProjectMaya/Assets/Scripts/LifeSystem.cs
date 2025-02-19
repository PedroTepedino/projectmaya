using System;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] private int _maxLife;
    public int MaxLife => _maxLife;
    public int _currentLife { get; private set; }

    private bool IsDead => _currentLife <= 0;

    public Action OnDie;
    public Action OnChangeLife;

    private void Awake()
    {
        _currentLife = _maxLife;
    }

    public LifeSystem(int maxLife)
    {
        _maxLife = maxLife;
        _currentLife = _maxLife;
    }

    public void Damage(int damage)
    {
        _currentLife -= damage;

        OnChangeLife?.Invoke();

        if (IsDead)
            Die();
    }

    private void Die()
    {
        OnDie?.Invoke();
        this.gameObject.SetActive(false);
    }

    public void Heal(int healAmount)
    {
        _currentLife += healAmount;

        if (_currentLife > _maxLife)
            _currentLife = _maxLife;

        OnChangeLife?.Invoke();
    }
}