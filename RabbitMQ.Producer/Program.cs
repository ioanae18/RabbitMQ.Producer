using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ.Producer
{
	static class Program
	{
		static void Main(string[] args)
		{
			//se creeaza ConnectionFactory
			//var factory = new ConnectionFactory
			//{
			//	Uri = new Uri("amqp://guest:guest@localhost:5672")
			//};

			//cream conexiunea default (fara parametri)
			//using var connection = factory.CreateConnection();
			//cream canalul
			//using var channel = connection.CreateModel();
			//dupa crearea canalului, trebuie sa cream o coada ce are un nume si cateva proprietati
			//durable = true -> sa astepte pana cand consumatorul primeste
			//channel.QueueDeclare("demo-queue",
			//	durable: true,
			//	exclusive: false,
			//	autoDelete: false,
			//	arguments: null);
			//var message = new { Name = "Producer", Message = "Hello!" };
			//var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

			//channel.BasicPublish("", "demo-queue", null, body);

			//Console.ReadLine();

			var factory = new ConnectionFactory() { HostName = "localhost" };
			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: "demo",
									 durable: false,
									 exclusive: false,
									 autoDelete: false,
									 arguments: null);

				string message = "Hello World!";
				var body = Encoding.UTF8.GetBytes(message);

				channel.BasicPublish(exchange: "",
									 routingKey: "demo",
									 basicProperties: null,
									 body: body);
				Console.WriteLine(" [x] Sent {0}", message);
			}

			Console.WriteLine(" Press [enter] to exit.");
			Console.ReadLine();

		}
	}
}
