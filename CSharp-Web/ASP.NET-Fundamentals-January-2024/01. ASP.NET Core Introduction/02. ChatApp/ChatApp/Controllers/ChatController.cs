namespace ChatApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models;

public class ChatController : Controller
{
	private static List<KeyValuePair<string, string>> sMessages = new();

	public IActionResult Show()
	{
		if (sMessages.Any() == false)
		{
			return this.View(new ChatViewModel());
		}

		var chatModel = new ChatViewModel
		{
			Messages = sMessages
				.Select(m => new MessageViewModel
				{
					Sender = m.Key,
					MessageText = m.Value
				})
				.ToList()
		};

		return this.View(chatModel);
	}

	[HttpPost]
	public IActionResult Send(ChatViewModel chat)
	{
		var newMessage = chat.CurrentMessage;

		sMessages.Add(new KeyValuePair<string, string>(newMessage.Sender, newMessage.MessageText));

		return this.RedirectToAction("Show");
	}
}