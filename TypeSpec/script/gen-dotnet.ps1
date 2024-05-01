$parttern = [regex]'openapi.(\w+).yaml'

Get-ChildItem "./tsp-output/@typespec/openapi3" | ForEach-Object -Process {
  if ($_ -is [System.IO.FileInfo])
  {
    $_.name -match $parttern
    $serverName = $Matches[1]
    nswag openapi2cscontroller `
      /input:"./tsp-output/@typespec/openapi3/openapi.$serverName.yaml" `
      /classname:${serverName} `
      /namespace:CanaRails.Controllers.$serverName `
      /output:"../Applications/Manage/Controllers/${serverName}Controller.cs" `
      /ControllerBaseClass:"Microsoft.AspNetCore.Mvc.Controller"
  }
}