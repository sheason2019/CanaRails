using System.Text.RegularExpressions;
using CanaRails.ContainerAdapter.Services;
using CanaRails.ContainerAdapter.Utils;
using k8s;

namespace CanaRails.ContainerAdapter.Test;

[TestClass]
public class ParseIdUtilsTest
{
  [TestMethod]
  public void TestParseHttpRouteName()
  {
    var id = 1;
    var name = $"canarails-http-route-by-app-{id}";
    var parseId = ParseIdUtils.ParseAppIdByHttpRouteName(name);

    if (parseId != id)
    {
      throw new Exception($"parseId {parseId} not equal to appId {id}");
    }
    else
    {
      return;
    }
  }

  [TestMethod]
  public void TestParseServiceName()
  {
    var id = 1;
    var name = $"canarails-service-by-entry-{id}";
    var parseId = ParseIdUtils.ParseEntryIdByServiceName(name);

    if (parseId != id)
    {
      throw new Exception($"parseId {parseId} not equal to appId {id}");
    }
    else
    {
      return;
    }
  }

  [TestMethod]
  public void TestParseDeployName()
  {
    var id = 1;
    var name = $"canarails-deployment-by-entry-{id}";
    var parseId = ParseIdUtils.ParseEntryIdByDeployName(name);

    if (parseId != id)
    {
      throw new Exception($"parseId {parseId} not equal to appId {id}");
    }
    else
    {
      return;
    }
  }
}
