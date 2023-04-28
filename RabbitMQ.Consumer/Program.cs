
//CONSUMER

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://odsbstrb:WqzIHX3r_dzHwhJuO6uDjMa2kasPZVnQ@cow.rmq2.cloudamqp.com/odsbstrb");

//bağlantı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//queue oluşturma
channel.QueueDeclare("example-rabbit", exclusive: false);

//queue'dan mesaj okuma
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume("example-rabbit", false, consumer);

consumer.Received += (sender, e) =>
{
    //kuyruğa gelen mesajın işlendiği yer
    //e.Body: kuyruktaki mesajın bütünsel verisi
    //e.Body.Span veya e.Body.ToArray(): kuyruktaki mesajın byte verisi

    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();

