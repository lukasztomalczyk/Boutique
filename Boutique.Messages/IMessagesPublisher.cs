namespace Boutique.Messages
{
    public interface IMessagesPublisher
    {
        MessagesPublisher ConnectionFactory(string connectionString);
        MessagesPublisher OpenConnection();
        MessagesPublisher CreateModel(string exchangeName);
        MessagesPublisher SetSubscriber(string sub);
        void SendMessage(string message);
    }
}