
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SingleRQuestionnaire.Models;
using SingleRQuestionnaire.Services;
using System.Collections.Concurrent;

[Authorize]
public class QuestionnaireHub : Hub
{
    private readonly MongoDbService _mongoDbService;

    public QuestionnaireHub(MongoDbService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }

    public async Task JoinPollGroup(string pollId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, pollId);

        var poll = await _mongoDbService.GetAsync(pollId);
        if (poll != null)
        {
            await Clients.Caller.SendAsync("voteUpdate", poll.Options);
        }
    }
    public async Task Vote(string pollId, string secenek)
    {
        var userId = Context.UserIdentifier;
        if (userId == null) return;

        var basarili = await _mongoDbService.VoteAsync(pollId, secenek, userId);

        if (basarili)
        {
            var poll = await _mongoDbService.GetAsync(pollId);
            if (poll != null)
            {
                await Clients.Group(pollId).SendAsync("voteUpdate", poll.GetVoteCounts());
            }
        }
    }
}