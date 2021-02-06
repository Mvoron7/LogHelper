/// <summary>
/// Абстракция вызовов в ядро на действия пользователя.
/// </summary>
namespace LogHelper.Abstraction
{
    public interface ICallBack
    {
        void StartReader(string name);
    }
}
