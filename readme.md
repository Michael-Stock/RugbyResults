Rugby Results
================
In order to run this application, you need a valid authentication token and this must be set in the appsettings.json

This program will retrieve a list of matches for the 2019-2020 season for Cardiff and store it in an SQL database

You can then use:

/api/match - Retrieves all matches

/api/match/:id - Retrieves a match for a particular match Id

TODO - In the Future
=====================

- Write cucumber acceptance tests using wire mock as the fake server
- Write an integration test for the match controller, enchancing the existing unit test
- Look into asynchronous database operations
- Add swagger definitions for the APIs
- Change unit tests to use fluent assertions