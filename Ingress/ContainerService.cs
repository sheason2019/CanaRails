using CanaRails.Adapters.IAdapter;
using CanaRails.Database;
using CanaRails.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Ingress;

public class ContainerService(
  CanaRailsContext context,
  IAdapter adapter
)
{
  public async Task<string[]> GetForwardUrls(int entryId)
  {
    var query = from containers in context.Containers
                join entries in context.Entries
                on containers.Entry.ID equals entries.ID
                where containers.Entry.ID.Equals(entryId)
                where containers.Version.Equals(entries.Version)
                select containers;
    var containerDictionary = query.ToDictionary(e => e.ContainerID);
    var infos = await adapter.Container.GetInfos([.. query]);
    return infos.
      Select(e => $"{e.IPAddress}:{containerDictionary[e.ContainerId].Port}").
      ToArray();
  }

  public string RandomBlancing(string[] candidates)
  {
    if (candidates.Length == 0)
    {
      throw new Exception("Candidates is empty");
    }

    var index = new Random().Next(candidates.Length - 1);
    return candidates[index];
  }
}