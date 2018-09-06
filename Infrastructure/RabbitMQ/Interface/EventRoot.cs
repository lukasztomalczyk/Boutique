namespace RabbitMQ.Interface
{
    public abstract class EventRoot
    {
        public virtual string EventScope => "ToAll";
    }
}