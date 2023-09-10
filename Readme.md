## Run locally:
`dotnet run --project src/web/web.csproj`

## Run docker:
`docker build -t backend .`
(current build time without caching: 70s)

`docker run --rm -p 5001:80 -e TokenManager__SecretKey=myHashKey backend`


## Google cloud run (still docker)
tage with project, repo, reponame like here:
https://cloud.google.com/artifact-registry/docs/docker/pushing-and-pulling

Go to google cloud Artifact Registry and get tag name from there. Add actual tag name in the end.

Go to cloud run service, create (or update) service
Set secret TokenManager__SecretKey -> Expose as Environment Variable

result:
https://calculator-k42qgew2la-uc.a.run.app/api

frontend:
https://next-k42qgew2la-oa.a.run.app/



## Todo:

 - dotnet run --project src/calculator/calculator.csproj -> Output.json gets created in solution root instead of project root.
 - appsettings.json from calculator and web are a duplicate wehn doing `dotnet publish -c Release -o out`