namespace Module.Core.Systems.Pool
{
    public interface IPoolable
    {
        //Release�� ����� Item�� Ǯ�� ��ȯ�Ǹ� ȣ��ȴ�.
        void OnReturnedToPool();
        //Get�� ����� Ǯ���� Item�� �����ö� ȣ��ȴ�.
        void OnTakeFromPool();
    }
}