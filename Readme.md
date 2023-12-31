### Run locally:

`dotnet run --project src/web/web.csproj`

`dotnet watch --project src/web/web.csproj`


tests: `dotnet test`

publish:
`dotnet publish -c Release -o out`

### Run docker locally:

`docker build -t backend .`
(current build time without caching: 70s)

`docker run --rm -p 5001:80 -e TokenManager__SecretKey=myHashKey -e loginpassword=myLoginPassword backend`


### Google cloud run (docker)

tag with project, repo, reponame like here:
https://cloud.google.com/artifact-registry/docs/docker/pushing-and-pulling

Go to google cloud Artifact Registry and get tag name from there. Add actual tag name in the end.

Go to cloud run service, create (or update) service

Set secret TokenManager__SecretKey -> Expose as Environment Variable
Set secret loginpassword -> Expose as Environment Variable

result:
https://calculator-k42qgew2la-uc.a.run.app/api

frontend:
https://next-k42qgew2la-oa.a.run.app/


### Secrets:

inside the web directory

`dotnet user-secrets init`

`dotnet user-secrets set "key" "value"`


In production these secrets need to be added via environment variables. Nesting ist done by using TWO! underscores __ (instead of :) there, as ":" is apparently not supported on all platforms.

(Locally - at least on windows - we would write `${env:super__sub}="mysecret"`)

### Todo:

 - update endpoints in readme

### /web

web is a dotnet web-api to serve endpoints consumed by the next frontend (that is being developped independantly)

We have
 - **/api/getdata GET** which gives back a list of CityWithStringSteps in the response-body. This can be consumed by the frontend.
 - **/api/update GET** which gives back a json file containing the most up-to-date connections
 - **/api/update POST** which loads up a json from which the latest connections will be computed
 - **/api/login POST** which sets a cookie containing a valid bearer token (if the password is correct)