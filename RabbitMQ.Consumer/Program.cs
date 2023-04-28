
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

//autoAck: false : mesajların hemen silinmesinin önüne geçip consumer'dan onay bekliyor.
channel.BasicConsume("example-rabbit", autoAck: false, consumer);

consumer.Received += (sender, e) =>
{
    //kuyruğa gelen mesajın işlendiği yer
    //e.Body: kuyruktaki mesajın bütünsel verisi
    //e.Body.Span veya e.Body.ToArray(): kuyruktaki mesajın byte verisi

    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

    // RabbitMQ'ya işlemin başarıyla gerçkleştiği bilgisini veriyoruz.
    channel.BasicAck(deliveryTag: e.DeliveryTag, false);

    //Burada BasicNack ile RabbitMQ'ya mesajı işleyemeyeceğimizin bilgisini veriyoruz. requeue: true olursa işleyemediğimiz mesaj işlenmek üzere yeniden kuyruğa girer, false ise kuyruğa eklemez ve siler.
    //channel.BasicNack(e.DeliveryTag, multiple: false, requeue: true);

    //consumerTag değerine karşılık gelen queue'daki tüm mesajlar reddedilerek işlenmez.
    //var consumerTag= channel.BasicConsume("example-rabbit", autoAck: false, consumer);
    //channel.BasicCancel(consumerTag);
};

Console.Read();

