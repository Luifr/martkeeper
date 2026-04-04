using System;

namespace Martkeeper.Interfaces;

public interface IState<T, U>
{
  public T State { get; }

  public void Init(Action<T> ChangeState, U data) { }

  public void Enter();
  public void Update(double delta);
  public void Exit();
}
