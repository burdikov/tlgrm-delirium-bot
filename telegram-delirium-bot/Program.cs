using System.Collections.Generic;
using Telegram.Bot;

namespace telegram_delirium_bot
{
    class Program
    {
        private const string token = "289179199:AAFbRCxuvIoX_Cg0TpowgDLc5dlnLYuxaPA";

        private static TelegramBotClient bot;

        private static Dictionary<long, GameRoom> rooms = new Dictionary<long, GameRoom>();

        static void Main(string[] args)
        {
            bot = new TelegramBotClient(token);
            bot.OnMessage += Bot_OnMessage;
            bot.StartReceiving();

            while (true) { }
        }

        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs args)
        {
            var id = args.Message.Chat.Id;
            if (!rooms.ContainsKey(id))
            {
                GameRoom room = new GameRoom(id, bot);
                rooms.Add(id, room);
            }
            rooms[id].HandleMessage(args);
        }
    }
}
