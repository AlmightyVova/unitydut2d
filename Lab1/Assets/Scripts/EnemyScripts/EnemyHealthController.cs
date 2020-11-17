using UnityEngine;

public delegate void EnemyDamaged(); 

namespace EnemyScripts
{
  public class EnemyHealthController : MonoBehaviour
  {
    public HealthSystem HealthSystem = new HealthSystem();
    private EnemyBehaviorController _ebc;

    public event EnemyDamaged OnEnemyDamaged;

    private void Start()
    {
      _ebc = gameObject.GetComponent<EnemyBehaviorController>();
    }

    private void Update()
    {
      if (HealthSystem.Health == 0)
      {
        OnEnemyDamaged?.Invoke();
      }
    }
  }
}