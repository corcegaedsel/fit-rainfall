# fit-rainfall
Web Api Coding Test

*** app settings file ***

*Logging (file-base):
 - default path: c:/logs/api-.log

*External API Access: (modify either property name or value to simulate error 505)
 - ExternalApiUri: https://environment.data.gov.uk/flood-monitoring/id/stations/{0}/readings?_sorted&_limit={1}

** Defaults
  - id (station id): default "E7050" => error 404 if non-existent station id or no reading found
  - count(1-100): default 10 => error 400 if not satified

Port 3000 - Kestrel
