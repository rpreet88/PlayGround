using System;
using Microsoft.AspNetCore.Mvc;

namespace DraftSimulator;

public class DraftStore
{

    private readonly ILogger<DraftStore> _logger;
    private Dictionary<Guid, Draft> _draftStore;

    public DraftStore(ILogger<DraftStore> logger)
    {
        _logger = logger;
        _draftStore = new Dictionary<Guid, Draft>();
    }

    public Draft Add(NewDraft newDraft)
    {
        Draft draft = new Draft(newDraft);
        _draftStore.Add(draft.DraftId, draft);
        return draft;
    }

    public Draft Get(Guid draftId)
    {
        Draft? draft;
        _draftStore.TryGetValue(draftId, out draft);

        if (draft is null)
        {
            throw new DraftNotFound();
        }

        return draft;
    }

    public void AddPlayer(Guid draftId, Guid teamId, Player player)
    {
        Draft? draft = Get(draftId);
        if (draft is null)
        {
            throw new DraftNotFound();
        }

        DraftTeam? team = draft?.DraftTeams?.FirstOrDefault(t => t.TeamId == teamId);
        if (team == null)
        {
            throw new TeamNotFound();
        }

        team.Players.Add(player);
    }
}
