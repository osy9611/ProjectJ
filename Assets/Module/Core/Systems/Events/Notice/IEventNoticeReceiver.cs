namespace Module.Core.Systems.Events
{
    public interface IEventNoticeListener
    {
        bool ReceivableFromEvent();
    }
}