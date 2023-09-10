### Run locally:
`dotnet run --project src/web/web.csproj`
`dotnet watch --project src/web/web.csproj`

publish:
`dotnet publish -c Release -o out`

### Run docker locally:
`docker build -t backend .`
(current build time without caching: 70s)

`docker run --rm -p 5001:80 -e TokenManager__SecretKey=myHashKey backend`


### Google cloud run (docker)
tage with project, repo, reponame like here:
https://cloud.google.com/artifact-registry/docs/docker/pushing-and-pulling

Go to google cloud Artifact Registry and get tag name from there. Add actual tag name in the end.

Go to cloud run service, create (or update) service
Set secret TokenManager__SecretKey -> Expose as Environment Variable

result:
https://calculator-k42qgew2la-uc.a.run.app/api

frontend:
https://next-k42qgew2la-oa.a.run.app/


### Secrets:
`dotnet user-secrets init`
`dotnet user-secrets set "key" "value"`

In production these secrets need to be added via environment variables. Nesting ist done by using TWO! underscores __ (instead of :) there, as ":" is apparently not supported on all platforms.

(Locally - at least on windows - we would write `${env:super__sub}="mysecret"`)

### Todo:

 - dotnet run --project src/calculator/calculator.csproj -> Output.json gets created in solution root instead of project root.
 - delete calculator/Program.cs, use it's content elsewhere
 - possibly move calculator/Exceptions to a domain project (which will be a class library)
 - actually embed the logic from calculator into web, think about sensible interfaces first

 # /web
web is a dotnet web-api to serve endpoints consumed by the next frontend (that is being developped independantly)

We have
 - */api GET* which gives back a json file containing the most up-to-date connections
 - */api POST* which loads up a json from which the latest connections will be computed
 - */api/login POST* which sets a cookie containing a valid bearer token (if the password is correct)