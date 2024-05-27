namespace CourseProj.Hubs;

using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class CommentHub : Hub
{
    public async Task SendComment(int itemId, string user, string message)
    {
        await Clients.Group(itemId.ToString()).SendAsync("ReceiveComment", user, message);
    }

    public async Task JoinGroup(string itemId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, itemId.ToString());
    }

    public async Task LeaveGroup(string itemId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, itemId.ToString());
    }
}
