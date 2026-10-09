using Microsoft.AspNetCore.SignalR;
using SignalR.Services;

namespace SignalR.Hubs
{
    public class PizzaHub : Hub
    {
        private readonly PizzaManager _pizzaManager;


        public PizzaHub(PizzaManager pizzaManager) {
            _pizzaManager = pizzaManager;
        }

        public override async Task OnConnectedAsync()
        {
            _pizzaManager.AddUser();
            await Clients.All.SendAsync("UpdateNbUsers", _pizzaManager.NbConnectedUsers);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _pizzaManager.RemoveUser();
            await Clients.All.SendAsync("UpdateNbUsers", _pizzaManager.NbConnectedUsers);
            await base.OnConnectedAsync();
        }

        public async Task SelectChoice(PizzaChoice choice)
        {
            var groupname = _pizzaManager.GetGroupName(choice);
            var ppizza = _pizzaManager.GetPizzaPrice(choice);
            var npizza = _pizzaManager.GetNbPizzas(choice);
            var money = _pizzaManager.GetMoney(choice);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
            await Clients.Caller.SendAsync("UpdatePizzaPrice", ppizza);
            await Clients.Caller.SendAsync("UpdateNbPizzasAndMoney", npizza, money);

        }

        public async Task UnselectChoice(PizzaChoice choice)
        {
            var groupname = _pizzaManager.GetGroupName(choice);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupname);

        }

        public async Task AddMoney(PizzaChoice choice)
        {
            var groupname = _pizzaManager.GetGroupName(choice);
            _pizzaManager.IncreaseMoney(choice);
            var money = _pizzaManager.GetMoney(choice);
            await Clients.Group(groupname).SendAsync("UpdateMoney", money);
        }

        public async Task BuyPizza(PizzaChoice choice)
        {
            var groupname = _pizzaManager.GetGroupName(choice);
            _pizzaManager.BuyPizza(choice);
            var npizza = _pizzaManager.GetNbPizzas(choice);
            var money = _pizzaManager.GetMoney(choice);
            await Clients.Group(groupname).SendAsync("UpdateNbPizzasAndMoney", npizza, money);
        }
    }
}
