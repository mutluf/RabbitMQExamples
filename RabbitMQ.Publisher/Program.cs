
//PUBLISHER

using RabbitMQ.Client;
using System.Text;

//bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new("key");

//bağlantı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel= connection.CreateModel();

//queue oluşturma
//rabbitMQ kuyruğa atacağı mesajları byte türünden kabul eder.
channel.QueueDeclare(queue:"example-rabbit", exclusive:false);

byte[] message = Encoding.UTF8.GetBytes("RabbitMQ");
channel.BasicPublish(exchange:"", routingKey:"example-rabbit", body:message);

Console.Read();



//durability
//channel.QueueDeclare(queue: "example-rabbit", exclusive: false, durable:true);
//IBasicProperties properties= channel.CreateBasicProperties();
//properties.Persistent= true;

//channel.BasicPublish(exchange: "", routingKey: "example-rabbit", body: message,basicProperties:properties);


