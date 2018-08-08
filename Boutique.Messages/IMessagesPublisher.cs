namespace Boutique.Messages
{
    public interface IMessagesPublisher<TOut> where TOut : class
    {
        TOut ConnectionFactory(string connectionString);
        TOut OpenConnection();
        TOut CreateModel(string exchangeName);
        TOut SetSubscriber(string sub);
        void SendMessage(string message);
    }
}