# /web
web is a dotnet web-api to serve endpoints consumed by the next frontend (that is being developped independantly)

We have
 - */api GET* which gives back a json file containing the most up-to-date connections
 - */api POST* which loads up a json from which the latest connections will be computed
 - */api/login POST* which sets a cookie containing a valid bearer token (if the password is correct)


# Launch for dev
 `cd web`
 `dotnet run` or `dotnet watch`

 use postman to hit the endpoints
http://localhost:5073/api/login?Password=mypassword POST
http://localhost:5073/api GET/POST

 # Docker run locally
 https://learn.microsoft.com/de-de/dotnet/core/docker/build-container?tabs=windows

 in short:
`docker build -t dotnetrange .`
`docker run --rm -p 3001:80 dotnetrange`

# Docker gcloud
tage with project, repo, reponame like here:
https://cloud.google.com/artifact-registry/docs/docker/pushing-and-pulling

result:
https://calculator-k42qgew2la-uc.a.run.app/api


# Secrets:
`dotnet user-secrets init`
`dotnet user-secrets set "key" "value"`

In production these secrets need to be added via environment variables. Nesting ist done by using TWO! underscores __ (instead of :) there, as ":" is apparently not supported on all platforms.

(Locally - at least on windows - we would write `${env:super__sub}="subidu"`)
