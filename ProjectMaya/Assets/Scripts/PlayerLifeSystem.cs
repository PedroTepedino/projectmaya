using System;

public class PlayerLifeSystem
{
    private readonly int _maxLife;
    private int _currentLife;

    private bool IsDead => _currentLife <= 0;

    public Action OnDie;
    public Action OnChangeLife;

    public PlayerLifeSystem(int maxLife)
    {
        _maxLife = maxLife;
    }

    private void OnEnable()
    {
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
    }

    public void Heal(int healAmount)
    {
        _currentLife += healAmount;

        if (_currentLife > _maxLife)
            _currentLife = _maxLife;
        
        OnChangeLife?.Invoke();
    }
}