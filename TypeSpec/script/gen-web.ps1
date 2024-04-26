$parttern = [regex]'openapi.(\w+).yaml'

Get-ChildItem "./tsp-output/@typespec/openapi3" | ForEach-Object -Process {
  if ($_ -is [System.IO.FileInfo])
  {
    $_.name -match $parttern
    $serverName = $Matches[1]
    nswag openapi2tsclient `
      /input:"./tsp-output/@typespec/openapi3/openapi.$serverName.yaml" `
      /output:"../Applications/Web/api/${serverName}.client.ts" `
      /ClassName:"${serverName}Client"
  }
}